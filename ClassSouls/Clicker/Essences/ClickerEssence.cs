using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using ClickerClass;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;

namespace ssm.ClassSouls.Clicker.Essences
{
    [ExtendsFromMod(ModCompatibility.ClikerClass.Name)]
    [JITWhenModsEnabled(ModCompatibility.ClikerClass.Name)]
    public class ClickerEssence : BaseEssence
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 4;
            Item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<ClickerDamage>() += 0.18f;
            player.GetCritChance<ClickerDamage>() += 0.10f;
            player.GetModPlayer<ClickerPlayer>().clickerRadius += 0.5f;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<ClickerEmblem>();

            recipe.AddIngredient<CopperClicker>();
            recipe.AddIngredient<TorchClicker>();
            recipe.AddIngredient<MyceliumClicker>();
            recipe.AddIngredient<HemoClicker>();
            recipe.AddIngredient<ImpishClicker>();
            recipe.AddIngredient<MagnetClicker>();
            recipe.AddIngredient<GoldClicker>();

            recipe.AddIngredient(ItemID.HallowedBar, 5);

            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
    }
}
