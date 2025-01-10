using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Armor.Silva;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ssm.Content.DamageClasses;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Items.Armor.CalamityShtun
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [AutoloadEquip(EquipType.Head)]
    public class SilvaHornedVisor : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "SetBonus";
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
            Item.defense = 20; //109
            Item.rare = ModContent.RarityType<DarkBlue>();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SilvaArmor>() && legs.type == ModContent.ItemType<SilvaLeggings>();
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.Shield().shieldCapacityMax2 += 200;
            player.Shield().shieldRegenSpeed += 8;
            player.Shield().shieldOn = true;

            var modPlayer = player.Calamity();
            modPlayer.silvaSet = true;
            player.GetAttackSpeed<ShtuxianDamage>() += 0.2f;
            //player.ShtunCal.silvaShtun = true;

            player.setBonus = this.GetLocalizedValue("SetBonus") + "\n" + CalamityUtils.GetTextValueFromModItem<SilvaArmor>("CommonSetBonus");
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<ShtuxianDamage>() += 0.30f;
            player.GetCritChance<ShtuxianDamage>() += 20;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<PlantyMush>(12).
                AddIngredient<EffulgentFeather>(10).
                AddIngredient<AscendantSpiritEssence>(4).
                AddTile<CosmicAnvil>().
                Register();
        }
    }
}
