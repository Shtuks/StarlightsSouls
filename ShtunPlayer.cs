using ssm.Content.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria;
using ssm.Core;
using ssm.Content.Mounts;
using Terraria.IO;
using Terraria.ID;
using Terraria.Localization;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using ssm.Content.Items.Accessories;
using FargowiltasSouls.Content.Buffs.Masomode;
using ssm.Content.Projectiles;

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
        public float throwerVelocity = 1f;
        public bool CyclonicFin;
        public int CyclonicFinCD;

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
            lastPos = Player.position;
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (ssm.dotMount.JustPressed)
            {
                if (shtuxianSoul)
                {
                    Player.AddBuff(ModContent.BuffType<DotBuff>(), 999999);
                }
            }
        }

        public override void OnEnterWorld()
        {
            if (!ModLoader.TryGetMod("ThoriumRework", out Mod _) && ModLoader.TryGetMod("ThoriumMod", out Mod _))
            {
                Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoRework"), Color.LimeGreen);
            }
            //if (!ModLoader.TryGetMod("SoABardHealer", out Mod _) && ModLoader.TryGetMod("SoA", out Mod _) && ModLoader.TryGetMod("ThoriumMod", out Mod _))
            //{
            //    Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoSoABardHealer1"), Color.Purple);
            //    Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoSoABardHealer2"), Color.Purple);
            //}
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (CyclonicFin)
            {
                target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 900);
                target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 900);

                if (hit.Crit && CyclonicFinCD <= 0 && proj.type != ModContent.ProjectileType<RazorbladeTyphoonFriendly>())
                {
                    CyclonicFinCD = 360;

                    float screenX = Main.screenPosition.X;
                    if (Player.direction < 0)
                        screenX += Main.screenWidth;
                    float screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                    Vector2 spawn = new Vector2(screenX, screenY);
                    Vector2 vel = target.Center - spawn;
                    vel.Normalize();
                    vel *= 27f;
                    Projectile.NewProjectile(proj.GetSource_FromThis(), spawn, vel, ModContent.ProjectileType<SpectralFishron>(), 300, 10f, proj.owner, target.whoAmI, 300);
                }
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
            throwerVelocity = 1f;
            CyclonicFin = false;

            //if (NoUsingItems > 0)
            //    NoUsingItems--;
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

    }
}
