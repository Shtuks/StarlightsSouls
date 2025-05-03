using Terraria;
using Terraria.ModLoader;
using ssm.CrossMod.CraftingStations;
using ssm.Content.Items.Consumables;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.Content.Items.Accessories;
using Fargowiltas.Items.Vanity;

namespace ssm.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class TrueLumberjackPants : ModItem
    {
        public override void SetDefaults()
        {
            ((Entity)this.Item).width = 18;
            ((Entity)this.Item).height = 18;
            this.Item.rare = 11;
            this.Item.expert = true;
            this.Item.value = Item.sellPrice(100, 0, 0, 0);
            this.Item.defense = int.MaxValue / 1000;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += int.MaxValue / 1000;
            player.GetCritChance(DamageClass.Generic) += int.MaxValue / 1000;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<LumberjackPants>();

            recipe.AddIngredient<Sadism>(100);
            recipe.AddIngredient<Soul>(4);

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}
