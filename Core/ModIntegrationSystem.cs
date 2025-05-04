using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Systems
{
    public class ModIntergationSystem : ModSystem
    {
        public override void PostSetupContent()
        {
            if (ModCompatibility.HEROSMod.Loaded || ModCompatibility.Dragonlens.Loaded || ModCompatibility.CheatSheet.Loaded)
            {
                //PrivateClassEdits.LoadAntiCheats();
            }
        }
        public static class BossChecklist
        {
            public static void AdjustValues()
            {
                ModCompatibility.SoulsMod.Mod.BossChecklistValues["MutantBoss"] = int.MaxValue;
                ModCompatibility.SoulsMod.Mod.BossChecklistValues["AbomBoss"] = 22.98f;
                //if (ModCompatibility.Redemption.Loaded){
                //    ModCompatibility.Redemption.Mod.BossChecklistValues["Nebuleus"] = 20f;}
                //if (ModCompatibility.SacredTools.Loaded){
                //    ModCompatibility.SacredTools.Mod.BossChecklistValues["Nihilus"] = 24f;}
            }
        }
    }
}