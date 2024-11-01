global using ssm;
global using FargowiltasSouls.Core.ModPlayers;
global using FargowiltasSouls.Core.Toggler;
using ssm.Sky;
using FargowiltasSouls.Content.Sky;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.NPCs;
using FargowiltasSouls.Content.Items.Dyes;
using FargowiltasSouls.Content.Items.Misc;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Tiles;
using FargowiltasSouls.Content.UI;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Bosses.DeviBoss;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Content.Patreon.Volknet;
using System.Reflection;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.UI;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.Localization;
using ssm.Items;
using ssm.Calamity;

namespace ssm
{
    public class ssm : Mod
    {
        // Swarms
        internal static bool SwarmActive;
        internal static int SwarmKills;
        internal static int SwarmTotal;
        internal static int SwarmSpawned;


        internal static ModKeybind shtuxianSuper;
        internal static ssm Instance;

        internal bool CalamityLoaded;
        internal bool FargoLoaded;
        internal bool SoulsLoaded;
        internal bool RedemptionLoaded;
        internal bool SoALoaded;

        internal Mod bossChecklist;

        public static bool amiactive;

        public override uint ExtraPlayerBuffSlots => 300u;


        public override void Load()
        {
            Instance = this;

            shtuxianSuper = KeybindLoader.RegisterKeybind(this, "Shtuxian Domination", "L");

            CaughtNPCItem.RegisterItems();

            if(ModLoader.TryGetMod("CalamityMod", out Mod kal) && ShtunConfig.Instance.CalCaughtNpcs)
            {
                CalCaughtNpcs.CalRegisterItems();
            }
            if (ModLoader.TryGetMod("ThoriumMod", out Mod tor) && ShtunConfig.Instance.TorCaughtNpcs)
            {
                //TorCaughtNpcs.TorRegisterItems();
            }
            if (ModLoader.TryGetMod("SacredTools", out Mod soa) && ShtunConfig.Instance.SoACaughtNpcs)
            {
                //SoACaughtNpcs.SoARegisterItems();
            }
            if (ModLoader.TryGetMod("Redemption", out Mod red) && ShtunConfig.Instance.RedCaughtNpcs)
            {
                //RedCaughtNpcs.RedRegisterItems();
            }

            //FargowiltasCrossmod.FargowiltasCrossmod.EnchantLoadingEnabled = true;
            SkyManager.Instance["ssm:Shtuxibus"] = new ShtuxibusSky();

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

        public override void Unload()
        {
            bossChecklist = null;
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
