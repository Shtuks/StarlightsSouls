using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Armor.Auric;
using CalamityMod.Items.Materials;
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
    public class AuricTeslaHornedVisor : ModItem, ILocalizedModType
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExperimentalContent;
        }
        public new string LocalizationCategory => "SetBonus";
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
            Item.defense = 50; //146
            Item.rare = ModContent.RarityType<Violet>();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<AuricTeslaBodyArmor>() && legs.type == ModContent.ItemType<AuricTeslaCuisses>();
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = this.GetLocalizedValue("SetBonus");
            var modPlayer = player.Calamity();

            modPlayer.tarraSet = true;
            //player.ShtunCal.tarraShtun = true;
            modPlayer.bloodflareSet = true;
            //player.ShtunCal.bloodflareShtun = true;
            modPlayer.silvaSet = true;
            //player.ShtunCal.godslayerShtun = true;
            modPlayer.godSlayer = true;
            //player.ShtunCal.auricShtun = true;
            modPlayer.auricSet = true;

            player.thorns += 3f;
            player.ignoreWater = true;
            player.lifeRegen += 5;

            player.Shield().shieldCapacityMax2 += 300;
            player.Shield().shieldRegenSpeed += 10;
            player.Shield().shieldOn = true;
        }

        public override void UpdateEquip(Player player)
        {
            var modPlayer = player.Calamity();
            modPlayer.auricBoost = true;
            player.GetDamage<ShtuxianDamage>() += 0.30f;
            player.GetAttackSpeed<ShtuxianDamage>() += 0.30f;
            player.GetCritChance<ShtuxianDamage>() += 30;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<SilvaHornedVisor>().
                AddIngredient<GodslayerHornedVisor>().
                AddIngredient<BloodflareHornedVisor>().
                AddIngredient<TarragonHornedVisor>().
                AddIngredient<AuricBar>(24).
                AddTile<CosmicAnvil>().
                Register();
        }
    }
}
