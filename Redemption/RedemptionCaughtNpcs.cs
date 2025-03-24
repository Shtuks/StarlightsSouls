using ssm.Items;
using Terraria.ModLoader;
using ssm.Core;
using Redemption.NPCs.Friendly.TownNPCs;
using Redemption.NPCs.Minibosses.Calavia;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    internal class RedemptionCaughtNpcs : ModSystem
    {
        public static void RedemptionRegisterItems()
        {
            CaughtNPCItem.Add("Fallen", ModContent.NPCType<Fallen>(), "'I am not very interested in talking, what ya want?'");
            CaughtNPCItem.Add("Fool", ModContent.NPCType<Newb>(), "'I can feel my memories arise from their deep slumber.'");
            CaughtNPCItem.Add("FriendlyTbot", ModContent.NPCType<TBot>(), "'Where I'm from, you rarely get to see anything beyond clouds.'");
            CaughtNPCItem.Add("Calavia", ModContent.NPCType<Calavia_NPC>(), "''");
            CaughtNPCItem.Add("Wayfarer", ModContent.NPCType<Daerel>(), "''");
        }
    }
}
