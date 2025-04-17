using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ID;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Content.Items.Accessories;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls;

namespace ssm.Content.Items.Accessories
{
    public class NekomiEnchant : BaseEnchant
    {
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");

        public override void SetStaticDefaults() => ItemID.Sets.ItemNoGravity[this.Type] = true;

        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(1, 0, 0, 0);
            this.Item.rare = 10;
            this.Item.accessory = true;
        }

        public override Color nameColor => new(200, 20, 250);

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (!(((TooltipLine)line).Mod == "Terraria") || !(((TooltipLine)line).Name == "ItemName"))
                return true;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin((SpriteSortMode)1, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
            GameShaders.Armor.GetShaderFromItemId(3562).Apply((Entity)this.Item, new DrawData?());
            Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float)line.X, (float)line.Y), Color.White, 1f, 0.0f, 0.0f, -1);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
            return false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ShtunPlayer>().equippedNekomiEnchantment = true;
            ModContent.Find<ModItem>(this.FargoSoul.Name, "SparklingAdoration").UpdateAccessory(player, false);
            
            if (player.AddEffect<NekomiEffect>(Item))
            {
                player.FargoSouls().NekomiSet = true;
            }

            player.slotsMinions += 1f;
            player.GetCritChance<GenericDamageClass>() += 7f;
            player.GetDamage<GenericDamageClass>() += 0.07f;

            //no longer exist
            //player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "DeviPresenceBuff").Type] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe(1);
            recipe.AddIngredient(this.FargoSoul, "DeviatingEnergy", 50);
            recipe.AddIngredient(this.FargoSoul, "SparklingAdoration", 1);
            recipe.AddIngredient(this.FargoSoul, "BrokenBlade", 1);
            recipe.AddIngredient(this.FargoSoul, "NekomiHood", 1);
            recipe.AddIngredient(this.FargoSoul, "NekomiHoodie", 1);
            recipe.AddIngredient(this.FargoSoul, "NekomiLeggings", 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class NekomiEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NekomiHood>();
        }
    }
}
