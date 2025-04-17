using FargowiltasSouls.Content.Items.Accessories.Souls;
using SacredTools.Content.Items.Accessories;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Items.Terrarium;

namespace ssm.CrossMod.Shields
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    public class TorSoaShield : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Shields && !ModCompatibility.Calamity.Loaded;
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                //defender to to celestial
                if (recipe.HasResult(ModContent.ItemType<CelestialShield>()) && recipe.HasIngredient(1613))
                {
                    recipe.RemoveIngredient(1613);
                    recipe.AddIngredient<TerrariumDefender>(1);
                }

                //celestial to reflection

                //reflection to colossus
                if (recipe.HasResult(ModContent.ItemType<ColossusSoul>()) && recipe.HasIngredient(1613))
                {
                    recipe.RemoveIngredient(1613);
                    recipe.AddIngredient<ReflectionShield>(1);
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    public class TorSoaShieldEffects : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Shields && !ModCompatibility.Calamity.Loaded;
        }
        public override bool InstancePerEntity => true;
        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<CelestialShield>()
                || Item.type == ModContent.ItemType<ColossusSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Thorium.Name, "TerrariumDefender").UpdateAccessory(player, false);
            }
            if (Item.type == ModContent.ItemType<ColossusSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "ReflectionShield").UpdateAccessory(player, false);
            }
        }
    }
}
