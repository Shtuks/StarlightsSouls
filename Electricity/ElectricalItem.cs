using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Electricity
{
    public class ElectricalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public bool canContainElectricity = false;
        public bool isCapacitor = false;
        public int chargePerUse = 0;
        public int charge = 0;
        public int chargeMax = 0;
        public int toBeFullGlobal = 0;
        public int mode = 0;
        public string modeSTR;

        public sealed override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (chargeMax > 0)
            {
                tooltips.Add(new TooltipLine(Mod, "Charge", "Charge: " + charge + " / " + chargeMax));
            }
            if (isCapacitor)
            {
                switch (mode)
                {
                    case 0: modeSTR = "Charge nothing."; break;
                    case 1: modeSTR = "Charge armor."; break;
                    case 2: modeSTR = "Charge held item."; break;
                    case 3: modeSTR = "Charge accessories."; break;
                    case 4: modeSTR = "Charge all."; break;
                    default: mode = 0; goto case 0;
                }
                tooltips.Add(new TooltipLine(Mod, "Mode", "Mode: " + modeSTR));
            }
        }

        public override void UpdateInventory(Item item, Player player)
        {
            if (canContainElectricity)
            {
                toBeFullGlobal = chargeMax - charge;
                charge = Utils.Clamp(charge, 0, chargeMax);
            }
            if (isCapacitor)
            {
                switch (mode)
                {
                    case 0: break;
                    case 1: break;
                    case 2: break;
                    case 3: break;
                    case 4: break;
                    default: mode = 0; goto case 0;
                }
            }
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (canContainElectricity)
            {
                toBeFullGlobal = chargeMax - charge;
                charge = Utils.Clamp(charge, 0, chargeMax);
            }
        }

        //public override bool CanUseItem(Item item, Player player)
        //{
        //    if (canContainElectricity)
        //    {
        //        int chargeNeed = chargePerUse;

        //        if (chargeNeed > 0f)
        //        {
        //            if (charge < chargeNeed)
        //                return false;

        //            if (player.CheckMana(item) && item.ModItem.CanUseItem(player))
        //                charge -= chargeNeed;
        //        }
        //    }
        //    return true;
        //}

        //public override bool? UseItem(Item item, Player player)
        //{
        //    if (isCapacitor)
        //    {
        //        mode++;
        //        if (mode >= 4) { mode = 0; }
        //    }
        //    return true;
        //}
    }
}
