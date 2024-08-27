using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using ssm.Content.Buffs;
using ssm.Content.Items;
using ssm.Content.Items.Accessories;
using ssm.Content.Items.Consumables;
using ssm.Content.Items.Materials;
using ssm.Content.Items.Singularities;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Potions;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs;
using CalamityMod.NPCs.Abyss;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Crags;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.Yharon;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.NPCs.PrimordialWyrm;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.SunkenSea;
using CalamityMod.NPCs.SulphurousSea;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.TownNPCs;

namespace ssm
{
    public partial class ShtunNpcs : GlobalNPC
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
		private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public override bool InstancePerEntity => true;
        public bool BeetleOffenseAura;
        public bool BeetleDefenseAura;
        public bool BeetleUtilAura;
        public int BeetleTimer;
        public bool PaladinsShield;
        public bool isWaterEnemy;
        public bool HasWhipDebuff;
        public static int boss = -1;
        public static int slimeBoss = -1;
        public static int eyeBoss = -1;
        public static int eaterBoss = -1;
        public static int brainBoss = -1;
        public static int beeBoss = -1;
        public static int skeleBoss = -1;
        public static int deerBoss = -1;
        public static int wallBoss = -1;
        public static int retiBoss = -1;
        public static int spazBoss = -1;
        public static int destroyBoss = -1;
        public static int primeBoss = -1;
        public static int queenSlimeBoss = -1;
        public static int empressBoss = -1;
        public static int betsyBoss = -1;
        public static int fishBoss = -1;
        public static int cultBoss = -1;
        public static int moonBoss = -1;
        public static int guardBoss = -1;
        public static int fishBossEX = -1;
        public static bool spawnFishronEX;
        public static int deviBoss = -1;
        public static int abomBoss = -1;
        public static int mutantBoss = -1;
        public static int Shtuxibus = -1;
        public static int championBoss = -1;
        public static bool Revengeance => CalamityMod.World.CalamityWorld.revenge;
        public static int eaterTimer;
        public override void ResetEffects(NPC npc)
        {
            PaladinsShield = false;
            HasWhipDebuff = false;
            if (BeetleTimer > 0 && --BeetleTimer <= 0)
            {
                BeetleDefenseAura = false;
                BeetleOffenseAura = false;
                BeetleUtilAura = false;
            }
        }
        public override void ModifyShop(NPCShop shop)
		{
        void AddItem(int itemID, int customPrice = -1, Condition condition = null, Condition[] conditions = null)
			{
				if (condition != (Condition)null)
				{
					conditions = (Condition[])(object)new Condition[1] { condition };
				}
				if (conditions != null)
				{
					if (customPrice != -1)
					{
						shop.Add(new Item(itemID, 1, 0)
						{
							shopCustomPrice = customPrice
						}, conditions);
					}
					else
					{
						shop.Add(itemID, conditions);
					}
				}
				else if (customPrice != -1)
				{
					shop.Add(new Item(itemID, 1, 0)
					{
						shopCustomPrice = customPrice
					}, Array.Empty<Condition>());
				}
				else
				{
					shop.Add(itemID, Array.Empty<Condition>());
				}
			}
		}
/*public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
        calamity.TryFind("SupremeCalamitas", out ModNPC scalNpc);
        calamity.TryFind("DesertScourge", out ModNPC sCOURGEnPC);

        if (npc.type == ModContent.NPCType<DesertScourgeHead>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScourgeFood>(), 1, 1, 1));
			}
             if (npc.type == ModContent.NPCType<SlimeGodCore>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PurifiedGelEnchant>(), 1, 1, 1));
			}
               if (npc.type == ModContent.NPCType<GiantClam>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShatteredPearl>(), 1, 1, 1));
			}
              if (npc.type == ModContent.NPCType<PerforatorHive>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FleshyHive>(), 1, 1, 1));
			}
              if (npc.type == ModContent.NPCType<HiveMind>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CorruptedEye>(), 1, 1, 1));
			}
              if (npc.type == ModContent.NPCType<Cryogen>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CryoHeart>(), 1, 1, 1));
			}
              if (npc.type == ModContent.NPCType<AquaticScourgeHead>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WormAcid>(), 1, 1, 1));
			}
              if (npc.type == ModContent.NPCType<BrimstoneElemental>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrimstoneKey>(), 1, 1, 1));
			}
              if (npc.type == ModContent.NPCType<CalamitasClone>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CloneFlames>(), 1, 1, 1));
			}
                   if (npc.type == ModContent.NPCType<Leviathan>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LeviathanEgg>(), 1, 1, 1));
			}
                   if (npc.type == ModContent.NPCType<PlaguebringerGoliath>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ToxicBackpack>(), 1, 1, 1));
			}
                         if (npc.type == ModContent.NPCType<AstrumAureus>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StarStone>(), 1, 1, 1));
			}
                   if (npc.type == ModContent.NPCType<AstrumDeusHead>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfectionShard>(), 1, 1, 1));
			}
                         if (npc.type == ModContent.NPCType<Yharon>())
			{
				  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<JungleTyrantAmulet>(), 1, 1, 1));
			}
            
            switch (npc.type)
            {
                case NPCID.KingSlime:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SlimeyCore>(), 4, 1, 1));
                    break;

                case NPCID.EyeofCthulhu:
                   npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FirstSpace>(), 4, 1, 1));
                    break;
             

                      default:
                    break;
            }
        }*/
    }
}
     