using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls;
using ssm.Content.Items.Weapons.Shtuxium;
using ssm.Content.Buffs;

namespace ssm.Content.Projectiles.Weapons
{
    public class ShtuxiumPrismDeathray : MutantSpecialDeathray
    {
        public ShtuxiumPrismDeathray() : base(6000) { }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            CooldownSlot = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.FargoSouls().CanSplit = false;
            Projectile.FargoSouls().TimeFreezeImmune = true;
            Projectile.FargoSouls().DeletionImmuneRank = 2;

            Projectile.hide = true;
            Projectile.penetrate = -1;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindProjectiles.Add(index);
        }

        public Vector2 TipOffset => 9f * Projectile.velocity * Projectile.scale;

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, Projectile.Center)) < TipOffset.Length() * 2)
                return true;

            return base.Colliding(projHitbox, targetHitbox);
        }

        public override void AI()
        {
            base.AI();

            Player player = Main.player[Projectile.owner];

            Vector2? vector78 = null;
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }

            Projectile spear = FargoSoulsUtil.ProjectileExists(FargoSoulsUtil.GetProjectileByIdentity(Projectile.owner, (int)Projectile.ai[1], ModContent.ProjectileType<ShtuxiumPrismHoldout>()));
            if (spear != null)
            {
                Projectile.timeLeft = 20000;
                float itemrotate = player.direction < 0 ? MathHelper.Pi : 0;
                if (Math.Abs(player.itemRotation) > Math.PI / 2)
                    itemrotate = itemrotate == 0 ? MathHelper.Pi : 0;
                Projectile.velocity = (player.itemRotation + itemrotate).ToRotationVector2();
                Projectile.Center = spear.Center + Main.rand.NextVector2Circular(5, 5);

                Projectile.position += Projectile.velocity * 164 * spear.scale * 0.45f;

                Projectile.damage = player.GetWeaponDamage(player.HeldItem);
                Projectile.CritChance = player.GetWeaponCrit(player.HeldItem);
                Projectile.knockBack = player.GetWeaponKnockback(player.HeldItem, player.HeldItem.knockBack);
            }
            else if (++Projectile.localAI[0] > 5)
            {
                Projectile.Kill();
                return;
            }

            if (Projectile.localAI[0] == 0f)
            {
                if (!Main.dedServ)
                    SoundEngine.PlaySound(new SoundStyle("FargowiltasSouls/Assets/Sounds/Zombie_104"), Projectile.Center);
            }
            float num801 = 5f;

            if (Projectile.localAI[0] == maxTime / 2)
            {
                if (Projectile.owner == Main.myPlayer && !(player.controlUseTile && player.altFunctionUse == 2 && player.HeldItem.type == ModContent.ItemType<ShtuxiumPrism>()))
                    Projectile.localAI[0] += 1f;
                else
                    Projectile.localAI[0] -= 1f;
            }
            else
            {
                Projectile.localAI[0] += 1f;
            }

            if (Projectile.localAI[0] >= maxTime)
            {
                Projectile.Kill();
                return;
            }
            Projectile.scale = (float)Math.Sin(Projectile.localAI[0] * 3.14159274f / maxTime) * 1.5f * num801;
            if (Projectile.scale > num801)
            {
                Projectile.scale = num801;
            }

            Projectile.scale *= spear.scale / 1.3f;
            Projectile.position += TipOffset;

            float num805 = 3f;
            float num806 = Projectile.width;
            Vector2 samplingPoint = Projectile.Center;
            if (vector78.HasValue)
            {
                samplingPoint = vector78.Value;
            }
            float[] array3 = new float[(int)num805];
            for (int i = 0; i < array3.Length; i++)
                array3[i] = 3000f;
            float num807 = 0f;
            int num3;
            for (int num808 = 0; num808 < array3.Length; num808 = num3 + 1)
            {
                num807 += array3[num808];
                num3 = num808;
            }
            num807 /= num805;
            float amount = 0.5f;
            Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num807, amount);
            Projectile.position -= Projectile.velocity;
            Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;

        }

        public override void PostAI()
        {
            base.PostAI();

            Projectile.hide = true;
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ChtuxlagorInferno>(), 180);
        }

        public float WidthFunction(float trailInterpolant)
        {
            float baseWidth = Projectile.scale * Projectile.width;
            return baseWidth;
        }

        public static Color ColorFunction(float trailInterpolant) =>
            Color.Lerp(
                ShtunUtils.Stalin ? new Color(255, 0, 0, 100) : new(30, 190, 160, 100),
                ShtunUtils.Stalin ? new Color(255, 191, 51, 100) : new(50, 255, 180, 100),
                trailInterpolant);
    }
}
