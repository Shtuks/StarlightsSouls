using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;
using ssm.Core;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ThoriumMod.Projectiles.Bard;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Terrarium;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossTheGrandThunderBird;
using ThoriumMod.Items.BossViscount;

namespace ssm.Thorium.Essences
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BardEssence : BaseEssence
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 4;
            Item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            BardEffect(player);
        }

        public override Color nameColor => new(255, 128, 0);

        private void BardEffect(Player player)
        {
            player.GetDamage<BardDamage>() += 0.18f;
            player.GetCritChance<BardDamage>() += 0.10f;
            player.GetAttackSpeed<BardDamage>() += 0.10f;

            //ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //thoriumPlayer.symphonicDamage += 0.18f;
            //thoriumPlayer.symphonicSpeed += .05f;
            //thoriumPlayer.symphonicCrit += 5;
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<BardEmblem>();

            recipe.AddIngredient<AntlionMaraca>();
            recipe.AddIngredient<SeashellCastanettes>();
            recipe.AddIngredient<Didgeridoo>();
            recipe.AddIngredient<Bagpipe>();
            recipe.AddIngredient<SkywareLute>();
            recipe.AddIngredient<ForestOcarina>();
            recipe.AddIngredient<MarineWineGlass>();

            recipe.AddIngredient(ItemID.HallowedBar, 5);

            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
    }
}
