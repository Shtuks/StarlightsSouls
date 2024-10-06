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
using System.Linq;
using Terraria.GameContent.Creative;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Content.Projectiles;

namespace ssm
{
    public partial class ShtunPlayer : ModPlayer
    {
        public bool antiCollision;
        public bool antiDeath;
        public bool antiDebuff;
        public bool antiImmunity;
        public bool MutantSoul;
        public bool DevianttSoul;
        public bool CelestialPower;
        public int ShtuxibusDeaths;
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        public bool ShtuxibusMinionBuff;
        public bool ChtuxlagorBuff;
        public bool ChtuxlagorHeart;
        public bool ChtuxlagorInferno;
        public int Screenshake;
        public bool equippedPhantasmalEnchantment;
        public bool equippedAbominableEnchantment;
        public bool equippedDeviatingEnchantment;
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
        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            return !ChtuxlagorBuff;
        }
        public override bool CanBeHitByProjectile(Projectile proj)
        {
            return !ChtuxlagorBuff;
        }

        List<int> prevDyes = null;

        public virtual void PreUpdate()
        {
            if (this.antiDeath)
            {
                this.Player.ghost = false;
                this.Player.dead = false;
            }
        }
        public virtual void PostUpdateMiscEffects()
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
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("antiCollision", (object)this.antiCollision);
            tag.Add("antiDeath", (object)this.antiDeath);
            tag.Add("antiDebuff", (object)this.antiDebuff);
            tag["ShtuxibusDeaths"] = ShtuxibusDeaths;
        }
        public override void LoadData(TagCompound tag)
        {
            this.antiCollision = tag.GetBool("antiCollision");
            this.antiDeath = tag.GetBool("antiDeath");
            this.antiDebuff = tag.GetBool("antiDebuff");
            ShtuxibusDeaths = tag.GetInt("ShtuxibusDeaths");
        }
        private void OnHitNPCEither(NPC target, int damage, float knockback, bool crit, DamageClass damageClass, Projectile projectile = null, Item item = null)
        {
            //doing this so that damage-inheriting effects dont double dip or explode due to taking on crit boost
            int GetBaseDamage()
            {
                int baseDamage = damage;
                if (projectile != null)
                    baseDamage = (int)(projectile.damage * Player.ActualClassDamage(projectile.DamageType));
                else if (item != null)
                    baseDamage = (int)(item.damage * Player.ActualClassDamage(item.DamageType));
                return baseDamage;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (this.equippedPhantasmalEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "MutantFangBuff").Type, 1000, false);
            if (this.equippedDeviatingEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "DeviPresenceBuff").Type, 1000, false);
            if (!this.equippedAbominableEnchantment)
                return;
            target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "AbomFangBuff").Type, 1000, false);
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

            DevianttSoul = false;
            MutantSoul = false;
            ShtuxibusSoul = false;
            ChtuxlagorBuff = false;
            CelestialPower = false;

            //if (NoUsingItems > 0)
            //    NoUsingItems--;
        }
        public override void UpdateDead() { }
        public void WingStats() { Player.wingTimeMax = 999999; Player.wingTime = Player.wingTimeMax; Player.ignoreWater = true; }
        public override bool ImmuneTo(
        PlayerDeathReason damageSource,
        int cooldownCounter,
        bool dodgeable)
        {
            return this.Player == Main.LocalPlayer && this.antiCollision;
        }
        public override bool PreKill(
        double damage,
        int hitDirection,
        bool pvp,
        ref bool playSound,
        ref bool genGore,
        ref PlayerDeathReason damageSource)
        {
            return !this.antiDeath;
        }
    }
}
