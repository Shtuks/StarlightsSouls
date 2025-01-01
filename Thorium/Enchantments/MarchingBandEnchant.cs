using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BardItems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.LodestoneEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class MarchingBandEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<MarchingBandEffect>(Item))
            {
                ModContent.Find<ModItem>(this.thorium.Name, "MarchingBandShako").UpdateArmorSet(player);
            }
            ModContent.Find<ModItem>(this.thorium.Name, "FullScore").UpdateAccessory(player, hideVisual);
        }

        public class MarchingBandEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarchingBandEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<MarchingBandShako>());
            recipe.AddIngredient(ModContent.ItemType<MarchingBandUniform>());
            recipe.AddIngredient(ModContent.ItemType<MarchingBandLeggings>());
            recipe.AddIngredient(ModContent.ItemType<FullScore>());
            recipe.AddIngredient(ModContent.ItemType<FrostwindCymbals>());
            recipe.AddIngredient(ModContent.ItemType<ShadowflameWarhorn>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
