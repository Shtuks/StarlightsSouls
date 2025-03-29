using Microsoft.Xna.Framework;
using ssm.Content.DamageClasses;
using Terraria;
using Terraria.ModLoader;

namespace ssm
{
    public class ShtunItems : GlobalItem
    {
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack)
        {

            if (item.CountsAsClass<UnitedModdedThrower>())
            {
                velocity *= player.Shtun().throwerVelocity;
            }
        }
    }
}
