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
using FargowiltasSouls.Content.Items;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;
using Fargowiltas.Items.Summons.SwarmSummons.Energizers;
using Fargowiltas.Items.Tiles;

namespace ssm.Content.Items.Materials
{
    public class TerraEnergizer : SoulsItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.rare = 1;
            Item.value = 100000;
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
            this.CreateRecipe(1)
            .AddIngredient<EnergizerTwins>()
            .AddIngredient<EnergizerQueenSlime>()
            .AddIngredient<EnergizerMoon>()
            .AddIngredient<EnergizerEye>()
            .AddIngredient<EnergizerCultist>()
            .AddIngredient<EnergizerBee>()
            .AddIngredient<EnergizerBetsy>()
            .AddIngredient<EnergizerBrain>()
            .AddIngredient<EnergizerDestroy>()
            .AddIngredient<EnergizerDeer>()
            .AddIngredient<EnergizerEmpress>()
            .AddIngredient<EnergizerGolem>()
            .AddIngredient<EnergizerPlant>()
            .AddIngredient<EnergizerSkele>()
            .AddIngredient<EnergizerWall>()
            .AddIngredient<EnergizerWorm>()
            .AddIngredient<EnergizerSlime>()
            .AddIngredient<EnergizerFish>()
            .AddIngredient<EnergizerDarkMage>()
            .AddTile<CrucibleCosmosSheet>()
            .Register();
        }
    }
}