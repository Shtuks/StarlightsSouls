using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Electricity
{
    public class ShtuxiumCapacitor : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.value = 100000;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.accessory = true;
            Item.Electricity().chargeMax = 1000000000;
            Item.Electricity().canContainElectricity = true;
            Item.Electricity().isCapacitor = true;
        }
    }
}
