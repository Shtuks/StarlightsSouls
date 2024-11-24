using ssm.Content.Projectiles.Minions;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Buffs.Minions
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class YharonBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<YharonMinion>()] > 0)
            {
                player.Shtun().yharon = true;
            }
            if (!player.Shtun().yharon)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}
