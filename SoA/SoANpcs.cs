using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using ssm.Systems;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public partial class SoANpcs : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void OnKill(NPC npc)
        {
            if(npc.type == ModContent.NPCType<Nihilus>() && !WorldSaveSystem.downedNihilus)
            {
                WorldSaveSystem.downedNihilus = true;
            }
        }
    }
}
