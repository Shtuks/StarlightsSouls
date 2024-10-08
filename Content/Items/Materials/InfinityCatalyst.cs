using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Items.Singularities;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;
using ssm.Calamity.Swarm.Energizers;
using ssm.Thorium.Swarm.Energizers;
using ssm.Content.Items.Materials;
//using ssm.SoA.Swarm.Energizers;
//using ssm.Redemption.Swarm.Energizers;
//using ssm.Homeward.Swarm.Energizers;


namespace ssm.Content.Items.Materials
{
    public class InfinityCatalyst : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Registers a vertical animation with 4 frames and each one will last 5 ticks ( / 2 second)
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 9));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation
            ItemID.Sets.ItemIconPulse[Item.type] = true; // The item pulses while in the player's inventory
            Item.ResearchUnlockCount = 99999; // Configure the amount of this item that's needed to research it in Journey mode.
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<AdamantiteSingularity>());
            Item.rare = 12;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale); // Makes this item glow when thrown out of inventory.
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            /*
            .AddIngredient<ThoriumSingularity>()
            .AddIngredient<SoAEnergizer>()
            .AddIngredient<RedemptionEnergizer>()
            .AddIngredient<RedemptionSingularity>()
            .AddIngredient<SoASingularity>()
            .AddIngredient<ThoriumEnergizer>()
            .AddIngredient<CalamitousEnergizer>()
            */
            .AddIngredient<TerraEnergizer>()
            .AddIngredient<TerrariaSingularity>()
            .AddIngredient<CalamitySingularity>()
            .AddTile<MutantsForgeTile>().Register();
        }
    }
}
