using Terraria;
using Terraria.ModLoader;
using CalamityMod.Items.Accessories;
using ssm.Core;
using SacredTools.Content.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using CalamityMod.Items.Accessories.Wings;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ID;

namespace ssm.CrossMod.Boots
{
    /*
        * Progression look like this:
        * terraspark
        * zephyr boots
        * angel treads
        * royal runners
        * aeolus boots
        * celestial treads
        * void spurs
        * elysean tracers
        * seraph tracers.
    */

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class SoACalBootsRecepies : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Boots && !ModLoader.HasMod(ModCompatibility.Thorium.Name);
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
                // treads to runners
                if (recipe.HasResult(ModContent.ItemType<RoyalRunners>()) && recipe.HasIngredient(5000))
                {
                    recipe.RemoveIngredient(5000);
                    recipe.AddIngredient<AngelTreads>(1);
                }
                // runners to aeolus
                if (recipe.HasResult(ModContent.ItemType<AeolusBoots>()) && (recipe.HasIngredient<AngelTreads>() || recipe.HasIngredient<ZephyrBoots>()))
                {
                    recipe.AddIngredient(ItemID.Ectoplasm, 10);
                    recipe.RemoveIngredient(ItemID.SoulofMight);
                    recipe.RemoveIngredient(ItemID.SoulofSight);
                    recipe.RemoveIngredient(ItemID.SoulofFright);
                    recipe.RemoveIngredient(ModContent.ItemType<AngelTreads>());
                    recipe.RemoveIngredient(ModContent.ItemType<ZephyrBoots>());
                    recipe.AddIngredient<RoyalRunners>(1);
                }
                //aeolus to celestial (if no cal dlc)
                if (recipe.HasResult(ModContent.ItemType<TracersCelestial>()) && recipe.HasIngredient<AngelTreads>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<AngelTreads>());
                    recipe.AddIngredient<AeolusBoots>(1);
                }
                //celestial to spurs
                if (recipe.HasResult(ModContent.ItemType<VoidSpurs>()) && recipe.HasIngredient<RoyalRunners>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<RoyalRunners>());
                    recipe.AddIngredient<TracersCelestial>(1);
                }
                //spurs to elysian
                if (recipe.HasResult(ModContent.ItemType<TracersElysian>()) && recipe.HasIngredient<TracersCelestial>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<TracersCelestial>());
                    recipe.AddIngredient<VoidSpurs>(1);
                }
                //elysian to seraph
                //cal code
                //seraph to supersonic (if no cal dlc)
                if (recipe.HasResult(ModContent.ItemType<SupersonicSoul>()) && !recipe.HasIngredient<TracersSeraph>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<AeolusBoots>());
                    recipe.AddIngredient<TracersSeraph>(1);
                }
                //drew to flight
                if (recipe.HasResult(ModContent.ItemType<FlightMasterySoul>()) && !recipe.HasIngredient<DrewsWings>())
                {
                    recipe.AddIngredient<DrewsWings>(1);
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class SoACalBootsEffects : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Boots && !ModLoader.HasMod(ModCompatibility.Thorium.Name);
        }
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<AngelTreads>()
                || Item.type == ModContent.ItemType<RoyalRunners>()
                || Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<TracersCelestial>()
                //|| Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "ZephyrBoots").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<RoyalRunners>()
                || Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<TracersCelestial>()
                //|| Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Calamity.Name, "AngelTreads").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<TracersCelestial>()
                //|| Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "RoyalRunners").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<TracersCelestial>()
                || Item.type == ModContent.ItemType<VoidSpurs>()
                || Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "AeolusBoots").UpdateAccessory(player, false);
            }

            if (//Item.type == ModContent.ItemType<VoidSpurs>()
                Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Calamity.Name, "TracersCelestial").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<TracersElysian>()
                || Item.type == ModContent.ItemType<TracersSeraph>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "VoidSpurs").UpdateAccessory(player, false);
            }
        }
    }

}
