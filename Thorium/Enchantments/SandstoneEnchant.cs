using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Sandstone;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.BossTheGrandThunderBird;
using ssm.Core;
using ThoriumMod.Items.NPCItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.MarchingBandEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SandstoneEnchant : BaseEnchant
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

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<SandstoneEffect>(Item))
            {
                //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "SandStoneHelmet").UpdateArmorSet(player);
            }
        }

        public class SandstoneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SandstoneEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SandStoneHelmet>());
            recipe.AddIngredient(ModContent.ItemType<SandStoneMail>());
            recipe.AddIngredient(ModContent.ItemType<SandStoneGreaves>());
            recipe.AddIngredient(ModContent.ItemType<StoneThrowingSpear>(), 300);
            recipe.AddIngredient(ModContent.ItemType<Scorpain>());
            recipe.AddIngredient(ModContent.ItemType<TalonBurst>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
