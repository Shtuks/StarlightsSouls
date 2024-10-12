using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BossThePrimordials.Slag;
using ssm.Core;
using ThoriumMod.Items.Cultist;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class PyromancerEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            modPlayer.PyroEnchant = true;

            ModContent.Find<ModItem>(this.thorium.Name, "PyromancerCowl").UpdateArmorSet(player);

            //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "PlasmaGenerator").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<PyromancerCowl>());
            recipe.AddIngredient(ModContent.ItemType<PyromancerTabard>());
            recipe.AddIngredient(ModContent.ItemType<PyromancerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<PlasmaGenerator>());
            recipe.AddIngredient(ModContent.ItemType<AncientFlame>());
            recipe.AddIngredient(ModContent.ItemType<AlmanacofAgony>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
