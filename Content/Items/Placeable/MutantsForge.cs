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

namespace ssm.Content.Items.Placeable
{
    public class MutantsForge : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
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
            ((Entity)this.Item).width = 28;
            ((Entity)this.Item).height = 14;
            this.Item.rare = 10;
            this.Item.maxStack = 99;
            this.Item.useTurn = true;
            this.Item.autoReuse = true;
            this.Item.useAnimation = 15;
            this.Item.useTime = 10;
            this.Item.useStyle = 1;
            this.Item.consumable = true;
            this.Item.value = Item.buyPrice(2, 0, 0, 0);
            this.Item.createTile = ModContent.TileType<MutantsForgeTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            if (ModLoader.HasMod("CalamityMod"))
                recipe.AddIngredient<DemonshadeWorkbench>();

            //.AddIngredient<XeniumRefinery>()
            //.AddIngredient<DraedonsForge>()
            //.AddIngredient<XeniumSmelter>()
            //.AddIngredient<GirusCorruptor>()
            //.AddIngredient<SoulForge>()
            //.AddIngredient<ThoriumAnvil>()
            //.AddIngredient<ArcaneArmorFabricator>()
            recipe.AddIngredient<EternalEnergy>(30);
            recipe.AddIngredient<CrucibleCosmos>();
            recipe.Register();
        }
    }
}
