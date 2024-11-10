using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BossThePrimordials.Rhapsodist;
using ThoriumMod.Items.BossForgottenOne;
using ssm.Core;
using ThoriumMod.Items.BardItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class RhapsodistEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

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
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //notes heal more and give random empowerments
            thoriumPlayer.armInspirator = true;
            //hotkey buff allies 
            //thoriumPlayer.setInspirator = true;
            //hotkey buff self
            //thoriumPlayer.setSoloist = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SoloistHat>()); //any..
            //recipe.AddIngredient(ModContent.ItemType<RallyHat>());
            recipe.AddIngredient(ModContent.ItemType<RhapsodistChestWoofer>());
            recipe.AddIngredient(ModContent.ItemType<RhapsodistBoots>());
            recipe.AddIngredient(ModContent.ItemType<JingleBells>());
            recipe.AddIngredient(ModContent.ItemType<Sousaphone>());
            recipe.AddIngredient(ModContent.ItemType<EdgeofImagination>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
