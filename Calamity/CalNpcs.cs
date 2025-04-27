using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public partial class CalNpcs : GlobalNPC
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public override bool InstancePerEntity => true;
    }
}
