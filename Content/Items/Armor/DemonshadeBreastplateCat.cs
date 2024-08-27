using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Items.Armor;

namespace ssm.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class DemonshadeBreastplateCat : ModItem
	{
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
		private readonly Mod Calamity = Terraria.ModLoader.ModLoader.GetMod("CalamityMod");

		public override void SetDefaults() {
			Item.width = 40; // Width of the item
			Item.height = 37; // Height of the item
			Item.value = Item.sellPrice(gold: 1000); // How many coins the item is worth
			Item.rare = ItemRarityID.Red; // The rarity of the item
			Item.defense = 180; // The amount of defense the item will give when equipped
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return head.type == ModContent.ItemType<DemonshadeHelmetCat>() && legs.type == ModContent.ItemType<DemonshadeLegginsCat>();
		}

		public override void UpdateEquip(Player player) {
            ModContent.Find<ModItem>(this.FargoSoul.Name, "MutantBody").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.Calamity.Name, "AuricTeslaBodyArmor").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.Calamity.Name, "GemTechBodyArmor").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.Calamity.Name, "DemonshadeBreastplate").UpdateArmorSet(player);
            player.statLifeMax2 += 700; // Increase how many hp points the player can have by 700
            player.GetDamage(DamageClass.Generic) += 2f;
            player.lifeRegen += 120;
		}
		public override void UpdateArmorSet(Player player) {
			player.GetDamage(DamageClass.Generic) += 1000 / 100f; // Increase dealt damage for all weapon classes by 20%
		}
	}
}
