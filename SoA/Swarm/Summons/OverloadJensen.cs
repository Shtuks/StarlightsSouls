using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;
using ssm.Core;
using SacredTools.NPCs.Boss.Abaddon;
using SacredTools.Items.Consumable.BossSummon;
using ssm.SoA.Swarm.Summons;
using Fargowiltas.Items.Summons.SwarmSummons;
using SacredTools.NPCs.Boss.Raynare;
using SacredTools.Content.Items.Tools.Summon;
using SacredTools.Content.NPCs.Boss.Jensen;

namespace ssm.SoA.Swarm.Summons
{
	[ExtendsFromMod(ModCompatibility.SacredTools.Name)]
	[JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
	public class OverloadJensen : SoA.Swarm.Summons.SwarmSummonBase
	{
		public OverloadJensen() : base(ModContent.NPCType<Jensen>(), 25)
		{
		}

        public override bool CanUseItem(Player player)
		{
			return !ssm.SwarmActive;
		}

		public override void AddRecipes()
		{
			this.CreateRecipe(1)
			.AddIngredient<Overloader>()
			.AddIngredient<JensenSummon>()
			.Register();
		}
	}
}