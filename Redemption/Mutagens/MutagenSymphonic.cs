using Redemption.Items.Materials.PostML;
using Terraria.ModLoader;
using Terraria;
using ThoriumMod.Items.ThrownItems;
using ssm.Thorium.Essences;
using ThoriumMod.Items.BardItems;
using ThoriumMod;
using ssm.Core;

namespace ssm.Redemption.Mutagens
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    public class MutagenSymphonic : ModItem
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
            player.GetDamage<BardDamage>() += 0.20f;
            player.GetCritChance<BardDamage>() += 0.10f;
            player.GetAttackSpeed<BardDamage>() += 0.10f;
            player.GetModPlayer<ThoriumPlayer>().bardBuffDuration += 2000;
            player.GetModPlayer<ThoriumPlayer>().bardResource += 10;
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ModContent.ItemType<ShootingStarFragment>(), 10).AddIngredient(ModContent.ItemType<EmptyMutagen>()).AddIngredient(ModContent.ItemType<BardEssence>())
                .AddTile(412)
                .Register();
        }
    }
}
