using ssm.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ssm.Content.Buffs;
using ssm;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ssm.Content.Buffs.Minions;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls;

namespace ssm.Content.Buffs.Minions
{
    public class ShtuxibusMinionBuff : ModBuff
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        int RitualDM = 0;
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ShtunPlayer>().ShtuxibusMinionBuff = true;
            if (player.whoAmI == Main.myPlayer)
            {
                const int damage = 1000;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<ShtuxibusSoulMinion>()] < 1)
                    ShtunUtils.NewSummonProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ModContent.ProjectileType<ShtuxibusSoulMinion>(), damage, 19f, player.whoAmI, 0f, -1f);
                //if (player.ownedProjectileCounts[ModContent.ProjectileType<PMR>()] < 1)
                //ShtunUtils.NewSummonProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ModContent.ProjectileType<PMR>(), RitualDM, 19f, player.whoAmI);
                /*if (player.GetModPlayer<ShtunPlayer>().EternityThing){
                    player.GetDamage(DamageClass.Generic) += 100f;
                    player.GetCritChance(DamageClass.Generic) += 32767;
                    player.maxMinions += 900;
                    player.maxTurrets += 900;
                    player.manaCost -= 0.9f;
                    player.ammoCost75 = true;
                    player.ghost = false;
                    //RitualDM = 1000000;
                    player.statLifeMax2 += 550000;
                    player.statManaMax2 += 999;
                    player.endurance += 0.9f;
                    player.lifeRegen += 10;
                    player.lifeRegenCount += 10;
                    player.lifeRegenTime += 10;
                    player.moveSpeed += 0.9f;
                    player.immune = true;
                    player.immuneNoBlink = true;
                    player.immuneTime = 20;
                    player.noFallDmg = true;
                    player.buffImmune[46] = true;
                    player.buffImmune[24] = true;
                    player.buffImmune[68] = true;
                    player.buffImmune[37] = true;
                    player.buffImmune[21] = true;
                    player.buffImmune[47] = true;
                    player.buffImmune[94] = true;
               }
                 else 
               {
                    player.GetDamage(DamageClass.Melee) += 2f;
                    player.GetCritChance(DamageClass.Magic) += 50;
                    player.GetCritChance(DamageClass.Ranged) += 50;
                    player.maxMinions += 5;
                    player.maxTurrets += 5;
                    player.manaCost -= 0.3f;
                    player.ammoCost75 = true;
                    player.ghost = false;
                    player.statLifeMax2 += 200;
                    player.statManaMax2 += 200;
                    player.endurance += 0.3f;}*/
            }
        }
    }
}
