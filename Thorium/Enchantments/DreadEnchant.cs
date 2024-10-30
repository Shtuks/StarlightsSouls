using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Dread;
using ThoriumMod.Items.BasicAccessories;
using ssm.Core;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.SummonItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.SoA.Enchantments.BlazingBruteEnchant;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DreadEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 7;
            Item.value = 200000;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            if (player.AddEffect<DreadEffect>(Item))
            {
                //toggle
                //dread set bonus
                player.moveSpeed += 0.8f;
                player.maxRunSpeed += 10f;
                player.runAcceleration += 0.05f;
                if (player.velocity.X > 0f || player.velocity.X < 0f)
                {
                    player.GetDamage(DamageClass.Generic) += 0.25f;
                    player.GetCritChance(DamageClass.Generic) += 0.20f;
                    player.endurance += 0.1f;
                    for (int i = 0; i < 2; i++)
                    {
                        int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 65, 0f, 0f, 0, default(Color), 1.75f);
                        int num2 = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 75, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num2].noGravity = true;
                        Main.dust[num].noLight = true;
                        Main.dust[num2].noLight = true;
                    }
                }
            }

            ModContent.Find<ModItem>("ssm", "DragonEnchant").UpdateAccessory(player, hideVisual);

            ModContent.Find<ModItem>(this.thorium.Name, "CrashBoots").UpdateAccessory(player, hideVisual);

            ModContent.Find<ModItem>(this.thorium.Name, "CursedCore").UpdateAccessory(player, hideVisual);
        }

        public class DreadEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DreadEnchant>();
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DreadSkull>());
            recipe.AddIngredient(ModContent.ItemType<DreadChestPlate>());
            recipe.AddIngredient(ModContent.ItemType<DreadGreaves>());
            recipe.AddIngredient(ModContent.ItemType<DragonEnchant>());
            recipe.AddIngredient(ModContent.ItemType<CrashBoots>());
            recipe.AddIngredient(ModContent.ItemType<CursedFlailCore>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
