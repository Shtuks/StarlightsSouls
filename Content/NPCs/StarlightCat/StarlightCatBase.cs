using ssm.Content.Buffs;
using ssm.Content.NPCs.Shtuxibus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.StarlightCat
{
    public abstract class StarlightCatBase : ModNPC
    {
        Player player => Main.player[NPC.target];
        bool spawned;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.NoMultiplayerSmoothingByType[NPC.type] = true;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }
        public override void SetDefaults()
        {
            NPC.BossBar = ModContent.GetInstance<ShtuxibusBar>();
            NPC.width = 320;
            NPC.height = 200;
            NPC.damage = 100000;
            NPC.lifeMax = 1000000000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.npcSlots = 745f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.netAlways = true;
            NPC.timeLeft = NPC.activeTime * 30;
            SceneEffectPriority = SceneEffectPriority.BossHigh;
        }
        public override bool CanHitPlayer(Player target, ref int CooldownSlot)
        {
            CooldownSlot = 1;
            return NPC.Distance(ShtunUtils.ClosestPointInHitbox(target, NPC.Center)) < Player.defaultHeight && NPC.ai[0] > -1;
        }
        void AntiCheat()
        {
            for (int j = 0; j < Main.player[Main.myPlayer].inventory.Length; j++)
            {
                if (Main.player[Main.myPlayer].inventory[j].type == ItemID.RodOfHarmony)
                {
                    int susindex = Main.LocalPlayer.FindItem(ItemID.RodOfHarmony);
                    Main.LocalPlayer.inventory[susindex].TurnToAir();
                }
            }
        }
        void ChooseNextAttack(params int[] args)
        {
            NPC.ai[0] = Main.rand.Next(args);
        }
        void ManageAurasAndPreSpawn()
        {
            //Things to do once on spawn
            if (!spawned)
            {
                spawned = true;
                this.NPC.life = this.NPC.lifeMax;
            }

            //Boss debuff
            Main.LocalPlayer.AddBuff(ModContent.BuffType<ShtuxibusCurse>(), 2);
        }
        void ManageNeededProjectiles()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) //checks for needed projs
            {
            }
        }
        bool AliveCheck(Player p, bool forceDespawn = false)
        {
            if (forceDespawn || ((!p.active || p.dead) && NPC.localAI[3] > 0))
            {
                NPC.TargetClosest();
                p = Main.player[NPC.target];
                if (forceDespawn || !p.active || p.dead)
                {
                    if (NPC.timeLeft > 30)
                        NPC.timeLeft = 30;
                    NPC.velocity.Y -= 1f;
                    if (NPC.timeLeft == 1)
                    {
                        if (NPC.position.Y < 0)
                            NPC.position.Y = 0;
                        //SkyManager.Instance.Deactivate("ssm:Chtuxlagor");
                    }
                    return false;
                }
            }

            //never despawn
            if (NPC.timeLeft < 3600)
                NPC.timeLeft = 3600;

            //go to surface
            if (player.Center.Y / 16f > Main.worldSurface)
            {
                NPC.velocity.X *= 0.95f;
                NPC.velocity.Y -= 1f;
                if (NPC.velocity.Y < -32f)
                    NPC.velocity.Y = -32f;
                return false;
            }
            return true;
        }
    }
}
