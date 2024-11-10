using ssm.Core;
using FargowiltasSouls.Content.Items.Armor;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ssm.Content.Items.Materials;

namespace ssm.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class QuantumChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = 11;
            Item.expert = true;
            Item.value = Item.sellPrice(100, 0, 0, 0);
            Item.defense = 200;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 5f;
            player.GetArmorPenetration(DamageClass.Generic) += 500;
            player.GetCritChance(DamageClass.Generic) += 100;
            player.statLifeMax2 += 2000;
            player.statManaMax2 += 2000;
            player.endurance += 0.5f;
            player.lifeRegen += 10;
            player.lifeRegenCount += 10;
            player.lifeRegenTime += 10;

            player.Shield().shieldOn = true;
            player.Shield().shieldCapacityMax2 += 10000;
            player.Shield().shieldRegenSpeed += 200f;
            player.Shield().shieldHitsRegen += 0.5f;
            player.Shield().shieldHitsCap += 10;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<ChtuxlagorShard>(1);
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