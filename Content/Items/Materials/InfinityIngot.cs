using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Items.Singularities;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;
using ssm.CrossMod.CraftingStations;

namespace ssm.Content.Items.Materials
{
    public class InfinityIngot : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 9));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;

            Item.ResearchUnlockCount = 9999;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<AdamantiteSingularity>());
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.InfinityIngot>());
            Item.rare = 12;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            this.CreateRecipe(1)
            .AddIngredient<InfinityCatalyst>(1)
            .AddIngredient<ShtuxiumBar>(40)
            .AddTile<MutantsForgeTile>()
            .Register();
        }
    }
}
