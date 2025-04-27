using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using MagicStorage;
using MagicStorage.Components;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ssm.MagicStorage;
public class TEShtunStorageUnit : TEStorageUnit
{
    internal enum NetOperations : byte
    {
        FullySync,
        Deposit,
        Withdraw,
        WithdrawStack,
        PackItems,
        Flatten
    }

    private struct NetOperation
    {
        public NetOperations netOperation { get; }

        public Item item { get; }

        public bool keepOneInFavorite { get; }

        public NetOperation(NetOperations _netOPeration, Item _item = null, bool _keepOneInFavorite = false)
        {
            netOperation = _netOPeration;
            item = _item;
            keepOneInFavorite = _keepOneInFavorite;
        }
    }

    private readonly Queue<NetOperation> netOpQueue = new Queue<NetOperation>();

    private HashSet<ItemData> hasItem = new HashSet<ItemData>();

    private HashSet<int> hasItemNoPrefix = new HashSet<int>();

    private HashSet<ItemData> hasSpaceInStack = new HashSet<ItemData>();

    internal List<Item> items = new List<Item>();

    internal bool receiving;

    public int ShtunCapacity
    {
        get
        {
            int style = Main.tile[((TileEntity)this).Position.X, ((TileEntity)this).Position.Y].TileFrameY / 36;

            if (style == 9)
            {
                return 800;
            }
            if (style == 11)
            {
                return 1000;
            }
            if (style == 13)
            {
                return 1200;
            }
            return 0;
        }
    }

    public override bool IsFull => this.items.Count >= ((TEStorageUnit)this).Capacity;

    public bool IsEmpty => this.items.Count == 0;

    public int NumItems => this.items.Count;

    public override bool ValidTile(in Tile tile)
    {
        if (tile.TileType == ModContent.TileType<StorageUnit>() && tile.TileFrameX % 36 == 0)
        {
            return tile.TileFrameY % 36 == 0;
        }
        return false;
    }

    public override bool HasSpaceInStackFor(Item check)
    {
        ItemData data = default(ItemData);
        data = new ItemData(check);
        return hasSpaceInStack.Contains(data);
    }

    public bool HasSpaceFor(Item check)
    {
        if (((TEAbstractStorageUnit)this).IsFull)
        {
            return ((TEAbstractStorageUnit)this).HasSpaceInStackFor(check);
        }
        return true;
    }

    public override bool HasItem(Item check, bool ignorePrefix = false)
    {
        if (ignorePrefix)
        {
            return hasItemNoPrefix.Contains(check.type);
        }
        ItemData data = default(ItemData);
        data = new ItemData(check);
        return hasItem.Contains(data);
    }

    public override IEnumerable<Item> GetItems()
    {
        return items;
    }

    public override void DepositItem(Item toDeposit)
    {
        if (Main.netMode == 1 && !receiving)
        {
            return;
        }
        Item original = toDeposit.Clone();
        TEShtunStorageUnit.DepositToItemCollection(items, toDeposit, ShtunCapacity, out var hasChange);
        if (hasChange && Main.netMode != 1)
        {
            if (Main.netMode == 2)
            {
                netOpQueue.Enqueue(new NetOperation(NetOperations.Deposit, original));
            }
            PostChangeContents();
        }
    }

    internal static bool DepositToItemCollection(List<Item> items, Item toDeposit, int capacity, out bool hasChange)
    {
        bool finished = false;
        hasChange = false;
        foreach (Item item in items)
        {
            if (ItemCombining.CanCombineItems(toDeposit, item, true) && item.stack < item.maxStack)
            {
                int total = item.stack + toDeposit.stack;
                int newStack = total;
                if (newStack > item.maxStack)
                {
                    newStack = item.maxStack;
                }
                Utility.CallOnStackHooks(item, toDeposit, newStack - item.stack);
                item.stack = newStack;
                if (toDeposit.favorited)
                {
                    item.favorited = true;
                }
                if (toDeposit.newAndShiny)
                {
                    item.newAndShiny = MagicStorageConfig.GlowNewItems;
                }
                hasChange = true;
                toDeposit.stack = total - newStack;
                if (toDeposit.stack <= 0)
                {
                    finished = true;
                    break;
                }
            }
        }
        if (!finished && items.Count < capacity)
        {
            Item item = toDeposit.Clone();
            items.Add(item);
            toDeposit.SetDefaults(0, noMatCheck: true);
            hasChange = true;
            finished = true;
        }
        return finished;
    }

    public override Item TryWithdraw(Item lookFor, bool locked = false, bool keepOneIfFavorite = false)
    {
        if (Main.netMode == 1 && !receiving)
        {
            return new Item();
        }
        Item original = lookFor.Clone();
        if (!TEShtunStorageUnit.WithdrawFromItemCollection(items, lookFor, out var result, keepOneIfFavorite))
        {
            return result;
        }
        if (Main.netMode != 1)
        {
            if (Main.netMode == 2)
            {
                netOpQueue.Enqueue(new NetOperation(NetOperations.Withdraw, original, keepOneIfFavorite));
            }
            PostChangeContents();
        }
        return result;
    }

    internal static bool WithdrawFromItemCollection(List<Item> items, Item lookFor, out Item result, bool keepOneIfFavorite = false, Action<int> onItemRemoved = null, Action<int, int> onItemStackReduced = null)
    {
        result = null;
        int k = items.Count - 1;
        while (true)
        {
            if (k >= 0)
            {
                Item item = items[k];
                if (ItemData.Matches(lookFor, item))
                {
                    int maxToTake = item.stack;
                    if (item.stack > 0 && item.favorited && keepOneIfFavorite)
                    {
                        maxToTake--;
                    }
                    int withdraw = Math.Min(lookFor.stack, maxToTake);
                    if (result != null)
                    {
                        if (!ItemCombining.CanCombineItems(result, item, true))
                        {
                            goto IL_00e1;
                        }
                        Utility.CallOnStackHooks(result, item, withdraw);
                        result.stack += withdraw;
                    }
                    else
                    {
                        result = item.Clone();
                        result.stack = withdraw;
                    }
                    onItemStackReduced?.Invoke(k, withdraw);
                    item.stack -= withdraw;
                    if (item.stack <= 0)
                    {
                        onItemRemoved?.Invoke(k);
                        items.RemoveAt(k);
                        item.TurnToAir();
                    }
                    lookFor.stack -= withdraw;
                    if (lookFor.stack <= 0)
                    {
                        break;
                    }
                }
                goto IL_00e1;
            }
            if (result != null && !result.IsAir)
            {
                break;
            }
            result = new Item();
            return false;
        IL_00e1:
            k--;
        }
        return true;
    }

    public bool UpdateTileFrame()
    {
        Tile topLeft = Main.tile[((TileEntity)this).Position.X, ((TileEntity)this).Position.Y];
        short tileFrameX = topLeft.TileFrameX;
        int style = ((!this.IsEmpty) ? ((!((TEAbstractStorageUnit)this).IsFull) ? 1 : 2) : 0);
        if (((TEAbstractStorageUnit)this).Inactive)
        {
            style += 3;
        }
        style *= 36;
        topLeft.TileFrameX = (short)style;
        Main.tile[((TileEntity)this).Position.X, ((TileEntity)this).Position.Y + 1].TileFrameX = (short)style;
        Main.tile[((TileEntity)this).Position.X + 1, ((TileEntity)this).Position.Y].TileFrameX = (short)(style + 18);
        Main.tile[((TileEntity)this).Position.X + 1, ((TileEntity)this).Position.Y + 1].TileFrameX = (short)(style + 18);
        return tileFrameX != style;
    }

    public void UpdateTileFrameWithNetSend()
    {
        if (Main.netMode != 1)
        {
            if (UpdateTileFrame())
            {
                NetMessage.SendTileSquare(-1, ((TileEntity)this).Position.X, ((TileEntity)this).Position.Y, 2, 2);
            }
        }
        else
        {
            NetHelper.RequestStorageUnitStyle(((TileEntity)this).Position);
        }
    }

    internal static void SwapItems(TEShtunStorageUnit unit1, TEShtunStorageUnit unit2)
    {
        TEShtunStorageUnit tEShtunStorageUnit = unit2;
        List<Item> list = unit2.items;
        List<Item> list2 = unit1.items;
        unit1.items = list;
        tEShtunStorageUnit.items = list2;
        tEShtunStorageUnit = unit2;
        HashSet<ItemData> hashSet = unit2.hasSpaceInStack;
        HashSet<ItemData> hashSet2 = unit1.hasSpaceInStack;
        unit1.hasSpaceInStack = hashSet;
        tEShtunStorageUnit.hasSpaceInStack = hashSet2;
        tEShtunStorageUnit = unit2;
        hashSet2 = unit2.hasItem;
        hashSet = unit1.hasItem;
        unit1.hasItem = hashSet2;
        tEShtunStorageUnit.hasItem = hashSet;
        tEShtunStorageUnit = unit2;
        HashSet<int> hashSet3 = unit2.hasItemNoPrefix;
        HashSet<int> hashSet4 = unit1.hasItemNoPrefix;
        unit1.hasItemNoPrefix = hashSet3;
        tEShtunStorageUnit.hasItemNoPrefix = hashSet4;
        if (Main.netMode == 2)
        {
            unit1.netOpQueue.Clear();
            unit2.netOpQueue.Clear();
            unit1.netOpQueue.Enqueue(new NetOperation(NetOperations.FullySync));
            unit2.netOpQueue.Enqueue(new NetOperation(NetOperations.FullySync));
        }
        unit1.PostChangeContents();
        unit2.PostChangeContents();
    }

    internal Item WithdrawStack()
    {
        if (Main.netMode == 1 && !receiving)
        {
            return new Item();
        }
        Item result = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        if (Main.netMode != 1)
        {
            if (Main.netMode == 2)
            {
                netOpQueue.Enqueue(new NetOperation(NetOperations.WithdrawStack));
            }
            PostChangeContents();
        }
        return result;
    }

    internal void PackItems()
    {
        if ((Main.netMode == 1 && !receiving) || items.Count < 2)
        {
            return;
        }
        items = TEShtunStorageUnit.Compact(items, out var didPack);
        if (didPack && Main.netMode != 1)
        {
            if (Main.netMode == 2)
            {
                netOpQueue.Enqueue(new NetOperation(NetOperations.PackItems));
            }
            PostChangeContents();
        }
    }

    internal bool FlattenFrom(TEShtunStorageUnit source, out List<Item> transferredItems)
    {
        transferredItems = null;
        if (Main.netMode == 1)
        {
            NetHelper.ClientRequestItemTransfer((TEStorageUnit)(object)this, (TEStorageUnit)(object)source);
            return false;
        }
        if (Main.netMode == 2)
        {
            return NetHelper.AttemptItemTransferAndSendResult((TEStorageUnit)(object)this, (TEStorageUnit)(object)source, out transferredItems, false);
        }
        TEShtunStorageUnit.AttemptItemTransfer(this, source, out transferredItems);
        if (transferredItems.Count == 0)
        {
            return false;
        }
        PostChangeContents();
        source.PostChangeContents();
        return true;
    }

    internal static List<Item> Compact(IEnumerable<Item> items, out bool didPack)
    {
        List<Item> packed = new List<Item>();
        didPack = false;
        foreach (Item item in items)
        {
            foreach (Item pack in packed)
            {
                if (!pack.IsAir && pack.stack < pack.maxStack && ItemCombining.CanCombineItems(item, pack, true))
                {
                    if (item.stack + pack.stack <= pack.maxStack)
                    {
                        Utility.CallOnStackHooks(pack, item, item.stack);
                        pack.stack += item.stack;
                        item.stack = 0;
                    }
                    else
                    {
                        Utility.CallOnStackHooks(pack, item, item.maxStack - pack.stack);
                        item.stack -= pack.maxStack - pack.stack;
                        pack.stack = pack.maxStack;
                    }
                    didPack = true;
                    break;
                }
            }
            if (item.stack > 0)
            {
                packed.Add(item);
            }
        }
        return packed;
    }

    internal static void AttemptItemTransfer(TEShtunStorageUnit destination, TEShtunStorageUnit source, out List<Item> transferredItems)
    {
        transferredItems = new List<Item>();
        if (source.IsEmpty || ((TEAbstractStorageUnit)destination).Inactive)
        {
            return;
        }
        for (int d = 0; d < destination.NumItems; d++)
        {
            Item dest = destination.items[d];
            if (dest.IsAir || dest.stack >= dest.maxStack)
            {
                continue;
            }
            for (int s = source.NumItems - 1; s >= 0; s--)
            {
                Item src = source.items[s];
                if (!src.IsAir && ItemCombining.CanCombineItems(dest, src, true))
                {
                    Item transferred = src.Clone();
                    if (dest.stack + src.stack <= dest.maxStack)
                    {
                        Utility.CallOnStackHooks(dest, src, src.stack);
                        dest.stack += src.stack;
                        src.stack = 0;
                        source.items.RemoveAt(s);
                    }
                    else
                    {
                        int diff = dest.maxStack - dest.stack;
                        Utility.CallOnStackHooks(dest, src, diff);
                        transferred.stack = diff;
                        src.stack -= diff;
                        dest.stack = dest.maxStack;
                    }
                    transferredItems.Add(transferred);
                }
            }
        }
        _ = transferredItems.Count;
        _ = 0;
        int nonPackedTransfer = 0;
        while (!((TEAbstractStorageUnit)destination).IsFull && !source.IsEmpty)
        {
            List<Item> list = source.items;
            Item withdrawn = list[list.Count - 1];
            source.items.RemoveAt(source.items.Count - 1);
            destination.items.Add(withdrawn);
            transferredItems.Add(withdrawn);
            nonPackedTransfer++;
        }
        _ = 0;
    }

    public override void SaveData(TagCompound tag)
    {
        ((TEStorageUnit)this).SaveData(tag);
        List<TagCompound> tagItems = items.Select(ItemIO.Save).ToList();
        tag.Set("Items", tagItems);
    }

    public override void LoadData(TagCompound tag)
    {
        ((TEStorageUnit)this).LoadData(tag);
        ClearItemsData();
        ItemData data = default(ItemData);
        foreach (Item item in tag.GetList<TagCompound>("Items").Select(ItemIO.Load))
        {
            items.Add(item);
            data = new ItemData(item);
            if (item.stack < item.maxStack)
            {
                hasSpaceInStack.Add(data);
            }
            hasItem.Add(data);
            hasItemNoPrefix.Add(data.Type);
        }
    }

    public void FullySync()
    {
        netOpQueue.Enqueue(new NetOperation(NetOperations.FullySync));
    }

    public override void NetSend(BinaryWriter trueWriter)
    {
        using MemoryStream buffer = new MemoryStream(65536);
        using BinaryWriter writer = new BinaryWriter(buffer);
        ((TEStorageUnit)this).NetSend(writer);
        if (netOpQueue.Count > ShtunCapacity / 2)
        {
            netOpQueue.Clear();
            netOpQueue.Enqueue(new NetOperation(NetOperations.FullySync));
        }
        writer.Write((ushort)items.Count);
        writer.Write((ushort)netOpQueue.Count);
        while (netOpQueue.Count > 0)
        {
            NetOperation netOp = netOpQueue.Dequeue();
            writer.Write((byte)netOp.netOperation);
            switch (netOp.netOperation)
            {
                case NetOperations.FullySync:
                    writer.Write(items.Count);
                    foreach (Item item in items)
                    {
                        ItemIO.Send(item, writer, writeStack: true, writeFavorite: true);
                    }
                    break;
                case NetOperations.Withdraw:
                    writer.Write(netOp.keepOneInFavorite);
                    ItemIO.Send(netOp.item, writer, writeStack: true, writeFavorite: true);
                    break;
                case NetOperations.Deposit:
                    ItemIO.Send(netOp.item, writer, writeStack: true, writeFavorite: true);
                    break;
            }
        }
        writer.Flush();
        byte[] data = null;
        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress))
            {
                deflateStream.Write(buffer.GetBuffer(), 0, (int)buffer.Length);
            }
            data = memoryStream.ToArray();
        }
        trueWriter.Write((ushort)data.Length);
        trueWriter.Write(data);
    }

    public override void NetReceive(BinaryReader trueReader)
    {
        using MemoryStream buffer = new MemoryStream();
        ushort bufferLen = trueReader.ReadUInt16();
        buffer.Write(trueReader.ReadBytes(bufferLen));
        buffer.Position = 0L;
        using DeflateStream decompressor = new DeflateStream(buffer, CompressionMode.Decompress, leaveOpen: true);
        using BinaryReader reader = new BinaryReader(decompressor);
        ((TEStorageUnit)this).NetReceive(reader);
        int serverItemsCount = reader.ReadUInt16();
        int opCount = reader.ReadUInt16();
        if (opCount > 0)
        {
            if (TileEntity.ByPosition.TryGetValue(((TileEntity)this).Position, out var te) && te is TEShtunStorageUnit otherUnit)
            {
                items = otherUnit.items;
                hasSpaceInStack = otherUnit.hasSpaceInStack;
                hasItem = otherUnit.hasItem;
                hasItemNoPrefix = otherUnit.hasItemNoPrefix;
            }
            int oldCount = items.Count;
            receiving = true;
            bool repairMetaData = true;
            ItemData data = default(ItemData);
            for (int i = 0; i < opCount; i++)
            {
                byte netOp = reader.ReadByte();
                if (Enum.IsDefined(typeof(NetOperations), netOp))
                {
                    switch ((NetOperations)netOp)
                    {
                        case NetOperations.FullySync:
                            {
                                repairMetaData = false;
                                ClearItemsData();
                                int itemsCount = reader.ReadInt32();
                                for (int j = 0; j < itemsCount; j++)
                                {
                                    Item item = ItemIO.Receive(reader, readStack: true, readFavorite: true);
                                    items.Add(item);
                                    data = new ItemData(item);
                                    if (item.stack < item.maxStack)
                                    {
                                        hasSpaceInStack.Add(data);
                                    }
                                    hasItem.Add(data);
                                    hasItemNoPrefix.Add(data.Type);
                                }
                                break;
                            }
                        case NetOperations.Withdraw:
                            {
                                bool keepOneIfFavorite = reader.ReadBoolean();
                                ((TEAbstractStorageUnit)this).TryWithdraw(ItemIO.Receive(reader, readStack: true, readFavorite: true), false, keepOneIfFavorite);
                                break;
                            }
                        case NetOperations.WithdrawStack:
                            WithdrawStack();
                            break;
                        case NetOperations.Deposit:
                            ((TEAbstractStorageUnit)this).DepositItem(ItemIO.Receive(reader, readStack: true, readFavorite: true));
                            break;
                        case NetOperations.PackItems:
                            PackItems();
                            break;
                    }
                }
                else if (Main.netMode != 2)
                {
                    Main.NewText($"NetRecive Bad OP: {netOp}", Color.Red);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"NetRecive Bad OP: {netOp}");
                    Console.ResetColor();
                }
            }
            if (repairMetaData)
            {
                RepairMetadata();
            }
            if (items.Count != oldCount)
            {
                UpdateTileFrameWithNetSend();
            }
            receiving = false;
        }
        else if (serverItemsCount != items.Count)
        {
            NetHelper.SyncStorageUnit(((TileEntity)this).Position);
        }
    }

    private void ClearItemsData()
    {
        items.Clear();
        hasSpaceInStack.Clear();
        hasItem.Clear();
        hasItemNoPrefix.Clear();
    }

    private void RepairMetadata()
    {
        hasSpaceInStack.Clear();
        hasItem.Clear();
        hasItemNoPrefix.Clear();
        ItemData data = default(ItemData);
        foreach (Item item in items)
        {
            data = new ItemData(item);
            if (item.stack < item.maxStack)
            {
                hasSpaceInStack.Add(data);
            }
            hasItem.Add(data);
            hasItemNoPrefix.Add(data.Type);
        }
    }

    public void PostChangeContents()
    {
        RepairMetadata();
        UpdateTileFrameWithNetSend();
        NetHelper.SendTEUpdate(((TileEntity)this).ID, ((TileEntity)this).Position);
    }
}
