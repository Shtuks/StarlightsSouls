using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Materials;

namespace ssm.Thorium.Swarm.Energizers
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumEnergizer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.rare = 1;
            Item.value = 1000000;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                    .AddIngredient<BoreanEnergizer>()
                    .AddIngredient<BuriedEnergizer>()
                    .AddIngredient<FallenEnergizer>()
                    .AddIngredient<ForgottenEnergizer>()
                    .AddIngredient<GraniteEnergizer>()
                    .AddIngredient<ThunderEnergizer>()
                    .AddIngredient<ViscountEnergizer>()
                    .AddIngredient<JellyfishEnergizer>()
                    .AddIngredient<LichEnergizer>()
                    .AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"))
                    .Register();
        }
    }
}