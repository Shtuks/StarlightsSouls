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
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Fargowiltas.Items.Tiles;
using Terraria.Localization;
using Terraria.DataStructures;
using FargowiltasSouls.Core.Toggler;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Content.Items.Accessories;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Content.Items.Accessories
{
    public class GaiaEnchant : BaseEnchant
    {
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        public override void SetStaticDefaults() => ItemID.Sets.ItemNoGravity[this.Type] = true;

        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(1, 0, 0, 0);
            this.Item.rare = 10;
            this.Item.accessory = true;
        }
        /*public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
          if (!(((TooltipLine) line).Mod == "Terraria") || !(((TooltipLine) line).Name == "ItemName"))
            return true;
          Main.spriteBatch.End();
          Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
          GameShaders.Armor.GetShaderFromItemId(2869).Apply((Entity) this.Item, new DrawData?());
          Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float) line.X, (float) line.Y), Color.White, 1f, 0.0f, 0.0f, -1);
          Main.spriteBatch.End();
          Main.spriteBatch.Begin((SpriteSortMode) 0, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
          return false;
        }*/

        public override Color nameColor => new(0, 255, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<GaiaEffect>(Item))
            {
                ModContent.Find<ModItem>(this.FargoSoul.Name, "GaiaHelmet").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.FargoSoul.Name, "GaiaPlate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.FargoSoul.Name, "GaiaGreaves").UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe(1);
            recipe.AddIngredient(this.FargoSoul, "GaiaGreaves", 1);
            recipe.AddIngredient(this.FargoSoul, "GaiaPlate", 1);
            recipe.AddIngredient(this.FargoSoul, "GaiaHelmet", 1);
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }

        public class GaiaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GaiaHelmet>();
        }
    }
}
