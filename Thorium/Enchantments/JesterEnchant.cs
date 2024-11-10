using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossQueenJellyfish;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;


namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class JesterEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

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
            modPlayer.JesterEnchant = true;

            ModContent.Find<ModItem>(this.thorium.Name, "FanLetter").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:AnyJesterMask");
            recipe.AddRecipeGroup("ssm:AnyJesterShirt");
            recipe.AddRecipeGroup("ssm:AnyJesterLeggings");
            recipe.AddRecipeGroup("ssm:AnyLetter");
            recipe.AddRecipeGroup("ssm:AnyTambourine");
            recipe.AddIngredient(ModContent.ItemType<SkywareLute>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
