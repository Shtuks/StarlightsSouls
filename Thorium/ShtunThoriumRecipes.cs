using Terraria.ModLoader;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria;
using ssm.Thorium.Souls;
using ThoriumMod.Items.Titan;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Donate;
using Terraria.ID;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    class ShtunThoriumRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            //jester mask
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Jester Mask", ModContent.ItemType<JestersMask>(), ModContent.ItemType<JestersMask2>());
            RecipeGroup.RegisterGroup("ssm:AnyJesterMask", group);
            //jester shirt
            group = new RecipeGroup(() => Lang.misc[37] + " Jester Shirt", ModContent.ItemType<JestersShirt>(), ModContent.ItemType<JestersShirt2>());
            RecipeGroup.RegisterGroup("ssm:AnyJesterShirt", group);
            //jester legging
            group = new RecipeGroup(() => Lang.misc[37] + " Jester Leggings", ModContent.ItemType<JestersLeggings>(), ModContent.ItemType<JestersLeggings2>());
            RecipeGroup.RegisterGroup("ssm:AnyJesterLeggings", group);
            //evil wood tambourine
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Wood Tambourine", ModContent.ItemType<EbonWoodTambourine>(), ModContent.ItemType<ShadeWoodTambourine>());
            RecipeGroup.RegisterGroup("ssm:AnyTambourine", group);
            //fan letter
            group = new RecipeGroup(() => Lang.misc[37] + " Fan Letter", ModContent.ItemType<FanLetter>(), ModContent.ItemType<FanLetter2>());
            RecipeGroup.RegisterGroup("ssm:AnyLetter", group);
            //bugle horn
            group = new RecipeGroup(() => Lang.misc[37] + " Bugle Horn", ModContent.ItemType<GoldBugleHorn>(), ModContent.ItemType<PlatinumBugleHorn>());
            RecipeGroup.RegisterGroup("ssm:AnyBugleHorn", group);
            //titan 
            group = new RecipeGroup(() => Lang.misc[37] + " Titan Headgear", ModContent.ItemType<TitanHelmet>(), ModContent.ItemType<TitanMask>(), ModContent.ItemType<TitanHeadgear>());
            RecipeGroup.RegisterGroup("ssm:AnyTitanHelmet", group);
        }
        public override void PostAddRecipes()
		{
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];
				if (recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<ThoriumSoul>())
				{
					recipe.AddIngredient<ThoriumSoul>();
				}
                if (recipe.HasResult<UniverseSoul>() && !recipe.HasIngredient<BardSoul>())
                {
                    recipe.AddIngredient<BardSoul>();
                }
                if (recipe.HasResult<UniverseSoul>() && !recipe.HasIngredient<GuardianAngelsSoul>())
                {
                    recipe.AddIngredient<GuardianAngelsSoul>();
                }
                if (recipe.HasResult<ColossusSoul>() && !recipe.HasIngredient<GuardianAngelsSoul>())
                {
                    recipe.AddIngredient<BlastShield>();
                }
                if (recipe.HasResult<HungeringBlossom>() && !recipe.HasIngredient(ItemID.NaturesGift))
                {
                    recipe.RemoveIngredient(ItemID.ManaFlower);
                    recipe.AddIngredient(ItemID.NaturesGift);
                }
                //if (recipe.HasResult<ColossusSoul>() && !recipe.HasIngredient<BlastShield>())
                //{
                //    recipe.AddIngredient<BlastShield>();
                //}
            }
		}
	}
}
