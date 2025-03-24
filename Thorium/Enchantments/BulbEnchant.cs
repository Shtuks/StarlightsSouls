using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.SummonItems;
using ssm.Core;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.SoA.Enchantments.BlazingBruteEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BulbEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 2;
            Item.value = 60000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.thorium.Name, "BloomingCrown").UpdateArmorSet(player);

            ModContent.Find<ModItem>(this.thorium.Name, "BloomingShield").UpdateAccessory(player, hideVisual);

            if (player.AddEffect<BulbEffect>(Item))
            {
                //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "KickPetal").UpdateAccessory(player, hideVisual);
            }

            ModContent.Find<ModItem>(this.thorium.Name, "FragrantCorsage").UpdateAccessory(player, hideVisual);
        }

        public class BulbEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BulbEnchant>();
            public override bool MutantsPresenceAffects => true;
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<BloomingCrown>());
            recipe.AddIngredient(ModContent.ItemType<BloomingTabard>());
            recipe.AddIngredient(ModContent.ItemType<BloomingLeggings>());
            recipe.AddIngredient(ModContent.ItemType<BloomingShield>());
            recipe.AddIngredient(ModContent.ItemType<FragrantCorsage>());
            recipe.AddIngredient(ModContent.ItemType<KickPetal>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
