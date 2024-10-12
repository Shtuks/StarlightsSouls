using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.MeleeItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.BossFallenBeholder;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BerserkerEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 7;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
           // ModContent.Find<ModItem>(this.thorium.Name, "BerserkerMask").UpdateArmorSet(player);

            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "MagmaEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "RapierBadge").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            //recipe.AddIngredient(ModContent.ItemType<BerserkerMask>());
            //recipe.AddIngredient(ModContent.ItemType<BerserkerBreastplate>());
            //recipe.AddIngredient(ModContent.ItemType<BerserkerGreaves>());
            recipe.AddIngredient(ModContent.ItemType<MagmaEnchant>());
            recipe.AddIngredient(ModContent.ItemType<RapierBadge>());
            recipe.AddIngredient(ModContent.ItemType<WyvernSlayer>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
