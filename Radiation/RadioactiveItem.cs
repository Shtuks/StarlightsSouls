using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Radiation
{
    public abstract class RadioactiveItem : ModItem
    {
        //Variables
        public int irradiationSpeed = 0;

        public override void UpdateInventory(Player player)
        {
            player.Radiation().irradiationSpeed += irradiationSpeed;
        }

        public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Charge", irradiationSpeed + " RAD/tick"));
        }
    }
}
