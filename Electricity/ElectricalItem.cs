using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

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
        public int toBeFull = 0;
        public int mode = 0;
        public string modeSTR;

        public sealed override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (canContainElectricity)
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
                }
                tooltips.Add(new TooltipLine(Mod, "Mode", "Mode: " + modeSTR));
            }
        }

        public override void UpdateInventory(Item item, Player player)
        {
            if (canContainElectricity || isCapacitor)
            {
                toBeFull = chargeMax - charge;
                charge = Utils.Clamp(charge, 0, chargeMax);
            }
            if (isCapacitor)
            {
                if (mode == 2 && !player.HeldItem.IsAir)
                {
                    chargeFromCapacitor(player.HeldItem, item);
                }
                else if (mode == 3)
                {
                    //chargeFromCapacitor(player.HeldItem, item);
                }
                else if (mode == 4)
                {
                    //chargeFromCapacitor(player.HeldItem, item);
                }
            }
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (canContainElectricity || isCapacitor)
            {
                toBeFull = chargeMax - charge;
                charge = Utils.Clamp(charge, 0, chargeMax);
            }
            if (isCapacitor)
            {
                if (mode == 2 && !player.HeldItem.IsAir)
                {
                    chargeFromCapacitor(player.HeldItem, item);
                }
                else if (mode == 3)
                {
                    //chargeFromCapacitor(player.HeldItem, item);
                }
                else if (mode == 4)
                {
                    //chargeFromCapacitor(player.HeldItem, item);
                }
            }
        }

        public void chargeFromCapacitor(Item itemToCharge, Item capacitor)
        {
            if(itemToCharge.Electricity().charge < itemToCharge.Electricity().chargeMax && itemToCharge != capacitor && capacitor.Electricity().charge > 0)
            {
                if(itemToCharge.Electricity().toBeFull > capacitor.Electricity().charge)
                {
                    itemToCharge.Electricity().charge += capacitor.Electricity().charge;
                    capacitor.Electricity().charge = 0;
                }
                else if (itemToCharge.Electricity().toBeFull < capacitor.Electricity().charge)
                {
                    capacitor.Electricity().charge -= itemToCharge.Electricity().toBeFull;
                    itemToCharge.Electricity().charge = itemToCharge.Electricity().chargeMax;
                }

                //if (ssm.debug) { ShtunUtils.DisplayLocalizedText("Charging."); }
            }
            else
            {
                //if(ssm.debug) { ShtunUtils.DisplayLocalizedText("Item can't be charged or already fully charged.");}
            }
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            charge = tag.GetInt("chargeShtun");
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            tag["chargeShtun"] = charge;
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(charge);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            charge = reader.Read();
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
        //    return base.CanUseItem(item, player);
        //}

        public override bool? UseItem(Item item, Player player)
        {
            if (isCapacitor)
            {
                mode++;
                if (mode == 4) { mode = 0; }
            }
            return base.UseItem(item, player);
        }
    }
}
