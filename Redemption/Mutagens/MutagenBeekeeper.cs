using Redemption.Items.Materials.PostML;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using BombusApisBee.BeeDamageClass;
using BombusApisBee.Items.Other.Crafting;
using ssm.ClassSouls.Beekeeper.Essences;

namespace ssm.Redemption.Mutagens
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.BeekeeperClass.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.BeekeeperClass.Name)]
    public class MutagenBeekeeper : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 28;
            base.Item.height = 36;
            base.Item.value = Item.sellPrice(0, 12);
            base.Item.rare = 11;
            base.Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<HymenoptraDamageClass>() += 0.20f;
            player.GetCritChance<HymenoptraDamageClass>() += 0.10f;
            player.GetModPlayer<BeeDamagePlayer>().BeeResourceMax2 += 100;
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ModContent.ItemType<PhotonFragment>(), 10).AddIngredient(ModContent.ItemType<EmptyMutagen>()).AddIngredient(ModContent.ItemType<BeekeeperEssence>())
                .AddTile(412)
                .Register();
        }
    }
}
