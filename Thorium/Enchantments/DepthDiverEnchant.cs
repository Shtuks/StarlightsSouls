using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.BossQueenJellyfish;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Painting;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Content.Buffs.Minions;
using ThoriumMod.Buffs;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DepthDiverEnchant : BaseEnchant
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
            //toggle
                for (int i = 0; i < 255; i++)
                {
                    Player player2 = Main.player[i];
                    if (player2.active && Vector2.Distance(player2.Center, player.Center) < 250f)
                    {
                    player.AddBuff(ModContent.BuffType<DepthDiverAura>(), 30, false);
                    }
                }

            ModContent.Find<ModItem>("ssm", "CoralEnchant").UpdateAccessory(player, hideVisual);

            //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "DrownedDoubloon").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DepthDiverHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DepthDiverChestplate>());
            recipe.AddIngredient(ModContent.ItemType<DepthDiverGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CoralEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DrownedDoubloon>());
            recipe.AddIngredient(ModContent.ItemType<MagicConch>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
