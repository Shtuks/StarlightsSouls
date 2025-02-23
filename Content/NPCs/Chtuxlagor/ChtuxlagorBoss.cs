using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.Chtuxlagor
{
    [AutoloadBossHead]
    public class ChtuxlagorBoss : ModNPC
    {
        private const int ArenaWidth = 500; // Ширина арены
        private const int ArenaHeight = 500; // Высота арены
        private const int AttackCooldown = 120; // Задержка между атаками
        private int attackTimer = 0;
        private int currentAttack = 0;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.NoMultiplayerSmoothingByType[NPC.type] = true;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.lifeMax = 1000000000;
            NPC.damage = 1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;
            NPC.npcSlots = 50f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 0, 1);
            NPC.aiStyle = -1;
        }

        public override void AI()
        {
            // Создание арены
            CreateArena();

            // Основной AI босса
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            if (attackTimer <= 0)
            {
                switch (currentAttack)
                {
                    case 0:
                        RandomPatternAttack(player);
                        break;
                    case 1:
                        IllusionTrapAttack(player);
                        break;
                    case 2:
                        GravitationalVortexAttack(player);
                        break;
                        // Добавьте другие атаки здесь
                }

                currentAttack = (currentAttack + 1) % 3; // Цикл атак
                attackTimer = AttackCooldown;
            }
            else
            {
                attackTimer--;
            }
        }

        private void CreateArena()
        {
            // Ограничение игрока в пределах арены
            Player player = Main.player[NPC.target];
            Vector2 arenaCenter = NPC.Center;

            if (player.position.X < arenaCenter.X - ArenaWidth / 2)
                player.position.X = arenaCenter.X - ArenaWidth / 2;
            if (player.position.X > arenaCenter.X + ArenaWidth / 2)
                player.position.X = arenaCenter.X + ArenaWidth / 2;
            if (player.position.Y < arenaCenter.Y - ArenaHeight / 2)
                player.position.Y = arenaCenter.Y - ArenaHeight / 2;
            if (player.position.Y > arenaCenter.Y + ArenaHeight / 2)
                player.position.Y = arenaCenter.Y + ArenaHeight / 2;
        }

        private void RandomPatternAttack(Player player)
        {
            // Атака со случайными паттернами
            for (int i = 0; i < 20; i++)
            {
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5));
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ProjectileID.BulletHighVelocity, 50, 1f, Main.myPlayer);
            }
        }

        private void IllusionTrapAttack(Player player)
        {
            // Атака с иллюзорными пулями
            for (int i = 0; i < 10; i++)
            {
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5));
                int type = Main.rand.NextBool() ? ProjectileID.BulletHighVelocity : ProjectileID.ShadowBeamFriendly; // Иллюзорные пули
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, type, 50, 1f, Main.myPlayer);
            }
        }

        private void GravitationalVortexAttack(Player player)
        {
            // Атака с гравитационными вихрями
            Vector2 vortexPosition = NPC.Center + new Vector2(Main.rand.Next(-200, 200), Main.rand.Next(-200, 200));
            Projectile.NewProjectile(NPC.GetSource_FromAI(), vortexPosition, Vector2.Zero, ProjectileID.VortexVortexLightning, 50, 1f, Main.myPlayer);
        }

        // Добавьте другие методы для атак здесь

        public override void OnKill()
        {
            // Финальная атака при смерти
            for (int i = 0; i < 100; i++)
            {
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10));
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ProjectileID.BulletHighVelocity, 50, 1f, Main.myPlayer);
            }
        }

    }
}