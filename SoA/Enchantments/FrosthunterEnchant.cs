using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using SacredTools.Content.Items.Armor.Decree;
using SacredTools.Content.Items.Accessories;
using SacredTools.Items.Weapons.Decree;
using SacredTools.Items.Weapons;
using FargowiltasSouls;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FrosthunterEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SacredTools;
        }

        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 1;
            Item.value = 50000;
        }

        public override Color nameColor => new(73, 94, 174);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<FrosthunterEffect>(Item))
            {
                player.GetModPlayer<SoAPlayer>().frosthunterEnchant = player.ForceEffect<FrosthunterEffect>() ? 2 : 1;
            }
        }
        public class FrosthunterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrosthunterEnchant>();

        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<FrosthunterHeaddress>();
            recipe.AddIngredient<FrosthunterWrappings>();
            recipe.AddIngredient<FrosthunterBoots>();
            recipe.AddIngredient<DecreeCharm>();
            recipe.AddIngredient<FrostGlobeStaff>();
            recipe.AddIngredient<FrostBeam>();
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
