using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Fearmonger;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using ssm.Content.SoulToggles;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class FearfallenEnchant : BaseEnchant
    {
        private readonly Mod ragnarok = ModLoader.GetMod("RagnarokMod");

        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(70, 63, 69);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<VeilEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "SpectralVeil").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "FearmongerGreathelm").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "NightfallenHelmet").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "FearmongerPlateMail").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "NightfallenBreastplate").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "FearmongerGreaves").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "NightfallenGreaves").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FearmongerGreathelm>());
            recipe.AddIngredient(ModContent.ItemType<FearmongerPlateMail>());
            recipe.AddIngredient(ModContent.ItemType<FearmongerGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SpectralVeil>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class VeilEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FearfallenEnchant>();
        }
    }
}
