using CalamityMod.Items.SummonItems;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class CalDlcItems : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<Terminus>())
            {
                tooltips.Clear();
                tooltips.Add(new TooltipLine(Mod, "Name", $"Terminus"));
                tooltips.Add(new TooltipLine(Mod, "PreMutant", $"[c/FF0000:StarlightCat's Tweaks:] Can be used before defeating the Mutant"));
            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<Terminus>())
                return true;

            return true;
        }

    }
}
