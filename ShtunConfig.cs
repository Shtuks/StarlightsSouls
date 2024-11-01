using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ModLoader.Config;

namespace ssm
{
    [BackgroundColor(32, 50, 32, 216)]
    public class ShtunConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static ShtunConfig Instance;

        [Header("General")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool ExtraContent { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool OldCalDlcBalance { get; set; }

        //[ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool WorldEnterMessage { get; set; }

        [Header("Crafting")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [SliderColor(60, 230, 100, 128)]
        [Range(500, 100000)]
        [DefaultValue(1000)]
        public int ItemsUsedInSingularity { get; set; }

        [Header("Shtuxibus")]

        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(false)]
        public bool Stalin { get; set; }

        [Header("Visual")]

        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool ForcedFilters;

        [Header("Calamity")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool CalSwarmItems { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool CalEnchantments { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool CalSingularities { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool CalCaughtNpcs { get; set; }

        [Header("SoA")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool SoASwarmItems { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool SoAEnchantments { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool SoASingularities { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool SoACaughtNpcs { get; set; }

        [Header("Thorium")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool TorSwarmItems { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool TorEnchantments { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool TorSingularities { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool TorCaughtNpcs { get; set; }

        [Header("Redemption")]

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool RedSwarmItems { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool RedEnchantments { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool RedSingularities { get; set; }

        [ReloadRequired]
        [BackgroundColor(60, 200, 60, 192)]
        [DefaultValue(true)]
        public bool RedCaughtNpcs { get; set; }
    }
}
