using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using ssm.Core;
using ThoriumMod.Items.Painting;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.BossThePrimordials.Dream;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DreamWeaverEnchant : BaseEnchant
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
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //all allies invuln hot key
            thoriumPlayer.setDreamWeaversHood = true;
            //enemies slowed and take more dmg hot key
            //thoriumPlayer.setDreamWeaversMask = true;
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DreamWeaversHelmet>()); //any..
            //recipe.AddIngredient(ModContent.ItemType<DreamWeaversHood>());
            recipe.AddIngredient(ModContent.ItemType<DreamWeaversTabard>());
            recipe.AddIngredient(ModContent.ItemType<DreamWeaversTreads>());
            recipe.AddIngredient(ModContent.ItemType<DragonHeartWand>());
            recipe.AddIngredient(ModContent.ItemType<SnackLantern>());
            recipe.AddIngredient(ModContent.ItemType<ChristmasCheer>());


            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
