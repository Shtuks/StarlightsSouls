using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.BardItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TemplarEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 3;
            Item.value = 80000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.thorium.Name, "TemplarsCirclet").UpdateArmorSet(player);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<TemplarsCirclet>());
            recipe.AddIngredient(ModContent.ItemType<TemplarsTabard>());
            recipe.AddIngredient(ModContent.ItemType<TemplarsLeggings>());
            recipe.AddIngredient(ModContent.ItemType<LifesGift>());
            recipe.AddIngredient(ModContent.ItemType<TemplarsGrace>());
            recipe.AddIngredient(ModContent.ItemType<Prophecy>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
