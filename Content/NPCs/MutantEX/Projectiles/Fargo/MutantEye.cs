using FargowiltasSouls;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Render.Primitives;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantEye : ModProjectile, IPixelPrimitiveDrawer
{
    public override string Texture => "Terraria/Images/Projectile_452";

    protected bool DieOutsideArena;

	private int ritualID = -1;

	public PrimDrawer TrailDrawer { get; private set; }

	public virtual int TrailAdditive => 0;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 15;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.aiStyle = -1;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 0;
		base.CooldownSlot = 1;
		this.DieOutsideArena = base.Projectile.type == ModContent.ProjectileType<MutantEye>();
	}

	public override void AI()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 15;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + 1.570796f;
		if (base.Projectile.localAI[0] < (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type])
		{
			base.Projectile.localAI[0] += 0.1f;
		}
		else
		{
			base.Projectile.localAI[0] = ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
		}
		base.Projectile.localAI[1] += 0.25f;
		if (!this.DieOutsideArena)
		{
			return;
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

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (!target.GetModPlayer<FargoSoulsPlayer>().BetsyDashing)
		{
			target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
			target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
			target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
			target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360);
			base.Projectile.timeLeft = 0;
		}
	}

	public override void Kill(int timeleft)
	{
		SoundEngine.PlaySound(SoundID.Zombie103, (Vector2?)base.Projectile.Center);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 144);
		base.Projectile.position.X -= base.Projectile.width / 2;
		base.Projectile.position.Y -= base.Projectile.height / 2;
		for (int index = 0; index < 2; index++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
		}
		for (int index1 = 0; index1 < 5; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 20, 255f, 0f, 0, default(Color), 2.5f);
			Main.dust[index2].noGravity = true;
			Main.dust[index2].velocity *= 3f;
			int index3 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 20, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[index3].velocity *= 2f;
			Main.dust[index3].noGravity = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White * base.Projectile.Opacity;
	}

	public float WidthFunction(float completionRatio)
	{
		return MathHelper.SmoothStep(base.Projectile.scale * (float)base.Projectile.width * 1.7f, 3.5f, completionRatio);
	}

	public Color ColorFunction(float completionRatio)
	{
		return Color.Lerp(Color.Cyan, Color.Transparent, completionRatio) * 0.7f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D glow = FargosTextureRegistry.BlobBloomTexture.Value;
		Color color = Color.Cyan;
		color.A = 0;
		Main.EntitySpriteDraw(glow, base.Projectile.Center - base.Projectile.velocity * 0.5f - Main.screenPosition, (Rectangle?)null, color * 0.6f, base.Projectile.rotation, glow.Size() * 0.5f, 0.25f, SpriteEffects.None, 0);
		Texture2D texture = TextureAssets.Projectile[base.Projectile.type].Value;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, Color.White, base.Projectile.rotation, texture.Size() * 0.5f, base.Projectile.scale, SpriteEffects.None, 0);
		return false;
	}

	public void DrawPixelPrimitives(SpriteBatch spriteBatch)
	{
	}

	public override void PostDraw(Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
		int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
		int y3 = num156 * base.Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, Color.White, base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None, 0);
	}
}
