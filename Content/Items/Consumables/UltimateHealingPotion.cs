using CalamityMod.Items.Potions;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Content.Items.Accessories;
using ssm.Core;
using ssm.CrossMod.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Consumables
{
    public class UltimateHealingPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.rare = 11;
            Item.value = Item.buyPrice(gold: 10);

            Item.healLife = 10000;
            Item.potion = true;
        }
        public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
            healValue = player.statLifeMax2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);

            if (ModCompatibility.SacredTools.Loaded)
            {
                ModCompatibility.SacredTools.Mod.TryFind<ModItem>("AsthraltiteHealingPotion", out ModItem soa);
                recipe.AddIngredient(soa ,50);
            }

            if (!ModCompatibility.SacredTools.Loaded && ModCompatibility.Calamity.Loaded)
            {
                ModCompatibility.Calamity.Mod.TryFind<ModItem>("OmegaHealingPotion", out ModItem cal);
                recipe.AddIngredient(cal, 50);
            }

            if (!ModCompatibility.SacredTools.Loaded && !ModCompatibility.Calamity.Loaded)
            {
                recipe.AddIngredient(ItemID.SuperHealingPotion, 50);
            }

            recipe.AddIngredient<EternalEnergy>(1);

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}