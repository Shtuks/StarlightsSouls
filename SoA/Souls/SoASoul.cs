using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Fargowiltas.Items.Tiles;
using ssm.SoA.Enchantments;
using ssm.SoA.Forces;
using ssm.Systems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Materials;
using SacredTools.Content.Items.Materials;

namespace ssm.SoA.Souls
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoASoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SacredTools;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.defense = 25;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 1000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "FoundationsForce").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "GenerationsForce").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SoranForce").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SyranForce").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<FoundationsForce>();
            recipe.AddIngredient<GenerationsForce>();
            recipe.AddIngredient<SoranForce>();
            recipe.AddIngredient<SyranForce>();
            recipe.AddIngredient<AbomEnergy>(10);
            recipe.AddIngredient<EmberOfOmen>(5);
            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
