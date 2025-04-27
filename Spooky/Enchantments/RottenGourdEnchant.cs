using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Spooky.Core;
using Spooky.Content.Items.SpookyBiome.Armor;
using Spooky.Content.Items.Quest;
using Spooky.Content.Items.Catacomb;
using Spooky.Content.Items.SpookyBiome;

namespace ssm.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class RottenGourdEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Spooky;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SpookyPlayer modPlayer = player.GetModPlayer<SpookyPlayer>();
            modPlayer.GourdSet = true;

            ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "GhostBook").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "RustyRing").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<GourdHead>();
            recipe.AddIngredient<GourdBody>();
            recipe.AddIngredient<GourdLegs>();
            recipe.AddIngredient<GhostBook>();
            recipe.AddIngredient<RustyRing>();
            recipe.AddIngredient<GourdFlail>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
