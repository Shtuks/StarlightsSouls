using CalamityMod.NPCs.TownNPCs;
using ssm.Items;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    internal class CalCaughtNpcs : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.CalCaughtNpcs;
        }
        public static void CalRegisterItems()
        {
            CaughtNPCItem.Add("BrimstoneWitch", ModContent.NPCType<WITCH>(), "'I love smell of brimstone at the morning.'");
            CaughtNPCItem.Add("Archmage", ModContent.NPCType<DILF>(), "'You have the makings of a good mage. What do you think about ice magic?'");
            CaughtNPCItem.Add("YharimWhere", ModContent.NPCType<FAP>(), "'I don't mind company but I'd rather be alone at night.'");
            CaughtNPCItem.Add("SeaKing", ModContent.NPCType<SEAHOE>(), "'To see Tyrant's serpent free of its shackles. It gave me chills.'");
            CaughtNPCItem.Add("Bandit", ModContent.NPCType<THIEF>(), "'Listen here. It's all in the wrist, in the wrist. Oh, forget it.'");
        }
    }
}
