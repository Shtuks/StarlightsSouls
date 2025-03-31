using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Buffs.Masomode;
using ssm.Content.Projectiles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;

namespace ssm.Content.Items.Accessories
{
    public class CyclonicFin : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 11;
            Item.value = Item.sellPrice(0, 17);
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(Main.DiscoR, 51, 255 - (int)(Main.DiscoR * 0.4));
                }
            }
        }

        public class CuteFishEXEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DeviEnergyHeader>();
            public override int ToggleItemType => ModContent.ItemType<CyclonicFin>();
        }

        public class SpectralFishEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DeviEnergyHeader>();
            public override int ToggleItemType => ModContent.ItemType<CyclonicFin>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.BuffType<OceanicMaulBuff>()] = true;
            player.buffImmune[ModContent.BuffType<CurseoftheMoonBuff>()] = true;

            if (player.AddEffect<SpectralFishEffect>(Item))
            {
                player.GetModPlayer<ShtunPlayer>().CyclonicFin = true;

                if (player.GetModPlayer<ShtunPlayer>().CyclonicFinCD > 0)
                    player.GetModPlayer<ShtunPlayer>().CyclonicFinCD--;
            }

            if (player.AddEffect<CuteFishEXEffect>(Item))
            {
                if (player.mount.Active && player.mount.Type == MountID.CuteFishron)
                {
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<CuteFishronRitual>()] < 1 && player.whoAmI == Main.myPlayer)
                        Projectile.NewProjectile(Item.GetSource_FromThis(), player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<CuteFishronRitual>(), 0, 0f, Main.myPlayer);

                    player.MountFishronSpecialCounter = 300;
                    player.GetDamage<GenericDamageClass>() += 0.15f;
                    player.GetCritChance<GenericDamageClass>() += 30f;
                    player.statDefense += 30;
                    player.lifeRegen += 3;
                    player.lifeRegenCount += 3;
                    player.lifeRegenTime += 3;

                    if (player.controlLeft == player.controlRight)
                    {
                        if (player.velocity.X != 0)
                            player.velocity.X -= player.mount.Acceleration * Math.Sign(player.velocity.X);
                        if (player.velocity.X != 0)
                            player.velocity.X -= player.mount.Acceleration * Math.Sign(player.velocity.X);
                    }

                    else if (player.controlLeft)
                    {
                        player.velocity.X -= player.mount.Acceleration * 4f;
                        if (player.velocity.X < -16f)
                            player.velocity.X = -16f;
                        if (!player.controlUseItem)
                            player.direction = -1;
                    }

                    else if (player.controlRight)
                    {
                        player.velocity.X += player.mount.Acceleration * 4f;
                        if (player.velocity.X > 16f)
                            player.velocity.X = 16f;
                        if (!player.controlUseItem)
                            player.direction = 1;
                    }

                    if (player.controlUp == player.controlDown)
                    {
                        if (player.velocity.Y != 0)
                            player.velocity.Y -= player.mount.Acceleration * Math.Sign(player.velocity.Y);
                        if (player.velocity.Y != 0)
                            player.velocity.Y -= player.mount.Acceleration * Math.Sign(player.velocity.Y);
                    }

                    else if (player.controlUp)
                    {
                        player.velocity.Y -= player.mount.Acceleration * 4f;
                        if (player.velocity.Y < -16f)
                            player.velocity.Y = -16f;
                    }

                    else if (player.controlDown)
                    {
                        player.velocity.Y += player.mount.Acceleration * 4f;
                        if (player.velocity.Y > 16f)
                            player.velocity.Y = 16f;
                    }
                }
            }
        }
    }
}
