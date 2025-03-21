using ssm.Content.Items.Materials;
using ssm.Content.Projectiles.Weapons;
using ssm.Content.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.CrossMod.CraftingStations;

namespace ssm.Content.Items.Weapons.Shtuxium
{
    public class ShtuxiumShortsword : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExperimentalContent;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = 11;
            Item.value = 100000;
            Item.useAnimation = (Item.useTime = 12);
            Item.useStyle = 13;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.damage = 3500;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6f;
            Item.shoot = ModContent.ProjectileType<ShtuxiumShortswordProj>();
            Item.shootSpeed = 2f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ShtuxiumBar>(6)
                .AddTile<MutantsForgeTile>()
                .Register();
        }
    }
}
