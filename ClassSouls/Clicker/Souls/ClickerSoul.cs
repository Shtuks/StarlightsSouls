using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ClickerClass;
using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Items.Accessories;

namespace ssm.ClassSouls.Clicker.Souls
{
    [ExtendsFromMod(ModCompatibility.ClikerClass.Name)]
    [JITWhenModsEnabled(ModCompatibility.ClikerClass.Name)]
    public class ClickerSoul : BaseSoul
    {
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
            player.GetDamage<ClickerDamage>() += 0.22f;
            player.GetCritChance<ClickerDamage>() += 0.10f;
            player.GetAttackSpeed<ClickerDamage>() += 0.15f;
            player.GetModPlayer<ClickerPlayer>().clickerRadius += 2;
            player.GetModPlayer<ClickerPlayer>().clickerBonus += 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(null, "ClickerEssence");
            recipe.AddIngredient<AbomEnergy>(10);

            recipe.AddIngredient<LordsClicker>();
            recipe.AddIngredient<DraconicClicker>();
            recipe.AddIngredient<HighTechClicker>();
            recipe.AddIngredient<RainbowClicker>();
            recipe.AddIngredient<NaughtyClicker>();
            recipe.AddIngredient<ShroomiteClicker>();
            recipe.AddIngredient<SeafoamClicker>();
            recipe.AddIngredient<ArthursClicker>();
            recipe.AddIngredient<AstralClicker>();
            recipe.AddIngredient<GamerCrate>();
            recipe.AddIngredient<ChocolateMilkCookies>();
            recipe.AddIngredient<SMedal>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
