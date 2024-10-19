using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;
using ssm.Core;
using ssm.Thorium.Swarm.Summons;
using ThoriumMod.NPCs.BossForgottenOne;
using Fargowiltas.Items.Summons.SwarmSummons;
using ThoriumMod.Items.BossForgottenOne;

namespace ssm.Thorium.Swarm.Summons
{
	[ExtendsFromMod(ModCompatibility.Thorium.Name)]
	[JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
	public class OverloadAbyssalion : SwarmSummonBase
	{
		public OverloadAbyssalion() : base(ModContent.NPCType<ForgottenOne>(), 25)
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
			.AddIngredient<AbyssalShadow>()
			.Register();
		}
	}
}