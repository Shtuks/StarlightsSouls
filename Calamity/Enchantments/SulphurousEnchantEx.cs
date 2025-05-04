using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.Sulphurous;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
using CalamityMod.Items.Armor.Victide;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class SulphurousEnchantEx : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        private readonly Mod fargocross = ModLoader.GetMod("FargowiltasCrossmod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }

        public override Color nameColor => new Color(181, 139, 161);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<SulphurEffect>(Item))
            {
                modPlayer.sulphurSet = true;
            }
            if (player.AddEffect<SandCloakEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "SandCloak").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<AmidiasEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "AmidiasPendant").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DiceEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "OldDie").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<MedallionEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "RustyMedallion").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(fargocross.Name, "SulphurEnchant").UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SulphurousHelmet>());
            recipe.AddIngredient(ModContent.ItemType<SulphurousBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<SulphurousLeggings>());
            recipe.AddIngredient(ModContent.ItemType<SandCloak>());
            recipe.AddIngredient(ModContent.ItemType<AmidiasPendant>());
            recipe.AddIngredient(ModContent.ItemType<OldDie>());
            recipe.AddIngredient(ModContent.ItemType<RustyMedallion>());
            recipe.AddIngredient(ModContent.ItemType<CausticCroakerStaff>());
            recipe.AddIngredient(ModContent.ItemType<SulphurEnchant>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
    public class SulphurEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
        public override int ToggleItemType => ModContent.ItemType<SulphurousEnchantEx>();
    }
    public class AmidiasEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
        public override int ToggleItemType => ModContent.ItemType<SulphurousEnchantEx>();
    }
    public class SandCloakEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
        public override int ToggleItemType => ModContent.ItemType<SulphurousEnchantEx>();
    }
    public class DiceEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
        public override int ToggleItemType => ModContent.ItemType<SulphurousEnchantEx>();
    }
    public class MedallionEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
        public override int ToggleItemType => ModContent.ItemType<SulphurousEnchantEx>();
    }
}