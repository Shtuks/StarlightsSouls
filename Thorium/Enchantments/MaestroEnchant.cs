using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ssm.Core;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Painting;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.LifeBloomEnchant;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.MarchingBandEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class MaestroEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 8;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //maestro
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //thoriumPlayer.setMaestro = true;
            ModContent.Find<ModItem>(this.thorium.Name, "MaestroWig").UpdateArmorSet(player);

            if (player.AddEffect<MetronomeEffect>(Item))
            {
                //metronome
                //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "Metronome").UpdateAccessory(player, hideVisual);
            }

            //conductor's baton
            ModContent.Find<ModItem>(this.thorium.Name, "ConductorsBaton").UpdateAccessory(player, hideVisual);

            ModContent.Find<ModItem>(this.Mod.Name, "MarchingBandEnchant").UpdateAccessory(player, hideVisual);

            //full score
            ModContent.Find<ModItem>(this.thorium.Name, "FullScore").UpdateAccessory(player, hideVisual);
        }

        public class MetronomeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MaestroEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<MaestroWig>());
            recipe.AddIngredient(ModContent.ItemType<MaestroSuit>());
            recipe.AddIngredient(ModContent.ItemType<MaestroLeggings>());
            recipe.AddIngredient(ModContent.ItemType<MarchingBandEnchant>());
            recipe.AddIngredient(ModContent.ItemType<Metronome>());
            recipe.AddIngredient(ModContent.ItemType<ConductorsBaton>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
