using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using SacredTools.Content.Items.Accessories;
using Terraria;
using ssm.Thorium.Souls;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    class ShtunThoriumRecipes : ModSystem
	{
		public override void PostAddRecipes()
		{
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];
				if (recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<ThoriumSoul>())
				{
					recipe.AddIngredient<ThoriumSoul>();
				}
			}
		}
	}
}
