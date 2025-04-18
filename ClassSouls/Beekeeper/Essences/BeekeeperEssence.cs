using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using BombusApisBee.BeeDamageClass;
using BombusApisBee.Items.Accessories.BeeKeeperDamageClass;
using BombusApisBee.Items.Weapons.BeeKeeperDamageClass;

namespace ssm.ClassSouls.Beekeeper.Essences
{
    [ExtendsFromMod(ModCompatibility.BeekeeperClass.Name)]
    [JITWhenModsEnabled(ModCompatibility.BeekeeperClass.Name)]
    public class BeekeeperEssence : BaseEssence
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
            player.GetDamage<HymenoptraDamageClass>() += 0.18f;
            player.GetCritChance<HymenoptraDamageClass>() += 0.10f;
            player.GetModPlayer<BeeDamagePlayer>().BeeResourceMax2 += 50;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<HoneyedEmblem>();

            recipe.AddIngredient<Beemerang>();
            recipe.AddIngredient<Ambrosia>();
            recipe.AddIngredient<Beemstick>();
            recipe.AddIngredient<StoneHoneycomb>();
            recipe.AddIngredient<TheTraitorsSaxophone>();
            recipe.AddIngredient<Skelecomb>();
            recipe.AddIngredient<NectarBolt>();

            recipe.AddIngredient(ItemID.HallowedBar, 5);

            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
    }
}
