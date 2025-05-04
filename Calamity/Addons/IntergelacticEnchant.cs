using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.CalPlayer;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using CatalystMod.Items.Armor.Intergelactic;
using CatalystMod.Items.Tools.GrapplingHooks;
using CatalystMod;
using CatalystMod.Buffs.DamageOverTime;
using CalamityMod;
using CatalystMod.Buffs;
using CatalystMod.Projectiles.Typeless;
using CatalystMod.Items.Accessories;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Catalyst.Name)]
    [JITWhenModsEnabled(ModCompatibility.Catalyst.Name)]
    public class IntergelacticEnchant : BaseEnchant
    {
        public int CurrentRockDamage;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 50000000;
        }

        public override Color nameColor => new(173, 52, 70);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CatalystPlayer modPlayer = player.GetModPlayer<CatalystPlayer>();
            if (player.AddEffect<IntergelacticEffect>(Item))
            {
                modPlayer.intergelacticSet = Item;

                //player.AddBuff(ModContent.BuffType<AstrageldonRocksSetbonus>(), 12);
                //int type = ModContent.ProjectileType<AstralRocksProj>();
                //float damageMultiplier = player.GetTotalDamage(DamageClass.Generic).Multiplicative - 1f;
                //int damage = (CurrentRockDamage = (int)(120f * (player.GetTotalDamage(DamageClass.Generic).Multiplicative + damageMultiplier)));
                //if (player.ownedProjectileCounts[type] <= 0 && player.whoAmI == Main.myPlayer)
                //{
                //    Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.One, type, damage, 10f, player.whoAmI);
                //}
            }

            player.buffImmune[ModContent.BuffType<AstralBlight>()] = true;
            modPlayer.resistMetanovaGravity = true;
            modPlayer.intergelacticAstralBlight = true;
            modPlayer.influxCore = Item;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:IntergelacticHelmet");
            recipe.AddIngredient(ModContent.ItemType<IntergelacticBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<IntergelacticGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CatalystMod.Items.Weapons.Typeless.ScytheoftheAbandonedGod>());
            recipe.AddIngredient(ModContent.ItemType<AstralLash>());
            recipe.AddIngredient(ModContent.ItemType<InfluxCluster>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public class IntergelacticEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IntergelacticEnchant>();
        }
    }
}
