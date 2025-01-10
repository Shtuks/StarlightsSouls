using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Content.Buffs;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles.Weapons
{
    public class ShtuxiumRifleDeathray : MutantSpecialDeathray
    {
        public ShtuxiumRifleDeathray() : base(20, 1.25f) { }

        public override void SetDefaults()
        {
            base.SetDefaults();

            CooldownSlot = -1;
            Projectile.hostile = false;
            Projectile.scale = 0.5f;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.hide = true;
            Projectile.penetrate = -1;

            Projectile.FargoSouls().TimeFreezeImmune = true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, Projectile.Center)) < TipOffset.Length())
                return true;

            return base.Colliding(projHitbox, targetHitbox);
        }

        Vector2 TipOffset => 18f * Projectile.scale * Projectile.velocity;

        public override void AI()
        {
            base.AI();
            Projectile.frameCounter += 60;

            Player player = Main.player[Projectile.owner];

            Projectile.velocity = Projectile.velocity.SafeNormalize(-Vector2.UnitY);

            if (player.active && !player.dead && !player.ghost)
            {
                Projectile.Center = player.Center + 50f * Projectile.velocity + TipOffset + Main.rand.NextVector2Circular(5, 5);
            }
            else
            {
                Projectile.Kill();
                return;
            }

            if (++Projectile.localAI[0] >= maxTime)
            {
                Projectile.Kill();
                return;
            }
            float amount = 0.5f;
            Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], 3000f, amount);
         
            Projectile.position -= Projectile.velocity;
            Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ChtuxlagorInferno>(), 180);
            if (Projectile.owner == Main.myPlayer)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<ShtuxiumBlast2>(), this.Projectile.damage, 0);
        }


        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

            ArmorShaderData shader = GameShaders.Armor.GetShaderFromItemId(ItemID.BrightTealDye);
            shader.Apply(Projectile, new Terraria.DataStructures.DrawData?());

            bool retval = base.PreDraw(ref lightColor);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

            return retval;
        }
    }
}
