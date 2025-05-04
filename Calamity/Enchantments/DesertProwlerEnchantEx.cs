using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.Items.Armor.DesertProwler;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Accessories;
using CalamityMod.Projectiles.Pets;
using CalamityMod.Buffs.Pets;
using CalamityMod.Items.Pets;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DesertProwlerEnchantEx : BaseEnchant
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
        public override Color nameColor => new(102, 89, 54);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<DesertProwlerEffect>(Item))
            {
                player.GetModPlayer<DesertProwlerPlayer>().desertProwlerSet = true;
            }
            if (player.AddEffect<LuxorEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "LuxorsGift").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DimeEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "ThiefsDime").UpdateAccessory(player, hideVisual);
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(ModContent.BuffType<GoldieBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<GoldieBuff>(), 3600, true, false);
                    }
                    const int damage = 100;
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<GoldiePet>()] < 1)
                        ShtunUtils.NewSummonProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<GoldiePet>(), damage, 8f, player.whoAmI);
                }
            }
            ModContent.Find<ModItem>(this.fargocross.Name, "DesertProwlerEnchant").UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerHat>());
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerShirt>());
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerPants>());
            recipe.AddIngredient(ModContent.ItemType<LuxorsGift>());
            recipe.AddIngredient(ModContent.ItemType<ThiefsDime>());
            recipe.AddIngredient(ModContent.ItemType<CrackshotColt>());
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerEnchant>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class DesertProwlerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DesertProwlerEnchantEx>();
        }
        public class LuxorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DesertProwlerEnchantEx>();
        }
        public class DimeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DesertProwlerEnchantEx>();
        }
    }
}