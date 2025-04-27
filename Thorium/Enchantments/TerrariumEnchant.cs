using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ssm.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ThoriumMod.Items.Terrarium;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.NPCItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.SandstoneEnchant;
using ssm.Content.SoulToggles;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TerrariumEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override Color nameColor => (new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10; //rainbow
            Item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<TerrariumEffect>(Item))
            {
                //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "TerrariumHelmet").UpdateArmorSet(player);
            }

            ModContent.Find<ModItem>("ssm", "ThoriumEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "TerrariumSurroundSound").UpdateAccessory(player, hideVisual);
        }

        public class TerrariumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TerrariumEnchant>();
            public override bool MutantsPresenceAffects => true;
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<TerrariumHelmet>());
            recipe.AddIngredient(ModContent.ItemType<TerrariumBreastPlate>());
            recipe.AddIngredient(ModContent.ItemType<TerrariumGreaves>());
            recipe.AddIngredient(ModContent.ItemType<ThoriumEnchant>());
            recipe.AddIngredient(ModContent.ItemType<TerrariumSurroundSound>());
            recipe.AddIngredient(ModContent.ItemType<ThoriumCube>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
