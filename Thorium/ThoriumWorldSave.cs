using System.IO;
using Terraria.ModLoader.IO;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Thorium
{
    public class ThoriumWorldSave : ModSystem
    {
        public static bool downedPatchWrek = false;
        public static bool downedCorpseBloom = false;
        public static bool downedIllusionist = false;

        public override void SaveWorldData(TagCompound tag)
        {
            if (downedPatchWrek)
            {
                tag["downedPatchWrek"] = true;
            }
            if (downedCorpseBloom)
            {
                tag["downedCorpseBloom"] = true;
            }
            if (downedIllusionist)
            {
                tag["downedIllusionist"] = true;
            }
        }
        public override void LoadWorldData(TagCompound tag)
        {
            downedPatchWrek = tag.ContainsKey("downedPatchWrek");
            downedCorpseBloom = tag.ContainsKey("downedCorpseBloom");
            downedIllusionist = tag.ContainsKey("downedIllusionist");
        }
        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedPatchWrek;
            flags[1] = downedCorpseBloom;
            flags[2] = downedIllusionist;
            writer.Write(flags);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedPatchWrek = flags[0];
            downedCorpseBloom = flags[1];
            downedIllusionist = flags[2];
        }

    }
}
