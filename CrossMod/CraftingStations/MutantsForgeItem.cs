using Fargowiltas.Common.Systems.Recipes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Fargowiltas.Items.Tiles;
using ssm.Content.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Core;

namespace ssm.CrossMod.CraftingStations
{
    public class MutantsForgeItem : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                    tooltipLine.OverrideColor = new Color?(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
            }
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 14;
            Item.rare = 10;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.buyPrice(2, 0, 0, 0);
            Item.createTile = ModContent.TileType<MutantsForgeTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            if (ModLoader.HasMod("CalamityMod"))
            {
                recipe.AddIngredient<DemonshadeWorkbenchItem>();
            }

            if (ModLoader.HasMod("SacredTools") && ShtunConfig.Instance.ExperimentalContent)
            {
                recipe.AddIngredient<SyranCraftingStationItem>();
            }

            if (ModLoader.HasMod("ThoriumMod"))
            {
                recipe.AddIngredient<DreamersForgeItem>();
            }

            if (ModLoader.HasMod("Redemption"))
            {
                recipe.AddIngredient<RedemptionCraftingStationItem>();
            }

            recipe.AddIngredient<EternalEnergy>(30);
            recipe.AddIngredient<CrucibleCosmos>();
            recipe.Register();
        }
    }
}
