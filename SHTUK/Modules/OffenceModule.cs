using FargowiltasSouls.Content.Items.Materials;
using ssm.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.SHTUK.Modules
{
    public class OffenceModule : BaseModule
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExperimentalContent;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 30;
            Item.rare = 11;
            Item.useStyle = 2;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.consumable = true;
            Item.UseSound = SoundID.Item3;
            Item.value = Item.sellPrice(10, 0, 0, 0);
        }

        public override bool? UseItem(Player player)
        {
            if (player.SHTUK().isAdvancedCyborg && player.Modules().offenceModule < tier)
            {
                player.Modules().offenceModule = tier;
            }
            return true;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ShtuxiumBar>();
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient<EternalEnergy>();
            recipe2.Register();

            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient<AbomEnergy>();
            recipe3.Register();

            Recipe recipe4 = CreateRecipe();
            recipe4.AddIngredient<Eridanium>();
            recipe4.Register();

            Recipe recipe5 = CreateRecipe();
            recipe5.AddIngredient<DeviatingEnergy>();
            recipe5.Register();
        }
    }
}
