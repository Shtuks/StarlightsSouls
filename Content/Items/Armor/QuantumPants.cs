using FargowiltasSouls.Content.Items.Armor;
using Microsoft.Xna.Framework;
using ssm.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using ssm.Core;
using Terraria.ModLoader;

namespace ssm.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class QuantumPants : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExtraContent;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = 11;
            Item.expert = true;
            Item.value = Item.sellPrice(100, 0, 0, 0);
            Item.defense = 150;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 5f;
            player.GetCritChance(DamageClass.Generic) += 100;
            player.endurance += 0.5f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<ShtuxiumBar>(20);
            recipe.AddIngredient<MutantBody>();
            recipe.AddIngredient<StyxChestplate>();

            if (ModCompatibility.Calamity.Loaded)
            {
                recipe.AddIngredient<TrueAuricTeslaBody>();
                recipe.AddIngredient<TrueMonstrositySuit>();
            }
        }
    }
}
