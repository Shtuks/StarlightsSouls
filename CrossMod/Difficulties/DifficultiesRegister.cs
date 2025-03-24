using FargowiltasCrossmod.Core.Calamity;
using ssm.Core;
using Terraria.ModLoader;

namespace ssm.CrossMod.Difficulties
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.Crossmod.Name)]
    internal class DifficultiesRegister : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExperimentalContent;
        }
        public override void PostSetupContent()
        {
            Mod cal = ModCompatibility.Calamity.Mod;
            cal.Call("AddDifficultyToUI", new TrueEternityRevDifficulty());
            cal.Call("AddDifficultyToUI", new TrueEternityDeathDifficulty());
        }
    }
}
