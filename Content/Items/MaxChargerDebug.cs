using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items
{
    public class MaxChargerDebug : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 100;
            Item.value = 10000;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)
        {
            
            return true;
        }
    }
}