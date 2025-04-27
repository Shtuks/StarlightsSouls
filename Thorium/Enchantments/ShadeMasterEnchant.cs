using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.ThrownItems;
using ssm.Core;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ShadeMasterEnchant : BaseEnchant
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
            Item.rare = 7;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.thorium.Name, "ShadeMasterMask").UpdateArmorSet(player);

            ModContent.Find<ModItem>(this.thorium.Name, "ShinobiSigil").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ShadeMasterMask>());
            recipe.AddIngredient(ModContent.ItemType<ShadeMasterGarb>());
            recipe.AddIngredient(ModContent.ItemType<ShadeMasterTreads>());
            recipe.AddIngredient(ModContent.ItemType<ShinobiSigil>());
            recipe.AddIngredient(ModContent.ItemType<ShadeKunai>(), 300);
            recipe.AddIngredient(ModContent.ItemType<TechniqueShadowDance>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
