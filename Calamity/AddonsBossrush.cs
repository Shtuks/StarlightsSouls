using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using Terraria.ID;
using CalamityMod.NPCs.AstrumDeus;
using CatalystMod.NPCs.Boss.Astrageldon;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Catalyst.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Catalyst.Name)]
    public class AddonsBossRush : ModSystem
    {
        [JITWhenModsEnabled("CatalystMod")]
        public override void PostSetupContent()
        {
            for (int i = Bosses.Count - 1; i >= 0; i--)
            {
                if (Bosses[i].EntityID == ModContent.NPCType<AstrumDeusHead>())
                {
                    Bosses.Add(new Boss(ModContent.NPCType<Astrageldon>()));
                }
            }

            //Adding bosses to boss rush
            Mod cal = ModCompatibility.Calamity.Mod;
        }
    }
}