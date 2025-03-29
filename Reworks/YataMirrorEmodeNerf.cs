using SacredTools.Content.Items.Accessories;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Reworks
{
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
