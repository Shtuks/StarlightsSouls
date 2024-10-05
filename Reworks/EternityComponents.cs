using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Reworks
{
    public class EternityComponents : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<EternitySoul>())
            {
                ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SoASoul").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(((ModType)this).Mod.Name, "CalamitySoul").UpdateAccessory(player, false);
            }
        }
    }
}
