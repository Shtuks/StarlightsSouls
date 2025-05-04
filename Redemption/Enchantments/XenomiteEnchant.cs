using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Redemption.Items.Accessories.PreHM;
using Redemption.Globals.Player;
using Redemption.BaseExtension;
using Redemption.Items.Armor.HM.Xenomite;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class XenomiteEnchant : BaseEnchant
    {
        public class XenomiteNecklace : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();

            public override int ToggleItemType => ModContent.ItemType<XenomiteEnchant>();
        }

        public class XenomiteArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();

            public override int ToggleItemType => ModContent.ItemType<XenomiteEnchant>();
        }


        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Redemption;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new Color(88, 126, 121);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<XenomiteNecklace>(base.Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.Redemption.Name, "NecklaceOfPerception").UpdateAccessory(player, hideVisual: true);
            }
            if (player.AddEffect<XenomiteArmorEffect>(base.Item))
            {
                player.RedemptionPlayerBuff().xenomiteBonus = true;
            }
            player.GetModPlayer<EnergyPlayer>().energyRegen += 10;
        }

        public class PureIronCross : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();

            public override int ToggleItemType => ModContent.ItemType<PureIronEnchant>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<XenomiteHelmet>();
            recipe.AddIngredient<XenomitePlate>();
            recipe.AddIngredient<XenomiteLeggings>();
            recipe.AddIngredient<Chernobyl>();
            recipe.AddIngredient<DAN>();
            recipe.AddIngredient<NecklaceOfPerception>();
            recipe.AddTile(125);

            recipe.Register();
        }
    }
}
