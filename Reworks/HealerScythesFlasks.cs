using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class HealerScythesFlasks : GlobalNPC
    {
        public override void OnHitByProjectile(NPC target, Projectile proj, NPC.HitInfo hit, int damageDone)
        {
            if (proj.DamageType == ModContent.GetInstance<HealerDamage>())
            {
                switch (proj.owner.ToPlayer().meleeEnchant)
                {
                    case 1:
                        target.AddBuff(BuffID.Venom, 180);
                        break;
                    case 2:
                        target.AddBuff(BuffID.CursedInferno, 180);
                        break;
                    case 3:
                        target.AddBuff(BuffID.OnFire, 180);
                        break;
                    case 4:
                        target.AddBuff(BuffID.Midas, 180);
                        break;
                    case 5:
                        target.AddBuff(BuffID.Ichor, 180);
                        break;
                    case 6:
                        target.AddBuff(BuffID.Confused, 180);
                        break;
                    case 8:
                        target.AddBuff(BuffID.Poisoned, 180);
                        break;
                }
            }
        }

        public override void OnHitByItem(NPC target, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (item.DamageType == ModContent.GetInstance<HealerDamage>())
            {
                switch (player.meleeEnchant)
                {
                    case 1:
                        target.AddBuff(BuffID.Venom, 180);
                        break;
                    case 2:
                        target.AddBuff(BuffID.CursedInferno, 180);
                        break;
                    case 3:
                        target.AddBuff(BuffID.OnFire, 180);
                        break;
                    case 4:
                        target.AddBuff(BuffID.Midas, 180);
                        break;
                    case 5:
                        target.AddBuff(BuffID.Ichor, 180);
                        break;
                    case 6:
                        target.AddBuff(BuffID.Confused, 180);
                        break;
                    case 8:
                        target.AddBuff(BuffID.Poisoned, 180);
                        break;
                }
            }
        }
    }
}
