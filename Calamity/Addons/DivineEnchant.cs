using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.CalPlayer;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using CalamityMod.Items;
using NoxusBoss.Content.Items.Accessories.Wings;
using NoxusBoss.Content.Items.Accessories.VanityEffects;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.WrathoftheGods.Name)]
    public class DivineEnchant : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 50000000;
        }

        public override Color nameColor => new(173, 52, 70);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();

            ModContent.Find<ModItem>(ModCompatibility.WrathoftheGods.Name, "DivineWings").UpdateAccessory(player, hideVisual);
            
            if (player.AddEffect<DivineEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.WrathoftheGods.Name, "DeificTouch").UpdateAccessory(player, hideVisual);
            }
        }

        public class DivineEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DivineEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Rock>());
            recipe.AddIngredient(ModContent.ItemType<DivineWings>());
            recipe.AddIngredient(ModContent.ItemType<DeificTouch>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
    }
}
