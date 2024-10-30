using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using ssm.Core;
using ThoriumMod.Projectiles.Bard;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using ThoriumMod.Items.BardItems;

namespace ssm.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BardSoul : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 1000000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Thorium(player);
        }

        private void Thorium(Player player)
        {
            //general
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            player.GetDamage<BardDamage>() += 0.30f;
            player.GetCritChance<BardDamage>() += 0.15f;
            player.GetAttackSpeed<BardDamage>() += 0.20f;
            player.GetModPlayer<ThoriumPlayer>().bardBuffDuration += 3000;
            player.GetModPlayer<ThoriumPlayer>().inspirationRegenBonus += 1;
            player.GetModPlayer<ThoriumPlayer>().bardResourceDropBoost += 1;
            player.GetModPlayer<ThoriumPlayer>().bardResource += 20;
            player.GetModPlayer<ThoriumPlayer>().bardHomingSpeedBonus += 10;
            player.GetModPlayer<ThoriumPlayer>().bardHomingRangeBonus += 10;

            //epic mouthpiece
            thoriumPlayer.accWindHoming = true;
            //thoriumPlayer.bardHomingBonus = 5f;

            //straight mute
            thoriumPlayer.accBrassMute2 = true;
            //digital tuner
            thoriumPlayer.accPercussionTuner2 = true;
            //guitar pick claw
            thoriumPlayer.bardBounceBonus = 10;
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(null, "BardEssence");
            recipe.AddIngredient<AbomEnergy>(10);
            recipe.AddIngredient<DigitalTuner>();
            recipe.AddIngredient<EpicMouthpiece>();
            recipe.AddIngredient<GuitarPickClaw>();
            recipe.AddIngredient<StraightMute>();
            recipe.AddIngredient<BandKit>();
            recipe.AddIngredient<SteamFlute>();
            recipe.AddIngredient<PrimeRoar>();
            //recipe.AddIngredient<Eski>();
            recipe.AddIngredient<Fishbone>();
            //recipe.AddIngredient<Accordion>();
            //recipe.AddIngredient<Ocarina>();
            recipe.AddIngredient<TheMaw>();
            recipe.AddIngredient<SonicAmplifier>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
