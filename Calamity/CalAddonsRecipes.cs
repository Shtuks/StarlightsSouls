using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using ssm.Core;
using CatalystMod.Items.Armor.Intergelactic;
using CalamityEntropy.Content.Items.Armor.VoidFaquir;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Catalyst.Name)]
    [JITWhenModsEnabled(ModCompatibility.Catalyst.Name)]
    public class CatalystRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup rec99 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Intergelactic Helmet", ModContent.ItemType<IntergelacticHeadMagic>(), ModContent.ItemType<IntergelacticHeadMelee>(), ModContent.ItemType<IntergelacticHeadSummon>(), ModContent.ItemType<IntergelacticHeadRanged>(), ModContent.ItemType<IntergelacticHeadRogue>());
            RecipeGroup.RegisterGroup("ssm:IntergelacticHelmet", rec99);

        }
    }
    [ExtendsFromMod(ModCompatibility.Entropy.Name)]
    [JITWhenModsEnabled(ModCompatibility.Entropy.Name)]
    public class EntropyRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup rec98 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Void Faquir Helmet", ModContent.ItemType<VoidFaquirCosmosHood>(), ModContent.ItemType<VoidFaquirDevourerHelm>(), ModContent.ItemType<VoidFaquirEvokerHelm>(), ModContent.ItemType<VoidFaquirLurkerMask>(), ModContent.ItemType<VoidFaquirShadowHelm>());
            RecipeGroup.RegisterGroup("ssm:VoidHelmet", rec98);

        }
    }
}


