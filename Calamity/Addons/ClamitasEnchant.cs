using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Clamity.Content.Bosses.Clamitas.Crafted.ClamitasArmor;
using Clamity.Content.Bosses.Clamitas.Crafted.Weapons;
using Clamity.Content.Bosses.Pyrogen.Drop;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Clamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Clamity.Name)]
    public class ClamitasEnchant : BaseEnchant
    {
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
            ModContent.Find<ModItem>(ModCompatibility.Clamity.Name, "HellFlare").UpdateAccessory(player, false);
            
            if (player.AddEffect<PyrogenEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.Clamity.Name, "SoulOfPyrogen").UpdateAccessory(player, false);
            }

            if (player.AddEffect<ClamitasEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.Clamity.Name, "ClamitasShellmet").UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ClamitasShellmet>());
            recipe.AddIngredient(ModContent.ItemType<ClamitasShellplate>());
            recipe.AddIngredient(ModContent.ItemType<ClamitasShelleggings>());
            recipe.AddIngredient(ModContent.ItemType<ClamitasCrusher>());
            recipe.AddIngredient(ModContent.ItemType<SoulOfPyrogen>());
            recipe.AddIngredient(ModContent.ItemType<HellFlare>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class PyrogenEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ClamitasEnchant>();
        }

        public class ClamitasEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ClamitasEnchant>();
        }
    }
}
