using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using gunrightsmod;
using ssm.Core;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using gunrightsmod.Content.Items;

namespace ssm.gunrightmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class RockSaltEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.gunrightsmod;
        }

        private readonly Mod gunrightsmod = ModLoader.GetMod("gunrightsmod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 200000;
        }

        public class RockSaltEnchantPlayer : ModPlayer
        {
            public bool rockSaltEnchantEquipped;

            public override void ResetEffects()
            {
                rockSaltEnchantEquipped = false;
            }

            public override void OnHitByNPC(NPC npc, int damage, bool crit)
            {
                TryApplySaltedWounds();
            }

            public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
            {
                TryApplySaltedWounds();
            }

            private void TryApplySaltedWounds()
            {
                if (rockSaltEnchantEquipped)
                {
                    Player.AddBuff(ModContent.BuffType<gunrightsmod.Content.Buffs.SaltedWounds>(), 600); // 5 seconds
                }
            }
        }

        public override Color nameColor => new(255, 128, 0);

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<RockSaltFedora>());
            recipe.AddIngredient(ModContent.ItemType<RockSaltChestplate>());
            recipe.AddIngredient(ModContent.ItemType<RockSaltLeggings>());
            recipe.AddIngredient(ModContent.ItemType<Naclslash>());
            recipe.AddIngredient(ModContent.ItemType<SodiumHamaxe>());
            recipe.AddIngredient(ModContent.ItemType<Saltpendant>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<RockSaltEnchantPlayer>().rockSaltEnchantEquipped = true;
        }
    }
}
