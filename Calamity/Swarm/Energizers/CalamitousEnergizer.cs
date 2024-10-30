using ssm.Content.Tiles;
using Terraria.ModLoader;

namespace ssm.Calamity.Swarm.Energizers
{
    public class CalamitousEnergizer : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.CalSwarmItems;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.rare = 1;
            Item.value = 1000000;
        }

        public override void AddRecipes()
        {
            this.CreateRecipe(1)
            .AddIngredient<AnahitaEnergizer>()
            .AddIngredient<FollyEnergizer>()
            .AddIngredient<HiveEnergizer>()
            .AddIngredient<DesertEnergizer>()
            .AddIngredient<CryoEnergizer>()
            .AddIngredient<BrimstoneEnergizer>()
            .AddIngredient<WaifuCloneEnergizer>()
            .AddIngredient<WaifuEnergizer>()
            .AddIngredient<AstralEnergizer>()
            .AddIngredient<AureusEnergizer>()
            .AddIngredient<CrabulonEnergizer>()
            .AddIngredient<DevouringEnergizer>()
            .AddIngredient<FleshyEnergizer>()
            .AddIngredient<PerforatorEnergizer>()
            .AddIngredient<PlagueEnergizer>()
            .AddIngredient<ProfandedEnergizer>()
            .AddIngredient<SlimeGodEnergizer>()
            .AddIngredient<ToxicEnergizer>()
            .AddTile<MutantsForgeTile>()
            .Register();
        }
    }
}