using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ssm.Systems
{
	public class WorldSaveSystem : ModSystem
	{
		public static bool downedShtuxibus = false;
		public override void ClearWorld() {
			downedShtuxibus = false;}
		public override void SaveWorldData(TagCompound tag) {
			if (downedShtuxibus) {
				tag["downedShtuxibus"] = true;}}
		public override void LoadWorldData(TagCompound tag) {
			downedShtuxibus = tag.ContainsKey("downedShtuxibus");}
		public override void NetSend(BinaryWriter writer) {
			var flags = new BitsByte();
			flags[0] = downedShtuxibus;
			writer.Write(flags);}
		public override void NetReceive(BinaryReader reader) {
			BitsByte flags = reader.ReadByte();
			downedShtuxibus = flags[0];
		}
	}
}