using Terraria;
using ssm.Core;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.ThrownItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LifeBinderEnchant : BaseEnchant
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
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.thorium.Name, "LifeBinderMask").UpdateArmorSet(player);

            ModContent.Find<ModItem>("ssm", "IridescentEnchant").UpdateAccessory(player, hideVisual);

            ModContent.Find<ModItem>(this.thorium.Name, "DewCollector").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LifeBinderMask>());
            recipe.AddIngredient(ModContent.ItemType<LifeBinderBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<LifeBinderGreaves>());
            recipe.AddIngredient(ModContent.ItemType<IridescentEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DewCollector>());
            recipe.AddIngredient(ModContent.ItemType<SunrayStaff>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
