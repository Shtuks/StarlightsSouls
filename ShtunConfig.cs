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
    private const int MinItemsUsedInSingularity = 500;
    private const int MaxItemsUsedInSingularity = 10000;
    public static ShtunConfig Instance;

    [Header("Crafting")]
    
    [BackgroundColor(60, 200, 60, 192)]
    [SliderColor(60, 230, 100, 128)]
    [Range(500, 100000)]
    [DefaultValue(1000)]
    public int ItemsUsedInSingularity { get; set; }

    [BackgroundColor(60, 200, 60, 192)]
    [DefaultValue(true)]
    public bool SoulOfShtundexCraftable { get; set; }

    [Header("Bosses")]

    [BackgroundColor(60, 200, 60, 192)]
    [DefaultValue(true)]
    public bool MultiversalHorrorOfChtuxlagorOnGetfixedboi { get; set; }

    [BackgroundColor(60, 200, 60, 192)]
    [DefaultValue(true)]
    public bool ShtuxibusRayOneshot { get; set; }

    [BackgroundColor(60, 200, 60, 192)]
    [DefaultValue(true)]
    public bool ShtuxibusKillInGodmode { get; set; }

    [BackgroundColor(60, 200, 60, 192)]
    [DefaultValue(true)]
    public bool ShtuxibusBreakRodOfHarmony { get; set; }

    [Header("Visual")]

    [BackgroundColor(60, 200, 60, 192)]
    [DefaultValue(true)]
    public bool ForcedFilters;
  }
}
