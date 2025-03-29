using Redemption.Items.Materials.PostML;
using Terraria.ModLoader;
using Terraria;
using ThoriumMod.Items.ThrownItems;
using ssm.Thorium.Essences;
using ThoriumMod;
using ssm.Core;
using ThoriumMod.Items.HealerItems;

namespace ssm.Redemption.Mutagens
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    public class MutagenHealing : ModItem
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
            player.GetDamage<HealerDamage>() += 0.20f;
            player.GetCritChance<HealerDamage>() += 0.10f;
            player.GetAttackSpeed(ThoriumDamageBase<HealerTool>.Instance) += 0.10f;
            player.GetModPlayer<ThoriumPlayer>().healBonus += 3;
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ModContent.ItemType<CelestialFragment>(), 10).AddIngredient(ModContent.ItemType<EmptyMutagen>()).AddIngredient(ModContent.ItemType<HealerEssence>())
                .AddTile(412)
                .Register();
        }
    }
}
