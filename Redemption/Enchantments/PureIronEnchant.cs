using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Redemption.Items.Weapons.PreHM.Summon;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.PreHM.PureIron;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.BaseExtension;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class PureIronEnchant : BaseEnchant
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

        public override Color nameColor => new Color(89, 89, 105);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<PureIronCross>(base.Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.Redemption.Name, "ErhanCross").UpdateAccessory(player, hideVisual: false);
            }

            player.RedemptionPlayerBuff().ElementalResistance[2] += 0.2f;
            player.RedemptionPlayerBuff().pureIronBonus = true;
            player.RedemptionPlayerBuff().MetalSet = true;
        }

        public class PureIronCross : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();

            public override int ToggleItemType => ModContent.ItemType<PureIronEnchant>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<PureIronHelmet>();
            recipe.AddIngredient<PureIronChestplate>();
            recipe.AddIngredient<PureIronGreaves>();
            recipe.AddIngredient<AntlerStaff>();
            recipe.AddIngredient<PureIronSword>();
            recipe.AddIngredient<ErhanCross>();
            recipe.AddTile(26);

            recipe.Register();
        }
    }
}
