using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ssm.Content.Tiles;
using ssm.Content.Items.Materials;
using ssm.Content.Projectiles.Weapons;
using ssm.CrossMod.CraftingStations;

namespace ssm.Content.Items.Weapons.Shtuxium
{
    public class ShtuxiumSpelltome : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 40;
            Item.damage = 4300;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 20;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5f;
            Item.crit = 50;
            Item.value = Item.sellPrice(platinum: 10);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item84;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ShtuxiumSpelltomeProj>();
            Item.shootSpeed = 16f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 12; i++)
            {
                Vector2 ringVel = ((MathHelper.TwoPi * i / 12f) + velocity.ToRotation()).ToRotationVector2() * velocity.Length() * 0.5f;
                Projectile.NewProjectile(source, position, ringVel, type, damage, knockback, Main.myPlayer, 0f, 0f);
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.SpellTome).
                AddIngredient<ShtuxiumBar>(5).
                AddTile(ModContent.TileType<MutantsForgeTile>()).
                Register();
        }
    }
}
