using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using ssm.Content.NPCs.Shtuxibus;
using ssm.Content.Items.Consumables;
using ssm.Content.Items.Materials;
using Terraria.Localization;
using ssm.Content.Buffs;
using ssm.Systems;
using ssm;

namespace ssm.Systems
{
	public class ModIntegrationsSystem : ModSystem
	{
		public override void PostSetupContent() {
			DoBossChecklistIntegration();}

		private void DoBossChecklistIntegration() {
			if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod)) {return;}
			if (bossChecklistMod.Version < new Version(1, 6)) {return;}
			string internalName = "Shtuxibus";
			float weight = 745f;
			Func<bool> downed = () => WorldSaveSystem.downedShtuxibus;
			int bossType = ModContent.NPCType<Shtuxibus>();
			int spawnItem = ModContent.ItemType<ShtuxianCurse>();
			List<int> collectibles = new List<int>(){ModContent.ItemType<ShtuxiumSoulShard>(),};

			bossChecklistMod.Call(
				"LogBoss",
				Mod,
				internalName,
				weight,
				downed,
				bossType,
				new Dictionary<string, object>() {
					["spawnInfo"] = (object) Language.GetText("Mods.ssm.NPCs.Shtuxibus.BossChecklistIntegration.SpawnInfo").WithFormatArgs(Array.Empty<object>()),
					["despawnMessage"] = (object) Language.GetText("Mods.ssm.NPCs.Shtuxibus.BossChecklistIntegration.DespawnMessage"),
					["spawnItems"] = spawnItem,
					["collectibles"] = collectibles,
				}
			);
		}
	}
}