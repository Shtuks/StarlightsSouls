using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Items.Singularities;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;

namespace ssm.Content.Items.Materials
{
    public class InfinityIngot : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 9));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the Item have an animation while in world (not held.). Use in combination with RegisterItemAnimation

            Item.ResearchUnlockCount = 9999; // Configure the amount of this Item that's needed to research it in Journey mode.
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<AdamantiteSingularity>());
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.InfinityIngot>());
            Item.rare = 12;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale); // Makes this Item glow when thrown out of inventory.
        }

        public override void AddRecipes()
        {
            this.CreateRecipe(1)
            .AddIngredient<InfinityCatalyst>(1)
            .AddIngredient<ShtuxiumBar>(40)
            .AddTile<ShtuxibusForgeTile>()
            .Register();
        }
    }
}
