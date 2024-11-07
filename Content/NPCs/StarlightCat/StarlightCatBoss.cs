using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ssm.Content.NPCs.Shtuxibus;
using System.IO;
using ssm.Content.Buffs;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using ssm.Content.Items.Accessories;
using Microsoft.Xna.Framework.Graphics;
using ssm.Content.Items.Consumables;
using ssm.Systems;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using System.Reflection;

/*
 * Blushiemagic but 1.4?
 */

namespace ssm.Content.NPCs.StarlightCat
{
    [AutoloadBossHead]
    public class StarlightCatBoss : ModNPC
    {
        //Variables
        Player player => Main.player[NPC.target];
        public static string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        internal static List<BaseProj> bullets = new List<BaseProj>();
        public static readonly SoundStyle ray = new("ssm/Assets/Sounds/beam") { };
        public static readonly SoundStyle rayCharge = new("ssm/Assets/Sounds/charge") { };
        public static readonly SoundStyle spawn = new("ssm/Assets/Sounds/chtuxSpawn") { };
        public int ritualProj, spriteProj, ringProj;
        internal static int[] index = [-1, -1, -1, -1, -1, -1];
        public const int ArenaSize = 1000;
        public bool playerInvulTriggered;
        public static MethodInfo CSGodmodeOn = default;
        public static MethodInfo HerosGodmodeOn = default;
        public static float flash = 0f;
        public bool Aura;
        public bool godmodeCheat = false;
        public bool spawnedConstructs; 
        public static Vector2 Origin;
        public bool spawned;
        public bool CameraFocus = false;
        public static int phase;
        public static bool active{
            get{return phase > 0;}}

        //Shards vars
        public int Health1;
        public int Health2;
        public int Health3;
        public int Health4;

        public int Shield1;
        public int Shield2;
        public int Shield3;
        public int Shield4;


        #region help funcs
        void SpawnTalk()
        {
            AntiCheat();
            if (ssm.legit)
            {
                if (!WorldSaveSystem.downedChtuxlagor)
                {
                    if (player.Shtun().ChtuxlagorDeaths < 1)
                    {
                        //SoundEngine.PlaySound(spawn, Origin);
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("I hope you enjoyed my mod.", Color.Teal);
                        }
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("This is your finale challenge.", Color.Teal);
                        }
                    }
                    if (player.Shtun().ChtuxlagorDeaths == 1)
                    {
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("Is one death not enough, " + userName + "?", Color.Teal);
                        }
                    }
                    if (player.Shtun().ChtuxlagorDeaths == 2)
                    {
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("How Interesting. But why do you keep coming back?", Color.Teal);
                        }
                    }
                    if (player.Shtun().ChtuxlagorDeaths == 3)
                    {
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("This is getting hillarious. Your tenacity amazes me.", Color.Teal);
                        }
                    }
                    if (player.Shtun().ChtuxlagorDeaths == 4)
                    {
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("Hey " + userName + ", you are getting boring for me.", Color.Teal);
                        }
                    }
                    if (player.Shtun().ChtuxlagorDeaths == 5)
                    {
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("What will you do if I just take away your ability to be respawn?", Color.Teal);
                        }
                    }
                    if (player.Shtun().ChtuxlagorDeaths == 6)
                    {
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("I have no words.", Color.Teal);
                        }
                    }
                    if (player.Shtun().ChtuxlagorDeaths == 7)
                    {
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("I will just count how many times you dead.", Color.Teal);
                        }
                    }
                    if (player.Shtun().ChtuxlagorDeaths > 7)
                    {
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("You dead" + player.Shtun().ChtuxlagorDeaths + "times.", Color.Teal);
                        }
                    }
                }
                else
                {
                    if (player.Shtun().ChtuxlagorDeaths < 1)
                    {
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("What? You completed my challenge and decided to summon me again?.", Color.Teal);
                        }
                        if (NPC.localAI[1] == 120)
                        {
                            ShtunUtils.DisplayLocalizedText("I don't understand why you like to suffer so much.", Color.Teal);
                        }
                    }
                    if (player.Shtun().ChtuxlagorDeaths == 1)
                    {
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("DIE " + userName, Color.Teal);
                        }
                    }
                    if (player.Shtun().ChtuxlagorDeaths == 2)
                    {
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("I will just count how many times you dead.", Color.Teal);
                        }
                    }
                    if (player.Shtun().ChtuxlagorDeaths > 2)
                    {
                        if (NPC.localAI[1] == 100)
                        {
                            ShtunUtils.DisplayLocalizedText("You dead" + player.Shtun().ChtuxlagorDeaths + "times.", Color.Teal);
                        }
                    }
                }
            }
            else
            {
                if (NPC.localAI[1] == 100)
                {
                    ShtunUtils.DisplayLocalizedText("You can't spawn me via cheats, " + userName + ".", Color.Teal);
                    player.Shtun().ERASE(player);
                    ssm.legit = false;
                }
            }
        }
        void AntiCheat()
        {
            for (int j = 0; j < Main.player[Main.myPlayer].inventory.Length; j++)
            {
                if (Main.player[Main.myPlayer].inventory[j].type == ItemID.RodOfHarmony)
                {
                    int susindex = Main.LocalPlayer.FindItem(ItemID.RodOfHarmony);
                    Main.LocalPlayer.inventory[susindex].TurnToAir();
                    if (NPC.localAI[1] == 100)
                    {
                        ShtunUtils.DisplayLocalizedText("You can't use that pink glowing staff against my attacks.", Color.Teal);
                    }
                }
                if (player.Shtun().shtuxianSoul)
                {
                    if (NPC.localAI[1] == 180)
                    {
                        ShtunUtils.DisplayLocalizedText("Shtuxian Soul won't help you much.", Color.Teal);
                    }
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
                if (ssm.debug) { ShtunUtils.DisplayLocalizedText("You have " + player.Shtun().ChtuxlagorDeaths + " deaths.", Color.Teal); }
                phase = 1;
                ssm.legit = false;
                this.NPC.life = this.NPC.lifeMax;
                spawned = true;
            }

            Origin = NPC.Center;

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
        void Move(Vector2 target, float speed, bool fastX = true, bool obeySpeedCap = true)
        {
            float turnaroundModifier = 1f;
            float maxSpeed = 24;

            if (Main.expertMode)
            {
                speed *= 2;
                turnaroundModifier *= 2f;
                maxSpeed *= 1.5f;
            }

            if (Math.Abs(NPC.Center.X - target.X) > 10)
            {
                if (NPC.Center.X < target.X)
                {
                    NPC.velocity.X += speed;
                    if (NPC.velocity.X < 0)
                        NPC.velocity.X += speed * (fastX ? 2 : 1) * turnaroundModifier;
                }
                else
                {
                    NPC.velocity.X -= speed;
                    if (NPC.velocity.X > 0)
                        NPC.velocity.X -= speed * (fastX ? 2 : 1) * turnaroundModifier;
                }
            }
            if (NPC.Center.Y < target.Y)
            {
                NPC.velocity.Y += speed;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speed * 2 * turnaroundModifier;
            }
            else
            {
                NPC.velocity.Y -= speed;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speed * 2 * turnaroundModifier;
            }

            if (obeySpeedCap)
            {
                if (Math.Abs(NPC.velocity.X) > maxSpeed)
                    NPC.velocity.X = maxSpeed * Math.Sign(NPC.velocity.X);
                if (Math.Abs(NPC.velocity.Y) > maxSpeed)
                    NPC.velocity.Y = maxSpeed * Math.Sign(NPC.velocity.Y);
            }
        }
        void AddConstruct(Vector2 offset)
        {
            //constructs.Add(Origin + ArenaSize * offset);
        }
        public static void DrawArena(SpriteBatch spriteBatch)
        {
            const int blockSize = 16;
            int centerX = (int)Origin.X;
            int centerY = (int)Origin.Y;
            Texture2D blockTexture = ModContent.Request<Texture2D>("ssm/Content/NPCs/StarlightCat/ChtuxlagorArenaBlock").Value;
            for (int x = centerX - ArenaSize - blockSize / 2; x <= centerX + ArenaSize + blockSize / 2; x += blockSize)
            {
                int y = centerY - ArenaSize - blockSize / 2;
                Vector2 drawPos = new Vector2(x - blockSize / 2, y - blockSize / 2) - Main.screenPosition;
                spriteBatch.Draw(blockTexture, drawPos, Color.White * 0.75f);
                drawPos.Y += 2 * ArenaSize + blockSize;
                spriteBatch.Draw(blockTexture, drawPos, Color.White * 0.75f);
            }
            for (int y = centerY - ArenaSize - blockSize / 2; y <= centerY + ArenaSize + blockSize / 2; y += blockSize)
            {
                int x = centerX - ArenaSize - blockSize / 2;
                Vector2 drawPos = new Vector2(x - blockSize / 2, y - blockSize / 2) - Main.screenPosition;
                spriteBatch.Draw(blockTexture, drawPos, Color.White * 0.75f);
                drawPos.X += 2 * ArenaSize + blockSize;
                spriteBatch.Draw(blockTexture, drawPos, Color.White * 0.75f);
            }
        }
        void DrawBullets(SpriteBatch spriteBatch)
        {
            foreach (BaseProj bullet in bullets)
            {
                spriteBatch.Draw(bullet.Texture, bullet.Position - new Vector2(bullet.Size) - Main.screenPosition, Color.White);
            }
        }
        private void MovementY(float targetY, float speedModifier)
        {
            if (NPC.Center.Y < targetY)
            {
                NPC.velocity.Y += speedModifier;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speedModifier * 2;
            }
            else
            {
                NPC.velocity.Y -= speedModifier;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(NPC.velocity.Y) > 24)
                NPC.velocity.Y = 24 * Math.Sign(NPC.velocity.Y);
        }
        void TryLifeSteal(Vector2 pos, int playerWhoAmI)
        {
            int totalHealPerHit = NPC.lifeMax / 100 * 10;

            const int max = 20;
            for (int i = 0; i < max; i++)
            {
                Vector2 vel = Main.rand.NextFloat(2f, 9f) * -Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi);
                float ai0 = NPC.whoAmI;
                float ai1 = vel.Length() / Main.rand.Next(30, 90); //window in which they begin homing in

                int healPerOrb = (int)(totalHealPerHit / max * Main.rand.NextFloat(0.95f, 1.05f));

                if (playerWhoAmI == Main.myPlayer && Main.player[playerWhoAmI].ownedProjectileCounts[ModContent.ProjectileType<MutantHeal>()] < 10)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), pos, vel, ModContent.ProjectileType<MutantHeal>(), healPerOrb, 0f, Main.myPlayer, ai0, ai1);

                    SoundEngine.PlaySound(SoundID.Item27, pos);
                }
            }
        }
        #endregion

        #region attack funcs
        void SpawnSphereRing(int max, float speed, int damage, float rotationModifier, float offset = 0)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            float rotation = 2f * (float)Math.PI / max;
            //int type = ModContent.ProjectileType<ChtuxlagorBulletRing>();
            //for (int i = 0; i < max; i++)
            //{
            //    Vector2 vel = speed * Vector2.UnitY.RotatedBy(rotation * i + offset);
            //    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, type, damage, 0f, Main.myPlayer, rotationModifier * NPC.spriteDirection, speed);
            //}
            SoundEngine.PlaySound(SoundID.Item84, NPC.Center);
        }
        void spawnConstructs()
        {
            AddConstruct(new Vector2(-0.8f, -0.8f));
            AddConstruct(new Vector2(0.8f, -0.8f));
            AddConstruct(new Vector2(-0.8f, 0.8f));
            AddConstruct(new Vector2(0.8f, 0.8f));
            spawnedConstructs = true;
        }
        #endregion

        #region attack
        void DeathrayPlusManyEyes() { }
        #endregion

        #region basic boss funcs
        public override bool PreAI()
        {
            if (!godmodeCheat)
            {
                if ((CSGodmodeOn != default && (bool)CSGodmodeOn.Invoke(null, new object[0])) || (HerosGodmodeOn != default && (bool)HerosGodmodeOn.Invoke(null, new object[0])))
                {
                    ShtunUtils.DisplayLocalizedText("Huh?", Color.Teal);
                    ShtunUtils.DisplayLocalizedText("A godmode?", Color.Teal);
                    if (!WorldSaveSystem.talk)
                    {
                        ShtunUtils.DisplayLocalizedText("What the point of playing game with godmode?", Color.Teal);
                        ShtunUtils.DisplayLocalizedText("Even i never use it for testing.", Color.Teal);
                        ShtunUtils.DisplayLocalizedText("Soul of Chtux'lag'or already enough for any 1.4 boss.", Color.Teal);
                        ShtunUtils.DisplayLocalizedText("Sadly Blushiemagic, SGAMod and other amazing mods that have bosses that break 4th wall like me only on 1.3.", Color.Teal);
                        ShtunUtils.DisplayLocalizedText("I don't like 1.4 era of modding even despite visual beauty and effects like shaders.", Color.Teal);
                        ShtunUtils.DisplayLocalizedText("All this Calamity favoritism, absence of insanity and interesting mechanics...", Color.Teal);
                        ShtunUtils.DisplayLocalizedText("Anyway.", Color.Teal);
                        WorldSaveSystem.talk = true;
                    }
                    ShtunUtils.DisplayLocalizedText("You can't be serious.", Color.Teal);

                    player.Shtun().ERASE(player);
                    ShtunUtils.DisplayLocalizedText("Don't even try to cheat this fight.", Color.Teal);

                    godmodeCheat = true;
                }
            }

            return false;
        }
        public override void BossLoot(ref string name, ref int potionType) { potionType = ModContent.ItemType<Items.Consumables.UltimateHealingPotion>(); }
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
            NPC.value = Item.buyPrice(999999);
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
            NPC.scale = 0.5f;
            NPC.timeLeft = NPC.activeTime * 30;
            SceneEffectPriority = SceneEffectPriority.BossHigh;
            //Music = MusicLoader.GetMusicSlot(this.Mod, "Assets/Music/Chtuxlagor");
        }
        public override bool CanHitPlayer(Player target, ref int CooldownSlot)
        {
            CooldownSlot = 1;
            return NPC.Distance(ShtunUtils.ClosestPointInHitbox(target, NPC.Center)) < Player.defaultHeight && NPC.ai[0] > -1;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(NPC.localAI[0]);
            writer.Write(NPC.localAI[1]);
            writer.Write(NPC.localAI[2]);
            writer.Write(NPC.localAI[3]);
        }
        public override bool CheckDead()
        {
            //if (NPC.ai[0] == 745)
            //    return true;
            //NPC.life = 1;
            //NPC.active = true;
            //if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] > -1)
            //{
            //    NPC.ai[0] = NPC.ai[0] >= 10 ? -1 : 10;
            //    NPC.ai[1] = 0;
            //    NPC.ai[2] = 0;
            //    NPC.ai[3] = 0;
            //    NPC.localAI[0] = 0;
            //    NPC.localAI[1] = 0;
            //    NPC.localAI[2] = 0;
            //    NPC.dontTakeDamage = true;
            //    NPC.netUpdate = true;
            //    ShtunUtils.ClearAllProjectiles(2, NPC.whoAmI, NPC.ai[0] < 0);
            //}
            return true;
        }
        public override void OnKill()
        {
            phase = 0;
            if (!playerInvulTriggered)
            {
                Item.NewItem(base.NPC.GetSource_Loot(), base.NPC.Hitbox, ModContent.ItemType<Sadism>(), 30000000);
            }
            NPC.SetEventFlagCleared(ref WorldSaveSystem.downedChtuxlagor, -1);
            if (player.Shtun().ChtuxlagorDeaths != 0)
            {
                ShtunUtils.DisplayLocalizedText("Congratulations, " + userName + ". You did it in " + player.Shtun().ChtuxlagorDeaths + " attempts.", Color.Teal);
                ShtunUtils.DisplayLocalizedText("Special thanks to Fargowilta, Javyz, Terry N Muse, Blushiemagic, DanYami, DevaVade, cheesenuggets, Mayeneznik, Yum, Jopojelly, ChickenBones, Jofairden, DivermanSam.", Color.Teal);
            }
            else
            {
                player.Shtun().ERASE(player);
                ShtunUtils.DisplayLocalizedText("No deaths? Technically this is impossible. Unless you are Yrmir. Anyway. 100% you just cheated.", Color.Teal);
            }
            player.Shtun().ChtuxlagorDeaths = 0;
            player.Shtun().ChtuxlagorHits = 0;
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            //idk
            NPC.localAI[0] = reader.ReadSingle();
            //timer
            NPC.localAI[1] = reader.ReadSingle();
            //internal attack timer
            NPC.localAI[2] = reader.ReadSingle();
            //Phaze
            NPC.localAI[3] = reader.ReadSingle();
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.rand.NextBool(10) && !ssm.debug)
            {
                ShtunUtils.DisplayLocalizedText("Oops! I slipped.");
                player.Shtun().ERASE(player);
            }
        }
        public override void AI()
        {
            phase = (int)NPC.localAI[3] + 1;

            if (ShtunUtils.AnyBossAlive()) { ShtunUtils.DisplayLocalizedText("What?"); }

            NPC.localAI[1]++;

            ShtunNpcs.Chtuxlagor = NPC.whoAmI;

            AliveCheck(player);
            SpawnTalk();
            ManageAurasAndPreSpawn();
            ManageNeededProjectiles();

            //attacks handler
            //switch ((int)NPC.ai[0])
            //{
            //    case 0: ; break;
            //    case 1: ; break;
            //    case 2: ; break;
            //    case 3: ; break;
            //    case 4: ; break;
            //    case 5: ; break;
            //    case 6: ; break;
            //    case 7: ; break;

            //default: NPC.ai[0] = 0; goto case 0;
            //}
        }
        #endregion
    }
}
