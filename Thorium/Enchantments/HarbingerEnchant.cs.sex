/*using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Items.HealerItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class HarbingerEnchant : BaseEnchant
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


            ShtunPlayer modPlayer = player.GetModPlayer<ShtunPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //set bonus
            player.statManaMax2 += (int)(player.statManaMax2 * 0.5);
            //harbinger
            if (player.statLife > (int)(player.statLifeMax2 * 0.75))
            {
                //thoriumPlayer.overCharge = true;
                player.GetDamage(DamageClass.Generic) += 0.5f;
            }

            //shade band
            //thoriumPlayer.shadeBand = true;
            //villain damage 
            //modPlayer.KnightEnchant = true;
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<HarbingerHelmet>());
            recipe.AddIngredient(ModContent.ItemType<HarbingerChestGuard>());
            recipe.AddIngredient(ModContent.ItemType<HarbingerGreaves>());
            recipe.AddIngredient(ModContent.ItemType<WhiteKnightEnchant>());
            recipe.AddIngredient(ModContent.ItemType<BlackholeCannon>());
            recipe.AddIngredient(ModContent.ItemType<SpiritStaff>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}*/
