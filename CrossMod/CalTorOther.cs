using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using ThoriumMod.Items.SummonItems;
using Terraria.ID;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using ThoriumMod.Items.Donate;

namespace ssm.CrossMod
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    public class CalTorOther : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ModContent.ItemType<StatisBlessing>()) && !recipe.HasIngredient(ModContent.ItemType<NecroticSkull>()))
                {
                    recipe.AddIngredient<NecroticSkull>(1);
                    recipe.RemoveIngredient(ItemID.PygmyNecklace);
                    recipe.AddIngredient<CoreofCalamity>(3);
                    recipe.RemoveIngredient(ModContent.ItemType<CoreofSunlight>());
                    recipe.RemoveTile(TileID.MythrilAnvil);
                    recipe.AddTile(TileID.LunarCraftingStation);
                }

                if (recipe.HasResult(ModContent.ItemType<StatisCurse>()) && !recipe.HasIngredient(ModContent.ItemType<GalacticaSingularity>()))
                {
                    recipe.RemoveIngredient(ItemID.FragmentStardust);
                    recipe.AddIngredient<GalacticaSingularity>(5);
                    recipe.AddIngredient<Necroplasm>(3);
                }

                if (recipe.HasResult(ModContent.ItemType<EtherealTalisman>()) && !recipe.HasIngredient(ModContent.ItemType<HungeringBlossom>()))
                {
                    recipe.AddIngredient<HungeringBlossom>(1);
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    public class CalTorOtherEffects : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<StatisCurse>()
                || Item.type == ModContent.ItemType<Nucleogenesis>())
            {
                player.maxMinions--;

                ModContent.Find<ModItem>(ModCompatibility.Thorium.Name, "NecroticSkull").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<EtherealTalisman>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Thorium.Name, "HungeringBlossom").UpdateAccessory(player, false);
            }
        }
    }
}
