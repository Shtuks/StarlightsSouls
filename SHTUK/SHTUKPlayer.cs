using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.IO;
using ssm.Content.Buffs;

namespace ssm.SHTUK
{
    public class SHTUKPlayer : ModPlayer
    {
        public bool isCyborg = false;
        public bool isAdvancedCyborg = false;
        public bool isShtuxiumCyborg = false;

        public float generalMult;
        public float damageMult;
        public float speedMult;

        public int defenceAdd;
        public int regenAdd;
        public int lifeAdd;
        public int critAdd;
        public int manaAdd;
        public int penetrationAdd;
        public int miningAdd;
        public int sentryAdd;
        public int minionsAdd;
        public int manaRegAdd;
        public int shieldAdd;
        public int shieldRegAdd;
        public float drAdd;

        public int maxOverload = 1000;
        public int overload;

        public int energy;
        public int energyConsumption;
        public int energyRegen;
        public int energyRegenCharging;
        internal int regenTimer = 0;
        public int energyMax;
        public int energyMax2;
        public float energyMaxMult;
        public int baseEnergyMax = 1000;


        public override void Initialize() { energyMax = baseEnergyMax; }
        public override void PostUpdateMiscEffects() { Update(); }
        public override void ResetEffects() { ResetVariables(); }
        public override void UpdateDead() { ResetVariables(); }

        public void ResetVariables()
        {
            energyMax2 = energyMax;
            energyRegen = 0;
            energyConsumption = 0;
            energyRegenCharging = 1;

            generalMult = 1f;
            damageMult = 0f;
            speedMult = 0f;

            defenceAdd = 0;
            penetrationAdd = 0;
            regenAdd = 0;
            manaAdd = 0;
            manaRegAdd = 0;
            drAdd = 0;
            minionsAdd = 0;
            sentryAdd = 0;
            shieldAdd = 0;
            shieldRegAdd = 0;
            critAdd = 0;
            lifeAdd = 0;
        }

        public void Update()
        {
            if (isCyborg)
            {
                Player.AddBuff(ModContent.BuffType<CyborgBuff>(), 10, false);

                removeEnergy(energyConsumption);
                addEnergy(energyRegen);

                energy = Utils.Clamp(energy, 0, energyMax2);

                if (energy > 0)
                {
                    Player.statDefense += defenceAdd;
                    Player.lifeRegen += regenAdd;
                    Player.endurance += drAdd;
                    Player.statManaMax2 += manaAdd;
                    Player.GetArmorPenetration<GenericDamageClass>() += penetrationAdd;
                    Player.maxMinions += minionsAdd;
                    Player.maxTurrets += sentryAdd;
                    Player.pickSpeed -= miningAdd;
                    Player.manaRegen += manaRegAdd;
                    Player.statLifeMax2 += lifeAdd;
                    Player.GetCritChance<GenericDamageClass>() += critAdd;
                    Player.Shield().shieldCapacityMax2 += shieldAdd;
                    Player.Shield().shieldRegenSpeed += shieldRegAdd;
                    Player.GetDamage<GenericDamageClass>() += damageMult;
                    Player.moveSpeed += speedMult;

                    if (generalMult > 1)
                    {
                        Player.manaRegen *= (int)generalMult;
                        Player.statDefense *= generalMult;
                        Player.lifeRegen *= (int)generalMult;
                        Player.endurance *= generalMult;
                        Player.GetArmorPenetration<GenericDamageClass>() *= generalMult;
                        Player.maxMinions *= (int)generalMult;
                        Player.maxTurrets *= (int)generalMult;
                        Player.statLifeMax2 *= (int)generalMult;
                        Player.statManaMax2 *= (int)generalMult;
                        Player.GetCritChance<GenericDamageClass>() *= generalMult;
                        Player.Shield().shieldCapacityMax2 *= (int)generalMult;
                        Player.Shield().shieldRegenSpeed *= generalMult;
                        Player.GetDamage<GenericDamageClass>() *= generalMult;
                        Player.moveSpeed *= generalMult;
                    }
                }
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag["isCyborg"] = isCyborg;
            tag["isAdvancedCyborg"] = isAdvancedCyborg;
            tag["isShtuxiumCyborg"] = isShtuxiumCyborg;
        }
        public override void LoadData(TagCompound tag)
        {
            isCyborg = tag.GetBool("isCyborg");
            isAdvancedCyborg = tag.GetBool("isAdvancedCyborg");
            isShtuxiumCyborg = tag.GetBool("isShtuxiumCyborg");
        }


        public void addEnergy(int? energy2)
        {
            if (isCyborg)
            {
                if (!energy2.HasValue)
                {
                    energy = energyMax2;
                }
                else
                {
                    energy += energy2.Value;
                }
            }
        }

        public void removeEnergy(int? energy2)
        {
            if (isCyborg)
            {
                if (!energy2.HasValue)
                {
                    energy = 0;
                }
                else
                {
                    energy -= energy2.Value;
                }
            }
        }
    }
}