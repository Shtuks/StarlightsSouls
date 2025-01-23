using CalamityMod;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items;
using CalamityMod.Items.Armor.GodSlayer;
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
    public class GodslayerHornedVisor : ModItem, ILocalizedModType
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
            Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
            Item.defense = 40; //116
            Item.rare = ModContent.RarityType<DarkBlue>();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<GodSlayerChestplate>() && legs.type == ModContent.ItemType<GodSlayerLeggings>();
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.Shield().shieldCapacityMax2 += 150;
            player.Shield().shieldRegenSpeed += 7;
            player.Shield().shieldOn = true;

            var modPlayer = player.Calamity();
            modPlayer.godSlayer = true;
            player.GetAttackSpeed<ShtuxianDamage>() += 0.2f;
            //player.ShtunCal.godslayerShtun = true;

            var hotkey = CalamityKeybinds.GodSlayerDashHotKey.TooltipHotkeyString();
            player.setBonus = this.GetLocalizedValue("SetBonus") + "\n" + CalamityUtils.GetTextFromModItem<GodSlayerChestplate>("CommonSetBonus").Format(hotkey, GodslayerArmorDash.GodslayerCooldown);

            if (modPlayer.godSlayerDashHotKeyPressed || (player.dashDelay != 0 && modPlayer.LastUsedDashID == GodslayerArmorDash.ID))
            {
                modPlayer.DeferredDashID = GodslayerArmorDash.ID;
                player.dash = 0;
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<ShtuxianDamage>() += 0.20f;
            player.GetCritChance<ShtuxianDamage>() += 15;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<CosmiliteBar>(14).
                AddIngredient<AscendantSpiritEssence>(4).
                AddTile<CosmicAnvil>().
                Register();
        }
    }
}
