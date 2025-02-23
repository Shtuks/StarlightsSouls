using Fargowiltas.Common.Systems.Recipes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Fargowiltas.Items.Tiles;
using ssm.Content.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Content.Items.Placeable;
using ssm.Core;

namespace ssm.Content.Items.Placeable
{
    public class MutantsForge : ModItem
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
                recipe.AddIngredient<DemonshadeWorkbench>();

            if (ModLoader.TryGetMod(ModCompatibility.SacredTools.Name, out Mod soa) && soa.TryFind<ModItem>("TiridiumInfuser", out ModItem currTile6) && soa.TryFind<ModItem>("FlariumInfuser", out ModItem currTile7) && soa.TryFind<ModItem>("NeilBrewer", out ModItem currTile8))
            {
                recipe.AddIngredient(currTile6.Type);
                recipe.AddIngredient(currTile7.Type);
                recipe.AddIngredient(currTile8.Type);
            }

            if (ModLoader.TryGetMod("ThoriumMod", out Mod tor) && tor.TryFind<ModItem>("SoulForge", out ModItem currTile3) && tor.TryFind<ModItem>("ArcaneArmorFabricator", out ModItem currTile4) && tor.TryFind<ModItem>("ThoriumAnvil", out ModItem currTile5))
            {
                recipe.AddIngredient(currTile3.Type);
                recipe.AddIngredient(currTile4.Type);
                recipe.AddIngredient(currTile5.Type);
            }

            recipe.AddIngredient<EternalEnergy>(30);
            recipe.AddIngredient<CrucibleCosmos>();
            recipe.Register();
        }
    }
}
