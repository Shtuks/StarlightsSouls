using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Armor.Tarragon;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using ssm.Content.DamageClasses;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Armor.CalamityShtun
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [AutoloadEquip(EquipType.Head)]
    public class TarragonHornedVisor : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "SetBonus";
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
            Item.defense = 30; //99
            Item.rare = ModContent.RarityType<Turquoise>();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TarragonBreastplate>() && legs.type == ModContent.ItemType<TarragonLeggings>();
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.Shield().shieldCapacityMax2 += 100;
            player.Shield().shieldRegenSpeed += 5;
            player.Shield().shieldOn = true;

            var modPlayer = player.Calamity();
            modPlayer.tarraSet = true;
            player.GetAttackSpeed<ShtuxianDamage>() += 0.15f;
            //player.ShtunCal.tarraShtun = true;

            var hotkey = CalamityKeybinds.ArmorSetBonusHotKey.TooltipHotkeyString();
            player.setBonus = this.GetLocalization("SetBonus").Format(hotkey) + "\n" + CalamityUtils.GetTextValueFromModItem<TarragonBreastplate>("CommonSetBonus");
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<ShtuxianDamage>() += 0.15f;
            player.GetCritChance<ShtuxianDamage>() += 15;
            player.endurance += 0.10f;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<UelibloomBar>(14).
                AddIngredient<DivineGeode>(12).
                AddTile(TileID.LunarCraftingStation).
                Register();
        }
    }
}
