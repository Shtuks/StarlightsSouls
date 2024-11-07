using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using ssm.Core;
using ssm.Thorium;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.SoA.Enchantments.BlazingBruteEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SacredEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 4;
            Item.value = 120000;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            modPlayer.SacredEnchant = true;

            ModContent.Find<ModItem>("ssm", "NoviceClericEnchant").UpdateAccessory(player, true);

            if (player.AddEffect<SacredEffect>(Item))
            {
                //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "KarmicHolder").UpdateAccessory(player, true);
            }
        }

        public class SacredEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SacredEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SacredBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<SacredHelmet>());
            recipe.AddIngredient(ModContent.ItemType<SacredLeggings>());
            recipe.AddIngredient(ModContent.ItemType<NoviceClericEnchant>());
            recipe.AddIngredient(ModContent.ItemType<KarmicHolder>());
            recipe.AddIngredient(ModContent.ItemType<Liberation>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
