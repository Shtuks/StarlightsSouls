using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Valadium;
using ThoriumMod.Items.BossForgottenOne;
using ThoriumMod.Items.Blizzard;
using ThoriumMod.Items.NPCItems;
using ssm.Core;
using ThoriumMod.Items.ThrownItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.BossFallenBeholder;
using ThoriumMod.Projectiles.Boss;
using ThoriumMod.Items.BossLich;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.TideTurnerEnchant;
using ssm.Content.SoulToggles;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ValadiumEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 5;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {


            ShtunPlayer modPlayer = player.GetModPlayer<ShtunPlayer>();

            //set bonus
            if (player.gravDir == -1f)
            {
                player.GetDamage(DamageClass.Generic) += 0.12f;
            }

            if (player.AddEffect<ValadiumEffect>(Item))
            {
                //toggle
                //eye of beholder
                ModContent.Find<ModItem>(this.thorium.Name, "BeholderGaze").UpdateAccessory(player, hideVisual);
            }
        }

        public class ValadiumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ValadiumEnchant>();
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ValadiumHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ValadiumBreastPlate>());
            recipe.AddIngredient(ModContent.ItemType<ValadiumGreaves>());
            recipe.AddIngredient(ModContent.ItemType<BeholderGaze>());
            recipe.AddIngredient(ModContent.ItemType<MirroroftheBeholder>());
            recipe.AddIngredient(ModContent.ItemType<TommyGun>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
