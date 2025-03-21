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
        private const int ArenaWidth = 500;
        private const int ArenaHeight = 500;
        private const int AttackCooldown = 120;
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
    }
}