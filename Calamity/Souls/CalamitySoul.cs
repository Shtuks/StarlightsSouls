using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using CalamityMod.Items.Accessories;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories;
using ssm.Calamity.Enchantments;
using ssm.Calamity.Addons;
using CalamityMod.Items.Materials;
using ssm.CrossMod.CraftingStations;
using ssm.Calamity.Forces;
using Terraria.ID;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Forces;
using CalamityMod.Items.Armor;


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
            this.Item.rare = ItemRarityID.Purple;
            this.Item.accessory = true;
            this.Item.defense = 30;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<ExplorationForceEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DevastationForceEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AnnihilationForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DesolationForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ExaltationForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GaleForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ElementsForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SalvationForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TheCommunity>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ShatteredCommunity>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ElementalArtifact>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PotJT>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<CirrusDress>().UpdateArmorSet(player);

            player.buffImmune[ModContent.Find<ModBuff>(this.FargoCross.Name, "CalamitousPresenceBuff").Type] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe(1);
            recipe.AddIngredient<ShatteredCommunity>();
            recipe.AddIngredient<TheCommunity>();
            recipe.AddIngredient<ExplorationForceEx>();
            recipe.AddIngredient<DevastationForceEx>();
            recipe.AddIngredient<DesolationForce>();
            recipe.AddIngredient<AnnihilationForce>();
            recipe.AddIngredient<ExaltationForce>();
            recipe.AddIngredient<SalvationForce>();
            recipe.AddIngredient<GaleForce>();
            recipe.AddIngredient<ElementsForce>();
            recipe.AddIngredient<BrandoftheBrimstoneWitch>();
            recipe.AddIngredient<PotJT>();
            recipe.AddIngredient<CirrusDress>();

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
