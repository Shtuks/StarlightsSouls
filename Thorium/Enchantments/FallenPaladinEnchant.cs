using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Misc;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class FallenPaladinEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 8;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //string oldSetBonus = player.setBonus;
            ModContent.Find<ModItem>(this.thorium.Name, "FallenPaladinFacegaurd").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.thorium.Name, "Prydwen").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "NirvanaStatuette").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "TemplarsCirclet").UpdateArmorSet(player);
            //player.setBonus = oldSetBonus;
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<FallenPaladinFaceguard>());
            recipe.AddIngredient(ModContent.ItemType<FallenPaladinCuirass>());
            recipe.AddIngredient(ModContent.ItemType<FallenPaladinGreaves>());
            recipe.AddIngredient(ModContent.ItemType<TemplarEnchant>());
            recipe.AddIngredient(ModContent.ItemType < Prydwen >()); //WHO TF NAMED THIS THING Wynebgwrthucher
            recipe.AddIngredient(ModContent.ItemType<NirvanaStatuette>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
