using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Donate;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CrierEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 1;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.thorium.Name, "CriersCap").UpdateArmorSet(player);

            ModContent.Find<ModItem>(this.thorium.Name, "LuckyRabbitsFoot").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CriersCap>());
            recipe.AddIngredient(ModContent.ItemType<CriersSash>());
            recipe.AddIngredient(ModContent.ItemType<CriersLeggings>());
            recipe.AddIngredient(ModContent.ItemType<LuckyRabbitsFoot>());
            recipe.AddIngredient(ModContent.ItemType<WoodenWhistle>());
            recipe.AddRecipeGroup("ssm:AnyBugleHorn");

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
