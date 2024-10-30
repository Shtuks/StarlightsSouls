using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Items.Singularities;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;
using ssm.Calamity.Swarm.Energizers;
//using ssm.Thorium.Swarm.Energizers;
using ssm.Content.Items.Singularities.Calamity;
using ssm.Thorium.Swarm.Energizers;
//using ssm.SoA.Swarm.Energizers;
//using ssm.Redemption.Swarm.Energizers;
//using ssm.Homeward.Swarm.Energizers;


namespace ssm.Content.Items.Materials
{
    public class InfinityCatalyst : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExtraContent;
        }
        public override void SetStaticDefaults()
        {
            // Registers a vertical animation with 4 frames and each one will last 5 ticks ( / 2 second)
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 9));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the Item have an animation while in world (not held.). Use in combination with RegisterItemAnimation
            ItemID.Sets.ItemIconPulse[Item.type] = true; // The Item pulses while in the player's inventory
            Item.ResearchUnlockCount = 99999; // Configure the amount of this Item that's needed to research it in Journey mode.
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<AdamantiteSingularity>());
            Item.rare = 12;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale); // Makes this Item glow when thrown out of inventory.
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<TerraEnergizer>();
            recipe.AddIngredient<TerrariaSingularity>();

            if (ModLoader.HasMod("CalamityMod"))
            {
                if (ShtunConfig.Instance.CalSingularities)
                    recipe.AddIngredient<CalamitySingularity>();
                if (ShtunConfig.Instance.CalSwarmItems)
                    recipe.AddIngredient<CalamitousEnergizer>();
            }
            if (ModLoader.HasMod("ThoriumMod"))
            {
                //if (ShtunConfig.Instance.TorSingularities)
                    //recipe.AddIngredient<ThoriumSingularity>();
                if (ShtunConfig.Instance.TorSwarmItems)
                    recipe.AddIngredient<ThoriumEnergizer>();
            }
            if (ModLoader.HasMod("SacredTools"))
            {
                //if (ShtunConfig.Instance.SoASingularities)
                    //recipe.AddIngredient<SoASingularity>()
                //if (ShtunConfig.Instance.SoASwarmItems)
                    //recipe.AddIngredient<SoAEnergizer>()
            }
            if (ModLoader.HasMod("Redemption"))
            {
                //if (ShtunConfig.Instance.RedSwarmItems)
                    //recipe.AddIngredient<RedemptionEnergizer>()
                //if (ShtunConfig.Instance.RedSingularities)
                    //recipe.AddIngredient<RedemptionSingularity>()
            }

            recipe.AddTile<ShtuxibusForgeTile>();

            recipe.Register();
        }
    }
}
