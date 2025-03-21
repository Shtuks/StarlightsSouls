using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossStarScouter;
using ssm.Core;
using ThoriumMod.Items.Cultist;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.DurasteelEnchant;
using ssm.Content.SoulToggles;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ConduitEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

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
            ShtunThoriumPlayer modPlayer = player.GetModPlayer<ShtunThoriumPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            if (player.AddEffect<ConduitEffect>(Item))
            {
                //toggle
                thoriumPlayer.conduitSet = true;
                thoriumPlayer.orbital = true;
                thoriumPlayer.orbitalRotation1 = Utils.RotatedBy(thoriumPlayer.orbitalRotation1, -0.10000000149011612, default(Vector2));

                Lighting.AddLight(player.position, 0.2f, 0.35f, 0.7f);
                if ((player.velocity.X > 0f || player.velocity.X < 0f) && thoriumPlayer.circuitStage < 6)
                {
                    thoriumPlayer.circuitCharge++;
                    for (int i = 0; i < 1; i++)
                    {
                        int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 185, 0f, 0f, 100, default(Color), 1f);
                        Main.dust[num].noGravity = true;
                    }
                }
            }
        }

        public class ConduitEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ConduitEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ConduitHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ConduitSuit>());
            recipe.AddIngredient(ModContent.ItemType<ConduitLeggings>());
            recipe.AddIngredient(ModContent.ItemType<VegaPhaser>());
            recipe.AddIngredient(ModContent.ItemType<ElectroRebounder>(), 300);
            recipe.AddIngredient(ModContent.ItemType<AncientSpark>());

            recipe.AddTile(TileID.CrystalBall);

            recipe.Register();
        }
    }
}
