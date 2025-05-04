using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Calamity.Souls
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class PotJT : ModItem
    {
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(1, 0, 0, 0);
            this.Item.rare = 10;
            this.Item.accessory = true;
            this.Item.defense = 13;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<AquaticHeartEffect>(Item))
                ModContent.GetInstance<AquaticHeart>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<HideofAstrumDeusEffect>(Item))
                ModContent.GetInstance<HideofAstrumDeus>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<ToxicHeartEffect>(Item))
                ModContent.GetInstance<ToxicHeart>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<AuricSoulEffect>(Item))
                ModContent.GetInstance<AuricSoulArtifact>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<SkullCrownEffect>(Item))
                ModContent.GetInstance<OccultSkullCrown>().UpdateAccessory(player, hideVisual);

            ModContent.GetInstance<BloodflareCore>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BlazingCore>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ElementalArtifact>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TheEvolution>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<LeviathanAmbergris>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<Affliction>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TheCommunity>().UpdateAccessory(player, hideVisual);

        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe(1);

            recipe.AddIngredient<AuricBar>(5);

            recipe.AddIngredient<ElementalArtifact>();
            recipe.AddIngredient<OccultSkullCrown>();
            recipe.AddIngredient<AuricSoulArtifact>();
            recipe.AddIngredient<BlazingCore>();
            recipe.AddIngredient<LeviathanAmbergris>();
            recipe.AddIngredient<TheEvolution>();
            recipe.AddIngredient<AquaticHeart>();
            recipe.AddIngredient<Affliction>();
            recipe.AddIngredient<TheCommunity>();
            recipe.AddIngredient<HideofAstrumDeus>();
            recipe.AddIngredient<ToxicHeart>();
            recipe.AddIngredient<BloodflareCore>();

            recipe.AddTile<CosmicAnvil>();

            recipe.Register();
        }

        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        [ExtendsFromMod(ModCompatibility.Calamity.Name)]
        public class AquaticHeartEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<AquaticHeart>();
        }
        public class HideofAstrumDeusEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<HideofAstrumDeus>();
        }
        public class ToxicHeartEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<ToxicHeart>();
        }
        public class AuricSoulEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<AuricSoulArtifact>();
        }
        public class SkullCrownEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<OccultSkullCrown>();
        }
    }
}
