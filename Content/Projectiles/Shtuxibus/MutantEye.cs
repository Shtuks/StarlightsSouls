using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls;
using ssm.Render.Primitives;
using FargowiltasSouls.Content.Projectiles;

namespace ssm.Content.Projectiles.Shtuxibus
{
    public class MutantEye : ModProjectile, IPixelPrimitiveDrawer
    {
        public PrimDrawer TrailDrawer { get; private set; } = null;
        public virtual int TrailAdditive => 0;
        protected bool DieOutsideArena;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 700;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
            CooldownSlot = 1;
            DieOutsideArena = Projectile.type == ModContent.ProjectileType<MutantEye>();
        }

        private int ritualID = -1;
        public override void AI()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.570796f;

            if (Projectile.localAI[0] < ProjectileID.Sets.TrailCacheLength[Projectile.type])
            {
                Projectile.localAI[0] += 0.1f;
            }
            else
                Projectile.localAI[0] = ProjectileID.Sets.TrailCacheLength[Projectile.type];

            Projectile.localAI[1] += 0.25f;

            if (DieOutsideArena)
            {
                if (ritualID == -1) //identify the ritual CLIENT SIDE
                {
                    ritualID = -2; //if cant find it, give up and dont try every tick

                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<MutantRitual>())
                        {
                            ritualID = i;
                            break;
                        }
                    }
                }

                Projectile ritual = FargoSoulsUtil.ProjectileExists(ritualID, ModContent.ProjectileType<MutantRitual>());
                if (ritual != null && Projectile.Distance(ritual.Center) > 1800f) //despawn faster
                    Projectile.timeLeft = 0;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            
            Projectile.timeLeft = 0;
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Zombie103, Projectile.Center);
            Projectile.position = Projectile.Center;
            Projectile.width = Projectile.height = 144;
            Projectile.position.X -= (float)(Projectile.width / 2);
            Projectile.position.Y -= (float)(Projectile.height / 2);
            for (int index = 0; index < 2; ++index)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
            for (int index1 = 0; index1 < 5; ++index1)
            {
                int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
                Main.dust[index2].noGravity = true;
                Dust dust1 = Main.dust[index2];
                dust1.velocity = dust1.velocity * 3f;
                int index3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229, 0.0f, 0.0f, 100, new Color(), 1.5f);
                Dust dust2 = Main.dust[index3];
                dust2.velocity = dust2.velocity * 2f;
                Main.dust[index3].noGravity = true;
            }
        }

       public override Color? GetAlpha(Color lightColor)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
		}

        public float WidthFunction(float completionRatio)
        {
            float baseWidth = Projectile.scale * Projectile.width * 1.7f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }

        public Color ColorFunction(float completionRatio)
        {
            return Color.Lerp(Color.Cyan, Color.Transparent, completionRatio) * 0.7f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D glow = ShtunRegs.BlobBloomTexture.Value;
            Color color = Color.Cyan;
            color.A = 0;
            Main.EntitySpriteDraw(glow, Projectile.Center - (Projectile.velocity * 0.5f) - Main.screenPosition, null, color * 0.6f, Projectile.rotation, glow.Size() * 0.5f, 0.25f, SpriteEffects.None, 0);

            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }

        public void DrawPixelPrimitives(SpriteBatch spriteBatch)
        {
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * Projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Color.White, Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);

        }
    }
}