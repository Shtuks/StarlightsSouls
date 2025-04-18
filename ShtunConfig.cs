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
        public bool SafeMode { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool DebugMode { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool ExperimentalContent { get; set; }

        [Header("Crafting")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [SliderColor(60, 230, 100, 128)]
        [Range(500, 100000)]
        [DefaultValue(1000)]
        public int ItemsUsedInSingularity { get; set; }

        [Header("Bosses")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool Stalin { get; set; }

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
        [DefaultValue(true)]
        public bool ThrowerMerge { get; set; }
    }
}
