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

namespace ssm.Content.Buffs.Minions
{
    public class DeviSoulBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {

            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ShtunPlayer>().DevianttSoul = true;
            if (player.whoAmI == Main.myPlayer)
            {
                const int damage = 10000;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<DevianttSoul>()] < 1)
                    ShtunUtils.NewSummonProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ModContent.ProjectileType<DevianttSoul>(), damage, 19f, player.whoAmI);
            }
        }
    }
}