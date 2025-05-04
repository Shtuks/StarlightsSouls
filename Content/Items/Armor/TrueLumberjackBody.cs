using Terraria;
using Terraria.ModLoader;
using ssm.CrossMod.CraftingStations;
using Fargowiltas.Items.Vanity;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.Content.Items.Accessories;
using ssm.Content.Items.Consumables;

namespace ssm.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class TrueLumberjackBody : AnticheatItem
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
            player.statLifeMax2 += int.MaxValue / 100000;
            player.statManaMax2 += int.MaxValue / 100000;
            player.endurance += int.MaxValue / 100;
            //infinitie regen
            player.statLife = player.statLifeMax2;
            player.lifeRegenCount += int.MaxValue / 1000;
            player.lifeRegenTime += int.MaxValue / 1000;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<LumberjackBody>();

            recipe.AddIngredient<Sadism>(100);
            recipe.AddIngredient<StargateSoul>(4);
            //recipe.AddIngredient<ShardOfStarlight>(30);

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}
