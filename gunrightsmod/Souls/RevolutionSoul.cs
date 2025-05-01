using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using gunrightsmod;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Fargowiltas.Items.Tiles;
using ssm.gunrightsmod.Enchantments;
using ssm.gunrightsmod.Forces;
using ssm.Systems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Materials;
using gunrightsmod.Content.Items;

namespace ssm.gunrightsmod.Souls
{
	[ExtendsFromMod(ModCompatibility.SacredTools.Name)]
	[JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
	public class RevolutionSoul : BaseSoul
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ShtunConfig.Instance.gunrightsmod;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.defense = 25;
			Item.accessory = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
			Item.rare = 11;
			Item.value = 1000000;
		}

    /*	
 
 Once everything else is done, I will remove this
 
 public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

			ModContent.Find<ModItem>(((ModType)this).Mod.Name, "Force1").UpdateAccessory(player, hideVisual);
			ModContent.Find<ModItem>(((ModType)this).Mod.Name, "Force2").UpdateAccessory(player, hideVisual);
			ModContent.Find<ModItem>(((ModType)this).Mod.Name, "Force3").UpdateAccessory(player, hideVisual);
			ModContent.Find<ModItem>(((ModType)this).Mod.Name, "Force4").UpdateAccessory(player, hideVisual);
		}

	*/

/*	public override void AddRecipes()
		{
			Recipe recipe = this.CreateRecipe();
			recipe.AddIngredient<IdiocracyForce>();
			recipe.AddIngredient<RadioactiveForce>();
			recipe.AddIngredient<Item1>(TheSecondAmendment);
			recipe.AddIngredient<Item2>(_____);
			recipe.AddTile<______WhereYouCraft_____>();
			recipe.Register();
		}
	}
}
*/
