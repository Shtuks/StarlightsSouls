using ssm.Content.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria;
using ssm.Content.NPCs.StarlightCat;
using ssm.Core;
using ssm.Content.Mounts;
using Terraria.IO;
using Terraria.Map;
using ssm.SHTUK.Modules;
using Terraria.ID;

namespace ssm
{
    public partial class ShtunPlayer : ModPlayer
    {
        public bool shtuxianSoul;
        public bool antiCollision;
        public bool antiDeath;
        public bool antiDebuff;
        public bool antiImmunity;
        public bool MutantSoul;
        private Vector2 lastPos;
        public bool DevianttSoul;
        public bool CelestialPower;
        public int frameShtuxibusAura;
        public bool ShtuxibusSetBonus;
        public bool CheatGodmode;
        public bool yharon;
        public bool geiger;
        public int frameCounter;
        public int ShtuxibusDeaths;
        public bool charging;
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        public bool ShtuxibusMinionBuff;
        public bool ChtuxlagorBuff;
        public bool ChtuxlagorHeart;
        public bool ChtuxlagorInferno;
        public int Screenshake;

        //SCB
        public int ChtuxlagorDeaths;
        public int ChtuxlagorLives;
        public int ChtuxlagorImmune;
        public int ChtuxlagorHealth;
        public int ChtuxlagorHits;

        //Enchants
        public bool equippedPhantasmalEnchantment;
        public bool equippedAbominableEnchantment;
        public bool equippedNekomiEnchantment;
        public bool equippedShtuxianEnchantment;
        public bool equippedMonstrosityEnchantment;
        public bool ShtuxibusSoul;
        public bool trueDevSets;

        public bool DeviGraze;
        public bool Graze;
        public float GrazeRadius;
        public int DeviGrazeCounter;
        public double DeviGrazeBonus;
        public bool dotMount;

        public override void PreUpdateBuffs()
        {
            if(StarlightCatBoss.phase > 0)
            {
                ChtuxlagorArena();
            }
            lastPos = Player.position;
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (ssm.shtuxianSuper.JustPressed)
            {
                int duration = 0;

                if (shtuxianSoul)
                    duration = 20;
                if (ChtuxlagorHeart)
                    duration = 5;
                if (equippedShtuxianEnchantment)
                    duration = 10;

                Player.AddBuff(ModContent.BuffType<ShtuxianDomination>(), duration * 60);
                Player.AddBuff(ModContent.BuffType<ChtuxlagorInferno>(), duration * 70);

                //SoundEngine.PlaySound(new SoundStyle("ssm/Assets/Sounds/ShtuxianSuper"), Player.Center);
            }

            if (ssm.shtukTeleport.JustPressed && Player.Modules().teleportModule && Main.myPlayer == Player.whoAmI)
            {
                Vector2 teleportLocation;
                teleportLocation.X = (float)Main.mouseX + Main.screenPosition.X;
                if (Player.gravDir == 1f)
                {
                    teleportLocation.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)Player.height;
                }
                else
                {
                    teleportLocation.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                }
                teleportLocation.X -= (float)(Player.width / 2);
                if (teleportLocation.X > 50f && teleportLocation.X < (float)(Main.maxTilesX * 16 - 50) && teleportLocation.Y > 50f && teleportLocation.Y < (float)(Main.maxTilesY * 16 - 50))
                {
                    if (!Collision.SolidCollision(teleportLocation, Player.width, Player.height))
                    {
                        Player.Teleport(teleportLocation, 4, 0);
                        NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)Player.whoAmI, teleportLocation.X, teleportLocation.Y, 1, 0, 0);
                    }
                }
            }


            if (ssm.shtukCharge.Current)
            {
                Player.SHTUK().addEnergy(Player.SHTUK().energyRegenCharging);
            }

            if (ssm.dotMount.JustPressed)
            {
                if (shtuxianSoul)
                {
                    Player.AddBuff(ModContent.BuffType<DotBuff>(), 999999);
                }

                //SoundEngine.PlaySound(new SoundStyle("ssm/Assets/Sounds/ShtuxianSuper"), Player.Center);
            }
        }

        public override void PreUpdateMovement()
        {
            if (Player.whoAmI == Main.myPlayer && dotMount)
            {
                float speed = Dot.MovementSpeed;

                if (Player.controlLeft)
                {
                    Player.velocity.X = -speed;
                    Player.ChangeDir(-1);
                }
                else if (Player.controlRight)
                {
                    Player.velocity.X = speed;
                    Player.ChangeDir(1);
                }
                else
                    Player.velocity.X = 0f;

                if (Player.controlUp || Player.controlJump)
                    Player.velocity.Y = -speed;

                else if (Player.controlDown)
                {
                    Player.velocity.Y = speed;
                    if (Collision.TileCollision(Player.position, Player.velocity, Player.width, Player.height, true, false, (int)Player.gravDir).Y == 0f)
                        Player.velocity.Y = 0.5f;
                }
                else
                    Player.velocity.Y = 0f;
            }
        }
        public override void ModifyScreenPosition()
        {
            if (Screenshake > 0)
                Main.screenPosition += Main.rand.NextVector2Circular(7, 7);
        }

        public Rectangle GetPrecisionHurtbox()
        {
            Rectangle hurtbox = Player.Hitbox;
            hurtbox.X += hurtbox.Width / 4;
            hurtbox.Y += hurtbox.Height / 4;
            hurtbox.Width = Math.Min(hurtbox.Width, hurtbox.Height);
            hurtbox.Height = Math.Min(hurtbox.Width, hurtbox.Height);
            hurtbox.X -= hurtbox.Width / 4;
            hurtbox.Y -= hurtbox.Height / 4;
            return hurtbox;
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            double damageMult = 1D;
            modifiers.SourceDamage *= (float)damageMult;

            if (ChtuxlagorHeart)
            {
                modifiers.SetMaxDamage(1000);
            }
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            return !ChtuxlagorBuff || !Player.HasBuff<ShtuxianDomination>();
        }
        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (ChtuxlagorBuff || Player.HasBuff<ShtuxianDomination>())
                return false;
            if (Player.HasBuff<DotBuff>() && !proj.Colliding(proj.Hitbox, GetPrecisionHurtbox()))
                return false;
            return true;
        }

        public override void PreUpdate()
        {
            if(ChtuxlagorHealth < 0)
            {
                ERASESOFT(Player);
                bool var = false;
                if (!var)
                {
                    ShtunUtils.DisplayLocalizedText("Well, i hope we will meet again.", Color.Teal);
                    var = true;
                }

            }
            if (Player.HasBuff<ShtuxianDomination>() || ChtuxlagorBuff)
            {
                Player.ghost = false;
                Player.dead = false;
            }
        }

        /*public override void PostUpdateMiscEffects()
        {
            if (antiCollision)
                Player.AddBuff(ModContent.BuffType<AntiCollision>(), 2, true, false);
            if (antiDeath)
                Player.AddBuff(ModContent.BuffType<AntiDeath>(), 2, true, false);
            if (antiDebuff)
            {
                Player.AddBuff(ModContent.BuffType<AntiDebuff>(), 2, true, false);
                for (int index = 0; index < BuffLoader.BuffCount; ++index)
                {
                    if (Main.debuff[index])
                    {
                        Player.buffImmune[index] = true;
                        Player.buffImmune[28] = false;
                        Player.buffImmune[34] = false;
                        Player.buffImmune[146] = false;
                        Player.buffImmune[87] = false;
                        Player.buffImmune[89] = false;
                        Player.buffImmune[48] = false;
                        Player.buffImmune[158] = false;
                        Player.buffImmune[215] = false;
                    }
                }
                if (Player.potionDelay > 0)
                    Player.potionDelay = 0;
            }
        }*/
        public override void SaveData(TagCompound tag)
        {
            tag["ShtuxibusDeaths"] = ShtuxibusDeaths;
            tag["ChtuxlagorDeaths"] = ChtuxlagorDeaths;
        }
        public override void LoadData(TagCompound tag)
        {
            ShtuxibusDeaths = tag.GetInt("ShtuxibusDeaths");
            ChtuxlagorDeaths = tag.GetInt("ChtuxlagorDeaths");
            if (ModCompatibility.Dragonlens.Loaded) {
                bool godmode = tag.GetBool("godMode");
                bool dogmode = tag.GetBool("dogMode");
            }
        }

        public void ERASE(Player player)
        {
            player.dead = true;
            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                    player.buffImmune[index] = false;
            }
            player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " got out of the scope of this pixel 2D game."), double.MaxValue, 10);
            player.ghost = true;
        }
        public void ERASESOFT(Player player)
        {
            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                    player.buffImmune[index] = false;
            }
            player.dead = true;
            player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " got out of the scope of this pixel 2D game."), double.MaxValue, 10);
        }
        public void ERASEULTIMATE(Player player)
        {
            //Main.WeGameRequireExitGame();
            player.KillMeForGood();
            WorldFile.SaveWorld();
            Environment.Exit(0);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //if (equippedPhantasmalEnchantment)
            //    target.AddBuff(ModContent.Find<ModBuff>(FargoSoul.Name, "MutantFangBuff").Type, 1000, false);
            //if (equippedNekomiEnchantment)
            //    target.AddBuff(ModContent.Find<ModBuff>(FargoSoul.Name, "DeviPresenceBuff").Type, 1000, false);
            //if (equippedAbominableEnchantment)
            //    target.AddBuff(ModContent.Find<ModBuff>(FargoSoul.Name, "AbomFangBuff").Type, 1000, false);
            if (ChtuxlagorHeart)
                target.AddBuff(ModContent.Find<ModBuff>("ssm", "ChtuxlagorInferno").Type, 1000, false);
        }

        public override void ResetEffects()
        {
            if (Screenshake > 0)
                Screenshake--;

            ShtuxibusSetBonus = false;
            equippedPhantasmalEnchantment = false;
            equippedAbominableEnchantment = false;
            equippedNekomiEnchantment = false;
            DevianttSoul = false;
            MutantSoul = false;
            geiger = false;
            ChtuxlagorHeart = false;
            shtuxianSoul = false;
            ShtuxibusSoul = false;
            CelestialPower = false;

            //if (NoUsingItems > 0)
            //    NoUsingItems--;
        }
        public override void OnEnterWorld()
        {
            if (ShtunConfig.Instance.WorldEnterMessage)
            {
                ShtunUtils.DisplayLocalizedText("I recommend looking at the mod's settings.", Color.LimeGreen);
                ShtunUtils.DisplayLocalizedText("Most of the content or rebalances are configurable.", Color.LimeGreen);
                ShtunUtils.DisplayLocalizedText("This message also can be toggled off in settings.", Color.LimeGreen);
                ShtunUtils.DisplayLocalizedText("                                     - StarlightCat", Color.LimeGreen);
            }
        }

        public override void UpdateDead() 
        {
            ssm.legit = false;
            ChtuxlagorHits = 0;
        }

        public override bool ImmuneTo(
        PlayerDeathReason damageSource,
        int cooldownCounter,
        bool dodgeable)
        {
            return Player.HasBuff<ShtuxianDomination>();
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            bool retVal = true;

            if (!NPC.AnyNPCs(ModContent.NPCType<StarlightCatBoss>()))
            {
                if (Player.HasBuff<ShtuxianDomination>() || ChtuxlagorBuff)
                {
                    retVal = false;
                }
                if (Player.Modules().ressurectionModule && Player.SHTUK().energy > 1000 && !Player.HasBuff(ModContent.BuffType<SHTUK.Modules.RessurectedBuff>()))
                {
                    Player.AddBuff(ModContent.BuffType<SHTUK.Modules.RessurectedBuff>(), 300);
                    Player.statLife = Player.statLifeMax2;
                    retVal = false;
                }
            }
            else
            {
                ChtuxlagorDeaths++;
            }

            return retVal;
        }

        //public void ChtuxlagorDamage(Player player, int projDamage)
        //{
        //    if (ChtuxlagorHealth <= 0f || ChtuxlagorImmune > 0 || Main.netMode == 2)
        //    {
        //        return;
        //    }
        //    int oldHealth = player.statLife;
        //    ChtuxlagorHealth -= projDamage;
        //    player.statLife = (int)(player.statLifeMax2 * ChtuxlagorHealth);
        //    int constDamage = (int)((200f - player.statDefense / 2f) * (1f - player.endurance));
        //    if (constDamage < 1)
        //    {
        //        constDamage = 1;
        //    }
        //    if (player.statLife > oldHealth - constDamage)
        //    {
        //        player.statLife = oldHealth - constDamage;
        //    }
        //    int damage = oldHealth - player.statLife;
        //    if (ChtuxlagorHealth > 0f && player.statLife > 0)
        //    {
        //        ChtuxlagorImmune = 60;
        //        if (!player.immune)
        //        {
        //            player.immune = true;
        //            player.immuneTime = 60;
        //        }
        //    }
        //    else if (Main.myPlayer == player.whoAmI)
        //    {
        //        if (ssm.debug)
        //        {
        //            Main.NewText("I do not think this should be possible for now.");
        //            ChtuxlagorHealth = 1;
        //            player.statLife = 1;
        //            ChtuxlagorImmune = 60;
        //            return;
        //        }
        //        player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " was split to pieces of code!"), int.MaxValue, 10);
        //        player.mount.Dismount(player);
        //        player.dead = true;
        //        player.respawnTimer = 600;
        //        if (Main.expertMode)
        //        {
        //            player.respawnTimer = 900;
        //        }
        //    }
        //}

        void ChtuxlagorArena()
        {
            Vector2 offset = Player.position - lastPos;
            if (offset.Length() > 32f)
            {
                offset.Normalize();
                offset *= 32f;
                Player.position = lastPos + offset;
            }
            Vector2 origin = StarlightCatBoss.Origin;
            float arenaSize = StarlightCatBoss.ArenaSize;
            if (Player.position.X <= origin.X - arenaSize)
            {
                Player.position.X = origin.X - arenaSize;
                if (Player.velocity.X < 0f)
                {
                    Player.velocity.X = 0f;
                }
            }
            else if (Player.position.X + Player.width >= origin.X + arenaSize)
            {
                Player.position.X = origin.X + arenaSize - Player.width;
                if (Player.velocity.X > 0f)
                {
                    Player.velocity.X = 0f;
                }
            }
            if (Player.position.Y <= origin.Y - arenaSize)
            {
                Player.position.Y = origin.Y - arenaSize;
                if (Player.velocity.Y < 0f)
                {
                    Player.velocity.Y = 0f;
                }
            }
            else if (Player.position.Y + Player.height >= origin.Y + arenaSize)
            {
                Player.position.Y = origin.Y + arenaSize - Player.height;
                if (Player.velocity.Y > 0f)
                {
                    Player.velocity.Y = 0f;
                }
            }
        }
    }
}
