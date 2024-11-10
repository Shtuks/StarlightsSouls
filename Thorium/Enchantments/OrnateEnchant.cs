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
    public class OrnateEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 7;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            thoriumPlayer.setOrnate = true;
            //concert tickets
            thoriumPlayer.bardResourceMax2 += 2;
            for (int i = 0; i < Main.myPlayer; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && i != player.whoAmI && (!player2.hostile || (player2.team == player.team && player2.team != 0)) && player2.DistanceSQ(player.Center) < 202500f)
                {
                    thoriumPlayer.inspirationRegenBonus += 0.02f;
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<OrnateHat>());
            recipe.AddIngredient(ModContent.ItemType<OrnateJerkin>());
            recipe.AddIngredient(ModContent.ItemType<OrnateLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ConcertTickets>());
            recipe.AddIngredient(ModContent.ItemType<OrichalcumSlideWhistle>());
            //recipe.AddIngredient(ModContent.ItemType<GreenTambourine>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
