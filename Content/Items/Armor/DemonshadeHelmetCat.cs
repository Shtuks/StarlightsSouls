using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ssm.Content.Items.Armor;

namespace ssm.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Head)]
	public class DemonshadeHelmetCat : ModItem
	{
		private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
		private readonly Mod Calamity = Terraria.ModLoader.ModLoader.GetMod("CalamityMod");
		public static readonly int AdditiveGenericDamageBonus = 20;
		public override void SetDefaults() {
			Item.width = 34; // Width of the item
			Item.height = 34; // Height of the item
			Item.value = Item.sellPrice(gold: 1000); // How many coins the item is worth
			Item.rare = ItemRarityID.Red; // The rarity of the item
			Item.defense = 120; // The amount of defense the item will give when equipped
		} 
		public override void UpdateEquip(Player player) {
			ModContent.Find<ModItem>(this.FargoSoul.Name, "MutantMask").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.Calamity.Name, "GemTechHeadgear").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.Calamity.Name, "AuricTeslaRoyalHelm").UpdateArmorSet(player);
			player.statManaMax2 += 700;
			player.maxMinions += 10; // Increase how many minions the player can have by one
            player.GetDamage(DamageClass.Generic) += 0.6f;
			player.GetCritChance(DamageClass.Generic) += 80f;
            player.GetAttackSpeed(DamageClass.Generic) += 1f;
			player.GetArmorPenetration(DamageClass.Generic) += 500f;
		}
	}
}
