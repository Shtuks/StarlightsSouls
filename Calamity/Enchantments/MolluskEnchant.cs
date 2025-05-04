using CalamityMod.Buffs.Summon;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Mollusk;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using ssm.Content.SoulToggles;
using ssm.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class MolluskEnchant : BaseEnchant
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
        public override Color nameColor => new(153, 200, 193);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<PearlEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "GiantPearl").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<EmblemEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "AquaticEmblem").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ShellfishEffect>(Item))
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                player.GetDamage<GenericDamageClass>() += 0.1f;
                calamityPlayer.molluskSet = true;
                player.maxMinions += 4;
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source_ItemUse = player.GetSource_ItemUse(base.Item);
                    if (player.FindBuffIndex(ModContent.BuffType<ShellfishBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<ShellfishBuff>(), 3600);
                    }

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<Shellfish>()] < 2)
                    {
                        int num = player.ApplyArmorAccDamageBonusesTo(140f);
                        Projectile.NewProjectileDirect(source_ItemUse, player.Center, -Vector2.UnitY, ModContent.ProjectileType<Shellfish>(), num, 0f, player.whoAmI).originalDamage = num;
                    }
                }

                player.Calamity().wearingRogueArmor = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MolluskShellmet>());
            recipe.AddIngredient(ModContent.ItemType<MolluskShellplate>());
            recipe.AddIngredient(ModContent.ItemType<MolluskShelleggings>());
            recipe.AddIngredient(ModContent.ItemType<GiantPearl>());
            recipe.AddIngredient(ModContent.ItemType<AquaticEmblem>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class PearlEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MolluskEnchant>();
        }
        public class EmblemEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MolluskEnchant>();
        }
        public class ShellfishEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MolluskEnchant>();
        }
    }
}
