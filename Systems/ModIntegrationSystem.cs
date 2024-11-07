using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using ssm.Content.NPCs.Shtuxibus;
using ssm.Content.Items.Consumables;
using ssm.Content.Items.Materials;
using Terraria.Localization;
using ssm.Content.Buffs;
using ssm.Systems;
using ssm.Core;
using ssm.Content.NPCs.ECH;
using ssm.Content.Items.ShtuxibusPlush;
using Terraria.ModLoader.Config;
using ssm.Content.NPCs.DukeFishronEX;
using ssm.Content.NPCs.StarlightCat;
using ssm.Core;

namespace ssm.Systems
{
    public class ModIntergationSystem : ModSystem
    {
        public override void PostSetupContent()
        {
            if (ShtunConfig.Instance.ExtraContent)
            {
                DoBossChecklistIntegration();
            }

            if (ModCompatibility.HEROSMod.Loaded || ModCompatibility.Dragonlens.Loaded || ModCompatibility.CheatSheet.Loaded)
            {
                PrivateClassEdits.LoadAntiCheats();
            }
        }

        private void DoBossChecklistIntegration()
        {
            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod)) { return; }
            if (bossChecklistMod.Version < new Version(1, 6)) { return; }
            string internalName1 = "Shtuxibus";
            float weight1 = 745f;
            Func<bool> downed1 = () => WorldSaveSystem.downedShtuxibus;
            int bossType1 = ModContent.NPCType<Shtuxibus>();
            int spawnItem1 = ModContent.ItemType<ShtuxianCurse>();
            List<int> collectibles1 = new List<int>() { ModContent.ItemType<ShtuxiumSoulShard>(), };

            string internalName2 = "Echdeath";
            float weight2 = 745.1f;
            Func<bool> downed2 = () => WorldSaveSystem.downedEch;
            int bossType2 = ModContent.NPCType<Echdeath>();
            int spawnItem2 = ModContent.ItemType<ShtuxibusFumo>();
            List<int> collectibles2 = new List<int>() { ModContent.ItemType<Sadism>(), };

            string internalName3 = "Duke Fishron EX";
            float weight3 = 744.99f;
            Func<bool> downed3 = () => WorldSaveSystem.downedFish;
            int bossType3 = ModContent.NPCType<DukeFishronEX>();
            int spawnItem3 = ModContent.ItemType<ShtuxibusFumo>();
            List<int> collectibles3 = new List<int>() { ModContent.ItemType<Sadism>(), };

            string internalName4 = "StarlightCat";
            float weight4 = float.MaxValue;
            Func<bool> downed4 = () => WorldSaveSystem.downedChtuxlagor;
            int bossType4 = ModContent.NPCType<StarlightCatBoss>();
            int spawnItem4 = ModContent.ItemType<ShtuxibusFumo>();
            List<int> collectibles4 = new List<int>() { ModContent.ItemType<Sadism>(), };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalName2,
                weight2,
                downed2,
                bossType2,
                new Dictionary<string, object>()
                {
                    ["spawnInfo"] = (object)Language.GetText("Mods.ssm.NPCs.Echdeath.BossChecklistIntegration.SpawnInfo").WithFormatArgs(Array.Empty<object>()),
                    ["despawnMessage"] = (object)Language.GetText("Mods.ssm.NPCs.Echdeath.BossChecklistIntegration.DespawnMessage"),
                    ["spawnItems"] = spawnItem2,
                    ["collectibles"] = collectibles2,
                }
            );

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalName1,
                weight1,
                downed1,
                bossType1,
                new Dictionary<string, object>()
                {
                    ["spawnInfo"] = (object)Language.GetText("Mods.ssm.NPCs.Shtuxibus.BossChecklistIntegration.SpawnInfo").WithFormatArgs(Array.Empty<object>()),
                    ["despawnMessage"] = (object)Language.GetText("Mods.ssm.NPCs.Shtuxibus.BossChecklistIntegration.DespawnMessage"),
                    ["spawnItems"] = spawnItem1,
                    ["collectibles"] = collectibles1,
                }
            );

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalName3,
                weight3,
                downed3,
                bossType3,
                new Dictionary<string, object>()
                {
                    ["spawnInfo"] = (object)Language.GetText("Mods.ssm.NPCs.DukeFishronEX.BossChecklistIntegration.SpawnInfo").WithFormatArgs(Array.Empty<object>()),
                    ["despawnMessage"] = (object)Language.GetText("Mods.ssm.NPCs.DukeFishronEX.BossChecklistIntegration.DespawnMessage"),
                    ["spawnItems"] = spawnItem3,
                    ["collectibles"] = collectibles3,
                }
            );

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalName4,
                weight4,
                downed4,
                bossType4,
                new Dictionary<string, object>()
                {
                    ["spawnInfo"] = (object)Language.GetText("Mods.ssm.NPCs.StarlightCatBoss.BossChecklistIntegration.SpawnInfo").WithFormatArgs(Array.Empty<object>()),
                    ["despawnMessage"] = (object)Language.GetText("Mods.ssm.NPCs.StarlightCatBoss.BossChecklistIntegration.DespawnMessage"),
                    ["spawnItems"] = spawnItem3,
                    ["collectibles"] = collectibles3,
                }
            );
        }

        public static class BossChecklist
        {
            public static void AdjustValues()
            {
                ModCompatibility.SoulsMod.Mod.BossChecklistValues["MutantBoss"] = 29f;
                /*if (Redemption.Loaded){
                	Redemption.Mod.BossChecklistValues["Nebuleus"] = 20f;}
                if (SacredTools.Loaded){
                    SacredTools.Mod.BossChecklistValues["Nihilus"] = 24f;}*/
            }
        }
    }
}