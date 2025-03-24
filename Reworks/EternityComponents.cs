using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.Calamity.Addons;
using ssm.Calamity.Souls;
using ssm.Content.Items.Accessories;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Reworks
{
    public class EternityComponents : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<ShtuxianSoul>())
            {
                if (ModLoader.TryGetMod("ThoriumMod", out Mod tor))
                {
                    ModContent.Find<ModItem>(((ModType)this).Mod.Name, "GuardianAngelsSoul").UpdateAccessory(player, false);
                    ModContent.Find<ModItem>(((ModType)this).Mod.Name, "BardSoul").UpdateAccessory(player, false);
                }
            }
            if (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<CalamitySoul>() || Item.type == ModContent.ItemType<ShtuxianSoul>())
            {
                if (ModCompatibility.Entropy.Loaded && ModCompatibility.Clamity.Loaded && ModCompatibility.Goozma.Loaded && ModCompatibility.Catalyst.Loaded)
                {
                    ModContent.Find<ModItem>(this.Mod.Name, "AddonsForce").UpdateAccessory(player, false);
                }
                if (ModCompatibility.WrathoftheGods.Loaded)
                {
                    ModContent.GetInstance<SolynsSigil>().UpdateAccessory(player, hideVisual);
                }
                if (ModCompatibility.Crossmod.Loaded)
                {
                    ModContent.GetInstance<GaleForce>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<ElementsForce>().UpdateAccessory(player, hideVisual);
                }
            }
            if (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<ShtuxianSoul>())
            {
                if (ModLoader.TryGetMod("Redemption", out Mod Redemption))
                {
                    //ModContent.Find<ModItem>(((ModType)this).Mod.Name, "RedemptionSoul").UpdateAccessory(player, false);
                }

                if (ModLoader.TryGetMod("SacredTools", out Mod SoA))
                {
                    ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SoASoul").UpdateAccessory(player, false);
                }

                if (ModLoader.TryGetMod("CalamityMod", out Mod kal) && ModLoader.TryGetMod("FargowiltasCrossmod", out Mod FargoIhateYou))
                {
                    ModContent.Find<ModItem>(((ModType)this).Mod.Name, "CalamitySoul").UpdateAccessory(player, false);
                }

                if (ModLoader.TryGetMod("ThoriumMod", out Mod tor))
                {
                    ModContent.Find<ModItem>(((ModType)this).Mod.Name, "ThoriumSoul").UpdateAccessory(player, false);
                }
            }
        }
    }
}