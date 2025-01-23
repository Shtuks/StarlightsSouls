using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.StarlightCat
{
    public class StarlightCatRotatingDeathray : ModProjectile, IPixelatedPrimitiveRenderer
    {
        public override string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/PhantasmalDeathrayML";
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.hostile = true; 
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 6000;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            float beamLength = 6000f;

            Vector2 direction = Vector2.Normalize(Projectile.velocity);

            Vector2 endPosition = Projectile.Center + direction * beamLength;

            float rotationSpeed = MathHelper.ToRadians(2f);
            Projectile.velocity = Projectile.velocity.RotatedBy(rotationSpeed);
        }

        //public override bool PreDraw(ref Color lightColor)
        //{
        //    Vector2 direction = Vector2.Normalize(Projectile.velocity);
        //    float beamLength = 600f;
        //    Vector2 endPosition = Projectile.Center + direction * beamLength;

        //    Terraria.GameContent.Drawing.l Lighting.AddLight(endPosition, 1f, 0.5f, 0.2f);
        //    return false;
        //}

        public override bool PreDraw(ref Color lightColor) => false;

        public float WidthFunction(float trailInterpolant) => Projectile.width * Projectile.scale * 1.3f;

        public static Color ColorFunction(float trailInterpolant)
        {
            Color color = Color.Red;
            color.A = 100;
            return color;
        }

        public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
        {
            if (Projectile.hide)
                return;

            ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.GenericDeathray");

            Vector2 laserEnd = Projectile.Center + Projectile.velocity.SafeNormalize(Vector2.UnitY) * 6000 * 1.1f;
            Vector2 initialDrawPoint = Projectile.Center - Projectile.velocity * 150f;

            Vector2[] baseDrawPoints = new Vector2[8];
            for (int i = 0; i < baseDrawPoints.Length; i++)
                baseDrawPoints[i] = Vector2.Lerp(initialDrawPoint, laserEnd, i / (float)(baseDrawPoints.Length - 1f));

            FargoSoulsUtil.SetTexture1(FargosTextureRegistry.MutantStreak.Value);

            shader.TrySetParameter("mainColor", new Color(255, 10, 10, 100));
            shader.TrySetParameter("stretchAmount", 3);
            shader.TrySetParameter("scrollSpeed", 2f);
            shader.TrySetParameter("uColorFadeScaler", 1f);
            shader.TrySetParameter("useFadeIn", true);

            PrimitiveRenderer.RenderTrail(baseDrawPoints, new(WidthFunction, ColorFunction, Pixelate: true, Shader: shader), 20);
        }
    }
}