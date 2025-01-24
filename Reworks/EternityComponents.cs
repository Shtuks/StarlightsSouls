using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.Calamity.Souls;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Reworks
{
    public class EternityComponents : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>())
            {
                if (ModLoader.TryGetMod("ThoriumMod", out Mod tor))
                {
                    ModContent.Find<ModItem>(((ModType)this).Mod.Name, "GuardianAngelsSoul").UpdateAccessory(player, false);
                    ModContent.Find<ModItem>(((ModType)this).Mod.Name, "BardSoul").UpdateAccessory(player, false);
                }
            }
            if (Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<CalamitySoul>())
            {
                ModContent.Find<ModItem>(this.Mod.Name, "AddonsForce").UpdateAccessory(player, false);
            }
            if (Item.type == ModContent.ItemType<EternitySoul>())
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

            if (Item.type == ModContent.ItemType<CosmoForce>() || Item.type == ModContent.ItemType<EternitySoul>())
            {
                ModContent.Find<ModItem>(((ModType)this).Mod.Name, "CelestialEnchant").UpdateAccessory(player, false);
            }
        }
    }
}