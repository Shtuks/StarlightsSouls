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

namespace ssm
{
    public class ssm : Mod
    {
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

        internal bool CalamityLoaded;
        internal bool FargoLoaded;
        internal bool SoulsLoaded;
        internal bool RedemptionLoaded;
        internal bool SoALoaded;

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

            if(ModLoader.TryGetMod("CalamityMod", out Mod kal))
            {
                CalCaughtNpcs.CalRegisterItems();
            }
            if (ModLoader.TryGetMod("ThoriumMod", out Mod tor))
            {
                //TorCaughtNpcs.TorRegisterItems();
            }
            if (ModLoader.TryGetMod("SacredTools", out Mod soa))
            {
                //SoACaughtNpcs.SoARegisterItems();
            }
            if (ModLoader.TryGetMod("Redemption", out Mod red))
            {
                //RedCaughtNpcs.RedRegisterItems();
            }

            //SkyManager.Instance["ssm:Shtuxibus"] = new ShtuxibusSky();
            SkyManager.Instance["ssm:Chtuxlagor"] = new ChtuxlagorSky();

            ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist);

            if (Main.netMode != NetmodeID.Server)
            {
                #region shaders
                //loading refs for shaders
                Ref<Effect> lcRef = new Ref<Effect>(Assets.Request<Effect>("Effects/LifeChampionShader", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> wcRef = new Ref<Effect>(Assets.Request<Effect>("Effects/WillChampionShader", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> gaiaRef = new Ref<Effect>(Assets.Request<Effect>("Effects/GaiaShader", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> textRef = new Ref<Effect>(Assets.Request<Effect>("Effects/TextShader", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> invertRef = new Ref<Effect>(Assets.Request<Effect>("Effects/Invert", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> finalSparkRef = new Ref<Effect>(Assets.Request<Effect>("Effects/FinalSpark", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> mutantDeathrayRef = new(Assets.Request<Effect>("Effects/PrimitiveShaders/MutantFinalDeathrayShader", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> willDeathrayRef = new(Assets.Request<Effect>("Effects/PrimitiveShaders/WillDeathrayShader", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> willBigDeathrayRef = new(Assets.Request<Effect>("Effects/PrimitiveShaders/WillBigDeathrayShader", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> deviBigDeathrayRef = new(Assets.Request<Effect>("Effects/PrimitiveShaders/DeviTouhouDeathrayShader", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> deviRingRef = new(Assets.Request<Effect>("Effects/PrimitiveShaders/DeviRingShader", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> genericDeathrayRef = new(Assets.Request<Effect>("Effects/PrimitiveShaders/GenericDeathrayShader", AssetRequestMode.ImmediateLoad).Value);
                Ref<Effect> blobTrailRef = new(Assets.Request<Effect>("Effects/PrimitiveShaders/BlobTrailShader", AssetRequestMode.ImmediateLoad).Value);
                GameShaders.Misc["PulseUpwards"] = new MiscShaderData(textRef, "PulseUpwards");
                GameShaders.Misc["PulseDiagonal"] = new MiscShaderData(textRef, "PulseDiagonal");
                GameShaders.Misc["PulseCircle"] = new MiscShaderData(textRef, "PulseCircle");
                GameShaders.Misc["ssm:MutantDeathray"] = new MiscShaderData(mutantDeathrayRef, "TrailPass");
                GameShaders.Misc["ssm:WillDeathray"] = new MiscShaderData(willDeathrayRef, "TrailPass");
                GameShaders.Misc["ssm:WillBigDeathray"] = new MiscShaderData(willBigDeathrayRef, "TrailPass");
                GameShaders.Misc["ssm:DeviBigDeathray"] = new MiscShaderData(deviBigDeathrayRef, "TrailPass");
                GameShaders.Misc["ssm:DeviRing"] = new MiscShaderData(deviRingRef, "TrailPass");
                GameShaders.Misc["ssm:GenericDeathray"] = new MiscShaderData(genericDeathrayRef, "TrailPass");
                GameShaders.Misc["ssm:BlobTrail"] = new MiscShaderData(blobTrailRef, "TrailPass");
                Filters.Scene["ssm:Solar"] = new Filter(Filters.Scene["MonolithSolar"].GetShader(), EffectPriority.Medium);
                Filters.Scene["ssm:Vortex"] = new Filter(Filters.Scene["MonolithVortex"].GetShader(), EffectPriority.Medium);
                Filters.Scene["ssm:Nebula"] = new Filter(Filters.Scene["MonolithNebula"].GetShader(), EffectPriority.Medium);
                Filters.Scene["ssm:Stardust"] = new Filter(Filters.Scene["MonolithStardust"].GetShader(), EffectPriority.Medium);
                #endregion shaders
            }
        }

        public override void PostSetupContent()
        {
            try
            {
                CalamityLoaded = ModLoader.GetMod("CalamityMod") != null;
                SoulsLoaded = ModLoader.GetMod("FargowiltasSouls") != null;
                FargoLoaded = ModLoader.GetMod("Fargowiltas") != null;
                SoALoaded = ModLoader.GetMod("SacredTools") != null;
                RedemptionLoaded = ModLoader.GetMod("Redemption") != null;
            }
            catch (Exception e)
            {
                Logger.Warn("ssm PostSetupContent Error: " + e.StackTrace + e.Message);
            }

            if (ModCompatibility.Thorium.Loaded)
            {
                PostSetupContentThorium.PostSetupContent_Thorium();
            }
            Func<string> cap = () => $"Shield Capacity: {Main.LocalPlayer.Shield().shieldCapacity}%";
            Func<string> reg = () => $"Shield Regeneration: {Main.LocalPlayer.Shield().shieldRegenSpeed}%";
            Func<string> max = () => $"Max Shield Capacity: {Main.LocalPlayer.Shield().shieldCapacityMax2}%";
            //Func<string> res = () => $"RAD resistance: {Main.LocalPlayer.Radiation().statRes}";
            //Func<string> rad = () => $"RAD: {Main.LocalPlayer.Radiation().statRad}";
            ModCompatibility.MutantMod.Mod.Call("AddStat", ItemID.ObsidianShield, cap);
            ModCompatibility.MutantMod.Mod.Call("AddStat", ItemID.SquireShield, reg);
            ModCompatibility.MutantMod.Mod.Call("AddStat", ItemID.PaladinsShield, max);
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
        public static int FindClosestHostileNPC(Vector2 location, float detectionRange, bool lineCheck = false)
        {
            NPC closestNpc = null;
            foreach (NPC n in Main.npc)
            {
                if (n.CanBeChasedBy() && n.Distance(location) < detectionRange && (!lineCheck || Collision.CanHitLine(location, 0, 0, n.Center, 0, 0)))
                {
                    detectionRange = n.Distance(location);
                    closestNpc = n;
                }
            }
            return closestNpc == null ? -1 : closestNpc.whoAmI;
        }
        public static int FindClosestHostileNPCPrioritizingMinionFocus(Projectile projectile, float detectionRange, bool lineCheck = false, Vector2 center = default)
        {
            if (center == default)
                center = projectile.Center;

            NPC minionAttackTargetNpc = projectile.OwnerMinionAttackTargetNPC;
            if (minionAttackTargetNpc != null && minionAttackTargetNpc.CanBeChasedBy() && minionAttackTargetNpc.Distance(center) < detectionRange
                && (!lineCheck || Collision.CanHitLine(center, 0, 0, minionAttackTargetNpc.Center, 0, 0)))
            {
                return minionAttackTargetNpc.whoAmI;
            }
            return FindClosestHostileNPC(center, detectionRange, lineCheck);
        }

    }
}
