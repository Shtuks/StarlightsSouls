using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.DD;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Items.Painting;
using ssm.Core;
using ssm.Thorium;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.BossMini;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SpiritTrapperEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 3;
            Item.value = 80000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //set bonus
            thoriumPlayer.setSpiritTrapper = true;
            modPlayer.SpiritTrapperEnchant = true;

            ModContent.Find<ModItem>(this.thorium.Name, "ScryingGlass").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SpiritTrapperCowl>());
            recipe.AddIngredient(ModContent.ItemType<SpiritTrapperCuirass>());
            recipe.AddIngredient(ModContent.ItemType<SpiritTrapperGreaves>());
            recipe.AddIngredient(ModContent.ItemType<InnerFlame>());
            recipe.AddIngredient(ModContent.ItemType<ScryingGlass>());
            recipe.AddIngredient(ModContent.ItemType<SpiritBlastWand>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
