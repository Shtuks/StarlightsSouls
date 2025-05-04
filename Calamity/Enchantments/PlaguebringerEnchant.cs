using CalamityMod.Buffs.Summon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.CalPlayer;
using ssm.Core;
using ssm.Content.SoulToggles;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Plaguebringer;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod;
using Terraria.DataStructures;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class PlaguebringerEnchant : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(0, 255, 0);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<VriliEffect>(Item))
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source_ItemUse = player.GetSource_ItemUse(base.Item);
                    if (player.FindBuffIndex(ModContent.BuffType<ViriliBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<ViriliBuff>(), 3600);
                    }

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<PlaguePrincess>()] < 1)
                    {
                        int num = player.ApplyArmorAccDamageBonusesTo(140f);
                        Projectile.NewProjectileDirect(source_ItemUse, player.Center, -Vector2.UnitY, ModContent.ProjectileType<PlaguePrincess>(), num, 0f, player.whoAmI).originalDamage = num;
                    }
                }
            }
            if (player.AddEffect<TheBeeEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "TheBee").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "PlaguebringerVisor").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "PlaguebringerCarapace").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "PlaguebringerPistons").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PlaguebringerVisor>());
            recipe.AddIngredient(ModContent.ItemType<PlaguebringerCarapace>());
            recipe.AddIngredient(ModContent.ItemType<PlaguebringerPistons>());
            recipe.AddIngredient(ModContent.ItemType<InfectedRemote>());
            recipe.AddIngredient(ModContent.ItemType<TheBee>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class TheBeeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlaguebringerEnchant>();
        }
        public class VriliEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlaguebringerEnchant>();
        }
    }
}
