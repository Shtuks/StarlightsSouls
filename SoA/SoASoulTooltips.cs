using FargowiltasSouls.Content.Items.Accessories.Souls;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class SoASoulsTooltips : GlobalItem
    {
        static string ExpandedTooltipLoc(string line) => Language.GetTextValue($"Mods.ssm.ExpandedTooltips.{line}");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.ssm.Items.AddedEffects.";

            if (item.type == ModContent.ItemType<ColossusSoul>() && !item.social)
            {
                tooltips.Insert(8, new TooltipLine(Mod, "SoAColossusSoul", Language.GetTextValue(key + "SoAColossus")));
            }

            if (item.type == ModContent.ItemType<SupersonicSoul>() && !item.social)
            {
                tooltips.Insert(8, new TooltipLine(Mod, "SoASupersonicSoul", Language.GetTextValue(key + "SoASupersonic")));
            }

            if (item.type == ModContent.ItemType<BerserkerSoul>() && !item.social)
            {
                tooltips.Insert(9, new TooltipLine(Mod, "SoABerserkerSoul", Language.GetTextValue(key + "SoABerserker")));
            }

            if (item.type == ModContent.ItemType<MasochistSoul>() && !item.social)
            {
                tooltips.Insert(9, new TooltipLine(Mod, "SoAMasoSoul", Language.GetTextValue(key + "SoAMaso")));
            }

            //if (item.type == ModContent.ItemType<ArchWizardsSoul>() && !item.social)
            //{
            //    tooltips.Insert(8, new TooltipLine(Mod, "SoAWizardSoul", Language.GetTextValue(key + "SoAWizard")));
            //}

            //if (item.type == ModContent.ItemType<SnipersSoul>() && !item.social)
            //{
            //    tooltips.Insert(8, new TooltipLine(Mod, "SoASniperSoul", Language.GetTextValue(key + "SoASniper")));
            //}

            //if (item.type == ModContent.ItemType<ConjuristsSoul>() && !item.social)
            //{
            //    tooltips.Insert(7, new TooltipLine(Mod, "SoAConjurSoul", Language.GetTextValue(key + "SoAConjurist")));
            //}

            if (item.type == ModContent.ItemType<UniverseSoul>() && !item.social)
            {
                tooltips.Insert(15, new TooltipLine(Mod, "SoAUniverseSoul",
                    Language.GetTextValue(key + "SoABerserker")// + "\n" +
                    //Language.GetTextValue(key + "SoASniper") + "\n" +
                    //Language.GetTextValue(key + "SoAWizard") + "\n" +
                    //Language.GetTextValue(key + "SoAConjurist")
                    ));
            }

            if (item.type == ModContent.ItemType<DimensionSoul>() && !item.social)
            {
                tooltips.Insert(15, new TooltipLine(Mod, "SoADimestionSoul",
                    //Language.GetTextValue(key + "SoATrawler") + "\n" +
                    Language.GetTextValue(key + "SoASupersonic") + "\n" +
                    Language.GetTextValue(key + "SoAColossus") + "\n" +
                   // Language.GetTextValue(key + "SoAFlight") + "\n" +
                    Language.GetTextValue(key + "SoAWorldshaper")
                    ));
            }
        }
    }
}
