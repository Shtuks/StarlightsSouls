using Terraria;
using Terraria.ModLoader;

namespace ssm.gunrightsmod.Buffs
{
    public class CeramicShell : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
        }
    }

    public class CeramicShellGlobalPlayer : ModPlayer
    {
        public bool hasCeramicShell;

        public override void ResetEffects()
        {
            hasCeramicShell = false;
        }

        public override void PostUpdateBuffs()
        {
            if (Player.HasBuff(ModContent.BuffType<CeramicShell>()))
            {
                hasCeramicShell = true;
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (hasCeramicShell)
            {
                modifiers.FinalDamage *= 0.2f;
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            if (hasCeramicShell)
            {
                modifiers.FinalDamage *= 0.2f;
            }
        }
    }
}
