using ssm.Items;
using Terraria.ModLoader;
using ssm.Core;
using SacredTools.Content.NPCs.Town;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    internal class SoACaughtNpcs : ModSystem
    {
        public static void SoARegisterItems()
        {
            CaughtNPCItem.Add("PureNymph", ModContent.NPCType<RNGNymph>(), "''");
            CaughtNPCItem.Add("Scavenger", ModContent.NPCType<Scavenger>(), "'Over in oz we got the craziest stuff! Heard of kanga? A cockie? Maybe even a dingie?'");
            CaughtNPCItem.Add("PandolarSalvager", ModContent.NPCType<Pandolar>(), "'Je lowu all teshat bomus.'");
            CaughtNPCItem.Add("Decorationist", ModContent.NPCType<Decorationist>(), "'Don't underestimate nails, they can be quite powerful if used right.'");
            CaughtNPCItem.Add("CloakedAlchemist", ModContent.NPCType<Neil>(), "'How high am I? Uh, 6 foot I think'");
        }
    }
}
