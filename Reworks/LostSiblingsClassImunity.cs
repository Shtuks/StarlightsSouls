using FargowiltasSouls.Core.Systems;
using SacredTools.NPCs.Boss.Lunarians;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class LostSiblingsClassImunity : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModCompatibility.Thorium.Loaded;
        }
        public override bool InstancePerEntity => true;

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (WorldSavingSystem.EternityMode) {
                if (npc.type == ModContent.NPCType<Nuba>())
                {
                    if (hit.DamageType != DamageClass.Magic)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Solarius>())
                {
                    if (hit.DamageType != DamageClass.Melee)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Novaniel>())
                {
                    if (hit.DamageType != DamageClass.Throwing)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Voxa>())
                {
                    if (hit.DamageType != DamageClass.Ranged)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Dustite>())
                {
                    if (hit.DamageType != DamageClass.Summon)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    public class LostSiblingsClassImunityThorium : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (WorldSavingSystem.EternityMode)
            {
                if (npc.type == ModContent.NPCType<Nuba>())
                {
                    if (hit.DamageType != DamageClass.Magic || hit.DamageType != HealerDamage.Instance)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Solarius>())
                {
                    if (hit.DamageType != DamageClass.Melee)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Novaniel>())
                {
                    if (hit.DamageType != DamageClass.Throwing)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Voxa>())
                {
                    if (hit.DamageType != DamageClass.Ranged || hit.DamageType != BardDamage.Instance)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
                if (npc.type == ModContent.NPCType<Dustite>())
                {
                    if (hit.DamageType != DamageClass.Summon)
                    {
                        hit.Damage = hit.Damage / 2;
                    }
                }
            }
        }
    }
}
