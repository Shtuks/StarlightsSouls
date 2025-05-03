using MagicStorage;
using MagicStorage.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ssm.Core;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.MagicStorage;
[ExtendsFromMod(ModCompatibility.MagicStorage.Name)]
[JITWhenModsEnabled(ModCompatibility.MagicStorage.Name)]
public class ShtunStorageUnit : StorageUnit
{
    public static int storageType;
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (Main.tile[i, j].TileFrameX % 36 == 18)
        {
            i--;
        }
        if (Main.tile[i, j].TileFrameY % 36 == 6)
        {
            j--;
        }
        if (TileEntity.ByPosition.ContainsKey(new Point16(i, j)) && Main.tile[i, j].TileFrameX / 36 % 3 != 0)
        {
            fail = true;
        }
    }

    public override bool CanExplode(int i, int j)
    {
        bool fail = false;
        bool discard = false;
        bool discard2 = false;
        ((ModTile)(object)this).KillTile(i, j, ref fail, ref discard, ref discard2);
        return !fail;
    }

    public override bool RightClick(int i, int j)
    {
        if (Main.tile[i, j].TileFrameX % 36 == 18)
        {
            i--;
        }
        if (Main.tile[i, j].TileFrameY % 36 == 18)
        {
            j--;
        }
        if (ShtunStorageUnit.TryUpgrade(i, j))
        {
            return true;
        }
        if (TileEntity.ByPosition.TryGetValue(new Point16(i, j), out var te))
        {
            TEStorageUnit storageUnit = (TEStorageUnit)(object)((te is TEStorageUnit) ? te : null);
            if (storageUnit != null)
            {
                Main.LocalPlayer.tileInteractionHappened = true;
                string obj = (((TEAbstractStorageUnit)storageUnit).Inactive ? Language.GetTextValue("Mods.MagicStorage.Inactive") : Language.GetTextValue("Mods.MagicStorage.Active"));
                string fullnessString = Language.GetTextValue("Mods.MagicStorage.Capacity", storageUnit.NumItems, storageUnit.Capacity);
                Main.NewText(obj + ", " + fullnessString);
                return ((StorageUnit)this).RightClick(i, j);
            }
        }
        return false;
    }

    private static bool TryUpgrade(int i, int j)
    {
        Player player = Main.LocalPlayer;
        Item item = player.HeldItem;
        int style = Main.tile[i, j].TileFrameY / 36;
        bool success = false;
        if (style == 7 && item.type == ModContent.ItemType<UpgradeDeviating>())
        {
            ShtunStorageUnit.SetStyle(i, j, 9);
            success = true;
        }
        else if (style == 9 && item.type == ModContent.ItemType<UpgradeAbominable>())
        {
            ShtunStorageUnit.SetStyle(i, j, 11);
            success = true;
        }
        else if (style == 11 && item.type == ModContent.ItemType<UpgradeEternal>())
        {
            ShtunStorageUnit.SetStyle(i, j, 13);
            success = true;
        }
        if (success)
        {
            if (!TileEntity.ByPosition.TryGetValue(new Point16(i, j), out var te) || !(te is TEShtunStorageUnit storageUnit))
            {
                return false;
            }
            storageUnit.UpdateTileFrame();
            NetMessage.SendTileSquare(Main.myPlayer, i, j, 2, 2);
            TEStorageHeart heart = ((TEAbstractStorageUnit)storageUnit).GetHeart();
            if (heart != null)
            {
                if (Main.netMode == 0)
                {
                    heart.ResetCompactStage(0);
                }
                else if (Main.netMode == 1)
                {
                    NetHelper.SendResetCompactStage(((TileEntity)(object)heart).Position);
                }
            }
            item.stack--;
            if (item.stack <= 0)
            {
                item.SetDefaults();
            }
            if (player.selectedItem == 58)
            {
                Main.mouseItem = item.Clone();
            }
            if (player.selectedItem == 58)
            {
                Main.mouseItem.stack--;
                if (Main.mouseItem.stack <= 0)
                {
                    Main.mouseItem.TurnToAir();
                }
            }
            else
            {
                item.stack--;
                if (item.stack <= 0)
                {
                    item.TurnToAir();
                }
            }
            player.ConsumeItem(item.type);
            SoundEngine.PlaySound(in SoundID.MaxMana, ((TileEntity)(object)storageUnit).Position.ToWorldCoordinates());
            Dust.NewDustPerfect(((TileEntity)(object)storageUnit).Position.ToWorldCoordinates(), 110, Vector2.Zero, 0, Color.Green, 2f);
            return true;
        }
        return false;
    }

    private static void SetStyle(int i, int j, int style)
    {
        Main.tile[i, j].TileFrameY = (short)(36 * style);
        Main.tile[i + 1, j].TileFrameY = (short)(36 * style);
        Main.tile[i, j + 1].TileFrameY = (short)(36 * style + 18);
        Main.tile[i + 1, j + 1].TileFrameY = (short)(36 * style + 18);
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];
        Vector2 drawPos = (Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange)) + 16f * new Vector2(i, j) - Main.screenPosition;
        Rectangle frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
        Color lightColor = Lighting.GetColor(i, j, Color.White);
        Color color = Color.Lerp(Color.White, lightColor, 0.5f);
        spriteBatch.Draw(ModContent.Request<Texture2D>("ssm/MagicStorage/ShtunStorageUnit_Glow", (AssetRequestMode)2).Value, drawPos, frame, color);
    }
}