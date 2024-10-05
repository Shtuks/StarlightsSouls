using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Body)]
    public class chtuxmantle : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40; // Width of the item
            Item.height = 37; // Height of the item
            Item.value = Item.sellPrice(gold: 1000); // How many coins the item is worth
            Item.rare = ItemRarityID.Red; // The rarity of the item
            Item.defense = 745745; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "ShtuxianSoul").UpdateAccessory(player, false);
            player.statLifeMax2 += 40000; // Increase how many hp points the player can have by 700
            player.GetDamage(DamageClass.Generic) += 1000f;
            player.lifeRegen += 9999999;
        }
    }
}
