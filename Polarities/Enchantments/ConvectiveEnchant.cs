using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.ConvectiveArmor;
using Polarities.Content.Items.Accessories.ExpertMode.Hardmode;
using Polarities;
using FargowiltasSouls;
using Polarities.Content.Items.Weapons.Magic.Guns.Hardmode;
using Polarities.Content.Items.Weapons.Magic.Books.Hardmode;

namespace ssm.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class ConvectiveEnchant : BaseEnchant
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

        public override Color nameColor => new(255, 119, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            PolaritiesPlayer modPlayer = player.GetModPlayer<PolaritiesPlayer>();
            modPlayer.convectiveSetBonusType = player.ProcessDamageTypeFromHeldItem();

            ModContent.GetInstance<BuildingEruption>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:ConvectiveHelms");
            recipe.AddIngredient(ModContent.ItemType<ConvectiveArmor>());
            recipe.AddIngredient(ModContent.ItemType<ConvectiveLeggings>());
            recipe.AddIngredient(ModContent.ItemType<BuildingEruption>());
            recipe.AddIngredient(ModContent.ItemType<WormSpewer>());
            recipe.AddIngredient(ModContent.ItemType<HadeanUpwelling>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
