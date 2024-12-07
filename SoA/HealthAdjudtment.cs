using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class HealthAdjudtment : GlobalNPC
    {
        public override void SetDefaults(NPC entity)
        {
            if (entity.type == ModContent.NPCType<Nihilus>())
            {

            }
        }
    }
}
