using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using CalamityMod.Items.Accessories;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Forces;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories;
using ssm.Calamity.Enchantments;
using ssm.Calamity.Addons;
using CalamityMod.Items.Materials;
using ssm.CrossMod.CraftingStations;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;


namespace ssm.Calamity.Souls
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class CalamitySoul : BaseSoul
    {
        private readonly Mod FargoCross = Terraria.ModLoader.ModLoader.GetMod("FargowiltasCrossmod");

        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(10, 0, 0, 0);
            this.Item.rare = 11;
            this.Item.accessory = true;
            this.Item.defense = 30;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<ShatteredCommunity>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ElementalArtifact>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PotJT>().UpdateAccessory(player, hideVisual);

            player.buffImmune[ModContent.Find<ModBuff>(this.FargoCross.Name, "CalamitousPresenceBuff").Type] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe(1);
            recipe.AddIngredient<ShatteredCommunity>();
            recipe.AddIngredient<GaleForce>();
            recipe.AddIngredient<ElementsForce>();
            recipe.AddIngredient<BrandoftheBrimstoneWitch>();
            recipe.AddIngredient<PotJT>();
            recipe.AddIngredient<DemonShadeEnchant>();
            recipe.AddIngredient<WulfrumEnchant>();
            recipe.AddIngredient<TitanHeartEnchant>();

            if (ModCompatibility.Catalyst.Loaded && ModCompatibility.Goozma.Loaded && ModCompatibility.Clamity.Loaded && ModCompatibility.WrathoftheGods.Loaded && ModCompatibility.Entropy.Loaded)
            {
                recipe.AddIngredient<AddonsForce>();
            }

            recipe.AddIngredient<AbomEnergy>(10);
            recipe.AddIngredient<ShadowspecBar>(5);
            recipe.AddTile<DemonshadeWorkbenchTile>();

            recipe.Register();
        }

        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        [ExtendsFromMod(ModCompatibility.Calamity.Name)]
        public abstract class CalamitySoulEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<CalamitySoulHeader>();
        }
    }
}
