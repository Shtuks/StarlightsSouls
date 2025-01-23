using ssm.Content.Items.Materials;
using ssm.Content.Projectiles.Weapons;
using ssm.Content.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Weapons.Shtuxium
{
    public class ShtuxiumYoyo : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Yoyo[Item.type] = true;
            ItemID.Sets.GamepadExtraRange[Item.type] = 50;
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 54;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.damage = 3200;
            Item.knockBack = 6f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.shoot = ModContent.ProjectileType<ShtuxiumYoyoProj>();
            Item.shootSpeed = 16f;

            Item.autoReuse = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = 1000000;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<ShtuxiumBar>(5).
                AddTile<MutantsForgeTile>().
                Register();
        }
    }
}
