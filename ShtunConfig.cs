using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ssm
{
    [BackgroundColor(32, 50, 32, 216)]
    public class ShtunConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static ShtunConfig Instance;

        [Header("General")]

        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool WorldEnterMessage { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool DebugMode { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool ExperimentalContent { get; set; }

        [Header("CrossMods")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Boots { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Shields { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool ThrowerMerge { get; set; }

        [Header("EnchantsLoad")]
        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Thorium { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool SacredTools { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Polarities { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Spirit { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Homeward { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Redemption { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Spooky { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool Beekeeper { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool gunrightsmod { get; set; }
    }
}
