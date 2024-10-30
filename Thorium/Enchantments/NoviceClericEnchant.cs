using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.SacredEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class NoviceClericEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 1;
            Item.value = 40000;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<NoviceClericEffect>(Item))
            {
                //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "NoviceClericCowl").UpdateArmorSet(player);
            }

            ModContent.Find<ModItem>(this.thorium.Name, "NursePurse").UpdateAccessory(player, hideVisual);
        }

        public class NoviceClericEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NoviceClericEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<NoviceClericCowl>());
            recipe.AddIngredient(ModContent.ItemType<NoviceClericTabard>());
            recipe.AddIngredient(ModContent.ItemType<NoviceClericPants>());
            recipe.AddIngredient(ModContent.ItemType<NursePurse>());
            recipe.AddIngredient(ModContent.ItemType<PalmCross>());
            recipe.AddIngredient(ModContent.ItemType<Renew>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
