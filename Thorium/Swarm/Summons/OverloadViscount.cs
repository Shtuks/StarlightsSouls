using Microsoft.Xna.Framework;
using Terraria;
using ssm.Calamity.Swarm.Summons;
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
using ThoriumMod.Items.BossQueenJellyfish;
using ThoriumMod.NPCs.BossViscount;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.HealerItems;

namespace ssm.Thorium.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class OverloadViscount : SwarmSummonBase
    {
        public OverloadViscount() : base(ModContent.NPCType<Viscount>(), 25)
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
            .AddIngredient<UnholyShards>(5)
            .Register();
        }
    }
}