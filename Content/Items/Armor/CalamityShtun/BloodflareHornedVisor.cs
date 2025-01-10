using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Armor.Bloodflare;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Armor.CalamityShtun
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [AutoloadEquip(EquipType.Head)]
    public class BloodflareHornedVisor : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "SetBonus";
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
            Item.defense = 40; //104
            Item.rare = ModContent.RarityType<PureGreen>();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BloodflareBodyArmor>() && legs.type == ModContent.ItemType<BloodflareCuisses>();
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.Shield().shieldCapacityMax2 += 150;
            player.Shield().shieldRegenSpeed += 6;
            player.Shield().shieldOn = true;

            var modPlayer = player.Calamity();
            player.GetAttackSpeed<MeleeDamageClass>() += 0.15f;
            modPlayer.bloodflareSet = true;
            //player.ShtunCal.bloodflareShtun = true;
            player.setBonus = this.GetLocalizedValue("SetBonus") + "\n" + CalamityUtils.GetTextValueFromModItem<BloodflareBodyArmor>("CommonSetBonus");
            player.crimsonRegen = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<MeleeDamageClass>() += 0.2f;
            player.GetCritChance<MeleeDamageClass>() += 10;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<BloodstoneCore>(22).
                AddIngredient<RuinousSoul>(4).
                AddTile(TileID.LunarCraftingStation).
                Register();
        }
    }
}
