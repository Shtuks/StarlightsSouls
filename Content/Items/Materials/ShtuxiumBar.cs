using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;

namespace ssm.Content.Items.Materials
{
	public class ShtuxiumBar : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 25;
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 100;}

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ShtuxiumBarTile>());
			Item.width = 20;
			Item.height = 20;
			Item.value = 100000;}
		public override void AddRecipes() {
			Recipe recipe = this.CreateRecipe(5);
				recipe.AddIngredient<ShtuxiumOre>(10);
				recipe.AddIngredient<ShtuxiumSoulShard>(5);
				recipe.AddTile<ShtuxibusForgeTile>();
				recipe.Register();}}}
