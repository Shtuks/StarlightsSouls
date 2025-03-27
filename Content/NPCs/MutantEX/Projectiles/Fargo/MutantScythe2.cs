using System.Collections.Generic;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles.Souls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantScythe2 : MutantScythe1
{
	public override string Texture => "FargowiltasSouls/Content/Bosses/AbomBoss/AbomSickle";
    public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.hide = false;
	}

	public override void PostAI()
	{
		if (base.Projectile.timeLeft == 180)
		{
			for (int i = 0; i < 20; i++)
			{
				int d = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 87, 0f, 0f, 0, default(Color), 2.5f);
				Main.dust[d].noGravity = true;
				Main.dust[d].velocity *= 6f;
			}
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<IronParry>(), 0, 0f, Main.myPlayer, 0f, 0f);
			}
		}
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
	}
}
