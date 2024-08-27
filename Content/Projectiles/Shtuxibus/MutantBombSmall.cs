using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using System;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Core.ModPlayers;

namespace ssm.Content.Projectiles.Shtuxibus
{
    public class MutantBombSmall : MutantBomb
    {
        public override string Texture => "Terraria/Images/Projectile_645";

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 275;
            Projectile.height = 275;
            Projectile.scale = 0.75f;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = false;
        }

        public override bool? CanDamage()
        {
            if (Projectile.frame > 2 && Projectile.frame <= 4)
            {
                Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().GrazeCD = 1;
                return false;
            }
            return true;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[0] = 1;
                Projectile.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
                SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            }

            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame--;
                    Projectile.Kill();
                }
            }
        }
    }
}