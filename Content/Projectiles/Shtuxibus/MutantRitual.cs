using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Content.Projectiles;
using CalamityMod.CalPlayer;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;


namespace ssm.Content.Projectiles.Shtuxibus
{
    public class MutantRitual : BaseArena
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
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
            //base.OnHitPlayer(target, damage, crit);
            // target.AddBuff(BuffID.Stoned, 300);
            if (ssm.amiactive)
            {
                //if (!target.HasBuff(ModContent.Find<ModBuff>(fargosouls.Name, "TimeFrozenBuff").Type))
                //target.AddBuff(ModContent.Find<ModBuff>(fargosouls.Name, "TimeFrozenBuff").Type, 300);
            }


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

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * Projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = Projectile.GetAlpha(lightColor);
            Texture2D glow = ssm.Instance.Assets.Request<Texture2D>("Content/Projectiles/Shtuxibus/MutantSphereGlow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            int rect1 = glow.Height;
            int rect2 = 0;
            Rectangle glowrectangle = new Rectangle(0, rect2, glow.Width, rect1);
            Vector2 gloworigin2 = glowrectangle.Size() / 2f;
            Color glowcolor = Color.Lerp(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0), Color.Transparent, 0.8f);

            for (int x = 0; x < 64; x++)
            {
                Vector2 drawOffset = new Vector2(threshold * Projectile.scale / 2f, 0f).RotatedBy(Projectile.ai[0]);
                drawOffset = drawOffset.RotatedBy(2f * MathHelper.Pi / 64f * x);
                const int max = 4;
                for (int i = 0; i < max; i++)
                {
                    Color color27 = color26;
                    color27 *= (float)(max - i) / max;
                    Vector2 value4 = Projectile.oldPos[i] + Projectile.Hitbox.Size() / 2 + drawOffset.RotatedBy(rotationPerTick * -i);
                    float num165 = Projectile.rotation;
                    Main.EntitySpriteDraw(texture2D13, value4 - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, Projectile.scale, SpriteEffects.None, 0);
                    //   Main.EntitySpriteDraw(glow, value4 - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(glowrectangle), glowcolor * ((float)(max - i) / max),
                    // Projectile.rotation, gloworigin2, Projectile.scale * 1.4f, SpriteEffects.None, 0);
                }
                Main.EntitySpriteDraw(texture2D13, Projectile.Center + drawOffset - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
                //   Main.EntitySpriteDraw(glow, Projectile.Center + drawOffset - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(glowrectangle), glowcolor,
                //           Projectile.rotation, gloworigin2, Projectile.scale * 1.3f, SpriteEffects.None, 0);
            }
            return false;
        }
    }
}