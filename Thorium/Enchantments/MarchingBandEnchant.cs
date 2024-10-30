using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BardItems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class MarchingBandEnchant : BaseEnchant
    {
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

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            //toggle
                //marching band set 
                //thoriumPlayer.setMarchingBand = true;

            ModContent.Find<ModItem>(this.thorium.Name, "FullScore").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<MarchingBandCap>());
            recipe.AddIngredient(ModContent.ItemType<MarchingBandUniform>());
            recipe.AddIngredient(ModContent.ItemType<MarchingBandLeggings>());
            recipe.AddIngredient(ModContent.ItemType<FullScore>());
            recipe.AddIngredient(ModContent.ItemType<FrostwindCymbals>());
            recipe.AddIngredient(ModContent.ItemType<ShadowflameWarhorn>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
