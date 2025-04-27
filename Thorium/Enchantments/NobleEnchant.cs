using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.BossStarScouter;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class NobleEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 3;
            Item.value = 80000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            ModContent.Find<ModItem>(this.thorium.Name, "RingofUnity").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "BrassCap").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "WaxyRosin").UpdateAccessory(player, hideVisual);

            //noble set bonus
            thoriumPlayer.setNoble = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<NoblesHat>());
            recipe.AddIngredient(ModContent.ItemType<NoblesJerkin>());
            recipe.AddIngredient(ModContent.ItemType<NoblesLeggings>());
            recipe.AddIngredient(ModContent.ItemType<RingofUnity>());
            recipe.AddIngredient(ModContent.ItemType<BrassCap>());
            recipe.AddIngredient(ModContent.ItemType<WaxyRosin>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
