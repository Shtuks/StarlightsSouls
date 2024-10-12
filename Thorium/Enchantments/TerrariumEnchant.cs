using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ssm.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ThoriumMod.Items.Terrarium;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.NPCItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TerrariumEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override Color nameColor => (new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10; //rainbow
            Item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "TerrariumHelmet").UpdateArmorSet(player);

            ModContent.Find<ModItem>("ssm", "ThoriumEnchant").UpdateAccessory(player, hideVisual);
            //ModContent.Find<ModItem>(this.thorium.Name, "TerrariumSubwoofer").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<TerrariumHelmet>());
            recipe.AddIngredient(ModContent.ItemType<TerrariumBreastPlate>());
            recipe.AddIngredient(ModContent.ItemType<TerrariumGreaves>());
            recipe.AddIngredient(ModContent.ItemType<ThoriumEnchant>());
            recipe.AddIngredient(ModContent.ItemType<TerrariumWings>());
            recipe.AddIngredient(ModContent.ItemType<ThoriumCube>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
