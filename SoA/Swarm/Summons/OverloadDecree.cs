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
using SacredTools.Content.NPCs.Boss.Decree;
using SacredTools.Content.Items.Tools.Summon;

namespace ssm.SoA.Swarm.Summons
{
	[ExtendsFromMod(ModCompatibility.SacredTools.Name)]
	[JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
	public class OverloadDecree : SoA.Swarm.Summons.SwarmSummonBase
	{
		public OverloadDecree() : base(ModContent.NPCType<Decree>(), 25)
		{
		}

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SoASwarmItems;
        }

        public override bool CanUseItem(Player player)
		{
			return !ssm.SwarmActive;
		}

		public override void AddRecipes()
		{
			this.CreateRecipe(1)
			.AddIngredient<Overloader>()
			.AddIngredient<DecreeSummon>()
			.Register();
		}
	}
}