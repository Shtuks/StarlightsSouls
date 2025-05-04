using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ssm.Systems
{
    public class WorldSaveSystem : ModSystem
    {
        public static bool talk = false;

        //i am dumb or soa don't have this
        public static bool downedNihilus = false;

        public static bool downedEch = false;
        public static bool downedFish = false;
        public static bool downedMutantEX = false;

        public static bool enragedMutantEX = false;

        public static bool TrueSuperChtuxlagorWrathModeOmegaEX = false;
        public static bool trueRevEternity = false;
        public static bool trueDeathEternity = false;

        public override void SaveWorldData(TagCompound tag)
        {
            if (downedEch)
            {
                tag["downedEch"] = true;
            }
            if (talk)
            {
                tag["talk"] = true;
            }
            if (downedNihilus)
            {
                tag["downedNihilus"] = true;
            }
            if (downedFish)
            {
                tag["downedFish"] = true;
            }
            if (TrueSuperChtuxlagorWrathModeOmegaEX)
            {
                tag["TrueSuperChtuxlagorWrathModeOmegaEX"] = true;
            }
            if (enragedMutantEX)
            {
                tag["enragedMutantEX"] = true;
            }
            if (trueRevEternity)
            {
                tag["trueRevEternity"] = true;
            }
            if (trueDeathEternity)
            {
                tag["trueDeathEternity"] = true;
            }
            if (downedMutantEX)
            {
                tag["downedMutantEX"] = true;
            }
        }
        public override void LoadWorldData(TagCompound tag)
        {
            downedEch = tag.ContainsKey("downedEch");
            talk = tag.ContainsKey("talk");
            downedFish = tag.ContainsKey("downedFish");
            downedNihilus = tag.ContainsKey("downedNihilus");
            enragedMutantEX = tag.ContainsKey("enragedMutantEX");
            TrueSuperChtuxlagorWrathModeOmegaEX = tag.ContainsKey("TrueSuperChtuxlagorWrathModeOmegaEX");
            trueRevEternity = tag.ContainsKey("trueRevEternity");
            trueDeathEternity = tag.ContainsKey("trueDeathEternity");
            downedMutantEX = tag.ContainsKey("downedMutantEX");
        }
        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[1] = downedEch;
            flags[3] = enragedMutantEX;
            flags[2] = TrueSuperChtuxlagorWrathModeOmegaEX;
            flags[4] = downedFish;
            flags[5] = talk;
            flags[6] = trueRevEternity;
            flags[7] = trueDeathEternity;
            flags[8] = downedMutantEX;
            flags[9] = downedNihilus;
            writer.Write(flags);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedEch = flags[1];
            enragedMutantEX = flags[3];
            TrueSuperChtuxlagorWrathModeOmegaEX = flags[2];
            downedFish = flags[4];
            talk = flags[5];
            trueRevEternity = flags[6];
            trueDeathEternity = flags[7];
            downedMutantEX = flags[8];
            downedNihilus = flags[9];
        }
    }
}