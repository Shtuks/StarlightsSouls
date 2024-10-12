using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Lodestone;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LodestoneEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 5;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "LodeStoneFaceGuard").UpdateArmorSet(player);

            ModContent.Find<ModItem>(this.thorium.Name, "AstroBeetleHusk").UpdateAccessory(player, true);
            ModContent.Find<ModItem>(this.thorium.Name, "ObsidianScale").UpdateAccessory(player, true);

            //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "SandweaversTiara").UpdateAccessory(player, true);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LodeStoneFaceGuard>());
            recipe.AddIngredient(ModContent.ItemType<LodeStoneChestGuard>());
            recipe.AddIngredient(ModContent.ItemType<LodeStoneShinGuards>());
            recipe.AddIngredient(ModContent.ItemType<AstroBeetleHusk>());
            recipe.AddIngredient(ModContent.ItemType<ObsidianScale>());
            recipe.AddIngredient(ModContent.ItemType<SandweaversTiara>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
