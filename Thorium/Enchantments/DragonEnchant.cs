using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Dragon;
using ssm.Core;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.BardItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.DepthDiverEnchant;
using ssm.Content.SoulToggles;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DragonEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }

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
            if (player.AddEffect<DragonEffect>(Item))
            {
                //toggle
                string oldSetBonus = player.setBonus;
                ModContent.Find<ModItem>(this.thorium.Name, "DragonMask").UpdateArmorSet(player);
                player.setBonus = oldSetBonus;
            }

            ModContent.Find<ModItem>(this.thorium.Name, "DragonTalonNecklace").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "TunePlayerMovementSpeed").UpdateAccessory(player, hideVisual);
        }

        public class DragonEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DragonEnchant>();
            public override bool ExtraAttackEffect => true;
            public override bool MutantsPresenceAffects => true;
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DragonMask>());
            recipe.AddIngredient(ModContent.ItemType<DragonBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<DragonGreaves>());
            recipe.AddIngredient(ModContent.ItemType<DragonTalonNecklace>());
            recipe.AddIngredient(ModContent.ItemType<TunePlayerMovementSpeed>());
            recipe.AddIngredient(ModContent.ItemType<EbonyTail>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
