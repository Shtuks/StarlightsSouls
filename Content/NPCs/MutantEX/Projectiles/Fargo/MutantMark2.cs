using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantMark2 : ModProjectile
{
	public override string Texture => "Terraria/Images/Projectile_226";
	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = 900;
		base.Projectile.aiStyle = -1;
		base.CooldownSlot = 1;
		base.Projectile.hide = true;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 1;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(SoundID.Item84, (Vector2?)base.Projectile.Center);
		}
		if ((base.Projectile.ai[0] -= 1f) == 0f)
		{
			base.Projectile.netUpdate = true;
			base.Projectile.velocity = Vector2.Zero;
		}
		if ((base.Projectile.ai[1] -= 1f) == 0f)
		{
			base.Projectile.netUpdate = true;
			Player target = Main.player[Player.FindClosest(base.Projectile.position, base.Projectile.width, base.Projectile.height)];
			base.Projectile.velocity = base.Projectile.DirectionTo(target.Center) * 15f;
			SoundEngine.PlaySound(SoundID.Item84, (Vector2?)base.Projectile.Center);
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(20, Main.rand.Next(60, 300));
		target.AddBuff(ModContent.BuffType<InfestedBuff>(), Main.rand.Next(60, 300));
		target.AddBuff(ModContent.BuffType<IvyVenomBuff>(), Main.rand.Next(60, 300));
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
		int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
		int y3 = num156 * base.Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		return false;
	}
}
