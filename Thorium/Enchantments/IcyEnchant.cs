using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.Icy;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class IcyEnchant : BaseEnchant
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

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            ModContent.Find<ModItem>(this.thorium.Name, "BlizzardPouch").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(this.thorium.Name, "IcyHeadgear").UpdateArmorSet(player);
        }

        public class IcyEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IcyEnchant>();
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<IcyHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<IcyMail>());
            recipe.AddIngredient(ModContent.ItemType<IcyGreaves>());
            recipe.AddIngredient(ModContent.ItemType<IceShaver>());
            recipe.AddIngredient(ModContent.ItemType<BlizzardPouch>());
            recipe.AddIngredient(ItemID.IceBoomerang);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
