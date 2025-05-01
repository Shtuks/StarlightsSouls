using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using gunrightsmod;
using ssm.Core;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using gunrightsmod.Content.Items;
using ssm.gunrightsmod.Projectiles;
using gunrightsmod.Buffs;
    
namespace ssm.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class SuperCeramicEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.gunrightsmod;
        }

        private readonly Mod gunrightsmod = ModLoader.GetMod("gunrightsmod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SuperCeramicPlayer>().hasSuperCeramic = true;
        }
       
        public class SuperCeramicPlayer : ModPlayer
        {
            public bool hasSuperCeramic;
            private int ceramicCooldown;

            public override void ResetEffects()
            {
                hasSuperCeramic = false;
            }

            public override void PostUpdate()
            {
                if (ceramicCooldown > 0)
                    ceramicCooldown--;
            }

            public override void PostUpdateEquips()
            {
                if (hasSuperCeramic && ceramicCooldown == 0 && !Player.HasBuff(ModContent.BuffType<Buffs.CeramicShell>()))
                {
                    Player.AddBuff(ModContent.BuffType<Buffs.CeramicShell>(), 60 * 60); // 60 seconds duration
                }
            }

            public override void OnHurt(Player.HurtInfo info)
            {
                if (Player.HasBuff(ModContent.BuffType<Buffs.CeramicShell>()))
                {
                    Player.ClearBuff(ModContent.BuffType<Buffs.CeramicShell>());
                    ceramicCooldown = 60 * 30; // 30 second cooldown

                    if (Main.myPlayer == Player.whoAmI)
                    {
                        Vector2 spawnPos = Player.Center;
                        int shardCount = 6;

                        for (int i = 0; i < shardCount; i++)
                        {
                            Vector2 velocity = new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-6f, -2f));
                            Projectile.NewProjectile(Player.GetSource_FromThis(), spawnPos, velocity, ModContent.ProjectileType<CeramicShard>(), 40, 5f, Player.whoAmI);
                        }
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = this.CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<SuperCeramicFedora>());
            recipe1.AddIngredient(ModContent.ItemType<SuperCeramicChestplate>());
            recipe1.AddIngredient(ModContent.ItemType<SuperCeramicLeggings>());
            recipe1.AddIngredient(ModContent.ItemType<CeramicHorseshoeBalloon>());
            recipe1.AddIngredient(ModContent.ItemType<CeramicMachete>());
            recipe1.AddIngredient(ModContent.ItemType<CopperShortmachinegun>());
            recipe1.AddTile(TileID.DemonAltar);
            recipe1.Register();

            Recipe recipe2 = this.CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<SuperCeramicFedora>());
            recipe2.AddIngredient(ModContent.ItemType<SuperCeramicChestplate>());
            recipe2.AddIngredient(ModContent.ItemType<SuperCeramicLeggings>());
            recipe2.AddIngredient(ModContent.ItemType<CeramicHorseshoeBalloon>());
            recipe2.AddIngredient(ModContent.ItemType<CeramicMachete>());
            recipe2.AddIngredient(ModContent.ItemType<TinShortmachinegun>());
            recipe2.AddTile(TileID.DemonAltar);
            recipe2.Register();
        }
    }
}
