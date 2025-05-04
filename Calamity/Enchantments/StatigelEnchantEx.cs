using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.Statigel;
using CalamityMod.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class StatigelEnchantEx : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        private readonly Mod ragnarok = ModLoader.GetMod("RagnarokMod");

        private readonly Mod calamitybardhealer = ModLoader.GetMod("CalamityBardHealer");

        private readonly Mod fargocross = ModLoader.GetMod("FargowiltasCrossmod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }

        public override Color nameColor => new(89, 170, 204);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<StarTaintedEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "StarTaintedGenerator").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<PolarizerEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "ManaPolarizer").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "StatigelHeadMelee").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "StatigelHeadMagic").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "StatigelHeadRanged").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "StatigelHeadRogue").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "StatigelHeadSummon").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "StatigelEarrings").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "StatigelFoxMask").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "StatigelHeadBard").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "StatigelHeadHealer").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "StatigelArmor").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "StatigelGreaves").UpdateArmorSet(player);
            ModContent.Find<ModItem>(fargocross.Name, "StatigelEnchant").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<StatigelHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<StatigelHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<StatigelHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<StatigelHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<StatigelHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<StatigelArmor>());
            recipe.AddIngredient(ModContent.ItemType<StatigelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<StarTaintedGenerator>());
            recipe.AddIngredient(ModContent.ItemType<ManaPolarizer>());
            recipe.AddIngredient(ModContent.ItemType<StatigelEnchant>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class StarTaintedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<StatigelEnchantEx>();
        }
        public class PolarizerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<StatigelEnchantEx>();
        }
    }
}