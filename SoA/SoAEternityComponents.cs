using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using SacredTools.Content.Items.Accessories;
using ssm.Content.Items.Accessories;
using ssm.Core;
using ssm.SoA.Toggles;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAEternityComponents : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
			if (item.type == ModContent.ItemType<ColossusSoul>() || item.type == ModContent.ItemType<DimensionSoul>() || item.type == ModContent.ItemType<EternitySoul>() || item.type == ModContent.ItemType<Soul>())
			{
				if (player.AddEffect<RoyalGuardEffect>(item))
				{
				    ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "RoyalGuard").UpdateAccessory(player, hideVisual);
				}
				ModContent.GetInstance<NightmareBlindfold>().UpdateAccessory(player, hideVisual);
			}
            if (item.type == ModContent.ItemType<WorldShaperSoul>() || item.type == ModContent.ItemType<DimensionSoul>() || item.type == ModContent.ItemType<EternitySoul>() || item.type == ModContent.ItemType<Soul>())
            {
                ModContent.GetInstance<LunarRing>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<RageSuppressor>().UpdateAccessory(player, hideVisual);
            }
            if (item.type == ModContent.ItemType<SupersonicSoul>() || item.type == ModContent.ItemType<DimensionSoul>() || item.type == ModContent.ItemType<EternitySoul>() || item.type == ModContent.ItemType<Soul>())
            {
                if (player.AddEffect<MilinticaDashEffect>(item))
                {
                    ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "MilinticaDash").UpdateAccessory(player, hideVisual);
                }
				if (player.AddEffect<HeartOfThePloughEffect>(item))
				{
                    ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "HeartOfThePlough").UpdateAccessory(player, hideVisual);
                }
			}
            if (item.type == ModContent.ItemType<MasochistSoul>() || item.type == ModContent.ItemType<EternitySoul>() || item.type == ModContent.ItemType<Soul>())
            {
				if (player.AddEffect<YataMirrorEffect>(item))
                {
                    ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "YataMirror").UpdateAccessory(player, hideVisual);
                }
                if (player.AddEffect<PrimordialCoreEffect>(item))
                {
                    ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "PrimordialCore").UpdateAccessory(player, hideVisual);
                }
            }
            if (item.type == ModContent.ItemType<BerserkerSoul>() || item.type == ModContent.ItemType<UniverseSoul>() || item.type == ModContent.ItemType<EternitySoul>() || item.type == ModContent.ItemType<Soul>())
            {
                if (player.AddEffect<FloraFistEffect>(item))
                {
                    ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "FloraFist").UpdateAccessory(player, hideVisual);
                }
            }

		}
    }
}