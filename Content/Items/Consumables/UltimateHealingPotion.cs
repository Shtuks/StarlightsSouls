using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.Content.Items.Consumables
{
	// This item showcases some advanced capabilities of healing potions. It heals a dynamic amount and adjusts its tooltip accordingly.
	// A typical healing potion can get rid of the ModifyTooltips and GetHealLife methods and just assign Item.healLife.
	// A mana potion is exactly the same, except Item.healMana is used instead. (Also GetHealMana would be used for dynamic mana recovery values)
	public class UltimateHealingPotion : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 30;}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item3;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.rare = 11;
			Item.value = Item.buyPrice(gold: 10);

			Item.healLife = 10000; // While we change the actual healing value in GetHealLife, Item.healLife still needs to be higher than 0 for the item to be considered a healing item
			Item.potion = true; // Makes it so this item applies potion sickness on use and allows it to be used with quick heal
		}
		public override void GetHealLife(Player player, bool quickHeal, ref int healValue) {
			// Make the item heal half the player's max health normally, or one fourth if used with quick heal
			healValue = player.statLifeMax2 / (quickHeal ? 4 : 2);
		}
	}
}