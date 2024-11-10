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
using ThoriumMod.NPCs.BossGraniteEnergyStorm;
using ThoriumMod.Items.BossGraniteEnergyStorm;

namespace ssm.Thorium.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class OverloadGranite : SwarmSummonBase
    {
        public OverloadGranite() : base(ModContent.NPCType<GraniteEnergyStorm>(), 25)
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
            .AddIngredient<UnstableCore>()
            .Register();
        }
    }
}