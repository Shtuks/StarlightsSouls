global using LumUtils = Luminance.Common.Utilities.Utilities;
global using FargowiltasSouls.Core.ModPlayers;
global using FargowiltasSouls.Core.Toggler;
using ssm.Content.Sky;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using System;
using ssm.Items;
using ssm.Calamity;
using ssm.Core;
using ssm.Systems;
using ssm.Content.Items;
using ssm.Thorium;
using System.Collections.Generic;
using ssm.SoA;
using ssm.Redemption;
using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Bosses.BanishedBaron;
using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using FargowiltasSouls.Content.Bosses.Champions.Earth;
using FargowiltasSouls.Content.Bosses.Champions.Life;
using FargowiltasSouls.Content.Bosses.Champions.Nature;
using FargowiltasSouls.Content.Bosses.Champions.Shadow;
using FargowiltasSouls.Content.Bosses.Champions.Spirit;
using FargowiltasSouls.Content.Bosses.Champions.Terra;
using FargowiltasSouls.Content.Bosses.Champions.Timber;
using FargowiltasSouls.Content.Bosses.Champions.Will;
using FargowiltasSouls.Content.Bosses.CursedCoffin;
using FargowiltasSouls.Content.Bosses.DeviBoss;
using FargowiltasSouls.Content.Bosses.Lifelight;
using FargowiltasSouls.Content.Bosses.TrojanSquirrel;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Pets;
using FargowiltasSouls.Content.Items.Placables.MusicBoxes;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Items.Weapons.Challengers;
using FargowiltasSouls.Core.Systems;
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


        internal static ModKeybind shtuxianSuper;
        internal static ModKeybind dotMount;
        internal static ModKeybind shtukTeleport;
        internal static ModKeybind shtukCharge;

        internal static ssm Instance;
        public static bool debug = ShtunConfig.Instance.DebugMode;

        internal Mod bossChecklist;

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

            shtuxianSuper = KeybindLoader.RegisterKeybind(this, "Shtuxian Domination", "L");
            dotMount = KeybindLoader.RegisterKeybind(this, "Dot Mount", "H");
            shtukTeleport = KeybindLoader.RegisterKeybind(this, "Teleportation Module", "Z");
            shtukCharge = KeybindLoader.RegisterKeybind(this, "Energy charging", "C");

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

            //SkyManager.Instance["ssm:Shtuxibus"] = new ShtuxibusSky();
            //SkyManager.Instance["ssm:Chtuxlagor"] = new ChtuxlagorSky();
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
            //Func<string> cap = () => $"Shield Capacity: {Main.LocalPlayer.Shield().shieldCapacity}%";
            //Func<string> reg = () => $"Shield Regeneration: {Main.LocalPlayer.Shield().shieldRegenSpeed}%";
            //Func<string> max = () => $"Max Shield Capacity: {Main.LocalPlayer.Shield().shieldCapacityMax2}%";
            //Func<string> res = () => $"RAD resistance: {Main.LocalPlayer.Radiation().statRes}";
            //Func<string> rad = () => $"RAD: {Main.LocalPlayer.Radiation().statRad}";
            //ModCompatibility.MutantMod.Mod.Call("AddStat", ItemID.ObsidianShield, cap);
            //ModCompatibility.MutantMod.Mod.Call("AddStat", ItemID.SquireShield, reg);
            //ModCompatibility.MutantMod.Mod.Call("AddStat", ItemID.PaladinsShield, max);
            //ModCompatibility.MutantMod.Mod.Call("AddStat", ModContent.ItemType<RadiationDebug>(), res);
            //ModCompatibility.MutantMod.Mod.Call("AddStat", ModContent.ItemType<RadiationDebug>(), rad);
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

        public override void Unload()
        {
            bossChecklist = null;
        }

        static float ColorTimer2;

        public static Color ShtuxibusColor()
        {
            Color mutantColor = new Color(28, 222, 152);
            Color abomColor = new Color(255, 224, 53);
            Color deviColor = new Color(255, 51, 153);
            ColorTimer2 += 0.5f;
            if (ColorTimer2 >= 300)
            {
                ColorTimer2 = 0;
            }

            if (ColorTimer2 < 100)
                return Color.Lerp(mutantColor, abomColor, ColorTimer2 / 100);
            else if (ColorTimer2 < 200)
                return Color.Lerp(abomColor, deviColor, (ColorTimer2 - 100) / 100);
            else
                return Color.Lerp(deviColor, mutantColor, (ColorTimer2 - 200) / 100);
        }
        public static Color ChtuxlagorColor()
        {
            Color mutantColor = new Color(28, 222, 152);
            Color abomColor = new Color(255, 224, 53);
            Color deviColor = new Color(255, 51, 153);
            ColorTimer2 += 0.5f;
            if (ColorTimer2 >= 300)
            {
                ColorTimer2 = 0;
            }

            if (ColorTimer2 < 100)
                return Color.Lerp(mutantColor, abomColor, ColorTimer2 / 100);
            else if (ColorTimer2 < 200)
                return Color.Lerp(abomColor, deviColor, (ColorTimer2 - 100) / 100);
            else
                return Color.Lerp(deviColor, mutantColor, (ColorTimer2 - 200) / 100);
        }
        public static Color ShtuxibusSkyColor()
        {
            Color Green = new Color(173, 247, 125);
            Color Yellow = new Color(233, 214, 94);
            Color Orange = new Color(238, 135, 66);
            Color Red = new Color(230, 68, 82);
            Color Pink = new Color(237, 71, 138);
            Color Magenta = new Color(187, 95, 247);
            Color Blue = new Color(138, 120, 254);
            Color Cyean = new Color(101, 209, 224);
            ColorTimer2 += 0.5f;
            if (ColorTimer2 >= 900)
            {
                ColorTimer2 = 0;
            }

            if (ColorTimer2 < 100)
                return Color.Lerp(Green, Yellow, ColorTimer2 / 100);
            else if (ColorTimer2 < 200)
                return Color.Lerp(Yellow, Orange, (ColorTimer2 - 100) / 100);
            else if (ColorTimer2 < 300)
                return Color.Lerp(Orange, Red, (ColorTimer2 - 200) / 100);
            else if (ColorTimer2 < 400)
                return Color.Lerp(Red, Pink, (ColorTimer2 - 300) / 100);
            else if (ColorTimer2 < 500)
                return Color.Lerp(Pink, Magenta, (ColorTimer2 - 400) / 100);
            else if (ColorTimer2 < 600)
                return Color.Lerp(Magenta, Blue, (ColorTimer2 - 500) / 100);
            else if (ColorTimer2 < 700)
                return Color.Lerp(Blue, Cyean, (ColorTimer2 - 600) / 100);
            else
                return Color.Lerp(Cyean, Green, (ColorTimer2 - 700) / 100);
        }
        public static Color ChtuxlagorSkyColor()
        {
            Color mutantColor = new Color(255, 0, 0);
            Color abomColor = new Color(0, 255, 0);
            Color deviColor = new Color(0, 0, 255);
            ColorTimer2 += 0.5f;
            if (ColorTimer2 >= 300)
            {
                ColorTimer2 = 0;
            }

            if (ColorTimer2 < 100)
                return Color.Lerp(mutantColor, abomColor, ColorTimer2 / 100);
            else if (ColorTimer2 < 200)
                return Color.Lerp(abomColor, deviColor, (ColorTimer2 - 100) / 100);
            else
                return Color.Lerp(deviColor, mutantColor, (ColorTimer2 - 200) / 100);
        }

    }
}
