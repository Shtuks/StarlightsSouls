using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ThoriumMod.Items.Terrarium;

namespace ssm.CrossMod.Boots
{
    /*
        * Progression look like this:
        * terraspark
        * zephyr boots
        * aeolus boots
        * terrarium sprinters.
    */

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TorBootsRecepies : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Boots && !ModLoader.HasMod(ModCompatibility.Calamity.Name) && !ModLoader.HasMod(ModCompatibility.SacredTools.Name);
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                //aeolus to sprinters
                if (recipe.HasResult(ModContent.ItemType<TerrariumParticleSprinters>()) && recipe.HasIngredient(5000))
                {
                    recipe.RemoveIngredient(5000);
                    recipe.AddIngredient<AeolusBoots>(1);
                }
                //sprinters to supersonic
                if (recipe.HasResult(ModContent.ItemType<SupersonicSoul>()) && recipe.HasIngredient(ModContent.ItemType<AeolusBoots>()))
                {
                    recipe.RemoveIngredient(ModContent.ItemType<AeolusBoots>());
                    recipe.AddIngredient<TerrariumParticleSprinters>(1);
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TorBootsEffects : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Boots && !ModLoader.HasMod(ModCompatibility.SacredTools.Name) && !ModLoader.HasMod(ModCompatibility.Calamity.Name);
        }
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<TerrariumParticleSprinters>()
                || Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "AeolusBoots").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<SupersonicSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Thorium.Name, "TerrariumParticleSprinters").UpdateAccessory(player, false);
            }
        }
    }
}

