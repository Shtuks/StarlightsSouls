using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using SacredTools.Content.Items.Accessories;
using ThoriumMod.Items.Terrarium;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;

namespace ssm.CrossMod.Boots
{
    /*
        * Progression look like this:
        * terraspark
        * zephyr boots
        * royal runners
        * terrarium particle sprinters
        * aeolus boots
        * void spurs.
        * 
    */

    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    public class SoATorBootsRecepies : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Boots && !ModLoader.HasMod(ModCompatibility.Calamity.Name);
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // zephyr to runners
                if (recipe.HasResult(ModContent.ItemType<RoyalRunners>()) && recipe.HasIngredient(5000))
                {
                    recipe.RemoveIngredient(5000);
                    recipe.AddIngredient<ZephyrBoots>(1);
                }
                // runners to sprinters
                if (recipe.HasResult(ModContent.ItemType<TerrariumParticleSprinters>()) && recipe.HasIngredient(5000))
                {
                    recipe.RemoveIngredient(5000);
                    recipe.AddIngredient<RoyalRunners>(1);
                }
                // sprinters to aeolus
                if (recipe.HasResult(ModContent.ItemType<AeolusBoots>()) && recipe.HasIngredient<ZephyrBoots>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<ZephyrBoots>());
                    recipe.AddIngredient<TerrariumParticleSprinters>(1);
                }
                //aeolus to spurs
                if (recipe.HasResult(ModContent.ItemType<VoidSpurs>()) && recipe.HasIngredient<RoyalRunners>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<RoyalRunners>());
                    recipe.AddIngredient<AeolusBoots>(1);
                }
                //spurs to supersonic
                if (recipe.HasResult(ModContent.ItemType<SupersonicSoul>()) && recipe.HasIngredient<ZephyrBoots>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<ZephyrBoots>());
                    recipe.AddIngredient<VoidSpurs>(1);
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    public class SoATorBootsEffects : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Boots && !ModLoader.HasMod(ModCompatibility.Calamity.Name);
        }
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<RoyalRunners>()
                || Item.type == ModContent.ItemType<TerrariumParticleSprinters>()
                || Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "ZephyrBoots").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<TerrariumParticleSprinters>()
                || Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "RoyalRunners").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Thorium.Name, "TerrariumParticleSprinters").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "AeolusBoots").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "VoidSpurs").UpdateAccessory(player, false);
            }
        }
    }

}

