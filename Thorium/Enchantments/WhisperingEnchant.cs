using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BossForgottenOne;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Painting;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.TideTurnerEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class WhisperingEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 8;
            Item.value = 250000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            if (player.AddEffect<WhisperingEffect>(Item))
            {
                //toggle
                thoriumPlayer.whisperingSet = true;
            }
        }

        public class WhisperingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WhisperingEnchant>();
            public override bool ExtraAttackEffect => true;
            public override bool MutantsPresenceAffects => true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<WhisperingHood>());
            recipe.AddIngredient(ModContent.ItemType<WhisperingTabard>());
            recipe.AddIngredient(ModContent.ItemType<WhisperingLeggings>());
            recipe.AddIngredient(ModContent.ItemType<WildUmbra>());
            recipe.AddIngredient(ModContent.ItemType<MindMelter>());
            recipe.AddIngredient(ModContent.ItemType<WhisperingDagger>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
