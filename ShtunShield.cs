using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace ssm
{
    public class ShtunShield : ModPlayer
    {
        //Changable variables
        public bool shieldOn; //Have you energy shield or no
        public bool drawShield; //Draw shield visual
        public float shieldRegenSpeed; //How many points shield regenerate per second
        public int shieldCapacityMax; //Maximum capacity of shield
        public int shieldCapacityMax2; //Maximum capacity of shield. Modify THIS to edit max shield
        public int shieldCapacity; //Current shield capacity
        public int shieldHits; //How many hits shield taken
        public bool noShieldHitsCap; //Remove hit cap of shield
        public float shieldHitsRegen; //Hits regeneration speed
        public int shieldHitsCap; //Maximum ammount of hits shield can take

        //Specific variables
        internal bool shieldAbsorbed; //Is shield fully absorbed damage
        internal int shieldCapacityMaxDefault = 0; //Maximum capacity of shield
        internal int shieldDamageBlocked; //How much shield absorbed before break
        internal int regenTimer = 0; //Timer for values regeneration

        public static readonly SoundStyle ShieldSound = new("ssm/Assets/Sounds/ShieldHit") {};
        public static readonly SoundStyle ShieldBreak = new("ssm/Assets/Sounds/ShieldBreak") {};
        public static readonly SoundStyle ShieldUP = new("ssm/Assets/Sounds/shield_up") {};

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            //Applying shield
            modifiers.ModifyHurtInfo += Player.Shield().ShtunModifyHurtInfo;

            //Shield sfx, hits counting
            if (shieldOn && shieldCapacity > 0)
            {
                modifiers.DisableSound();
                SoundEngine.PlaySound(ShieldSound, Player.Center);
                if (noShieldHitsCap){shieldHits++;}
            }
        }

        public override void Initialize(){shieldCapacityMax = shieldCapacityMaxDefault;}
        public override void PostUpdateMiscEffects(){UpdateShield();}
        public override void ResetEffects(){ResetVariables();}
        public override void UpdateDead(){ResetVariables();}

        private void ResetVariables()
        {
            //Reset shield
            shieldRegenSpeed = 1f;
            shieldCapacityMax2 = shieldCapacityMax;
            shieldHitsCap = 10;
            shieldOn = false;
            noShieldHitsCap = false;
            shieldHitsRegen = 0.3f;
        }

        public void ShtunModifyHurtInfo(ref Player.HurtInfo info)
        {
            //Shield count incoming damage
            if(shieldOn)
            {
                //Check if damage higher than shield capacity
                if (shieldCapacity > info.Damage)
                {
                    if (ssm.debug) {ShtunUtils.DisplayLocalizedText("Shield successfully blocked all damage", Color.White);}

                    shieldDamageBlocked = info.Damage;
                    shieldAbsorbed = true;
                    shieldCapacity = shieldCapacity - shieldDamageBlocked;
                    if (ssm.debug) { ShtunUtils.DisplayLocalizedText("Shield blocked " + shieldDamageBlocked, Color.White); }
                    shieldDamageBlocked = 0;
                }
                //Check if damage lower than shield capacity
                else if (shieldCapacity < info.Damage)
                {
                    if (ssm.debug) { ShtunUtils.DisplayLocalizedText("Shield absorbed part of damage", Color.White); }

                    SoundEngine.PlaySound(ShieldBreak, Player.Center);
                    info.Damage -= shieldCapacity;
                    shieldCapacity = 0;
                }
            }
        }

        public void InstaHeal(int? healAmount = null)
        {
            //Manualy heal shield
            if (shieldOn)
            {
                if (!healAmount.HasValue)
                {
                    healAmount = shieldCapacityMax2;
                }
                else 
                {
                    if (ssm.debug) { ShtunUtils.DisplayLocalizedText("Healed " + healAmount, Color.White); }
                    shieldCapacity += healAmount.Value; 
                }
            }
        }
        
        public void InstaKill(int? damageAmmount = null)
        {
            //Manualy deal damage to shield
            if (shieldOn)
            {
                if (!damageAmmount.HasValue)
                {
                    damageAmmount = shieldCapacityMax2;
                }
                else
                {
                    if (ssm.debug) { ShtunUtils.DisplayLocalizedText("Removed " + damageAmmount, Color.White); }
                    shieldCapacity -= damageAmmount.Value;
                }
            }
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            //Full damage absorbtion
            if (shieldAbsorbed)
            {
                if (ssm.debug) { ShtunUtils.DisplayLocalizedText("FreeDodge", Color.White); }
                shieldAbsorbed = false;
                return true;
            }
            return base.FreeDodge(info);
        }

        public void UpdateShield()
        {
            //Shield values regeneration
            if (shieldOn)
            {
                if(shieldCapacity == shieldCapacityMax)
                {
                    SoundEngine.PlaySound(ShieldUP, Player.Center);
                }

                regenTimer++; //increace 1 per tick.

                //Regenerate hits
                if (shieldHits > 0)
                {
                    if (regenTimer > 60 / shieldHitsRegen)
                    {
                        shieldHits -= 1;
                        regenTimer = 0;
                    }
                }

                //Regenerate capacity
                if (shieldHits < shieldHitsCap)
                {
                    if (regenTimer > 60 / shieldRegenSpeed)
                    {
                        shieldCapacity += 1;
                        regenTimer = 0;
                    }
                }

                //Cap max values
                shieldCapacity = Utils.Clamp(shieldCapacity, 0, shieldCapacityMax2);
            }
        }
    }
}
