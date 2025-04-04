﻿using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Essences;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using ssm.Core;
using ssm.Redemption.Mutagens;
using ssm.SoA.Essences;
using ssm.SoA.Souls;
using ssm.Thorium.Essences;
using ssm.Thorium.Souls;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name)]
    public class RedemptionDlcRecipes : ModSystem
    {
        //public override bool IsLoadingEnabled(Mod mod)
        //{
        //    return !ModLoader.HasMod(ModCompatibility.SacredTools.Name) && !ModLoader.HasMod(ModCompatibility.Thorium.Name);
        //}
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.HasResult<VagabondsSoul>() && !recipe.HasResult<MutagenThrowingCal>())
                {
                    recipe.AddIngredient<MutagenThrowingCal>();
                    recipe.RemoveIngredient(ModContent.ItemType<OutlawsEssence>());
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    public class RedemptionTorRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<BardSoul>() && !recipe.HasResult<MutagenSymphonic>())
                {
                    recipe.AddIngredient<MutagenSymphonic>();
                    recipe.RemoveIngredient(ModContent.ItemType<BardEssence>());
                }
                if (recipe.HasResult<GuardianAngelsSoul>() && !recipe.HasResult<MutagenHealing>())
                {
                    recipe.AddIngredient<MutagenHealing>();
                    recipe.RemoveIngredient(ModContent.ItemType<HealerEssence>());
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    public class RedemptionTorThrowerRecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModLoader.HasMod(ModCompatibility.Calamity.Name) && !ModLoader.HasMod(ModCompatibility.SacredTools.Name);
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<OlympiansSoul>() && !recipe.HasResult<MutagenThrowing>())
                {
                    recipe.AddIngredient<MutagenThrowing>();
                    recipe.RemoveIngredient(ModContent.ItemType<SlingerEssence>());
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name)]
    public class RedemptionSoARecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModLoader.HasMod(ModCompatibility.Calamity.Name) && !ModLoader.HasMod(ModCompatibility.Thorium.Name);
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.HasResult<StalkerSoul>() && !recipe.HasResult<MutagenThrowingSoA>())
                {
                    recipe.AddIngredient<MutagenThrowingSoA>();
                    recipe.RemoveIngredient(ModContent.ItemType<StalkerEssence>());
                }
            }
        }
    }
}
