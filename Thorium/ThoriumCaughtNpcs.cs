using ssm.Items;
using Terraria.ModLoader;
using ssm.Core;
using ThoriumMod.NPCs;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    internal class ThoriumCaughtNpcs : ModSystem
    {
        public static void ThoriumRegisterItems()
        {
            CaughtNPCItem.Add("Blacksmith", ModContent.NPCType<Blacksmith>(), "'Welcome! I'm busy workin' on a commission weapon, if you touch anything, consider it sold!'");
            CaughtNPCItem.Add("Cook", ModContent.NPCType<Cook>(), "'I've got a lot of orders from the other townsfolk right now, make it quick!'");
            CaughtNPCItem.Add("Cobbler", ModContent.NPCType<Cobbler>(), "'You can learn a lot by walking a mile in someone else's boots. If only some of these townsfolk would walk a mile in yours, then they could understand...'");
            CaughtNPCItem.Add("ConfusedZombie", ModContent.NPCType<ConfusedZombie>(), "'I gotta thank you for letting me stick around. Only coming out at night is pretty boring...'");
            CaughtNPCItem.Add("DesertAcolyte", ModContent.NPCType<DesertAcolyte>(), "'We certainly have some strange people around this town, don't we...?'");
            CaughtNPCItem.Add("Diverman", ModContent.NPCType<Diverman>(), "'Have you ever try to drink a bottle of water, while drowning, to save yourself? I know I have!'");
            CaughtNPCItem.Add("Druid", ModContent.NPCType<Druid>(), "'The natural world is quite a wonder. Every little place has its own charm. Some more than others!'");
            CaughtNPCItem.Add("Spiritualist", ModContent.NPCType<Spiritualist>(), "'Energy, chi, spirit... it's all the same to me. There is power in all things'");
            CaughtNPCItem.Add("Tracker", ModContent.NPCType<Tracker>(), "'Seen any interesting beasts lurking around lately? Minotaurs? Beholders? Gorgons?'");
            CaughtNPCItem.Add("WeaponMaster", ModContent.NPCType<WeaponMaster>(), "'Should you have the coin, I would be willing to accompany you in your endeavors...'");
        }
    }
}
