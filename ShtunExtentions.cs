using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace ssm
{
    public static class ShtunExtintion
    {
        /// <summary>
        /// Adjusts a TooltipLine to account for prefixes. <br />
        /// Inteded to be used specifically for item names. <br />
        /// This only modifies it in the inventory.
        /// </summary>
        public static TooltipLine ArticlePrefixAdjustment(this TooltipLine itemName, int prefixID, string[] localizationArticles)
        {
            List<string> splitName = itemName.Text.Split(' ').ToList();

            for (int i = 0; i < localizationArticles.Length; i++)
                if (splitName.Remove(localizationArticles[i]))
                {
                    splitName.Insert(0, localizationArticles[i]);
                    break;
                }

            itemName.Text = string.Join(" ", splitName);
            return itemName;
        }

        /// <summary>
        /// Uses <see cref="Enumerable.First{TSource}(IEnumerable{TSource}, System.Func{TSource, bool})"/> to find the specified tooltip line. <br />
        /// Returns true if the tooltipLine isn't null and false if it is. <br />
        /// Assumes Terraria as the mod.
        /// </summary>
        public static bool TryFindTooltipLine(this List<TooltipLine> tooltips, string tooltipName, out TooltipLine tooltipLine)
        {
            tooltips.TryFindTooltipLine(tooltipName, "Terraria", out tooltipLine);

            return tooltipLine != null;
        }

        /// <summary>
        /// Uses <see cref="Enumerable.First{TSource}(IEnumerable{TSource}, System.Func{TSource, bool})"/> to find the specified tooltip line. <br />
        /// Returns true if the tooltipLine isn't null and false if it is.
        /// </summary>
        public static bool TryFindTooltipLine(this List<TooltipLine> tooltips, string tooltipName, string tooltipMod, out TooltipLine tooltipLine)
        {
            tooltipLine = tooltips.First(line => line.Name == tooltipName && line.Mod == tooltipMod);

            return tooltipLine != null;
        }
    }
}