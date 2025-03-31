using SacredTools.Content.Items.Accessories;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class YataMirrorEmodeNerf : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item entity)
        {
            if(entity.type == ModContent.ItemType<YataMirror>())
            {
                entity.damage = 300;
            }
        }
    }
}
