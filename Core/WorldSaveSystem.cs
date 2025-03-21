using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ssm.Systems
{
    public class WorldSaveSystem : ModSystem
    {
        public static bool talk = false;

        public static bool downedEch = false;
        public static bool downedFish = false;
        public static bool downedShtuxibus = false;
        public static bool downedChtuxlagor = false;

        public static bool TrueSuperChtuxlagorWrathModeOmegaEX = false;
        public static bool trueRevEternity = false;
        public static bool trueDeathEternity = false;

        public override void SaveWorldData(TagCompound tag)
        {
            if (downedShtuxibus)
            {
                tag["downedShtuxibus"] = true;
            }
            if (downedEch)
            {
                tag["downedEch"] = true;
            }
            if (talk)
            {
                tag["talk"] = true;
            }
            if (downedFish)
            {
                tag["downedFish"] = true;
            }
            if (TrueSuperChtuxlagorWrathModeOmegaEX)
            {
                tag["TrueSuperChtuxlagorWrathModeOmegaEX"] = true;
            }
            if (downedChtuxlagor)
            {
                tag["downedChtuxlagor"] = true;
            }
            if (trueRevEternity)
            {
                tag["trueRevEternity"] = true;
            }
            if (trueDeathEternity)
            {
                tag["trueDeathEternity"] = true;
            }
        }
        public override void LoadWorldData(TagCompound tag)
        {
            downedShtuxibus = tag.ContainsKey("downedShtuxibus");
            downedEch = tag.ContainsKey("downedEch");
            talk = tag.ContainsKey("talk");
            downedFish = tag.ContainsKey("downedFish");
            downedChtuxlagor = tag.ContainsKey("downedChtuxlagor");
            TrueSuperChtuxlagorWrathModeOmegaEX = tag.ContainsKey("TrueSuperChtuxlagorWrathModeOmegaEX");
            trueRevEternity = tag.ContainsKey("trueRevEternity");
            trueDeathEternity = tag.ContainsKey("trueDeathEternity");
        }
        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedShtuxibus;
            flags[1] = downedEch;
            flags[3] = downedChtuxlagor;
            flags[2] = TrueSuperChtuxlagorWrathModeOmegaEX;
            flags[4] = downedFish;
            flags[5] = talk;
            flags[6] = trueRevEternity;
            flags[7] = trueDeathEternity;
            writer.Write(flags);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedShtuxibus = flags[0];
            downedEch = flags[1];
            downedChtuxlagor = flags[3];
            TrueSuperChtuxlagorWrathModeOmegaEX = flags[2];
            downedFish = flags[4];
            talk = flags[5];
            trueRevEternity = flags[6];
            trueDeathEternity = flags[7];
        }
    }
}