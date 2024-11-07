using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.ShtunMagicStorage
{
    [ExtendsFromMod(ModCompatibility.MagicStorage.Name)]
    [JITWhenModsEnabled(ModCompatibility.MagicStorage.Name)]
    public class StorageUnitShtuxian : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.rare = 11;
			Item.value = Item.sellPrice(platinum: 10);
			Item.createTile = ModContent.TileType<ShtunStorageUnit>();
			Item.placeStyle = 7;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			//recipe.AddIngredient<StorageUnitMutant>();
			//recipe.AddIngredient<UpgradeShtuxian>();
			recipe.Register();
		}
	}
}
