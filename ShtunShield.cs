using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using ssm.Content.Items.Accessories;
using CalamityMod.Items.Accessories;

namespace ssm
{
    public partial class ShtunShield : ModPlayer
    {
        //Changable variables
        public bool shieldOn; //Have you energy shield or no
        public bool drawShield; //Draw shield visual
        public float shieldRegenSpeed; //How many points shield regenerate per second
        public int shieldCapacityMax; //Maximum capacity of shield
        public int shieldCapacity; //Current shield capacity
        public int shieldHits; //How many hits shield taken
        public bool noShieldHitsCap; //Remove hit cap of shield
        public float shieldHitsRegen; //How many hits shield taken
        public int shieldHitsCap; //Maximum ammount of hits shield can take

        //Specific variables
        internal bool debug = true; //Debug mode
        internal bool shieldAbsorbed; //Is shield fully absorbed damage
        internal int shieldDamageBlocked; //How much shield absorbed before break
        internal int shieldCapacityRegenTimer = 0; //Timer for regeneration

        public static readonly SoundStyle ShieldSound = new("ssm/Assets/Sounds/ShieldHit") {};

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            //Applying shield
            modifiers.ModifyHurtInfo += Player.Shield().ShtunModifyHurtInfo;

            //Shield sfx, hits counting
            if (shieldOn && shieldCapacity > 0)
            {
                modifiers.DisableSound();
                SoundEngine.PlaySound(ShieldSound, Player.Center);
                shieldHits++;
            }
        }

        public void ShtunModifyHurtInfo(ref Player.HurtInfo info)
        {
            //Shield count incoming damage
            if(shieldOn)
            {
                //Check if damage higher than shield capacity
                if (shieldCapacity > info.Damage)
                {
                    if (debug) {ShtunUtils.DisplayLocalizedText("Shield successfully blocked all damage", Color.White);}

                    shieldDamageBlocked = info.Damage;
                    shieldAbsorbed = true;
                    shieldCapacity = shieldCapacity - shieldDamageBlocked;
                    shieldDamageBlocked = 0;
                }
                //Check if damage lower than shield capacity
                else if (shieldCapacity < info.Damage)
                {
                    if (debug) { ShtunUtils.DisplayLocalizedText("Shield absorbed part of damage", Color.White); }

                    info.Damage -= shieldCapacity;
                    shieldCapacity = 0;
                }
            }
        }

        public override void Initialize()
        {
            //Set basic shield values
            shieldHitsCap = 10;
            shieldRegenSpeed = 1;
        }

        public void InstaHeal(int healAmount)
        {
            //Manualy heal shield
            if (shieldOn)
            {
                if (healAmount > 0)
                {
                    shieldCapacity += healAmount;
                    if (debug) { ShtunUtils.DisplayLocalizedText("Healed" + healAmount, Color.White); }
                }
                else 
                {
                    if (debug) { ShtunUtils.DisplayLocalizedText("Shield capacity set to maximum", Color.White); }
                    shieldCapacity = shieldCapacityMax; 
                }
            }
        }
        public void InstaKill(int damageAmmount)
        {
            //Manualy deal damage to shield
            if (shieldOn)
            {
                if (damageAmmount > 0)
                {
                    shieldCapacity += damageAmmount;
                    if (debug) { ShtunUtils.DisplayLocalizedText("Removed" + damageAmmount, Color.White); }
                }
                else
                {
                    if (debug) { ShtunUtils.DisplayLocalizedText("Shield capacity set to 0", Color.White); }
                    shieldCapacity = 0;
                }
            }
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            //Full damage absorbtion
            if (shieldAbsorbed)
            {
                if (debug) { ShtunUtils.DisplayLocalizedText("FreeDodge", Color.White); }
                shieldAbsorbed = false;
                return true;
            }
            return base.FreeDodge(info);
        }

        public override void PostUpdateMiscEffects()
        {
            //Shield values regeneration
            if (shieldOn) 
            {
                //Regenerate hits and capacity
                shieldCapacityRegenTimer++; //increace 1 per tick.
                if (shieldCapacityRegenTimer > 60 / shieldRegenSpeed)
                {
                    shieldCapacity += 1;
                    shieldCapacityRegenTimer = 0;
                }

                //Cap max values
                shieldHits = Utils.Clamp(shieldHits, 0, shieldHitsCap);
                shieldCapacity = Utils.Clamp(shieldCapacity, 0, shieldCapacityMax);
            }
            else
            {
                //Reset shield
                noShieldHitsCap = false;
                shieldRegenSpeed = 0;
                shieldHits = 0;
                shieldHitsCap = 0;
                shieldHitsRegen = 0;
                shieldCapacityMax = 0;
                shieldCapacity = 0;
            }
        }

        public override void ResetEffects()
        {
            shieldOn = false;
        }
    }
}
