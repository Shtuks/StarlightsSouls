using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using ssm.Core;
using Terraria.ModLoader;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Armor.GemTech;
using ssm.Content.Tiles;
using ssm.CrossMod.CraftingStations;

namespace ssm.Content.Items.Armor
{
    //[ExtendsFromMod(ModCompatibility.Calamity.Name)]
    //[JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [AutoloadEquip(EquipType.Legs)]
    public class TrueMonstrosityPants : ModItem
    {
        public override void SetDefaults()
        {
            ((Entity)this.Item).width = 18;
            ((Entity)this.Item).height = 18;
            this.Item.rare = 11;
            this.Item.expert = true;
            this.Item.value = Item.sellPrice(10, 0, 0, 0);
            this.Item.defense = 100;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 1f;
            player.GetCritChance(DamageClass.Generic) += 2f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<MonstrosityPants>();

            recipe.AddTile<MutantsForgeTile>();
        }
    }
}
