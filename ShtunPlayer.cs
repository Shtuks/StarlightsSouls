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
using FargowiltasSouls.Core.Globals;
using CalamityMod.Items.Weapons.Magic;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;

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
        public bool DevianttSoul;
        public bool CelestialPower;
        public int frameShtuxibusAura;
        public int ShtuxibusSetBonusItem;
        public int frameCounter;
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
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (ssm.shtuxianSuper.JustPressed)
            {
                int duration = 0;

                if (shtuxianSoul)
                    duration = 5;
                if (ChtuxlagorHeart)
                    duration = 10;

                Player.AddBuff(ModContent.BuffType<ShtuxianDomination>(), duration * 60);

                //SoundEngine.PlaySound(new SoundStyle("ssm/Assets/Sounds/ShtuxianSuper"), Player.Center);
            }
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
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
            if (Player.HasBuff<ShtuxianDomination>() || ChtuxlagorBuff)
            {
                Player.ghost = false;
                Player.dead = false;
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
        }
        public override void LoadData(TagCompound tag)
        {
            ShtuxibusDeaths = tag.GetInt("ShtuxibusDeaths");
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (this.equippedPhantasmalEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "MutantFangBuff").Type, 1000, false);
            if (this.equippedDeviatingEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "DeviPresenceBuff").Type, 1000, false);
            if (this.equippedAbominableEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "AbomFangBuff").Type, 1000, false);
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

            DevianttSoul = false;
            MutantSoul = false;
            ShtuxibusSoul = false;
            //ChtuxlagorBuff = false;
            CelestialPower = false;

            //if (NoUsingItems > 0)
            //    NoUsingItems--;
        }
        public override void OnEnterWorld()
        {
            if (ShtunConfig.Instance.WorldEnterMessage)
            {
                ShtunUtils.DisplayLocalizedText("WARNING! In this update Shtuxibus, Echdeath and their items was moved to Extra Content.", Color.LimeGreen);
                ShtunUtils.DisplayLocalizedText("All fargo expansions like souls, swarms and caught npc now also switchable for every mod.", Color.LimeGreen);
                ShtunUtils.DisplayLocalizedText("You can toggle content in server-side mod settings. This will require reload of game.", Color.LimeGreen);
                ShtunUtils.DisplayLocalizedText("This message also can be toggled off in settings.", Color.LimeGreen);
                ShtunUtils.DisplayLocalizedText("                                                                      - StarlightCat", Color.LimeGreen);
            }
        }

        public override void UpdateDead() {}
        public void WingStats() { Player.wingTimeMax = 999999; Player.wingTime = Player.wingTimeMax; Player.ignoreWater = true; }
        public override bool ImmuneTo(
        PlayerDeathReason damageSource,
        int cooldownCounter,
        bool dodgeable)
        {
            return this.Player == Main.LocalPlayer && Player.HasBuff<ShtuxianDomination>();
        }
        public override bool PreKill(
        double damage,
        int hitDirection,
        bool pvp,
        ref bool playSound,
        ref bool genGore,
        ref PlayerDeathReason damageSource)
        {
            return !Player.HasBuff<ShtuxianDomination>();
        }
    }
}
