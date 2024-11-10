using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Blizzard;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using ThoriumMod.Items.Misc;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class WhiteKnightEnchant : BaseEnchant
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
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //shade band
            //thoriumPlayer.shadeBand = true;

            ModContent.Find<ModItem>(this.thorium.Name, "WhiteKnightMask").UpdateArmorSet(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<WhiteKnightMask>());
            recipe.AddIngredient(ModContent.ItemType<WhiteKnightTabard>());
            recipe.AddIngredient(ModContent.ItemType<WhiteKnightLeggings>());
            //recipe.AddIngredient(ModContent.ItemType<ShadeBand>());
            recipe.AddIngredient(ModContent.ItemType<PrismiteGemLock>());
            //recipe.AddIngredient(ModContent.ItemType<FrostFang>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}