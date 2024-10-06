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

namespace ssm.Systems
{
    public class ModIntegrationsSystem : ModSystem
    {
        public static class Souls
        {
            public const string Name = "FargowiltasSouls";
            public static bool Loaded => ModLoader.HasMod(Name);
            public static FargowiltasSouls.FargowiltasSouls Mod => ModLoader.GetMod(Name) as FargowiltasSouls.FargowiltasSouls;
        }

        public static class Mutant
        {
            public const string Name = "SacredTools";
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
            public const string Name = "SacredTools";
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
            string internalName = "Shtuxibus";
            float weight = 745f;
            Func<bool> downed = () => WorldSaveSystem.downedShtuxibus;
            int bossType = ModContent.NPCType<Shtuxibus>();
            int spawnItem = ModContent.ItemType<ShtuxianCurse>();
            List<int> collectibles = new List<int>() { ModContent.ItemType<ShtuxiumSoulShard>(), };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalName,
                weight,
                downed,
                bossType,
                new Dictionary<string, object>()
                {
                    ["spawnInfo"] = (object)Language.GetText("Mods.ssm.NPCs.Shtuxibus.BossChecklistIntegration.SpawnInfo").WithFormatArgs(Array.Empty<object>()),
                    ["despawnMessage"] = (object)Language.GetText("Mods.ssm.NPCs.Shtuxibus.BossChecklistIntegration.DespawnMessage"),
                    ["spawnItems"] = spawnItem,
                    ["collectibles"] = collectibles,
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