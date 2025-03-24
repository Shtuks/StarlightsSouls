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
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.SandstoneEnchant;
using ssm.Content.SoulToggles;

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

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();

            if (player.AddEffect<TideHunterEffect>(Item))
            {
                //tide hunter set bonus
                modPlayer.TideHunterEnchant = true;
            }

            //angler bowl
            ModContent.Find<ModItem>(this.thorium.Name, "AnglerBowl").UpdateAccessory(player, hideVisual);
        }

        public class TideHunterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TideHunterEnchant>();
            public override bool ExtraAttackEffect => true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<TideHunterCap>());
            recipe.AddIngredient(ModContent.ItemType<TideHunterChestpiece>());
            recipe.AddIngredient(ModContent.ItemType<TideHunterLeggings>());
            recipe.AddIngredient(ModContent.ItemType<AnglerBowl>());
            recipe.AddIngredient(ModContent.ItemType<HydroPickaxe>());


            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
