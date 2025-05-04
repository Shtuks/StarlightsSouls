using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.Items.Armor.MarniteArchitect;
using CalamityMod.Items.Accessories;
using CalamityMod.Buffs.Mounts;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class MarniteEnchantEx : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

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

        public override Color nameColor => new(153, 200, 193);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MarniteArchitectPlayer>().setEquipped = true;
            if (player.AddEffect<LiftEffect>(Item))
            {
                player.GetModPlayer<MarniteArchitectPlayer>().mounted = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(ModContent.BuffType<MarniteLiftBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<MarniteLiftBuff>(), 3600, true, false);
                    }
                }
            }
            if (player.AddEffect<HallowedEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "HallowedRune").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<RoverEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "RoverDrive").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(this.fargocross.Name, "MarniteEnchant").UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MarniteArchitectHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<MarniteArchitectToga>());
            recipe.AddIngredient(ModContent.ItemType<MarniteEnchant>());
            recipe.AddIngredient(ModContent.ItemType<HallowedRune>());
            recipe.AddIngredient(ModContent.ItemType<RoverDrive>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class LiftEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarniteEnchantEx>();
        }
        public class HallowedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarniteEnchantEx>();
        }
        public class RoverEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarniteEnchantEx>();
        }
    }
}
