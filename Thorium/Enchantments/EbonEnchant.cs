using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class EbonEnchant : BaseEnchant
    {
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

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.thorium.Name, "EbonHood").UpdateArmorSet(player);

            ModContent.Find<ModItem>(this.thorium.Name, "DarkHeart").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<EbonHood>());
            recipe.AddIngredient(ModContent.ItemType<EbonCloak>());
            recipe.AddIngredient(ModContent.ItemType<EbonLeggings>());
            recipe.AddIngredient(ModContent.ItemType<DarkHeart>());
            recipe.AddIngredient(ModContent.ItemType<LeechBolt>());
            recipe.AddIngredient(ModContent.ItemType<ShadowWand>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
