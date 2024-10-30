using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Materials;
using ssm.Core;
using CalamityMod.Tiles.Furniture.CraftingStations;

namespace ssm.Content.Items.Singularities.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class CorrodedSingularity : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.CalSingularities;
        }
        public int i = ModContent.GetInstance<ShtunConfig>().ItemsUsedInSingularity;
        public override void SetStaticDefaults()
        {
            // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 6));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the Item have an animation while in world (not held.). Use in combination with RegisterItemAnimation
            ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the Item have no gravity

            Item.ResearchUnlockCount = 9999; // Configure the amount of this Item that's needed to research it in Journey mode.
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 1000000000; // Makes the Item worth 574575363641346352314 gold.
            Item.rare = -13;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale); // Makes this Item glow when thrown out of inventory.
        }
        public override void AddRecipes()
        {
            this.CreateRecipe(1).AddIngredient<CorrodedFossil>(i).AddTile<DraedonsForge>().Register();
        }
    }
}
