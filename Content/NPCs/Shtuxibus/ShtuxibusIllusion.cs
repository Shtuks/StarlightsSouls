using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.Shtuxibus
{
    [AutoloadBossHead]
    public class ShtuxibusIllusion : ModNPC
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public override string Texture => "ssm/Content/NPCs/Shtuxibus/Shtuxibus";

        public override void SetStaticDefaults()
        {

            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;



            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 50;
            NPC.damage = 10000;
            NPC.defense = 745;
            NPC.lifeMax = 745000000;
            NPC.dontTakeDamage = true;
            NPC.HitSound = SoundID.NPCHit57;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * 0.5f);
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f);
        }

        public override bool CanHitPlayer(Player target, ref int CooldownSlot)
        {
            return false;
        }

        public override void AI()
        {
            NPC mutant = ShtunUtils.NPCExists(NPC.ai[0], ModContent.NPCType<Shtuxibus>());
            if (mutant == null || mutant.ai[0] < 18 || mutant.ai[0] > 19 || mutant.life <= 1)
            {
                NPC.life = 0;
                NPC.HitEffect();
                // NPC.StrikeNPCNoInteraction(9999, 0f, 0);
                NPC.active = false;
                for (int i = 0; i < 40; i++)
                {
                    int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 5);
                    Main.dust[d].velocity *= 2.5f;
                    Main.dust[d].scale += 0.5f;
                }
                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 229, 0f, 0f, 0, default(Color), 2f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].noLight = true;
                    Main.dust[d].velocity *= 9f;
                }
                return;
            }

            NPC.target = mutant.target;
            NPC.damage = mutant.damage;
            NPC.defDamage = mutant.damage;

            NPC.frame.Y = mutant.frame.Y;

            if (NPC.HasValidTarget)
            {
                Vector2 target = Main.player[mutant.target].Center;
                Vector2 distance = target - mutant.Center;
                NPC.Center = target;
                NPC.position.X += distance.X * NPC.ai[1];
                NPC.position.Y += distance.Y * NPC.ai[2];
                NPC.direction = NPC.spriteDirection = NPC.position.X < Main.player[NPC.target].position.X ? 1 : -1;
            }
            else
            {
                NPC.Center = mutant.Center;
            }

            if (--NPC.ai[3] == 0)
            {
                int ai0;
                if (NPC.ai[1] < 0)
                    ai0 = 0;
                else if (NPC.ai[2] < 0)
                    ai0 = 1;
                else
                    ai0 = 2;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(mutant.GetSource_FromThis(), NPC.Center, Vector2.UnitY * -5, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantPillar").Type, ShtunUtils.ScaledProjectileDamage(mutant.damage, 4f / 3), 0, Main.myPlayer, ai0, NPC.whoAmI);
            }
        }

        public override bool CheckActive() => false;

        public override bool PreKill() => false;

        public override void FindFrame(int frameHeight)
        {
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
        }
    }
}