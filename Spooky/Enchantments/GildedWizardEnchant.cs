using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Spooky.Core;
using Spooky.Content.Items.SpookyBiome.Armor;
using Spooky.Content.Items.SpookyBiome;
using Spooky.Content.Tiles.SpookyBiome.Furniture;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;

namespace ssm.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class GildedWizardEnchant : BaseEnchant
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
            if (player.HasItem(74))
            {
                player.GetDamage(DamageClass.Magic) += 0.2f;
                return;
            }

            float num = 0.02f;
            int num2 = player.CountItem(73);
            if (num2 < 10)
            {
                player.GetDamage(DamageClass.Magic) += num * (float)num2;
            }
            else
            {
                player.GetDamage(DamageClass.Magic) += 0.2f;
            }

            if (player.HasItem(74))
            {
                player.manaCost -= 0.2f;
                return;
            }

            float num22 = 0.02f;
            int num2222 = player.CountItem(73);
            if (num2222 < 10)
            {
                player.manaCost -= num22 * (float)num2222;
            }
            else
            {
                player.manaCost -= 0.2f;
            }

            ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "BustlingGlowshroom").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:AnyGildedHat");
            recipe.AddIngredient<WizardGangsterBody>();
            recipe.AddIngredient<WizardGangsterLegs>();
            recipe.AddIngredient<BustlingGlowshroom>();
            recipe.AddIngredient<ShroomWhip>();
            recipe.AddIngredient<SwoleMushroomStatueItem>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
