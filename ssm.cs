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
using ssm.Content.UI;
using Terraria.UI;
using ssm.CrossMod.CraftingStations;

namespace ssm
{
    public partial class ssm : Mod
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

        internal static ModKeybind dotMount;

        public UserInterface _bossSummonUI;
        public BossSummonUI _bossSummonState;
        public bool _showBossSummonUI;

        internal static ssm Instance;
        public static bool debug = ShtunConfig.Instance.DebugMode;

        private delegate void UIModDelegate(object instance, SpriteBatch spriteBatch);
        public static bool amiactive;
        public static readonly BindingFlags UniversalBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        public static bool legit;
        public static int OS;
        public static int[] AllStationIDs { get; private set; }
        public static string userName = Environment.UserName;
        public static string filePath = "C:/Users/" + userName + "/Documents/My Games/Terraria/tModLoader/StarlightSouls";

        //public override uint ExtraPlayerBuffSlots => 300u;

        public static int SwarmMinDamage
        {
            get
            {
                float num = ((!ssm.HardmodeSwarmActive) ? ((float)(50 + 3 * ssm.SwarmItemsUsed)) : ((float)(60 + 40 * ssm.SwarmItemsUsed)));
                return (int)num;
            }
        }
        private void BossChecklistCompatibility()
        {
            if (ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
            {
                static bool AllPlayersAreDead() => Main.player.All(plr => !plr.active || plr.dead);

                void Add(string type, string bossName, float priority, List<int> npcIDs, Func<bool> downed, Func<bool> available, List<int> collectibles, List<int> spawnItems, bool hasKilledAllMessage, string portrait = null)
                {
                    bossChecklist.Call(
                        $"Log{type}",
                        this,
                        bossName,
                        priority,
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
                    float.MaxValue,
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
                    new List<int> { ModContent.ItemType<MutantsForgeItem>() },
                    true
                );

                if (ModCompatibility.SacredTools.Loaded)
                {
                    ModCompatibility.SacredTools.Mod.TryFind<ModNPC>("Nihilus", out ModNPC Nihilus);
                    ModCompatibility.SacredTools.Mod.TryFind<ModItem>("EmberOfOmen", out ModItem Ember);
                    ModCompatibility.SacredTools.Mod.TryFind<ModItem>("NihilusObelisk", out ModItem Obelisk);
                    
                    Add("Boss",
                    "Nihilus",
                    int.MaxValue-100,
                    new List<int> {Nihilus.Type},
                    () => WorldSaveSystem.downedNihilus,
                    () => true,
                    new List<int> {
                        Ember.Type
                    },
                    new List<int> { Obelisk.Type },
                    true
                );
                }
            }
        }

        public override void Load()
        {
            //// Load the overlay texture
            //overlayTexture = ModContent.Request<Texture2D>("ssm/Assets/ModIcon/LifeStar", AssetRequestMode.ImmediateLoad).Value;

            //// Get the UIModItem.Draw method using reflection
            //Type uiModItemType = typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.UIModItem");
            //MethodInfo drawMethod = uiModItemType.GetMethod("Draw", BindingFlags.Instance | BindingFlags.Public);

            //// Add the hook with a compatible delegate
            //MonoModHooks.Add(drawMethod, (Action<object, SpriteBatch>)DrawHook);

            _bossSummonUI = new UserInterface();
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

        public override void Unload()
        {
            //overlayTexture = null;
            if (_bossSummonUI != null)
            {
                _bossSummonUI = null;
            }
            Instance = null;
        }

        public void ShowBossSummonUI()
        {
            if (_bossSummonState == null)
            {
                _bossSummonState = new BossSummonUI();
                _bossSummonUI.SetState(_bossSummonState);
            }
            _showBossSummonUI = true;
        }
        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("Wikithis", out Mod wikithis) && !Main.dedServ)
            {
                wikithis.Call("AddModURL", this, "https://terrariamods.wiki.gg/wiki/Community_Souls_Expansion/{}");
            }
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
