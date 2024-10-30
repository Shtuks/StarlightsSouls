using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Illumite;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.MeleeItems;
using ThoriumMod.Items.Donate;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class IllumiteEnchant : BaseEnchant
    {
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

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            modPlayer.IllumiteEnchant = true;

            ModContent.Find<ModItem>(this.thorium.Name, "TheNuclearOption").UpdateAccessory(player, true);
            ModContent.Find<ModItem>(this.thorium.Name, "TunePlayerLifeRegen").UpdateAccessory(player, true);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<IllumiteMask>());
            recipe.AddIngredient(ModContent.ItemType<IllumiteChestplate>());
            recipe.AddIngredient(ModContent.ItemType<IllumiteGreaves>());
            recipe.AddIngredient(ModContent.ItemType<TheNuclearOption>());
            recipe.AddIngredient(ModContent.ItemType<TunePlayerLifeRegen>());
            recipe.AddIngredient(ModContent.ItemType<HandCannon>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
