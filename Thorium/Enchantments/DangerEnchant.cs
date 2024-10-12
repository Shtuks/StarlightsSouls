/*using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Items.NPCItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DangerEnchant : BaseEnchant
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


            ShtunPlayer modPlayer = player.GetModPlayer<ShtunPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Venom] = true;

            //nightshade flower
            thoriumPlayer.nightshadeBoost = true;
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DangerHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DangerMail>());
            recipe.AddIngredient(ModContent.ItemType<DangerGreaves>());
            recipe.AddIngredient(ModContent.ItemType<NightShadePetal>());
            recipe.AddIngredient(ModContent.ItemType<TrackerBlade>());
            recipe.AddIngredient(ItemID.Rally);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
*/