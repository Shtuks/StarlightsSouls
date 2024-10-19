using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ssm.Core;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Titan;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossForgottenOne;
using ThoriumMod.Items.BossLich;
using ThoriumMod.Items.RangedItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.HealerItems;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TitanEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //set bonus
            player.GetDamage(DamageClass.Generic) += 0.1f;
            //crystal eye mask
            player.GetCritChance(DamageClass.Generic) += 0.1f;
            //abyssal shell
            thoriumPlayer.AbyssalShell = true;
            //music player
            ModContent.Find<ModItem>(this.thorium.Name, "TunePlayerDamageReduction").UpdateAccessory(player, true);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:AnyTitanHelmet");
            recipe.AddIngredient(ModContent.ItemType<TitanBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<TitanGreaves>());
            recipe.AddIngredient(ModContent.ItemType<MaskoftheCrystalEye>());
            recipe.AddIngredient(ModContent.ItemType<AbyssalShell>());
            recipe.AddIngredient(ModContent.ItemType<TunePlayerDamageReduction>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
