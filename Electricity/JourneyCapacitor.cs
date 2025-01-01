using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Electricity
{
    public class JourneyCapacitor : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.accessory = true;
            Item.Electricity().charge = int.MaxValue / 2;
            Item.Electricity().chargeMax = int.MaxValue;
            Item.Electricity().canContainElectricity = true;
            Item.Electricity().isCapacitor = true;
        }

        public override void UpdateEquip(Player player)
        {
            Item.Electricity().charge = int.MaxValue / 2;
        }
        public override void UpdateInventory(Player player)
        {
            Item.Electricity().charge = int.MaxValue / 2;
        }
    }
}
