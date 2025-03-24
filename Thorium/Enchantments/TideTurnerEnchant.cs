using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.BossForgottenOne;
using ThoriumMod.Items.Misc;
using ssm.Core;
using ThoriumMod.Items.Donate;
using ssm.Thorium;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.SoA.Enchantments.BlazingBruteEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TideTurnerEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new Color(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //mini crits and daggers
            //modPlayer.TideTurnerEnchant = true;

            if (player.AddEffect<TideTurnerEffect>(Item))
            {
                //toggle
                //floating globs and defense
                thoriumPlayer.tideHelmet = true;
                //if (thoriumPlayer.tideOrb < 10)
                //{
                //    timer++;
                //    if (timer > 30)
                //    {
                //        float num = 30f;
                //        int num2 = 0;
                //        while (num2 < num)
                //        {
                //            Vector2 vector = Vector2.UnitX * 0f;
                //            vector += -Utils.RotatedBy(Vector2.UnitY, (num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(25f, 25f);
                //            vector = Utils.RotatedBy(vector, Utils.ToRotation(player.velocity), default(Vector2));
                //            int num3 = Dust.NewDust(player.Center, 0, 0, 113, 0f, 0f, 0, default(Color), 1f);
                //            Main.dust[num3].scale = 1.6f;
                //            Main.dust[num3].noGravity = true;
                //            Main.dust[num3].position = player.Center + vector;
                //            Main.dust[num3].velocity = player.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                //            int num4 = num2;
                //            num2 = num4 + 1;
                //        }
                //        thoriumPlayer.tideOrb++;
                //        timer = 0;
                //    }
                //}
            }

            //set bonus damage to healing hot key
            thoriumPlayer.setTideTurner = true;


            ModContent.Find<ModItem>(this.thorium.Name, "PlagueLordFlask").UpdateAccessory(player, hideVisual);
        }

        public class TideTurnerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TideTurnerEnchant>();
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<TideTurnerHelmet>());
            recipe.AddIngredient(ModContent.ItemType<TideTurnerBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<TideTurnerGreaves>());
            recipe.AddIngredient(ModContent.ItemType<PlagueLordFlask>());
            recipe.AddIngredient(ModContent.ItemType<PoseidonCharge>());
            recipe.AddIngredient(ModContent.ItemType<TidalWave>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
