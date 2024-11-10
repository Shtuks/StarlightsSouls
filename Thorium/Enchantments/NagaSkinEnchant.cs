using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.BossMini;
using ssm.Core;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Items.Painting;
using ThoriumMod.Items.BossForgottenOne;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class NagaSkinEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 5;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoSoulsPlayer fargoPlayer = player.GetModPlayer<FargoSoulsPlayer>();

            //naga effect
            if (player.wet)
            {
                fargoPlayer.AttackSpeed += 0.2f;
            }

            //quicker in water
            player.ignoreWater = true;
            if (player.wet)
            {
                player.moveSpeed += 0.15f;
            }

            ModContent.Find<ModItem>(this.thorium.Name, "OceanRetaliation").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<NagaSkinMask>());
            recipe.AddIngredient(ModContent.ItemType<NagaSkinSuit>());
            recipe.AddIngredient(ModContent.ItemType<NagaSkinTail>());
            recipe.AddIngredient(ModContent.ItemType<OceanRetaliation>());
            recipe.AddIngredient(ModContent.ItemType<Eelrod>());
            recipe.AddIngredient(ModContent.ItemType<OldGodsVision>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
