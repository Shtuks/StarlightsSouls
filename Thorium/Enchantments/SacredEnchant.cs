using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using ssm.Core;
using ssm.Thorium;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SacredEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

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
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            //sacred effect
            modPlayer.SacredEnchant = true;

            ModContent.Find<ModItem>("ssm", "NoviceClericEnchant").UpdateAccessory(player, true);

            //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "KarmicHolder").UpdateAccessory(player, true);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            //recipe.AddIngredient(ModContent.ItemType<HallowedPaladinHelmet>());
            //recipe.AddIngredient(ModContent.ItemType<HallowedPaladinBreastplate>());
            //recipe.AddIngredient(ModContent.ItemType<HallowedPaladinLeggings>());
            recipe.AddIngredient(ModContent.ItemType<NoviceClericEnchant>());
            recipe.AddIngredient(ModContent.ItemType<KarmicHolder>());
            recipe.AddIngredient(ModContent.ItemType<Liberation>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
