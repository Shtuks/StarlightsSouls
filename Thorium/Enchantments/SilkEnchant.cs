using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.EarlyMagic;
using ssm.Core;
using ThoriumMod.Items.BasicAccessories;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.SummonItems;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.ThoriumEnchant;
using ssm.Content.SoulToggles;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SilkEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 0;
            Item.value = 20000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.thorium.Name, "SilkHat").UpdateArmorSet(player);

            ModContent.Find<ModItem>(this.thorium.Name, "ArtificersFocus").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "ArtificersShield").UpdateAccessory(player, hideVisual);

            if (player.AddEffect<ArtificersEffect>(Item))
            {
                ModContent.Find<ModItem>(this.thorium.Name, "ArtificersRocketeers").UpdateAccessory(player, hideVisual);
            }
        }

        public class ArtificersEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilkEnchant>();
            public override bool MutantsPresenceAffects => true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SilkHat>());
            recipe.AddIngredient(ModContent.ItemType<SilkTabard>());
            recipe.AddIngredient(ModContent.ItemType<SilkLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ArtificersFocus>());
            recipe.AddIngredient(ModContent.ItemType<ArtificersShield>());
            recipe.AddIngredient(ModContent.ItemType<ArtificersRocketeers>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
