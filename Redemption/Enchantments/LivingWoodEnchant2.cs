using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Redemption.Items.Armor.PreHM.LivingWood;
using Redemption.Items.Weapons.PreHM.Summon;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class LivingWoodEnchant2 : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Redemption;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new Color(206, 182, 95);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[20] = true;
            player.GetDamage(DamageClass.Summon) += 0.03f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<LivingWoodHelmet>();
            recipe.AddIngredient<LivingWoodBody>();
            recipe.AddIngredient<LivingWoodLeggings>();
            recipe.AddIngredient(4281);
            recipe.AddIngredient<LogStaff>();
            recipe.AddIngredient(2196);
            recipe.AddTile(26);

            recipe.Register();
        }
    }
}
