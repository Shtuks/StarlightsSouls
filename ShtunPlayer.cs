using Terraria.ID;
using Terraria.Audio;
using System.Collections.Generic;
using ssm.Content.Projectiles.Deathrays;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;
using ssm.Content.Buffs;
using ssm;
using Terraria.Graphics.Shaders;
using FargowiltasSouls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ssm.Content.Items.Accessories;
//using ssm.Content.Projectiles.AccesProjectiles;
using ReLogic.Content;
using System.IO;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Effects;
//using ssm.Buffs.SoulsEffects;
//using ssm.Content.Buffs.Minions;
using ssm.Content.Items;
using ssm.Content.Buffs.Anti;
using Terraria;
using ssm.Content.NPCs.StarlightCat;
using ssm.Core;
using ThoriumMod.Items.ZRemoved;

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
        public int frameCounter;
        public int ShtuxibusDeaths;
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        public bool ShtuxibusMinionBuff;
        public bool ChtuxlagorBuff;
        public bool ChtuxlagorHeart;
        public bool ChtuxlagorInferno;
        public int Screenshake;

        public int ChtuxlagorDeaths;
        public int ChtuxlagorLives;
        public int ChtuxlagorImmune;
        public int ChtuxlagorHealth;
        public int ChtuxlagorHits;

        public bool equippedPhantasmalEnchantment;
        public bool equippedAbominableEnchantment;
        public bool equippedNekomiEnchantment;
        public bool equippedShtuxianEnchantment;
        public bool ShtuxibusSoul;

        int timeru = 0;
        public bool DeviGraze;
        public bool Graze;
        public float GrazeRadius;
        public int DeviGrazeCounter;
        public double DeviGrazeBonus;
        private int WeaponUseTimer => Player.GetModPlayer<ShtunPlayer>().WeaponUseTimer;
        public bool DoubleTap
        {
            get
            {
                return Main.ReversedUpDownArmorSetBonuses ?
                    Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[1] > 0 && Player.doubleTapCardinalTimer[1] != 15
                    : Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[0] > 0 && Player.doubleTapCardinalTimer[0] != 15;
            }
        }
        public void TryAdditionalAttacks(int damage, DamageClass damageType)
        {
            if (Player.whoAmI != Main.myPlayer)
                return;
        }

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
            return !ChtuxlagorBuff || Player.HasBuff<ShtuxianDomination>();
        }
        public override bool CanBeHitByProjectile(Projectile proj)
        {
            return !ChtuxlagorBuff || Player.HasBuff<ShtuxianDomination>();
        }

        List<int> prevDyes = null;

        public override void PreUpdate()
        {
            if(ChtuxlagorHealth < 0)
            {
                ERASESOFT(Player);
                ShtunUtils.DisplayLocalizedText("Well, i hope we will meet again.", Color.Teal);
            }
            if (Player.HasBuff<ShtuxianDomination>() || ChtuxlagorBuff)
            {
                Player.ghost = false;
                Player.dead = false;
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if(Main.expertMode && NPC.AnyNPCs(ModContent.NPCType<StarlightCatBoss>()))
            {
                ChtuxlagorHits++;
            }
        }
        /*public override void PostUpdateMiscEffects()
        {
            if (this.antiCollision)
                this.Player.AddBuff(ModContent.BuffType<AntiCollision>(), 2, true, false);
            if (this.antiDeath)
                this.Player.AddBuff(ModContent.BuffType<AntiDeath>(), 2, true, false);
            if (this.antiDebuff)
            {
                this.Player.AddBuff(ModContent.BuffType<AntiDebuff>(), 2, true, false);
                for (int index = 0; index < BuffLoader.BuffCount; ++index)
                {
                    if (Main.debuff[index])
                    {
                        this.Player.buffImmune[index] = true;
                        this.Player.buffImmune[28] = false;
                        this.Player.buffImmune[34] = false;
                        this.Player.buffImmune[146] = false;
                        this.Player.buffImmune[87] = false;
                        this.Player.buffImmune[89] = false;
                        this.Player.buffImmune[48] = false;
                        this.Player.buffImmune[158] = false;
                        this.Player.buffImmune[215] = false;
                    }
                }
                if (this.Player.potionDelay > 0)
                    this.Player.potionDelay = 0;
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
        private void OnHitNPCEither(NPC target, int damage, float knockback, bool crit, DamageClass damageClass, Projectile projectile = null, Item Item = null)
        {
            int GetBaseDamage()
            {
                int baseDamage = damage;
                if (projectile != null)
                    baseDamage = (int)(projectile.damage * Player.ActualClassDamage(projectile.DamageType));
                else if (Item != null)
                    baseDamage = (int)(Item.damage * Player.ActualClassDamage(Item.DamageType));
                return baseDamage;
            }
        }

        public void ERASE(Player player)
        {
            player.ghost = true;
            player.dead = true;
            player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " got out of the scope of this pixel 2D game."), double.MaxValue, 10);
            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                    player.buffImmune[index] = false;
            }
        }
        public void ERASESOFT(Player player)
        {
            player.dead = true;
            player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " got out of the scope of this pixel 2D game."), double.MaxValue, 10);
            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                    player.buffImmune[index] = false;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //if (this.equippedPhantasmalEnchantment)
            //    target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "MutantFangBuff").Type, 1000, false);
            //if (this.equippedNekomiEnchantment)
            //    target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "DeviPresenceBuff").Type, 1000, false);
            //if (this.equippedAbominableEnchantment)
            //    target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "AbomFangBuff").Type, 1000, false);
            if (ChtuxlagorHeart)
                target.AddBuff(ModContent.Find<ModBuff>("ssm", "ChtuxlagorInferno").Type, 1000, false);
        }

        public override void ModifyScreenPosition()
        {
            if (Screenshake > 0)
                Main.screenPosition += Main.rand.NextVector2Circular(7, 7);
        }
        public void FlightMasterySoul()
        {
            Player.wingTimeMax = 999999;
            Player.wingTime = Player.wingTimeMax;
            Player.ignoreWater = true;
            Player.empressBrooch = true;
            Player.gravity = Player.defaultGravity * 1.5f;
            if (Player.controlDown && Player.controlJump && !Player.mount.Active)
            {
                Player.position.Y -= Player.velocity.Y;
                if (Player.velocity.Y > 0.1f)
                    Player.velocity.Y = 0.1f;
                else if (Player.velocity.Y < -0.1f)
                    Player.velocity.Y = -0.1f;
            }
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
            ChtuxlagorHits = 0;
        }
        public void WingStats() { Player.wingTimeMax = 999999; Player.wingTime = Player.wingTimeMax; Player.ignoreWater = true; }
        public override bool ImmuneTo(
        PlayerDeathReason damageSource,
        int cooldownCounter,
        bool dodgeable)
        {
            return this.Player == Main.LocalPlayer && Player.HasBuff<ShtuxianDomination>();
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
            }
            else
            {
                ChtuxlagorDeaths++;
            }

            return retVal;
        }

        public void ChtuxlagorDamage(Player player, int projDamage)
        {
            if (ChtuxlagorHealth <= 0f || ChtuxlagorImmune > 0 || Main.netMode == 2)
            {
                return;
            }
            int oldHealth = player.statLife;
            ChtuxlagorHealth -= projDamage;
            player.statLife = (int)(player.statLifeMax2 * ChtuxlagorHealth);
            int constDamage = (int)((200f - player.statDefense / 2f) * (1f - player.endurance));
            if (constDamage < 1)
            {
                constDamage = 1;
            }
            if (player.statLife > oldHealth - constDamage)
            {
                player.statLife = oldHealth - constDamage;
            }
            int damage = oldHealth - player.statLife;
            if (ChtuxlagorHealth > 0f && player.statLife > 0)
            {
                ChtuxlagorImmune = 60;
                if (!player.immune)
                {
                    player.immune = true;
                    player.immuneTime = 60;
                }
            }
            else if (Main.myPlayer == player.whoAmI)
            {
                if (ssm.debug)
                {
                    Main.NewText("I do not think this should be possible for now.");
                    ChtuxlagorHealth = 1;
                    player.statLife = 1;
                    ChtuxlagorImmune = 60;
                    return;
                }
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " was split to pieces of code!"), int.MaxValue, 10);
                player.mount.Dismount(player);
                player.dead = true;
                player.respawnTimer = 600;
                if (Main.expertMode)
                {
                    player.respawnTimer = 900;
                }
            }
        }

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
