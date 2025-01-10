using FargowiltasSouls;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent;
using FargowiltasSouls.Content.Projectiles.BossWeapons;

namespace ssm.Content.Projectiles.Weapons
{
    public class ShtuxiumPrismHoldout : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 60;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 0;
            Projectile.FargoSouls().CanSplit = false;
            Projectile.FargoSouls().TimeFreezeImmune = true;
            Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
        }

        public float scaletimer;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 rrp = player.RotatedRelativePoint(player.MountedCenter, true);

            UpdatePlayerVisuals(player, rrp);

            if (Projectile.owner == Main.myPlayer)
            {
                UpdateAim(rrp, player.HeldItem.shootSpeed);
            }

            scaletimer++;

            if (Projectile.localAI[0] == 0)
                Projectile.localAI[0] = Projectile.damage;

            bool stillInUse = player.channel && !player.noItems && !player.dead;

            if (stillInUse)
            {
                Vector2 beamVelocity = Vector2.Normalize(Projectile.velocity);
                if (beamVelocity.HasNaNs())
                {
                    beamVelocity = -Vector2.UnitY;
                }

                int uuid = Projectile.GetByUUID(Projectile.owner, Projectile.whoAmI);

                if (player.ownedProjectileCounts[ModContent.ProjectileType<ShtuxiumPrismDeathray>()] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), Projectile.Center, beamVelocity, ModContent.ProjectileType<ShtuxiumPrismDeathray>(), Projectile.damage, 0, Projectile.owner, 1, uuid);
                }

                Projectile.netUpdate = true;
            }
            else if (!stillInUse)
            {
                Projectile.Kill();
            }
        }

        private void UpdateAim(Vector2 source, float speed)
        {
            Vector2 aim = Vector2.Normalize(Main.MouseWorld - source);
            if (aim.HasNaNs())
            {
                aim = -Vector2.UnitY;
            }

  
            aim = Vector2.Normalize(Vector2.Lerp(Vector2.Normalize(Projectile.velocity), aim, 0.08f));
            aim *= speed;

            if (aim != Projectile.velocity)
            {
                Projectile.netUpdate = true;
            }
            Projectile.velocity = aim;
        }

        private void UpdatePlayerVisuals(Player player, Vector2 playerHandPos)
        {
            Projectile.Center = playerHandPos;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.spriteDirection = Projectile.direction;

            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;

            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
        }

        public override bool? CanDamage()
        {
            Projectile.maxPenetrate = 1;
            return null;
        }

        public override Color? GetAlpha(Color lightColor) => Color.White * Projectile.Opacity;

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int spriteSheetOffset = frameHeight * Projectile.frame;
            Vector2 sheetInsertPosition = (Projectile.Center + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();

            Color drawColor = Color.Teal;
            Main.EntitySpriteDraw(texture, sheetInsertPosition, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale, effects, 0f);
            return false;
        }

        //public override void PostDraw(Color lightColor)
        //{
        //    Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
        //    int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
        //    int y3 = num156 * Projectile.frame; //ypos of upper left corner of sprite to draw
        //    Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
        //    Vector2 origin2 = rectangle.Size() / 2f;
        //    Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
        //}
    }
}
