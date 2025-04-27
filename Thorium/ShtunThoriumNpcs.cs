using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.NPCs.BossMini;
using ssm.Core;


namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public partial class ShtunThoriumNpcs : GlobalNPC
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        public override bool InstancePerEntity => true;
        public override bool PreKill(NPC npc)
        {
            #region newshopitemdisplay
            bool doDeviText = false;
            if (npc.type == ModContent.NPCType<PatchWerk>() && !ThoriumWorldSave.downedPatchWrek)
            {
                doDeviText = true;
                ThoriumWorldSave.downedPatchWrek = true;
            }
            if (npc.type == ModContent.NPCType<Illusionist>() && !ThoriumWorldSave.downedIllusionist)
            {
                doDeviText = true;
                ThoriumWorldSave.downedIllusionist = true;
            }
            if (npc.type == ModContent.NPCType<CorpseBloom>() && !ThoriumWorldSave.downedCorpseBloom)
            {
                doDeviText = true;
                ThoriumWorldSave.downedCorpseBloom = true;
            }
            if (doDeviText && Main.netMode != NetmodeID.Server)
            {
                Main.NewText("A new item has been unlocked in Deviantt's shop!", Color.HotPink);
            }
            #endregion

    //        if (NoLoot)
    //        {
    //            return false;
    //        }
    //        if (SwarmActive && ssm.SwarmActive && Main.netMode != NetmodeID.MultiplayerClient)
    //        {
    //            if (npc.type == ModContent.NPCType<BuriedChampion>())
    //            {
    //                Swarm(npc, ModContent.NPCType<BuriedChampion>(), ModContent.ItemType<BuriedChampionTreasureBag>(), ModContent.ItemType<BuriedChampionTrophy>(), ModContent.ItemType<BuriedEnergizer>());
    //            }
    //            else if (npc.type == ModContent.NPCType<FallenBeholder>())
    //            {
    //                Swarm(npc, ModContent.NPCType<FallenBeholder>(), ModContent.ItemType<FallenBeholderTreasureBag>(), ModContent.ItemType<FallenBeholderTrophy>(), ModContent.ItemType<FallenEnergizer>());
    //            }
    //            else if (npc.type == ModContent.NPCType<Lich>())
    //            {
    //                Swarm(npc, ModContent.NPCType<Lich>(), ModContent.ItemType<LichTreasureBag>(), ModContent.ItemType<LichTrophy>(), ModContent.ItemType<LichEnergizer>());
    //            }
    //            else if (npc.type == ModContent.NPCType<QueenJellyfish>())
    //            {
    //                Swarm(npc, ModContent.NPCType<QueenJellyfish>(), ModContent.ItemType<QueenJellyfishTreasureBag>(), ModContent.ItemType<QueenJellyfishTrophy>(), ModContent.ItemType<JellyfishEnergizer>());
    //            }
    //            else if (npc.type == ModContent.NPCType<GraniteEnergyStorm>())
    //            {
    //                Swarm(npc, ModContent.NPCType<GraniteEnergyStorm>(), ModContent.ItemType<GraniteEnergyStormTreasureBag>(), ModContent.ItemType<GraniteEnergyStormTrophy>(), ModContent.ItemType<GraniteEnergizer>());
    //            }
    //            //else if (npc.type == ModContent.NPCType<StarScouter>())
    //            //{
    //            //    Swarm(npc, ModContent.NPCType<StarScouter>(), ModContent.ItemType<StarScouterTreasureBag>(), ModContent.ItemType<StarScouterTrophy>(), ModContent.ItemType<PerforatorEnergizer>());
    //            //}
    //            else if (npc.type == ModContent.NPCType<BoreanStrider>())
    //            {
    //                Swarm(npc, ModContent.NPCType<BoreanStrider>(), ModContent.ItemType<BoreanStriderTreasureBag>(), ModContent.ItemType<BoreanStriderTrophy>(), ModContent.ItemType<BoreanEnergizer>());
    //            }
				//else if (npc.type == ModContent.NPCType<Viscount>())
				//{
				//	Swarm(npc, ModContent.NPCType<Viscount>(), ModContent.ItemType<ViscountTreasureBag>(), ModContent.ItemType<ViscountTrophy>(), ModContent.ItemType<ViscountEnergizer>());
				//}
				//else if (npc.type == ModContent.NPCType<TheGrandThunderBird>())
    //            {
    //                Swarm(npc, ModContent.NPCType<TheGrandThunderBird>(), ModContent.ItemType<TheGrandThunderBirdTreasureBag>(), ModContent.ItemType<TheGrandThunderBirdTrophy>(), ModContent.ItemType<ThunderEnergizer>());
    //            }
				//else if (npc.type == ModContent.NPCType<ForgottenOne>())
				//{
				//	Swarm(npc, ModContent.NPCType<ForgottenOne>(), ModContent.ItemType<ForgottenOneTreasureBag>(), ModContent.ItemType<ForgottenOneTrophy>(), ModContent.ItemType<ForgottenEnergizer>());
				//}
				//return false;
    //        }
    //        else
    //        {
                return true;
    //        }
        }
    }
}