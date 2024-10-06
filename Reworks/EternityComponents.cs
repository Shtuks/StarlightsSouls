using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Reworks
{
    public class EternityComponents : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<EternitySoul>())
            {
                if(ModLoader.TryGetMod("Redemption", out Mod Redemption))
                {
                    //ModContent.Find<ModItem>(((ModType)this).Mod.Name, "RedemptionSoul").UpdateAccessory(player, false);
                }

                if (ModLoader.TryGetMod("SacredTools", out Mod SoA))
                {
                    ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SoASoul").UpdateAccessory(player, false);
                }

                if (ModLoader.TryGetMod("CalamityMod", out Mod kal))
                {
                    ModContent.Find<ModItem>(((ModType)this).Mod.Name, "CalamitySoul").UpdateAccessory(player, false);
                }

                //if (ModLoader.TryGetMod("ContinentOfJourney", out Mod Homeward))
                //{
                //    ModContent.Find<ModItem>(((ModType)this).Mod.Name, "HomewardSoul").UpdateAccessory(player, false);
                //}

                //if (ModLoader.TryGetMod("Spirit", out Mod Spirit))
                //{
                //    ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SpiritSoul").UpdateAccessory(player, false);
                //}

                //if (ModLoader.TryGetMod("sots", out Mod sots))
                //{
                //    ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SotsSoul").UpdateAccessory(player, false);
                //}
            }
        }
    }
}
