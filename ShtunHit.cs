using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using ssm.Content.NPCs.MutantEX;

namespace ssm
{
    public partial class ShtunHit : ModPlayer
    {
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            double damageMult = 1D;
            modifiers.SourceDamage *= (float)damageMult;

            if (Player.Shtun().ChtuxlagorHeart)
            {
                modifiers.SetMaxDamage(1000);
            }
            if (Player.HasBuff<MutantDesperationBuff>() && NPC.AnyNPCs(ModContent.NPCType<MutantEX>()))
            {
                Player.AddBuff(ModContent.BuffType<TimeFrozenBuff>(),10);
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (Player.Shtun().ChtuxlagorHits > 10)
            {
                Player.Shtun().ERASE(Player);
                Player.Shtun().ChtuxlagorHits = 0;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.Shtun().equippedPhantasmalEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "MutantFangBuff").Type, 1000, false);
            if (Player.Shtun().equippedAbominableEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "AbomFangBuff").Type, 1000, false);
            if (Player.Shtun().ChtuxlagorHeart)
                target.AddBuff(ModContent.Find<ModBuff>("ssm", "ChtuxlagorInferno").Type, 1000, false);
        }
    }

    public partial class ShtunNPCHit : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (npc.type == ModContent.NPCType<MutantBoss>())
            {
                modifiers.SetMaxDamage(50000);
            }
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (npc.type == ModContent.NPCType<MutantBoss>())
            {
                modifiers.SetMaxDamage(50000);
            }
        }
        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            if (npc.type == ModContent.NPCType<MutantBoss>())
            {
                hit.InstantKill = false;
            }
        }
    }
}
