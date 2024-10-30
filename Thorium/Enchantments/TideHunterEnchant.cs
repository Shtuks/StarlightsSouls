using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.BossQueenJellyfish;
using ssm.Core;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Painting;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TideHunterEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 3;
            Item.value = 80000;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            //tide hunter set bonus
            modPlayer.TideHunterEnchant = true;
            //angler bowl
            ModContent.Find<ModItem>(this.thorium.Name, "AnglerBowl").UpdateAccessory(player, hideVisual);
            //yew set bonus
            modPlayer.YewEnchant = true;
            //goblin war shield
            ModContent.Find<ModItem>(this.thorium.Name, "GoblinWarshield").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<TideHunterCap>());
            recipe.AddIngredient(ModContent.ItemType<TideHunterChestpiece>());
            recipe.AddIngredient(ModContent.ItemType<TideHunterLeggings>());
            recipe.AddIngredient(ModContent.ItemType<YewWoodEnchant>());
            recipe.AddIngredient(ModContent.ItemType<AnglerBowl>());
            recipe.AddIngredient(ModContent.ItemType<HydroPickaxe>());


            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
