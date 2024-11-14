using ssm.Content.Projectiles.Minions;
using ssm;
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
using ssm.Core;
using SacredTools.Projectiles.Lunar;
using ssm.Thorium.Enchantments;

namespace ssm.Content.Buffs.Minions
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class StellarGuardianBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {

            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                const int damage = 1000;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<StellarGuardian>()] < 1)
                    ShtunUtils.NewSummonProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ModContent.ProjectileType<StellarGuardian>(), damage, 8f, player.whoAmI);
            }
        }
    }
}