using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Legs value here will result in TML expecting a X_Legs.png file to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Legs)]
    public class DemonshadeLegginsCat : ModItem
    {
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        private readonly Mod Calamity = Terraria.ModLoader.ModLoader.GetMod("CalamityMod");
        public override void SetDefaults()
        {
            Item.width = 32; // Width of the item
            Item.height = 24; // Height of the item
            Item.value = Item.sellPrice(gold: 10000); // How many coins the item is worth
            Item.rare = ItemRarityID.Red; // The rarity of the item
            Item.defense = 120; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            ModContent.Find<ModItem>(this.FargoSoul.Name, "MutantPants").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.Calamity.Name, "AuricTeslaCuisses").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.Calamity.Name, "GemTechSchynbaulds").UpdateArmorSet(player);
            player.GetDamage(DamageClass.Generic) += 0.5f;
            player.moveSpeed += 0.30f; // 30% move speed
        }
    }
}
