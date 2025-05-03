using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using Luminance.Common.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles
{
    [AutoloadBossHead]
    public class MutantIllusion : ModNPC
    {
        public override string Texture => "ssm/Content/NPCs/MutantEX/MutantEX";
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[base.NPC.type] = 5;
            NPCID.Sets.CantTakeLunchMoney[base.Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 140;
            NPC.height = 124;
            base.NPC.damage = 360;
            base.NPC.defense = 400;
            base.NPC.lifeMax = 700000000;
            base.NPC.dontTakeDamage = true;
            base.NPC.HitSound = SoundID.NPCHit57;
            base.NPC.noGravity = true;
            base.NPC.noTileCollide = true;
            base.NPC.knockBackResist = 0f;
            base.NPC.lavaImmune = true;
            base.NPC.aiStyle = -1;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            base.NPC.damage = (int)((float)base.NPC.damage * 0.5f);
            base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.5f * balance);
        }

        public override bool CanHitPlayer(Player target, ref int CooldownSlot)
        {
            return false;
        }

        public override void AI()
        {
            NPC mutant = FargoSoulsUtil.NPCExists(base.NPC.ai[0], ModContent.NPCType<MutantEX>());
            if (mutant == null || mutant.ai[0] < 18f || mutant.ai[0] > 19f || mutant.life <= 1)
            {
                base.NPC.life = 0;
                base.NPC.HitEffect();
                base.NPC.SimpleStrikeNPC(int.MaxValue, 0, crit: false, 0f, null, damageVariation: false, 0f, noPlayerInteraction: true);
                base.NPC.active = false;
                for (int i = 0; i < 40; i++)
                {
                    int d = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5);
                    Main.dust[d].velocity *= 2.5f;
                    Main.dust[d].scale += 0.5f;
                }
                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 229, 0f, 0f, 0, default(Color), 2f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].noLight = true;
                    Main.dust[d].velocity *= 9f;
                }
                return;
            }
            base.NPC.target = mutant.target;
            base.NPC.damage = mutant.damage;
            base.NPC.defDamage = mutant.damage;
            base.NPC.frame.Y = mutant.frame.Y;
            if (base.NPC.HasValidTarget)
            {
                Vector2 target = Main.player[mutant.target].Center;
                Vector2 distance = target - mutant.Center;
                base.NPC.Center = target;
                base.NPC.position.X += distance.X * base.NPC.ai[1];
                base.NPC.position.Y += distance.Y * base.NPC.ai[2];
                base.NPC.direction = (base.NPC.spriteDirection = ((base.NPC.position.X < Main.player[base.NPC.target].position.X) ? 1 : (-1)));
            }
            else
            {
                base.NPC.Center = mutant.Center;
            }
            if ((base.NPC.ai[3] -= 1f) == 0f)
            {
                int ai0 = ((!(base.NPC.ai[1] < 0f)) ? ((base.NPC.ai[2] < 0f) ? 1 : 2) : 0);
                if (FargoSoulsUtil.HostCheck)
                {
                    Projectile.NewProjectile(mutant.GetSource_FromThis(), base.NPC.Center, Vector2.UnitY * -5f, ModContent.ProjectileType<MutantPillar>(), FargoSoulsUtil.ScaledProjectileDamage(mutant.damage, 1.3333334f), 0f, Main.myPlayer, ai0, base.NPC.whoAmI);
                }
            }
            if (Main.getGoodWorld && (base.NPC.localAI[0] += 1f) > 6f)
            {
                base.NPC.localAI[0] = 0f;
                base.NPC.AI();
            }
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
        }
    }
}