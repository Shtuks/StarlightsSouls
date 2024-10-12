using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Items.Consumable;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class FungusEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            thoriumPlayer.setFungus = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<FungusHat>());
            recipe.AddIngredient(ModContent.ItemType<FungusGuard>());
            recipe.AddIngredient(ModContent.ItemType<FungusLeggings>());

            //recipe.AddIngredient(ModContent.ItemType<SporeBook>());
            recipe.AddIngredient(ModContent.ItemType<SwampSpike>());
            recipe.AddIngredient(ModContent.ItemType<SporeCoatingItem>(), 10);

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
