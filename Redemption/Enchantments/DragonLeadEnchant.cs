using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Redemption.Items.Accessories.PreHM;
using Redemption.BaseExtension;
using System;
using Redemption.Items.Armor.PreHM.DragonLead;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class DragonLeadEnchant : BaseEnchant
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

        public override Color nameColor => new Color(116, 100, 127);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.RedemptionPlayerBuff().ElementalResistance[4] += 0.2f;
            player.RedemptionPlayerBuff().dragonLeadBonus = true;
            player.RedemptionPlayerBuff().MetalSet = true;
            ModContent.Find<ModItem>(ModCompatibility.Redemption.Name, "HeartInsignia").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DragonLeadSkull>());
            recipe.AddIngredient(ModContent.ItemType<DragonLeadRibplate>());
            recipe.AddIngredient(ModContent.ItemType<DragonLeadGreaves>());
            recipe.AddIngredient(ModContent.ItemType<HeartInsignia>());
            recipe.AddIngredient<DragonCleaver>();
            recipe.AddIngredient<DragonSlayersBow>();

            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
