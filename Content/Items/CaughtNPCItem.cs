//using Fargowiltas.Common.Configs;
//using Fargowiltas.NPCs;
using Fargowiltas.Utilities.Extensions;
using ssm.Content.NPCs;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Items
{
    public class CaughtNPCItem : ModItem
    {
        internal static Dictionary<int, int> CaughtTownies = new();

        public override string Name => _name;

        public string _name;
        public int AssociatedNpcId;
        public string NpcQuote;

        public CaughtNPCItem()
        {
            _name = base.Name;
            AssociatedNpcId = NPCID.None;
            NpcQuote = "";
        }

        public CaughtNPCItem(string internalName, int associatedNpcId, string npcQuote = "")
        {
            _name = internalName;
            AssociatedNpcId = associatedNpcId;
            NpcQuote = npcQuote;
        }

        public override bool IsLoadingEnabled(Mod mod) => AssociatedNpcId != NPCID.None;

        protected override bool CloneNewInstances => true;

        public override ModItem Clone(Item item)
        {
            CaughtNPCItem clone = base.Clone(item) as CaughtNPCItem;
            clone._name = _name;
            clone.AssociatedNpcId = AssociatedNpcId;
            clone.NpcQuote = NpcQuote;
            return clone;
        }

        public override bool IsCloneable => true;

        public override void Unload()
        {
            CaughtTownies.Clear();
        }

        public override string Texture => AssociatedNpcId < NPCID.Count ? $"Terraria/Images/NPC_{AssociatedNpcId}" : NPCLoader.GetNPC(AssociatedNpcId).Texture;

        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(6, Main.npcFrameCount[AssociatedNpcId]));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
        }

        public override void SetDefaults()
        {
            Item.DefaultToCapturedCritter(AssociatedNpcId);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item44;

            if (AssociatedNpcId == NPCID.Angler)
            {
                Item.bait = 15;
            }
        }

        public override void PostUpdate()
        {
            if (AssociatedNpcId != NPCID.Guide || !Item.lavaWet || NPC.AnyNPCs(NPCID.WallofFlesh))
            {
                return;
            }

            NPC.SpawnWOF(Item.position);
            Item.TurnToAir();
        }

        public override bool CanUseItem(Player player)
        {
            return player.IsTileWithinRange(Player.tileTargetX, Player.tileTargetY) && !WorldGen.SolidTile(Player.tileTargetX, Player.tileTargetY) && NPC.CountNPCS(AssociatedNpcId) < 5;
        }

        public override bool? UseItem(Player player)
        {
            return true;
        }

        public static void RegisterItems()
        {
            CaughtTownies = new Dictionary<int, int>();
            Add("Monstrocity", ModContent.NPCType<Monstrocity>(), "'Hey buddy you got any food?'");
        }

        public static void Add(string internalName, int id, string quote)
        {
            CaughtNPCItem item = new(internalName, id, quote);
            ssm.Instance.AddContent(item);
            CaughtTownies.Add(id, item.Type);
        }
    }

    public class CaughtGlobalNPC : GlobalNPC
    {
        private static HashSet<int> npcCatchableWasFalse;

        public override void Load()
        {
            npcCatchableWasFalse = new HashSet<int>();
        }

        public override void Unload()
        {
            if (npcCatchableWasFalse != null)
            {
                foreach (var type in npcCatchableWasFalse)
                {
                    //Failing to unload this properly causes it to bleed into un-fargowiltas gameplay, causing various issues such as clients not being able to join a server
                    Main.npcCatchable[type] = false;
                }
                npcCatchableWasFalse = null;
            }
        }

        public override void SetDefaults(NPC npc)
        {
            int type = npc.type;
            if (CaughtNPCItem.CaughtTownies.ContainsKey(type) //&& ModContent.GetInstance<FargoServerConfig>().CatchNPCs
                                                              )
            {
                npc.catchItem = (short)CaughtNPCItem.CaughtTownies.FirstOrDefault(x => x.Key.Equals(type)).Value;
                if (!Main.npcCatchable[type])
                {
                    npcCatchableWasFalse.Add(type);
                    Main.npcCatchable[type] = true;
                }
            }
        }
    }
}
