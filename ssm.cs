global using LumUtils = Luminance.Common.Utilities.Utilities;
global using FargowiltasSouls.Core.ModPlayers;
global using FargowiltasSouls.Core.Toggler;
using ssm.Content.Sky;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using System;
using ssm.Items;
using ssm.Core;
using ssm.Systems;
using ssm.Thorium;
using System.Collections.Generic;
using ssm.SoA;
using ssm.Redemption;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Pets;
using FargowiltasSouls.Content.Items.Placables.MusicBoxes;
using System.Linq;
using Terraria.Localization;
using FargowiltasSouls.Content.Items.Placables.Trophies;
using ssm.Content.Items.Consumables;
using ssm.Content.NPCs.MutantEX;

namespace ssm
{
    public class ssm : Mod
    {
        public Dictionary<string, float> BossChecklistValues = new Dictionary<string, float>
        {
            {"DukeFishronEX", float.MaxValue-5},
            {"Shtuxibus", float.MaxValue-4},
            {"Echdeath", float.MaxValue-3},
            {"MutantEX", float.MaxValue-2},
            {"Chtuxlagor", float.MaxValue-1},
            {"StarlightCatBoss", float.MaxValue}
        };

        // Swarms
        public static bool PostMLSwarmActive;
        public static bool HardmodeSwarmActive;
        public static bool SwarmNoHyperActive;
        public static int SwarmItemsUsed;
        internal static bool SwarmSetDefaults;
        public static bool SwarmActive;
        public static int SwarmKills;
        public static int SwarmTotal;
        public static int SwarmSpawned;

        internal static ModKeybind dotMount;

        internal static ssm Instance;
        public static bool debug = ShtunConfig.Instance.DebugMode;

        public static bool amiactive;
        public static readonly BindingFlags UniversalBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        public static bool legit;
        public static int OS;
        public static int[] AllStationIDs { get; private set; }
        public static string userName = Environment.UserName;
        public static string filePath = "C:/Users/" + userName + "/Documents/My Games/Terraria/tModLoader/StarlightSouls";

        public override uint ExtraPlayerBuffSlots => 300u;

        public static int SwarmMinDamage
        {
            get
            {
                float num = ((!ssm.HardmodeSwarmActive) ? ((float)(50 + 3 * ssm.SwarmItemsUsed)) : ((float)(60 + 40 * ssm.SwarmItemsUsed)));
                return (int)num;
            }
        }

        public override void PostAddRecipes()
        {
            AllStationIDs = ShtunUtils.GetAllCraftingStationTileIDs().ToArray();
        }

        private void BossChecklistCompatibility()
        {
            if (ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
            {
                static bool AllPlayersAreDead() => Main.player.All(plr => !plr.active || plr.dead);

                void Add(string type, string bossName, List<int> npcIDs, Func<bool> downed, Func<bool> available, List<int> collectibles, List<int> spawnItems, bool hasKilledAllMessage, string portrait = null)
                {
                    bossChecklist.Call(
                        $"Log{type}",
                        this,
                        bossName,
                        BossChecklistValues[bossName],
                        downed,
                        npcIDs,
                        new Dictionary<string, object>()
                        {
                            { "spawnItems", spawnItems },
                            { "availability", available },
                            { "despawnMessage", hasKilledAllMessage ? new Func<NPC, LocalizedText>(npc =>
                                        AllPlayersAreDead() ? Language.GetText($"Mods.{Name}.NPCs.{bossName}.BossChecklistIntegration.KilledAllMessage") : Language.GetText($"Mods.{Name}.NPCs.{bossName}.BossChecklistIntegration.DespawnMessage")) :
                                    Language.GetText($"Mods.{Name}.NPCs.{bossName}.BossChecklistIntegration.DespawnMessage") },
                            {
                                "customPortrait",
                                portrait == null ? null : new Action<SpriteBatch, Rectangle, Color>((spriteBatch, rect, color) =>
                                {
                                    Texture2D tex = Assets.Request<Texture2D>(portrait, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                                    Rectangle sourceRect = tex.Bounds;
                                    float scale = Math.Min(1f, (float)rect.Width / sourceRect.Width);
                                    spriteBatch.Draw(tex, rect.Center.ToVector2(), sourceRect, color, 0f, sourceRect.Size() / 2, scale, SpriteEffects.None, 0);
                                })
                            }
                        }
                    );
                }

                Add("Boss",
                    "MutantEX",
                    new List<int> { ModContent.NPCType<MutantEX>() },
                    () => WorldSaveSystem.downedMutantEX,
                    () => true,
                    new List<int> {
                        ModContent.ItemType<MutantMusicBox>(),
                        ModContent.ItemType<Sadism>(),
                        ModContent.ItemType<MutantTrophy>(),
                        ModContent.ItemType<SpawnSack>(),
                        ModContent.ItemType<PhantasmalEnergy>()
                    },
                    new List<int> { ModContent.ItemType<MutantsCurseEX>() },
                    true
                );

            }
        }


        public override void Load()
        {
            ModIntergationSystem.BossChecklist.AdjustValues();

            Instance = this;
            OS = OSType();

            dotMount = KeybindLoader.RegisterKeybind(this, "Dot Mount", "H");

            CaughtNPCItem.RegisterItems();

            if (ModLoader.TryGetMod("ThoriumMod", out Mod tor))
            {
                ThoriumCaughtNpcs.ThoriumRegisterItems();
            }
            if (ModLoader.TryGetMod("SacredTools", out Mod soa))
            {
                SoACaughtNpcs.SoARegisterItems();
            }
            if (ModLoader.TryGetMod("Redemption", out Mod red))
            {
                RedemptionCaughtNpcs.RedemptionRegisterItems();
            }

            SkyManager.Instance["ssm:MutantEX"] = new MutantEXSky();

            ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist);
        }

        public override void PostSetupContent()
        {
            BossChecklistCompatibility();
            if (ModCompatibility.Thorium.Loaded)
            {
                PostSetupContentThorium.PostSetupContent_Thorium();
            }
        }
       
        public int OSType()
        {
            OperatingSystem os = Environment.OSVersion;
            PlatformID platform = os.Platform;
            switch (platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    filePath = "C:/Users/" + userName + "/Documents/My Games/Terraria/tModLoader/StarlightSouls";
                    return 0;
                case PlatformID.Unix:
                    filePath = "/home/" + userName + "/.local/share/Terraria/tModLoader/StarlightSouls";
                    return 1;
                case PlatformID.MacOSX:
                    filePath = "/Users/" + userName + "/Library/Application Support/Terraria/tModLoader/StarlightSouls";
                    return 2;
                default:
                    filePath = Main.SavePath + "/ModLoader/StarlightSouls";
                    break;
            }
            return -1;
        }
    }
}
