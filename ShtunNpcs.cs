using System;
using Terraria;
using Terraria.ModLoader;

namespace ssm
{
    public partial class ShtunNpcs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool isWaterEnemy;
        public int chtuxlagorInferno;
        public static int ECH = -1;
        public static int DukeEX = -1;
        public static int boss = -1;
        public static int mutantEX = -1;
        public bool CelestialPower;

        public override void ModifyShop(NPCShop shop)
        {
            void AddItem(int ItemID, int customPrice = -1, Condition condition = null, Condition[] conditions = null)
            {
                if (condition != (Condition)null)
                {
                    conditions = (Condition[])(object)new Condition[1] { condition };
                }
                if (conditions != null)
                {
                    if (customPrice != -1)
                    {
                        shop.Add(new Item(ItemID, 1, 0)
                        {
                            shopCustomPrice = customPrice
                        }, conditions);
                    }
                    else
                    {
                        shop.Add(ItemID, conditions);
                    }
                }
                else if (customPrice != -1)
                {
                    shop.Add(new Item(ItemID, 1, 0)
                    {
                        shopCustomPrice = customPrice
                    }, Array.Empty<Condition>());
                }
                else
                {
                    shop.Add(ItemID, Array.Empty<Condition>());
                }
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (chtuxlagorInferno > 0)
                ApplyDPSDebuff(npc.lifeMax / 10, npc.lifeMax / 100, ref npc.lifeRegen, ref damage);
        }

        public override void PostAI(NPC npc)
        {
            if (chtuxlagorInferno > 0)
            {
                chtuxlagorInferno--;
            }
        }
        
        public void ApplyDPSDebuff(int lifeRegenValue, int damageValue, ref int lifeRegen, ref int damage)
        {
            if (lifeRegen > 0)
            {
                lifeRegen = 0;
            }

            lifeRegen -= lifeRegenValue;
            if (damage < damageValue)
            {
                damage = damageValue;
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            //if((projectile.type == ModContent.ProjectileType<ShtuxiumBlast>() || projectile.type == ModContent.ProjectileType<ShtuxiumBlast2>() || projectile.type == ModContent.ProjectileType<ShtuxiumBlast3>()) && npc.life > npc.lifeMax / 100)
            //{
            //    npc.life -= npc.lifeMax / 100;
            //}
        }
    }
}
