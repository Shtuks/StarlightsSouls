using FargowiltasSouls.Content.Items.Accessories.Souls;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class ThoriumSoulsTooltips : GlobalItem
    {
        static string ExpandedTooltipLoc(string line) => Language.GetTextValue($"Mods.ssm.ExpandedTooltips.{line}");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.ssm.Items.AddedEffects.";

            if (item.type == ModContent.ItemType<ColossusSoul>() && !item.social)
            {
                tooltips.Insert(8, new TooltipLine(Mod, "ThoriumColossusSoul", Language.GetTextValue(key + "ThoriumColossus")));
            }

            if (item.type == ModContent.ItemType<UniverseSoul>() && !item.social)
            {
                tooltips.Insert(15, new TooltipLine(Mod, "ThoriumUniverseSoul",
                    Language.GetTextValue(key + "ThoriumUniverse")));
            }

            if (item.type == ModContent.ItemType<DimensionSoul>() && !item.social)
            {
                tooltips.Insert(15, new TooltipLine(Mod, "ThoriumDimestionSoul",
                    Language.GetTextValue(key + "ThoriumColossus")));
            }
        }
    }
}
