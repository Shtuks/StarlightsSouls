using Terraria;
using Terraria.ID;
using ssm.Core;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Items.Dev;
using ThoriumMod.Items.Painting;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.DurasteelEnchant;
using ssm.Content.SoulToggles;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumEnchant : BaseEnchant
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
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //diverman meme
            modPlayer.ThoriumEnchant = true;

            if (player.AddEffect<CrietzEffect>(Item))
            {
                //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "Crietz").UpdateAccessory(player, hideVisual);
            }

            ModContent.Find<ModItem>(this.thorium.Name, "BandofReplenishment").UpdateAccessory(player, hideVisual);

        }

        public class CrietzEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ThoriumEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ThoriumHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ThoriumMail>());
            recipe.AddIngredient(ModContent.ItemType<ThoriumGreaves>());
            recipe.AddIngredient(ModContent.ItemType<Crietz>());
            recipe.AddIngredient(ModContent.ItemType<BandofReplenishment>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
