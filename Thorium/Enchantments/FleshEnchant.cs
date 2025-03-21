using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Flesh;
using ssm.Core;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.HealerItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Calamity.Addons;
using ssm.Content.SoulToggles;
using static ssm.Calamity.Addons.IntergelacticEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class FleshEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<FleshMaskEffect>(Item))
            {
                ModContent.Find<ModItem>(this.thorium.Name, "FleshMask").UpdateArmorSet(player);
            }
            if (player.AddEffect<VampireGlandEffect>(Item))
            {
                ModContent.Find<ModItem>(this.thorium.Name, "VampireGland").UpdateAccessory(player, true);
            }
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<FleshMask>());
            recipe.AddIngredient(ModContent.ItemType<FleshBody>());
            recipe.AddIngredient(ModContent.ItemType<FleshLegs>());
            recipe.AddIngredient(ModContent.ItemType<VampireGland>());
            recipe.AddIngredient(ModContent.ItemType<FleshMace>());
            recipe.AddIngredient(ModContent.ItemType<BloodBelcher>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class FleshMaskEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FleshEnchant>();
        }
        public class VampireGlandEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FleshEnchant>();
        }
    }
}
