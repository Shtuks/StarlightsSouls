using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Luminance.Core.Graphics;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Buffs.Masomode;
using ssm.Content.Items.Accessories;
using ssm.Core;
using ssm.Content.Items;
using ssm.Content.Items.Materials;
using ssm.Content.Buffs.Minions;
using ssm.Content.SoulToggles;
using ssm.Content.Tiles;

namespace ssm.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    internal class ShtuxianSoul : BaseSoul
    {
        private readonly Mod fargosouls = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        private readonly Mod FargoMoreSoulsCompat = Terraria.ModLoader.ModLoader.GetMod("FargoMoreSoulsCompat");
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f; // Falling glide speed
            ascentWhenRising = 0.15f; // Rising speed
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(
            Player player,
            ref float speed,
            ref float acceleration)
        { speed = 18f; acceleration = 0.75f; }

        public override void SetStaticDefaults() { ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(7000, 12f, 4f); }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            this.Item.accessory = true;
            ItemID.Sets.ItemNoGravity[this.Item.type] = true;
            this.Item.rare = -11;
            this.Item.maxStack = 1;
            this.Item.value = Item.buyPrice(745, 745, 745, 745);
        }

        public virtual bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((!(((TooltipLine)line).Mod == "Terraria") || !(((TooltipLine)line).Name == "ItemName")) && !(((TooltipLine)line).Name == "FlavorText"))
                return true;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin((SpriteSortMode)1, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
            GameShaders.Armor.GetShaderFromItemId(2870).Apply((Entity)this.Item, new DrawData?());
            Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float)line.X, (float)line.Y), Color.White, 1f, 0.0f, 0.0f, -1);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
            return false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<FargoSoulsPlayer>().MutantEyeCD > 0) { player.GetModPlayer<FargoSoulsPlayer>().MutantEyeCD--; }

            ModContent.Find<ModItem>(this.fargosouls.Name, "EternitySoul").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "EternityForce").UpdateAccessory(player, false);

            //ModContent.Find<ModItem>(this.FargoMoreSoulsCompat.Name, "SoulOfTmod").UpdateAccessory(player, false);

            if (ModLoader.TryGetMod("Redemption", out Mod Redemption)){
                //ModContent.Find<ModItem>(((ModType)this).Mod.Name, "RedemptionSoul").UpdateAccessory(player, false);}
            if (ModLoader.TryGetMod("SacredTools", out Mod SoA)){
                ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SoASoul").UpdateAccessory(player, false);}
            if (ModLoader.TryGetMod("CalamityMod", out Mod kal)){
                ModContent.Find<ModItem>(((ModType)this).Mod.Name, "CalamitySoul").UpdateAccessory(player, false);}

            if (!Main.zenithWorld) { player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "TimeFrozenBuff").Type] = true; }
            player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "MutantPresenceBuff").Type] = true;
            player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "CoffinTossBuff").Type] = true;
            if (!Main.zenithWorld) { player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "MutantRebirthBuff").Type] = true; }
            player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "GodEaterBuff").Type] = true;
            if (!Main.zenithWorld) { player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "FossilReviveCDBuff").Type] = true; }
            player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "BerserkerInstallCDBuff").Type] = true;
            if (!Main.zenithWorld) { player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "TimeStopCDBuff").Type] = true; }
            if (!Main.zenithWorld) { player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "AbomRebirthBuff").Type] = true; }
            if (!Main.zenithWorld) { player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "AbomCooldownBuff").Type] = true; }

            player.AddBuff(ModContent.BuffType<SadismBuff>(), 2);

            player.accWatch = 3;
            player.GetDamage(DamageClass.Generic) += 1000 / 100f;
            player.buffImmune[194] = true;
            player.buffImmune[68] = true;
            player.statDefense = player.statDefense *= 2;
            player.statDefense = player.statDefense += 1000;
            player.statLifeMax2 += 20000;
            player.lifeRegen += 700;
            player.blockRange += 700;
            player.noKnockback = true;
            player.GetAttackSpeed(DamageClass.Generic) += 10f;
            player.GetArmorPenetration(DamageClass.Generic) += 745f;
            player.buffImmune[30] = true;
            player.buffImmune[21] = true;
            player.buffImmune[36] = true;
            player.buffImmune[35] = true;
            player.buffImmune[194] = true;
            player.buffImmune[33] = true;
            player.buffImmune[32] = true;
            player.buffImmune[47] = true;
            player.buffImmune[46] = true;
            player.buffImmune[120] = true;
            player.buffImmune[20] = true;
            player.buffImmune[199] = true;
            player.buffImmune[323] = true;
            player.buffImmune[169] = true;
            player.buffImmune[163] = true;
            player.buffImmune[38] = true;
            player.buffImmune[68] = true;
            player.buffImmune[145] = true;
            player.buffImmune[88] = true;
            player.buffImmune[44] = true;
            player.buffImmune[119] = true;
            player.buffImmune[353] = true;
            player.buffImmune[195] = true;
            player.buffImmune[69] = true;
            player.buffImmune[22] = true;
            player.buffImmune[153] = true;
            player.buffImmune[196] = true;
            player.buffImmune[39] = true;
            player.buffImmune[148] = true;
            player.buffImmune[23] = true;
            player.buffImmune[31] = true;
            player.buffImmune[67] = true;
            player.buffImmune[37] = true;
            player.buffImmune[72] = true;
            player.buffImmune[103] = true;
            player.buffImmune[137] = true;
            player.buffImmune[94] = true;
            player.lifeRegen *= 2;
            player.palladiumRegen = true;
            player.crimsonRegen = true;
            player.GetAttackSpeed(DamageClass.Generic) += 10f;
            player.accFishingLine = true;
            player.arcticDivingGear = true;
            player.accTackleBox = true;
            player.shinyStone = true;
            player.pStone = true;
            player.noFallDmg = true;
            player.accLavaFishing = true;
            player.fishingSkill += 3000000;
            player.lavaImmune = true;
            player.maxMinions += 900;
            player.manaCost -= 0.1f;
            player.aggro += 777;
            player.carpetTime *= 3;
            player.coinLuck *= 7f;
            player.waterWalk = true;
            player.lavaRose = true;
            player.goldRing = true;
            player.lifeRegen *= 10;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe(1);
            recipe.AddIngredient<EternityForce>(1);
            recipe.AddIngredient<Materials.InfinityIngot>(5);
            recipe.AddIngredient(this.FargoSoul, "EternitySoul", 1);
            recipe.AddTile<ShtuxibusForgeTile>();
            recipe.Register();
        }

        public abstract class ShtuxianSoulEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShtuxianSoulHeader>();
        }
    }
}
