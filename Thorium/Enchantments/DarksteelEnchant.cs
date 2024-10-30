using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Darksteel;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Placeable;
using ssm.Core;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.RangedItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DarksteelEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

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
            //darksteel bonuses
            player.noKnockback = true;
            player.iceSkate = true;
            player.dash = 1;

            //steel effect
            if (player.statLife == player.statLifeMax2)
            {
                player.endurance += 0.1f;
            }

            ModContent.Find<ModItem>(this.thorium.Name, "SpikedBracer").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "BallnChain").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DarksteelFaceGuard>());
            recipe.AddIngredient(ModContent.ItemType<DarksteelBreastPlate>());
            recipe.AddIngredient(ModContent.ItemType<DarksteelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SteelEnchant>());
            recipe.AddIngredient(ModContent.ItemType<BallnChain>());
            recipe.AddIngredient(ModContent.ItemType<StrongestLink>());


            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
