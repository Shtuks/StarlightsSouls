using SacredTools.Content.Items.Accessories;
using ssm.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class VoidSpursToWings : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
        public override void VerticalWingSpeeds(Item item, Player player, ref float ascentWhenFalling, ref float ascentWhenRising,ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            if (item.type == ModContent.ItemType<VoidSpurs>())
            {
                ascentWhenFalling = 0.85f;
                ascentWhenRising = 0.15f;
                maxCanAscendMultiplier = 1.1f;
                maxAscentMultiplier = 2.7f;
                constantAscend = 0.135f;
            }
        }

        public override void SetStaticDefaults()
        {
            ArmorIDs.Wing.Sets.Stats[(int)ModContent.ItemType<VoidSpurs>()] = new WingStats(200, 9f, 2.75f, true, 11.6f, 11.6f);
        }
    }
}
