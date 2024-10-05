using ssm.Content.Projectiles.Minions;
using ssm.Content.Buffs;
using ssm;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Buffs.Minions;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using FargowiltasSouls;

namespace ssm.Content.Buffs.Minions
{
    public class CelestialPowerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

            if (player.whoAmI == Main.myPlayer)
            {
                player.GetModPlayer<ShtunPlayer>().CelestialPower = true;
                const int damage = 1000;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<StardustP>()] < 1)
                    ShtunUtils.NewSummonProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ModContent.ProjectileType<StardustP>(), damage, 8f, player.whoAmI);
                if (player.ownedProjectileCounts[ModContent.ProjectileType<SolarP>()] < 1)
                    ShtunUtils.NewSummonProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ModContent.ProjectileType<SolarP>(), damage, 8f, player.whoAmI);
                if (player.ownedProjectileCounts[ModContent.ProjectileType<VortexP>()] < 1)
                    ShtunUtils.NewSummonProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ModContent.ProjectileType<VortexP>(), damage, 8f, player.whoAmI);
                if (player.ownedProjectileCounts[ModContent.ProjectileType<NebulaP>()] < 1)
                    ShtunUtils.NewSummonProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ModContent.ProjectileType<NebulaP>(), damage, 8f, player.whoAmI);
            }
        }
    }
}