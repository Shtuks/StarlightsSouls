using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.ModPlayers;


namespace ssm.Content.Projectiles.Shtuxibus
{
    public class MutantRitual : BaseArena
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        public override string Texture => "ssm/Content/Projectiles/Shtuxibus/ShtuxibusRitualProj";
        public int npcType => ModContent.NPCType<NPCs.Shtuxibus.Shtuxibus>();
        private const float realRotation = MathHelper.Pi / 140f;
        private bool MutantDead;
        public MutantRitual() : base(realRotation, 1800f, ModContent.NPCType<NPCs.Shtuxibus.Shtuxibus>()) { }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.projFrames[Projectile.type] = 1;
        }

        public override bool? CanDamage()
        {
            if (MutantDead)
                return false;
            return base.CanDamage();
        }

        protected override void Movement(NPC npc)
        {

            float targetRotation;
            if (npc.ai[0] == 19)
            {
                Projectile.velocity = Vector2.Zero;

                targetRotation = -realRotation / 2;
            }
            else
            {
                Projectile.velocity = npc.Center - Projectile.Center;
                if (npc.ai[0] == 36)
                    Projectile.velocity /= 20f; //much faster for slime rain
                else if (npc.ai[0] == 22 || npc.ai[0] == 23 || npc.ai[0] == 25)
                    Projectile.velocity /= 40f; //move faster for direct dash, predictive throw
                else
                    Projectile.velocity /= 60f;

                targetRotation = realRotation;
            }

            const float increment = realRotation / 40;
            if (rotationPerTick < targetRotation)
            {
                rotationPerTick += increment;
                if (rotationPerTick > targetRotation)
                    rotationPerTick = targetRotation;
            }
            else if (rotationPerTick > targetRotation)
            {
                rotationPerTick -= increment;
                if (rotationPerTick < targetRotation)
                    rotationPerTick = targetRotation;
            }

            MutantDead = npc.ai[0] <= -6;
        }

        public override void AI()
        {

            base.AI();
            NPC npc = ShtunUtils.NPCExists(Projectile.ai[1], npcType);
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 1)
                    Projectile.frame = 0;
            }

        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (!target.HasBuff(ModContent.Find<ModBuff>(fargosouls.Name, "TimeFrozenBuff").Type))
                target.AddBuff(ModContent.Find<ModBuff>(fargosouls.Name, "TimeFrozenBuff").Type, 300);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (ssm.amiactive)
            {
                return Color.White * Projectile.Opacity * (targetPlayer == Main.myPlayer && !MutantDead ? 1f : 0.15f);
            }
            else
            {
                return Color.White * Projectile.Opacity * 0f;
            }
        }
    }
}