using FargowiltasSouls.Content.Items.Materials;
using ssm.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SHTUK.Modules
{
    public abstract class BaseModule : ModItem
    {
        public int tier;
        public bool crafted;

        public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (crafted)
            {
                tooltips.Add(new TooltipLine(Mod, "tier", "Tier - " + tier + " -"));
            }
        }

        public override void OnCraft(Recipe recipe)
        {
            crafted = true;

            if (recipe.HasIngredient<ShtuxiumBar>())
            {
                tier = 5;
            }
            if (recipe.HasIngredient<EternalEnergy>())
            {
                tier = 4;
            }
            if (recipe.HasIngredient<AbomEnergy>())
            {
                tier = 3;
            }
            if (recipe.HasIngredient<Eridanium>())
            {
                tier = 2;
            }
            if (recipe.HasIngredient<DeviatingEnergy>())
            {
                tier = 1;
            }
        }
    }
}
