using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BiotechEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //thoriumPlayer.essenceSet = true;

            //if (player.ownedProjectileCounts[thorium.ProjectileType("LifeEssence")] < 1)
            //{
            //    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("LifeEssence"), 0, 0f, player.whoAmI, 0f, 0f);
            //}
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            //recipe.AddIngredient(ModContent.ItemType<LifeWeaverHood>());
            //recipe.AddIngredient(ModContent.ItemType<LifeWeaverGarment>());
            //recipe.AddIngredient(ModContent.ItemType<LifeWeaverLeggings>());
            recipe.AddIngredient(ModContent.ItemType<LifeEssenceApparatus>());
            recipe.AddIngredient(ModContent.ItemType<NullZoneStaff>());
            recipe.AddIngredient(ModContent.ItemType<BarrierGenerator>());


            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
