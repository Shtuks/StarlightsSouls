using FargowiltasSouls.Content.Buffs.Boss;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantSpazmarang : MutantRetirang
{
    public override string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/Spazmarang";
    public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 12;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(39, 120);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
	}
}
