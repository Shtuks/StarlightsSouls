using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;
using Fargowiltas.Items.Summons.SwarmSummons;
using ssm.Core;
using ssm.Thorium.Swarm.Summons;
using ThoriumMod.NPCs.BossBuriedChampion;
using ThoriumMod.Items.BossGraniteEnergyStorm;
using ThoriumMod.NPCs.BossQueenJellyfish;
using ThoriumMod.NPCs.BossStarScouter;
using ThoriumMod.Items.BossStarScouter;

namespace ssm.Thorium.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class OverloadSaucer : SwarmSummonBase
    {
        public OverloadSaucer() : base(ModContent.NPCType<StarScouter>(), 25)
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
            .AddIngredient<StarCaller>()
            .Register();
        }
    }
}