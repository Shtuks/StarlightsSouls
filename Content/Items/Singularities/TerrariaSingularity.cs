using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using FargowiltasSouls.Content.Items;

namespace ssm.Content.Items.Singularities
{
    public class TerrariaSingularity : SoulsItem
    {

        public override void SetDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 6));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation
            ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity
            Item.rare = 12;
        }

        public override void SafeModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                    tooltipLine.OverrideColor = new Color?(Main.DiscoColor);
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color?(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
        }
        public override void AddRecipes()
        {
            CreateRecipe()
              .AddIngredient<AdamantiteSingularity>()
              .AddIngredient<ChlorophyteSingularity>()
              .AddIngredient<CobaltSingularity>()
              .AddIngredient<CopperSingularity>()
              .AddIngredient<CrimtaneSingularity>()
              .AddIngredient<HallowSingularity>()
              .AddIngredient<GoldSingularity>()
              .AddIngredient<HellstoneSingularity>()
              .AddIngredient<LeadSingularity>()
              .AddIngredient<LuminiteSingularity>()
              .AddIngredient<MeteoriteSingularity>()
              .AddIngredient<MythrillSingularity>()
              .AddIngredient<OrichalcumSingularity>()
              .AddIngredient<PalladiumSingularity>()
              .AddIngredient<ShroomiteSingularity>()
              .AddIngredient<SilverSingularity>()
              .AddIngredient<SpectreSingularity>()
              .AddIngredient<TinSingularity>()
              .AddIngredient<TitaniumSingularity>()
              .AddIngredient<TungstenSingularity>()
              .AddTile<DraedonsForge>().Register();
        }
    }
}
