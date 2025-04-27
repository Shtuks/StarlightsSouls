using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Bronze;
using ThoriumMod.Items.ThrownItems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.BossBuriedChampion;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BronzeEnchant : BaseEnchant
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
            Item.rare = 2;
            Item.value = 60000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            //lightning
            modPlayer.BronzeEnchant = true;

            ModContent.Find<ModItem>(this.thorium.Name, "ChampionsRebuttal").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "SpartanSandles").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "OlympicTorch").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<BronzeHelmet>());
            recipe.AddIngredient(ModContent.ItemType<BronzeBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<BronzeGreaves>());
            recipe.AddIngredient(ModContent.ItemType<OlympicTorch>());
            recipe.AddIngredient(ModContent.ItemType<ChampionsRebuttal>());
            recipe.AddIngredient(ModContent.ItemType<SpartanSandles>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
