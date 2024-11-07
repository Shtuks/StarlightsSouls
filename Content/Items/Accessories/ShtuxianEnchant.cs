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
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Content.Items.Accessories;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using CalamityMod.Items.Accessories;
using ssm.Content.Items.Armor;
using ssm.Content.Items.Materials;

namespace ssm.Content.Items.Accessories
{
    public class ShtuxianEnchant : BaseEnchant
    {
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExtraContent;
        }

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

        public override Color nameColor => new(10, 255, 10);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shield().noShieldHitsCap = true;
            player.Shield().shieldRegenSpeed += 10;

            ModContent.Find<ModItem>(this.Mod.Name, "ChtuxlagorHeart").UpdateAccessory(player, true);
            player.Shtun().equippedShtuxianEnchantment = true;

            if (player.AddEffect<ShtuxianEffect>(Item))
            {
                //ModContent.Find<ModItem>(this.Mod.Name, "QuantumHelmet").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Mod.Name, "QuantumChestplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Mod.Name, "QuantumPants").UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ShtuxiumSoulShard>(50);
            recipe.AddIngredient<ChtuxlagorHeart>();
            //recipe.AddIngredient<ShtuxianSlasher>();
            //recipe.AddIngredient<QuantumHelmet>();
            recipe.AddIngredient<QuantumChestplate>();
            recipe.AddIngredient<QuantumPants>();
            recipe.Register();
        }

        public class ShtuxianEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShtuxianSoulHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShtuxianEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
    }
}
