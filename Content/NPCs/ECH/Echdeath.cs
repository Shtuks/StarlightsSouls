using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using FargowiltasSouls;
using FargowiltasSouls.Content.NPCs;
using ssm.Content.Items.Consumables;
using System.Drawing.Drawing2D;
using ssm.Systems;

namespace ssm.Content.NPCs.ECH
{
    [AutoloadBossHead]
    public class Echdeath : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 11;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                PortraitScale = 2f,
            };
        }

        public override void SetDefaults()
        {
            NPC.width = 60;
            NPC.height = 60;
            NPC.damage = int.MaxValue / 10;
            NPC.defense = int.MaxValue / 10;
            NPC.lifeMax = int.MaxValue / 10;
            NPC.HitSound = SoundID.NPCHit57;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.boss = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<Sadism>();
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = 1;
            return true;
        }

        public override void AI()
        {
            if (!NPC.HasValidTarget)
            {
                //NPC.ai[0] = 0;
                NPC.TargetClosest();
                /*if (!NPC.HasValidTarget)
                {
                    NPC.active = false;
                    return;
                }*/
            }

            NPC.life = NPC.lifeMax;
            NPC.damage = NPC.defDamage;
            NPC.defense = NPC.defDefense;

            NPC.ai[0] += 0.05f;
            
            if (NPC.velocity == Vector2.Zero)
                NPC.velocity = -Vector2.UnitY * NPC.ai[0];

            if (NPC.HasValidTarget)
            {
                Player player = Main.player[NPC.target];
                NPC.direction = NPC.spriteDirection = NPC.Center.X < player.Center.X ? 1 : -1;
                if (NPC.ai[1] == 1)
                    NPC.position += (player.position - player.oldPosition) * 0.25f;
                NPC.velocity = NPC.DirectionTo(player.Center) * NPC.ai[0];
                if (NPC.velocity.Length() > NPC.Distance(player.Center))
                    NPC.Center = player.Center;

                if (NPC.timeLeft < 600)
                    NPC.timeLeft = 600;
            }
            else
            {
                NPC.velocity = Vector2.Normalize(NPC.velocity) * NPC.ai[0];
                if (NPC.timeLeft > 60)
                    NPC.timeLeft = 60;
            }

            NPC.scale = 1f + NPC.ai[0] / 4f;
            int fullSize = (int)(40 * NPC.scale);
            
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[1] == 1)
                {
                    for (int i = -fullSize / 2; i <= fullSize / 2; i += 8)
                    {
                        for (int j = -fullSize / 2; j <= fullSize / 2; j += 8)
                        {
                            int tileX = (int)(NPC.Center.X + i) / 16;
                            int tileY = (int)(NPC.Center.Y + j) / 16;

                            //out of bounds checks
                            //if (tileX > -1 && tileX < Main.maxTilesX && tileY > -1 && tileY < Main.maxTilesY)
                            //{
                            //    Tile tile = Framing.GetTileSafely(tileX, tileY);
                            //    if (tile.type != 0 || tile.wall != 0)
                            //    {
                            //        WorldGen.KillTile(tileX, tileY, noItem: true);
                            //        WorldGen.KillWall(tileX, tileY);
                            //        if (Main.netMode == NetmodeID.Server)
                            //            NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, tileX, tileY);
                            //    }
                            //}
                        }
                    }
                }

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].type != NPC.type && NPC.Distance(Main.npc[i].Center) < fullSize / 2)
                    {
                        ShtunUtils.DisplayLocalizedText(":echdeath:", Color.Green);
                        for (int j = 0; j < 100; j++)
                            CombatText.NewText(Main.npc[i].Hitbox, Color.Red, Main.rand.Next(NPC.damage), true);
                    }
                }
            }

            if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost
                && (NPC.Hitbox.Intersects(Main.LocalPlayer.Hitbox)) || (NPC.ai[1] == 1 && NPC.Distance(Main.LocalPlayer.Center) < fullSize / 2))
            {
                Main.NewText(":echdeath:", Color.Red);
                Main.LocalPlayer.ResetEffects();
                Main.LocalPlayer.ghost = true;
                Main.LocalPlayer.KillMe(PlayerDeathReason.ByNPC(NPC.whoAmI), NPC.damage, 0);
                for (int i = 0; i < 100; i++)
                    CombatText.NewText(Main.LocalPlayer.Hitbox, Color.Red, (int)(NPC.damage * Main.rand.NextFloat(0.75f, 1f)), true);
            }

            if (NPC.ai[1] == 1)
            {
                if (NPC.localAI[0] == 0)
                {
                    Main.NewText("Echdeath has enraged.", Color.DarkRed);
                    NPC.localAI[0] = 1;
                    for (int i = 0; i < NPC.buffImmune.Length; i++)
                        NPC.buffImmune[i] = true;
                }
                while (NPC.buffType[0] != 0)
                    NPC.DelBuff(0);
                if (NPC.ai[2] == 0) //force life back to max until it works
                {
                    if (NPC.life == NPC.lifeMax)
                        NPC.ai[2] = 1;
                    NPC.life = NPC.lifeMax;
                }
            }
            else
            {
                if (NPC.ai[0] > 30)
                    NPC.ai[0] = 30;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (++NPC.frameCounter > 34 - NPC.ai[0])
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= Main.npcFrameCount[NPC.type] * frameHeight)
                    NPC.frame.Y = 0;
            }
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            ref StatModifier local = ref modifiers.FinalDamage;
            local *= 0.01f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (target.active && !target.dead && !target.ghost)
            {
                Main.NewText(":echdeath:", Color.Red);
                target.ResetEffects();
                target.ghost = true;
                target.KillMe(PlayerDeathReason.ByNPC(NPC.whoAmI), NPC.damage, 0);
                for (int i = 0; i < 100; i++)
                    CombatText.NewText(target.Hitbox, Color.Red, (int)(NPC.damage * Main.rand.NextFloat(0.75f, 1f)), true);
            }
        }

        public override bool CheckDead()
        {
            if (WorldSaveSystem.TrueSuperChtuxlagorWrathModeOmegaEX)
            {
                return true;
            }

            NPC.active = true;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NPC.life = 1;
            }
            else
            {
                NPC.life = NPC.lifeMax;
                NPC.ai[1] = 1;
            }
            return false;
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = NPC.spriteDirection < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Npc[NPC.type].Value;
            Rectangle rectangle = NPC.frame;
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = lightColor;
            color26 = NPC.GetAlpha(color26);

            SpriteEffects effects = NPC.spriteDirection < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Main.spriteBatch.Draw(texture2D13, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), NPC.GetAlpha(lightColor), NPC.rotation, origin2, NPC.scale, effects, 0f);
            return false;
        }
    }
}
