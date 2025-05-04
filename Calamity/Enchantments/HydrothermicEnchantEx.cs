using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Hydrothermic;
using ssm.Content.SoulToggles;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;

namespace ssm.Calamity.Enchantments
{
    public class HydrothermicEnchantEx : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

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

        public override Color nameColor => new(248, 182, 89);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<VoidEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "VoidofExtinction").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<EtherealEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "EtherealExtorter").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "HydrothermicHeadMagic").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "HydrothermicHeadSummon").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "HydrothermicHeadMelee").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "HydrothermicHeadRogue").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "HydrothermicHeadRanged").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "HydrothermicHat").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "HydrothermicGasMask").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "HydrothermicArmor").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "HydrothermicSubligar").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.fargocross.Name, "HydrothermicEnchant").UpdateAccessory(player, hideVisual);
        }
        public class VoidEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<HydrothermicEnchantEx>();
        }

        public class EtherealEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<HydrothermicEnchantEx>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HydrothermicHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicArmor>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicSubligar>());
            recipe.AddIngredient(ModContent.ItemType<EtherealExtorter>());
            recipe.AddIngredient(ModContent.ItemType<VoidofExtinction>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicEnchant>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
