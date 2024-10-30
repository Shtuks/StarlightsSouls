using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.NPCItems;
using ssm.Core;
using ThoriumMod.Items.HealerItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class PlagueDoctorEnchant : BaseEnchant
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
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //plague effect
            thoriumPlayer.setPlague = true;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<PlagueDoctorsMask>());
            recipe.AddIngredient(ModContent.ItemType<PlagueDoctorsGarb>());
            recipe.AddIngredient(ModContent.ItemType<PlagueDoctorsLeggings>());
            recipe.AddIngredient(ModContent.ItemType<GasContainer>(), 300);
            recipe.AddIngredient(ModContent.ItemType<CombustionFlask>(), 300);
            recipe.AddIngredient(ModContent.ItemType<NitrogenVial>(), 300);



            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
