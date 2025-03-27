using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantFragment : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/CelestialFragment";

    private int ritualID = -1;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.aiStyle = -1;
		base.Projectile.scale = 1.25f;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 600;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		base.Projectile.velocity *= 0.985f;
		base.Projectile.rotation += base.Projectile.velocity.X / 30f;
		base.Projectile.frame = (int)base.Projectile.ai[0];
		if (Main.rand.NextBool(15))
		{
			int type = (int)base.Projectile.ai[0] switch
			{
				0 => 242, 
				1 => 127, 
				2 => 229, 
				_ => 135, 
			};
			Dust obj = Main.dust[Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, type)];
			obj.velocity *= 4f;
			obj.fadeIn = 1f;
			obj.scale = 1f + Main.rand.NextFloat() + (float)Main.rand.Next(4) * 0.3f;
			obj.noGravity = true;
		}
		if (this.ritualID == -1)
		{
			this.ritualID = -2;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<MutantRitual>())
				{
					this.ritualID = i;
					break;
				}
			}
		}
		Projectile ritual = FargoSoulsUtil.ProjectileExists(this.ritualID, ModContent.ProjectileType<MutantRitual>());
		if (ritual != null && base.Projectile.Distance(ritual.Center) > 1200f)
		{
			base.Projectile.timeLeft = 0;
		}
	}

	public override void Kill(int timeLeft)
	{
		int type = (int)base.Projectile.ai[0] switch
		{
			0 => 242, 
			1 => 127, 
			2 => 229, 
			_ => 135, 
		};
		for (int i = 0; i < 20; i++)
		{
			Dust obj = Main.dust[Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, type)];
			obj.velocity *= 6f;
			obj.fadeIn = 1f;
			obj.scale = 1f + Main.rand.NextFloat() + (float)Main.rand.Next(4) * 0.3f;
			obj.noGravity = true;
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(ModContent.BuffType<HexedBuff>(), 120);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		switch ((int)base.Projectile.ai[0])
		{
		case 0:
			target.AddBuff(ModContent.BuffType<ReverseManaFlowBuff>(), 180);
			break;
		case 1:
			target.AddBuff(ModContent.BuffType<AtrophiedBuff>(), 180);
			break;
		case 2:
			target.AddBuff(ModContent.BuffType<JammedBuff>(), 180);
			break;
		default:
			target.AddBuff(ModContent.BuffType<AntisocialBuff>(), 180);
			break;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
        Vector2 drawPosition = Projectile.Center - Main.screenPosition;
        int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
        int y3 = num156 * Projectile.frame; 
        Rectangle rectangle = new(0, y3, texture.Width, num156);
        Vector2 origin2 = rectangle.Size() / 2f;
        Color color = Projectile.GetAlpha(lightColor);
        Main.EntitySpriteDraw(texture, drawPosition, rectangle, color, Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
}
