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
using ssm;
using ssm.Content.NPCs.Chtuxlagor;

namespace ssm.Systems
{
    public class ModIntegrationsSystem : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExtraContent;
        }

        public static class Souls
        {
            public const string Name = "FargowiltasSouls";
            public static bool Loaded => ModLoader.HasMod(Name);
            public static FargowiltasSouls.FargowiltasSouls Mod => ModLoader.GetMod(Name) as FargowiltasSouls.FargowiltasSouls;
        }

        public static class Mutant
        {
            public const string Name = "Fargowiltas";
            public static bool Loaded => ModLoader.HasMod(Name);
            public static Mod Mod => ModLoader.GetMod(Name);
        }

        public static class Redemption
        {
            public const string Name = "Redemption";
            public static bool Loaded => ModLoader.HasMod(Name);
            public static Mod Mod => ModLoader.GetMod(Name);
        }

        public static class SacredTools
        {
            public const string Name = "SacredTools";
            public static bool Loaded => ModLoader.HasMod(Name);
            public static Mod Mod => ModLoader.GetMod(Name);
        }

        public static class Calamity
        {
            public const string Name = "CalamityMod";
            public static bool Loaded => ModLoader.HasMod(Name);
            public static Mod Mod => ModLoader.GetMod(Name);
        }

        public override void PostSetupContent()
        {
            DoBossChecklistIntegration();
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
            int spawnItem2 = ModContent.ItemType<ShtuxianCurseEX>();
            List<int> collectibles2 = new List<int>() { ModContent.ItemType<Sadism>(), };

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
        }

        public static class BossChecklist
        {
            public static void AdjustValues()
            {
                Souls.Mod.BossChecklistValues["MutantBoss"] = 29f;
                /*if (Redemption.Loaded){
                	Redemption.Mod.BossChecklistValues["Nebuleus"] = 20f;}
                if (SacredTools.Loaded){
                    SacredTools.Mod.BossChecklistValues["Nihilus"] = 24f;}*/
            }
        }
    }
}