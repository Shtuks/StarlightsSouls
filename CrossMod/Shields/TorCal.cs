using Terraria;
using Terraria.ModLoader;
using CalamityMod.Items.Accessories;
using ssm.Core;
using ThoriumMod.Items.Terrarium;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using CalamityMod.Items.Accessories.Wings;

namespace ssm.CrossMod.Shields
{
    /*
        * Progression look like this:
        * terraspark
        * zephyr boots
        * angel treads
        * terrarium particle sprinters
        * aeolus boots
        * celestial treads
        * elysean tracers
        * seraph tracers.
    */

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    public class TorCalBootsRecepies : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Boots && !ModLoader.HasMod(ModCompatibility.SacredTools.Name);
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // zephyr to treads (if no dlc)
                if (recipe.HasResult(ModContent.ItemType<AngelTreads>()) && recipe.HasIngredient(5000))
                {
                    recipe.RemoveIngredient(5000);
                    recipe.AddIngredient<ZephyrBoots>(1);
                }
                // treads to sprinters
                if (recipe.HasResult(ModContent.ItemType<TerrariumParticleSprinters>()) && recipe.HasIngredient(5000))
                {
                    recipe.RemoveIngredient(5000);
                    recipe.AddIngredient<AngelTreads>(1);
                }
                // sprinters to aeolus
                if (recipe.HasResult(ModContent.ItemType<AeolusBoots>()) && (recipe.HasIngredient<AngelTreads>() || recipe.HasIngredient<ZephyrBoots>()))
                {
                    recipe.RemoveIngredient(ModContent.ItemType<AngelTreads>());
                    recipe.RemoveIngredient(ModContent.ItemType<ZephyrBoots>());
                    recipe.AddIngredient<TerrariumParticleSprinters>(1);
                }
                //aeolus to celestial (if no cal dlc)
                if (recipe.HasResult(ModContent.ItemType<TracersCelestial>()) && recipe.HasIngredient<AngelTreads>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<AeolusBoots>());
                    recipe.AddIngredient<AeolusBoots>(1);
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    public class TorCalBootsEffects : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Boots && !ModLoader.HasMod(ModCompatibility.SacredTools.Name);
        }
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<AngelTreads>()
                || Item.type == ModContent.ItemType<TerrariumParticleSprinters>()
                || Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<TracersCelestial>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "ZephyrBoots").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<TerrariumParticleSprinters>()
                || Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<TracersCelestial>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Calamity.Name, "AngelTreads").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<TracersCelestial>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Thorium.Name, "TerrariumParticleSprinters").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<TracersCelestial>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "AeolusBoots").UpdateAccessory(player, false);
            }
        }
    }

}

