using ssm.Content.Items.Materials;
using ssm.Content.Projectiles.Weapons;
using ssm.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Ammo
{
    public class ShtuxiumBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 46;
            Item.damage = 200;
            Item.DamageType = DamageClass.Ranged;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 4f;
            Item.value = Item.sellPrice(gold: 1);
            Item.shoot = ModContent.ProjectileType<ShtuxianBulletProj>();
            Item.shootSpeed = 0.1f;
            Item.ammo = AmmoID.Bullet;
            Item.rare = 11;
        }

        public override void AddRecipes()
        {
            CreateRecipe(999).
                AddIngredient<ShtuxiumBar>().
                AddIngredient(ItemID.EmptyBullet, 999).
                AddTile<MutantsForgeTile>().
                Register();
        }
    }
}
