using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.TreasureBags;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Yharon;
using Microsoft.Xna.Framework;
using ssm.Calamity.Swarm.Energizers;
using ssm.Calamity;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;
using CalamityMod.World;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class EnergizedCalNpc : GlobalNPC
    {
        private int go = 1;

        public override bool InstancePerEntity => true;

        public bool SwarmActive(NPC npc)
        {
            return npc.GetGlobalNPC<CalNpcs>().SwarmActive;
        }

        public override void SetDefaults(NPC npc)
        {
            int num = 30000;
            int num2 = 300000;
            int num3 = 3000000;
            bool flag = true;

            if (ssm.SwarmSetDefaults)
            {
                if (npc.type == ModContent.NPCType<Crabulon>())
                {
                    npc.lifeMax = num;
                }
                else if (npc.type == ModContent.NPCType<DesertScourgeHead>())
                {
                    npc.lifeMax = num;
                }
                else if (npc.type == ModContent.NPCType<SupremeCalamitas>())
                {
                    npc.lifeMax = num3;
                    ssm.PostMLSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<AstrumAureus>())
                {
                    npc.lifeMax = num2;
                    ssm.HardmodeSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<CalamitasClone>())
                {
                    npc.lifeMax = num2;
                    ssm.HardmodeSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<PerforatorHive>())
                {
                    npc.lifeMax = num;
                }
                else if (npc.type == ModContent.NPCType<DevourerofGodsHead>())
                {
                    npc.lifeMax = num3;
                    ssm.PostMLSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<Cryogen>())
                {
                    npc.lifeMax = num2;
                    ssm.HardmodeSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<BrimstoneElemental>())
                {
                    npc.lifeMax = num2;
                    ssm.HardmodeSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<Bumblefuck>())
                {
                    npc.lifeMax = num3;
                    ssm.PostMLSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<ProfanedGuardianCommander>())
                {
                    npc.lifeMax = num3;
                    ssm.PostMLSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<AquaticScourgeHead>())
                {
                    npc.lifeMax = num2;
                    ssm.HardmodeSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<PlaguebringerGoliath>())
                {
                    npc.lifeMax = num2;
                    ssm.HardmodeSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<Anahita>())
                {
                    npc.lifeMax = num2;
                    ssm.HardmodeSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<AstrumDeusHead>())
                {
                    npc.lifeMax = num2;
                    ssm.HardmodeSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<HiveMind>())
                {
                    npc.lifeMax = num;
                }
                else if (npc.type == ModContent.NPCType<Yharon>())
                {
                    npc.lifeMax = num3;
                    ssm.PostMLSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<Providence>())
                {
                    npc.lifeMax = num3;
                    ssm.PostMLSwarmActive = true;
                }
                else if (npc.type == ModContent.NPCType<RavagerBody>())
                {
                    if (!CalamityMod.DownedBossSystem.downedProvidence)
                    {
                        npc.lifeMax = num2;
                        ssm.HardmodeSwarmActive = true;
                    }
                    else 
                    {
                        npc.lifeMax = num3;
                        ssm.PostMLSwarmActive = true;
                    }
                }
                flag = false;
            }
            else
            {
                flag = false;
            }
            if (!ssm.SwarmActive)
            {
                return;
            }
            if (!flag)
            {
                flag = true;
                switch (npc.type)
                {
                    case 267:
                        npc.lifeMax = 1000;
                        break;
                    case 14:
                    case 15:
                        npc.lifeMax = num / 12;
                        break;
                    case 36:
                        npc.lifeMax = num / 12;
                        break;
                    case 128:
                    case 129:
                    case 130:
                    case 131:
                        npc.lifeMax = num2 / 5;
                        break;
                    case 139:
                        npc.lifeMax = num2 / 50;
                        break;
                    case 263:
                    case 264:
                        npc.lifeMax = num2 / 20;
                        break;
                    case 265:
                        npc.lifeMax = num2 / 40;
                        break;
                    case 246:
                    case 247:
                    case 249:
                        npc.lifeMax = num2 / 4;
                        break;
                    case 396:
                    case 397:
                        npc.lifeMax = num2 / 4;
                        break;
                    default:
                        flag = false;
                        break;
                }
            }
            if (ShtunSets.NPCs.SwarmHealth[npc.type] != 0)
            {
                flag = true;
                npc.lifeMax = ShtunSets.NPCs.SwarmHealth[npc.type];
            }
            if (flag && ssm.SwarmItemsUsed > 1)
            {
                npc.lifeMax *= ssm.SwarmItemsUsed;
            }
            int num4 = ssm.SwarmMinDamage * 2;
            if (!npc.townNPC && npc.lifeMax > 10 && npc.damage > 0 && npc.damage < num3)
            {
                npc.damage = num4;
            }
        }

        public override bool PreAI(NPC npc)
        {
            if (ssm.SwarmNoHyperActive)
            {
                return true;
            }
            if (ssm.HardmodeSwarmActive && Main.GameUpdateCount % 2 == 0)
            {
                return true;
            }
            if (npc.type == 400)
            {
                return true;
            }
            if (ssm.SwarmActive && !npc.townNPC && npc.lifeMax > 1 && go < 2)
            {
                go++;
                npc.AI();
                float num = 0.5f;
                Vector2 position = npc.position + npc.velocity * num;
                if (!Collision.SolidCollision(position, npc.width, npc.height))
                {
                    npc.position = position;
                }
            }
            return true;
        }

        public override void PostAI(NPC npc)
        {
            if (go == 2)
            {
                go = 1;
            }
        }
    }
}