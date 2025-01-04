using FargowiltasSouls.Content.Items.Accessories.Souls;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    public class CalTorTooltips : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.ssm.Items.AddedEffects.";

            if (item.type == ModContent.ItemType<MasochistSoul>() && !item.social)
            {
                tooltips.Insert(9, new TooltipLine(Mod, "CalTerrariumDefender", Language.GetTextValue(key + "CalDefender")));
            }
        }
    }
}
