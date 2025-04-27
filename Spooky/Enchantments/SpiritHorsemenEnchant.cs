using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Spooky.Core;
using Spooky.Content.Items.Cemetery.Armor;
using Spooky.Content.Items.BossBags.Accessory;
using Spooky.Content.Items.Catacomb;
using Spooky.Content.Items.Cemetery;
using Spooky.Content.Projectiles.Cemetery;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Thorium.Enchantments;
using static ssm.Thorium.Enchantments.BiotechEnchant;

namespace ssm.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SpiritHorsemenEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Spooky;
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

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<SpiritHorsemenEffect>(Item))
            {
                SpookyPlayer modPlayer = player.GetModPlayer<SpookyPlayer>();
                player.GetModPlayer<SpookyPlayer>().HorsemanSet = true;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<PumpkinHead>()] <= 0)
                {
                    Projectile.NewProjectile(null, player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<PumpkinHead>(), 0, 0f, player.whoAmI);
                }
            }
            ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "SkullAmulet").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "SpiritAmulet").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<SpiritHorsemanHead>();
            recipe.AddIngredient<SpiritHorsemanBody>();
            recipe.AddIngredient<SpiritHorsemanLegs>();
            recipe.AddIngredient<SpiritAmulet>();
            recipe.AddIngredient<SkullAmulet>();
            recipe.AddIngredient<SpiritScroll>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class SpiritHorsemenEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HorrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritHorsemenEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
