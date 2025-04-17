using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using ssm.Content.Items.Accessories;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Reworks
{
    public class SpeedCap : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<SupersonicSoul>() || entity.type == ModContent.ItemType<DimensionSoul>() || entity.type == ModContent.ItemType<EternitySoul>();
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (player.AddEffect<SpeedCapEffect>(item))
            {
                player.moveSpeed = 2f;
            }
            base.UpdateAccessory(item, player, hideVisual);
        }
        public class SpeedCapEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SupersonicHeader>();
            public override int ToggleItemType => ModContent.ItemType<SupersonicSoul>();
        }
    }
}
