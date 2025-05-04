using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Redemption.Items.Accessories.PreHM;
using Redemption.BaseExtension;
using Redemption.Items.Armor.PreHM.DragonLead;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Redemption.Items.Armor.HM.Hardlight;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using Redemption.Items.Weapons.HM.Summon;
using Redemption.Items.Accessories.HM;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class HardlightEnchant : BaseEnchant
    {
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

        public override Color nameColor => new Color(0, 242, 170);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<HardlightEffect>(base.Item))
            {
                if (player.HeldItem.DamageType == DamageClass.Magic)
                {
                    player.RedemptionPlayerBuff().hardlightBonus = 2;
                }
                if (player.HeldItem.DamageType == DamageClass.Melee)
                {
                    player.RedemptionPlayerBuff().hardlightBonus = 3;
                }
                if (player.HeldItem.DamageType == DamageClass.Summon)
                {
                    player.RedemptionPlayerBuff().hardlightBonus = 4;
                }
                if (player.HeldItem.DamageType == DamageClass.Ranged)
                {
                    player.RedemptionPlayerBuff().hardlightBonus = 5;
                }
            }
            if (player.AddEffect<ShieldGeneratorEffect>(base.Item))
            {
                player.RedemptionPlayerBuff().shieldGenerator = true;
            }
            if (player.AddEffect<SlayerEffect>(base.Item))
            {
                player.RedemptionPlayerBuff().shieldGenerator = true;
            }
        }

        public class HardlightEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();

            public override int ToggleItemType => ModContent.ItemType<HardlightEnchant>();
        }

        public class ShieldGeneratorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();

            public override int ToggleItemType => ModContent.ItemType<HardlightEnchant>();
        }
        public class SlayerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();

            public override int ToggleItemType => ModContent.ItemType<HardlightEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:HardlightHelms");
            recipe.AddIngredient<HardlightPlate>();
            recipe.AddIngredient<HardlightBoots>();
            recipe.AddIngredient<SlayerGun>();
            recipe.AddIngredient<PocketShieldGenerator>();
            recipe.AddIngredient<SlayerController>();

            recipe.AddTile(125);

            recipe.Register();
        }
    }
}
