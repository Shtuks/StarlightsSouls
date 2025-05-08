using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ssm.Core;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.Consumable;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Projectiles.Minions;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using FargowiltasSouls;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LivingWoodEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 1;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();

            ModContent.Find<ModItem>(this.thorium.Name, "LivingWoodMask").UpdateArmorSet(player);

            player.maxMinions++;

            if (player.AddEffect<LivingWoodEffect>(Item))
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    const int damage = 10;
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<LivingWoodAcornPro>()] < 1)
                        FargoSoulsUtil.NewSummonProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<LivingWoodAcornPro>(), damage, 8f, player.whoAmI);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LivingWoodMask>());
            recipe.AddIngredient(ModContent.ItemType<LivingWoodChestguard>());
            recipe.AddIngredient(ModContent.ItemType<LivingWoodBoots>());
            recipe.AddIngredient(ModContent.ItemType<LivingLeaf>(), 39);
            recipe.AddIngredient(ModContent.ItemType<LivingWoodAcorn>());
            recipe.AddIngredient(ModContent.ItemType<ChiTea>(), 5);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class LivingWoodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LivingWoodEnchant>();

            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
