using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.BasicAccessories;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.TransformItems;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class MagmaEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Green;
            Item.value = 60000;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //toggle
               // ModContent.Find<ModItem>(this.thorium.Name, "MagmaHelmet").UpdateArmorSet(player);

            //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "SpringSteps").UpdateAccessory(player, hideVisual);

            //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "SlagStompers").UpdateAccessory(player, hideVisual);

            ModContent.Find<ModItem>(this.thorium.Name, "MoltenSpearTip").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<MagmaGill>());
            recipe.AddIngredient(ModContent.ItemType<MagmaLocket>());
            recipe.AddIngredient(ModContent.ItemType<MagmaCharm>());
            recipe.AddIngredient(ModContent.ItemType<SpringSteps>());
            recipe.AddIngredient(ModContent.ItemType<SlagStompers>());
            recipe.AddIngredient(ModContent.ItemType<MoltenSpearTip>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
