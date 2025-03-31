using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using ThoriumMod.Items.BossStarScouter;
using ssm.Content.DamageClasses;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.BossBuriedChampion;
using ThoriumMod.Items.Coral;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.Bronze;

namespace ssm.Thorium.Essences
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SlingerEssence : BaseEssence
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModLoader.HasMod(ModCompatibility.Calamity.Name);
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 4;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            HealEffect(player);
        }

        private void HealEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            player.GetDamage<UnitedModdedThrower>() += 0.18f;
            player.GetCritChance<UnitedModdedThrower>() += 0.10f;
            player.Shtun().throwerVelocity += 0.10f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<NinjaEmblem>();

            recipe.AddIngredient<GaussFlinger>();
            recipe.AddIngredient<NaiadShiv>();
            recipe.AddIngredient<GelGlove>();
            recipe.AddIngredient<ChampionsGodHand>();
            recipe.AddIngredient<EnchantedKnife>(500);
            recipe.AddIngredient<BloomingShuriken>(500);
            recipe.AddIngredient<CoralCaltrop>(500);

            recipe.AddIngredient(ItemID.HallowedBar, 5);

            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
    }
}
