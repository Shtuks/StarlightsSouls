using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using System.Collections.Generic;
using Terraria.Localization;
using ThoriumMod.Items.HealerItems;
using ssm.Core;

namespace ssm.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class GuardianAngelsSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 750000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Thorium(player);
        }

        private void Thorium(Player player)
        {
            //general
            //ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            //thoriumPlayer.radiantBoost += 0.3f;
            //thoriumPlayer.radiantSpeed -= 0.2f;
            //thoriumPlayer.healingSpeed += 0.2f;
            //thoriumPlayer.radiantCrit += 15;

            //support stash
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SupportSash").UpdateAccessory(player, true);

            //saving grace
            //thoriumPlayer.crossHeal = true;
            //thoriumPlayer.healBloom = true;

            //soul guard
            //thoriumPlayer.graveGoods = true;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && player2 != player && Vector2.Distance(player2.Center, player.Center) < 400f)
                {
                    //player2.AddBuff(thorium.BuffType("AegisAura"), 30, false);
                }
            }

            //archdemon's curse
            //thoriumPlayer.darkAura = true;

            //archangels heart
            //thoriumPlayer.healBonus += 5;

            //medical bag
            //thoriumPlayer.medicalAcc = true;

            //head mirror arrow 
            /*if (SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.HeadMirror))
            {
                float num = 0f;
                int num2 = player.whoAmI;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && Main.player[i] != player && !Main.player[i].dead && (Main.player[i].statLifeMax2 - Main.player[i].statLife) > num)
                    {
                        num = (Main.player[i].statLifeMax2 - Main.player[i].statLife);
                        num2 = i;
                    }
                }
                if (player.ownedProjectileCounts[thorium.ProjectileType("HealerSymbol")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("HealerSymbol"), 0, 0f, player.whoAmI, 0f, 0f);
                }
                for (int j = 0; j < 1000; j++)
                {
                    Projectile projectile = Main.projectile[j];
                    if (projectile.active && projectile.owner == player.whoAmI && projectile.type == thorium.ProjectileType("HealerSymbol"))
                    {
                        projectile.timeLeft = 2;
                        projectile.ai[1] = num2;
                    }
                }
            }*/

        }
    }
}
