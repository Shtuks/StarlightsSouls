using Microsoft.Xna.Framework;
using ssm.Content.Items.Materials;
using ssm.Content.Projectiles.Weapons;
using ssm.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Weapons.Shtuxium
{
    public class ShtuxiumRifle : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 2600;
            Item.crit = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 64;
            Item.height = 26;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 20;
            Item.value = Item.sellPrice(platinum: 10);
            Item.rare = ItemRarityID.Purple;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ShtuxiumRifleDeathray>();
            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.SafeNormalize(Vector2.Zero);
            type = Item.shoot;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ShtuxiumBar>(5)
                .AddTile<MutantsForgeTile>()
                .Register();
        }
    }
}
