using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.LivingWoodEnchant;
using ThoriumMod.Projectiles.Minions;
using ThoriumMod.Projectiles.Healer;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BiotechEnchant : BaseEnchant
    {

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            if (player.AddEffect<BiotechEffect>(Item))
            {
                thoriumPlayer.bioTechSet = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    const int damage = 100;
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<BiotechProbe>()] < 1)
                        ShtunUtils.NewSummonProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<BiotechProbe>(), damage, 8f, player.whoAmI);
                }
            }

        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<BioTechGarment>());
            recipe.AddIngredient(ModContent.ItemType<BioTechHood>());
            recipe.AddIngredient(ModContent.ItemType<BioTechLeggings>());
            recipe.AddIngredient(ModContent.ItemType<LifeEssenceApparatus>());
            recipe.AddIngredient(ModContent.ItemType<NullZoneStaff>());
            recipe.AddIngredient(ModContent.ItemType<BarrierGenerator>());


            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class BiotechEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BiotechEnchant>();

            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
