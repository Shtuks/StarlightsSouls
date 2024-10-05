using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Luminance.Core.Graphics;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Items.Dyes;
using ssm.Content.Items.Dyes;
using CalamityMod.Items.Dyes;

namespace ssm.Content.Items.Singularities
{
    public class CalamitySingularity : ModItem
    {
        public int i = ModContent.GetInstance<ShtunConfig>().ItemsUsedInSingularity;
        public override void SetStaticDefaults()
        {
            // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 6));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation
            ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity
            Item.ResearchUnlockCount = 9999; // Configure the amount of this item that's needed to research it in Journey mode.
        }

        public virtual bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((!(((TooltipLine)line).Mod == "Terraria") || !(((TooltipLine)line).Name == "ItemName")) && !(((TooltipLine)line).Name == "FlavorText"))
                return true;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin((SpriteSortMode)1, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
            ManagedShader shader = ShaderManager.GetShader("CalamityMod.CalamitousDyeShader");
            shader.TrySetParameter("mainColor", (object)new Color(42, 42, 99));
            shader.TrySetParameter("secondaryColor", (object)FargowiltasSouls.FargowiltasSouls.EModeColor());
            shader.Apply("PulseUpwards");
            Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float)line.X, (float)line.Y), Color.White, 1f, 0.0f, 0.0f, -1);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
            return false;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 1000000000; // Makes the item worth 574575363641346352314 gold.
            Item.rare = -13;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale); // Makes this item glow when thrown out of inventory.
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<SolarVeilSingularity>()
            .AddIngredient<WulfrumSingularity>()
            .AddIngredient<RottenMatterSingularity>()
            .AddIngredient<PlagueCanisterSingularity>()
            .AddIngredient<PurifiedGelSingularity>()
            .AddIngredient<CorrodedSingularity>()
            .AddIngredient<AnnihilationAshSingularity>()
            .AddIngredient<AeraliteSingularity>()
            .AddIngredient<BloodSampleSingularity>()
            .AddIngredient<AstralSingularity>()
            .AddIngredient<BloodstoneSingularity>()
            .AddIngredient<DepthSingularity>()
            .AddIngredient<AscendantSingularity>()
            .AddIngredient<NecroplasmSingularity>()
            .AddIngredient<AuricSingularity>()
            .AddIngredient<CosmiliteSingularity>()
            .AddIngredient<CryoniteSingularity>()
            .AddIngredient<PerenialSingularity>()
            .AddIngredient<ScoriaSingularity>()
            .AddIngredient<SeaRemainsSingularity>()
            .AddIngredient<WulfrumSingularity>()
            .AddIngredient<MeldSingularity>()
            .AddIngredient<UelibloomSingularity>()
            .AddTile<DraedonsForge>().Register();
        }
    }
}
