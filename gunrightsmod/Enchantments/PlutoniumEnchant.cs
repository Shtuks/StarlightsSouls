using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using gunrightsmod.Content.Items;

namespace smm.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class PlutoniumEnchant : BaseEnchant
    {
        private int attackCounter = 0;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.gunrightsmod;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<PlutoniumEnchantPlayer>().hasPlutoniumEnchant)
            {
                if (player.HeldItem.useTime > 0 && player.ItemTime == 0)
                {
                    attackCounter++;
                }

                if (attackCounter >= 4)
                {
                    attackCounter = 0;
                    FireHomingProjectile(player);
                }
            }
        }

        private void FireHomingProjectile(Player player)
        {
            int baseDamage = player.HeldItem.damage;
            int damage = (int)(baseDamage * 1.25f);

            int type = ModContent.ProjectileType<projectiles.HomingBullet>();
            Vector2 spawnPosition = player.Center + new Vector2(0, -40);
            Vector2 velocity = Vector2.Zero;

            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), spawnPosition, velocity, type, damage, 0f, player.whoAmI);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<PlutoniumFacemask>());
            recipe.AddIngredient(ModContent.ItemType<PlutoniumChestplate>());
            recipe.AddIngredient(ModContent.ItemType<PlutoniumPants>());
            recipe.AddIngredient(ModContent.ItemType<Needler>());
            recipe.AddIngredient(ModContent.ItemType<PlutoniumSword>());
            recipe.AddIngredient(ModContent.ItemType<PlutoniumBow>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }

    public class PlutoniumEnchantPlayer : ModPlayer
    {
        public bool hasPlutoniumEnchant;

        public override void ResetEffects()
        {
            hasPlutoniumEnchant = false;
        }
    }
}
