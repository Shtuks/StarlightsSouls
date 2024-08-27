using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles
{
    public class ShtunGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public int counter;
        public bool Rainbow;
        public int GrazeCD;
        public int DeletionImmuneRank;
    public Func<Projectile, bool> GrazeCheck = projectile =>
            projectile.Distance(Main.LocalPlayer.Center) < Math.Min(projectile.width, projectile.height) / 2 + Player.defaultHeight + Main.LocalPlayer.GetModPlayer<ShtunPlayer>().GrazeRadius
            && (projectile.ModProjectile == null ? true : projectile.ModProjectile.CanDamage() != false)
            && Collision.CanHit(projectile.Center, 0, 0, Main.LocalPlayer.Center, 0, 0);
    }
}

