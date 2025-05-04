using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Redemption.Items.Armor.PreHM.CommonGuard;
using Redemption.Items.Accessories.PreHM;
using Redemption.BaseExtension;
using System;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class CommonGuardEnchant : BaseEnchant
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

        public override Color nameColor => new Color(139, 145, 156);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.12f;
            player.endurance += 0.06f;
            player.RedemptionPlayerBuff().MetalSet = true;
            if (Main.rand.NextBool(10) && Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame)
            {
                int index = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, 30);
                Main.dust[index].noGravity = true;
                Main.dust[index].velocity -= player.velocity * 0.5f;
            }

            ModContent.Find<ModItem>(ModCompatibility.Redemption.Name, "Wardbreaker").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(ModCompatibility.Redemption.Name, "KeepersCirclet").UpdateAccessory(player, hideVisual);

            if (player.AddEffect<CommonGuardBauble>(base.Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.Redemption.Name, "TrappedSoulBauble").UpdateAccessory(player, hideVisual: false);
            }
        }

        public class CommonGuardBauble : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();

            public override int ToggleItemType => ModContent.ItemType<CommonGuardEnchant>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:CommonGuardHelm");
            recipe.AddIngredient(ModContent.ItemType<CommonGuardPlateMail>());
            recipe.AddIngredient(ModContent.ItemType<CommonGuardGreaves>());
            recipe.AddIngredient(ModContent.ItemType<Wardbreaker>());
            recipe.AddIngredient(ModContent.ItemType<KeepersCirclet>());
            recipe.AddIngredient(ModContent.ItemType<TrappedSoulBauble>());

            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
