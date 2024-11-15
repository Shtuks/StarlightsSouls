using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Electricity
{
    public abstract class BaseElectricalArmor : ModItem
    {
        //Variables
        public int chargePerUse = 0;
        public int maxCharge = 0;
        public int charge = 0;

        public override void UpdateEquip(Player player)
        {
            charge = Utils.Clamp(charge, 0, maxCharge);
        }

        //Move to baseelectricaltool
        //public override bool CanUseItem(Player player)
        //{
        //    int chargeNeed = chargePerUse;

        //    if (chargeNeed > 0f)
        //    {
        //        if (charge < chargeNeed)
        //            return false;

        //        if (player.CheckMana(Item) && Item.ModItem.CanUseItem(player))
        //            charge -= chargeNeed;
        //    }
        //    return true;
        //}

        public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Charge", "Charge: " + charge + " / " + maxCharge));
        }
    }
}
