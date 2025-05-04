using FargowiltasSouls.Content.Items.Accessories.Forces;
using Terraria.ModLoader;
using Terraria;
using ssm.Redemption.Enchantments;

namespace ssm.Redemption.Forces
{
    [JITWhenModsEnabled(new string[] { "Redemption" })]
    [ExtendsFromMod(new string[] { "Redemption" })]
    public class AdvancementForce : BaseForce
    {
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(base.Mod.Name, "LivingWoodEnchant2").UpdateAccessory(player, hideVisual: false);
            ModContent.Find<ModItem>(base.Mod.Name, "CommonGuardEnchant").UpdateAccessory(player, hideVisual: false);
            ModContent.Find<ModItem>(base.Mod.Name, "PureIronEnchant").UpdateAccessory(player, hideVisual: false);
            ModContent.Find<ModItem>(base.Mod.Name, "DragonLeadEnchant").UpdateAccessory(player, hideVisual: false);
            ModContent.Find<ModItem>(base.Mod.Name, "HardlightEnchant").UpdateAccessory(player, hideVisual: false);
            ModContent.Find<ModItem>(base.Mod.Name, "XeniumEnchant").UpdateAccessory(player, hideVisual: false);
            ModContent.Find<ModItem>(base.Mod.Name, "XenomiteEnchant").UpdateAccessory(player, hideVisual: false);
        }

        public override void AddRecipes()
        {
            Recipe recipe = base.CreateRecipe();

            recipe.AddIngredient<LivingWoodEnchant2>();
            recipe.AddIngredient<CommonGuardEnchant>();
            recipe.AddIngredient<PureIronEnchant>();
            recipe.AddIngredient<DragonLeadEnchant>();
            recipe.AddIngredient<HardlightEnchant>();
            recipe.AddIngredient<XeniumEnchant>();
            recipe.AddIngredient<XenomiteEnchant>();

            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
