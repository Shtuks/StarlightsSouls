using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.RangedItems;
using ThoriumMod.Items.Tracker;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.ThrownItems;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class AstroEnchant : BaseEnchant
    {

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ThoriumPlayer modPlayer = player.GetModPlayer<ThoriumPlayer>();
            modPlayer.setAstro = true;
            player.maxMinions++;
            player.maxTurrets++;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<AstroHelmet>());
            recipe.AddIngredient(ModContent.ItemType<AstroSuit>());
            recipe.AddIngredient(ModContent.ItemType<AstroBoots>());
            recipe.AddIngredient(ModContent.ItemType<MeteorHeadStaff>());
            recipe.AddIngredient(ModContent.ItemType<TechniqueMeteorStomp>());
            recipe.AddIngredient(ModContent.ItemType<MeteoriteClusterBomb>(), 300);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
