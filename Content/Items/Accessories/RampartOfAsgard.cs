using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using ssm;
using Terraria.ModLoader;

namespace ssm.Content.Items.Accessories
{
    public class RampartOfAsgard : ModItem
    {
        private const double ContactDamageReduction = 0.15;
        public const double DefenseDamageMultiplier = 0.5;
        private readonly int NanomachinesDuration = 120;
        private readonly int NanomachinesHealPerFrame = 3;
        private readonly int NanomachinePauseAfterDamage = 60;

        public override void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

        public override void SetDefaults()
        {
            this.Item.defense = 50;
            ((Entity)this.Item).width = 66;
            ((Entity)this.Item).height = 64;
            this.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
            this.Item.accessory = true;
            this.Item.rare = -12;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer calamityPlayer = player.Calamity();
            calamityPlayer.sponge = true;
            calamityPlayer.spongeShieldVisible = !hideVisual;
            if (calamityPlayer.SpongeShieldDurability > 0)
            {
                Player player1 = player;
                player1.statDefense = player.statDefense += TheSponge.ShieldActiveDefense;
                player.endurance += TheSponge.ShieldActiveDamageReduction;
            }
            if (!player.GetModPlayer<ShtunPlayer>().hadNanomachinesLastFrame)
                calamityPlayer.adrenaline = 0.0f;
            calamityPlayer.draedonsHeart = true;
            player.GetModPlayer<ShtunPlayer>().hadNanomachinesLastFrame = true;
            calamityPlayer.AdrenalineDuration = this.NanomachinesDuration;
            calamityPlayer.contactDamageReduction += 0.15;
            calamityPlayer.absorber = true;
            calamityPlayer.dAmulet = true;
            player.longInvince = true;
            player.pStone = true;
            ++player.lifeRegen;
            if ((double)player.statLife <= (double)player.statLifeMax2 * 0.5)
                player.AddBuff(62, 5, true, false);
            player.noKnockback = true;
            if ((double)player.statLife > (double)player.statLifeMax2 * 0.25)
            {
                player.hasPaladinShield = true;
                if (((Entity)player).whoAmI != Main.myPlayer && player.miscCounter % 10 == 0)
                {
                    int player2 = Main.myPlayer;
                    if (Main.player[player2].team == player.team && player.team != 0)
                    {
                        double num1 = (double)((Entity)player).position.X - (double)((Entity)Main.player[player2]).position.X;
                        float num2 = ((Entity)player).position.Y - ((Entity)Main.player[player2]).position.Y;
                        if (Math.Sqrt(num1 * num1 + (double)num2 * (double)num2) < 800.0)
                            Main.player[player2].AddBuff(43, 20, true, false);
                    }
                }
            }
            player.statLifeMax2 += 125;
            calamityPlayer.purity = true;
            calamityPlayer.alwaysHoneyRegen = true;
            calamityPlayer.honeyDewHalveDebuffs = true;
            calamityPlayer.livingDewHalveDebuffs = true;
            if (!calamityPlayer.rOoze && !calamityPlayer.aAmpoule && !hideVisual)
                Lighting.AddLight(((Entity)player).Center, new Vector3(1.32f, 1.32f, 1.82f));
            calamityPlayer.DashID = AsgardianAegisDash.ID;
            player.dashType = 0;
            player.noKnockback = true;
            player.fireWalk = true;
            ++player.lifeRegen;
            player.buffImmune[24] = true;
            player.buffImmune[46] = true;
            player.buffImmune[44] = true;
            player.buffImmune[33] = true;
            player.buffImmune[36] = true;
            player.buffImmune[30] = true;
            player.buffImmune[20] = true;
            player.buffImmune[32] = true;
            player.buffImmune[31] = true;
            player.buffImmune[35] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;
            player.buffImmune[194] = true;
            player.buffImmune[156] = true;
            player.buffImmune[ModContent.BuffType<HolyFlames>()] = true;
            player.buffImmune[ModContent.BuffType<GodSlayerInferno>()] = true;
            if (!Collision.DrownCollision(((Entity)player).position, ((Entity)player).width, ((Entity)player).height, player.gravDir, false))
                return;
        }

        public override void AddRecipes()
        {
            this.CreateRecipe(1).AddIngredient<AsgardianAegis>(1).AddIngredient<RampartofDeities>(1).AddIngredient<Radiance>(1).AddIngredient<TheSponge>(1).AddIngredient<TheAbsorber>(1).AddIngredient<DraedonsHeart>(1).AddIngredient<ShadowspecBar>(20).AddTile<DraedonsForge>().Register();
        }
    }
}