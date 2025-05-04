using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.OmegaBlue;
using CalamityMod.Items.Weapons.Summon;
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
    public class OmegaBlueEnchant : BaseEnchant
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
        public override Color nameColor => new(31, 79, 156);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<TruffleEffect>(Item))
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source_ItemUse = player.GetSource_ItemUse(base.Item);
                    if (player.FindBuffIndex(ModContent.BuffType<MutatedTruffleBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<MutatedTruffleBuff>(), 3600);
                    }

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<MutatedTruffleMinion>()] < 1)
                    {
                        int num = player.ApplyArmorAccDamageBonusesTo(140f);
                        Projectile.NewProjectileDirect(source_ItemUse, player.Center, -Vector2.UnitY, ModContent.ProjectileType<MutatedTruffleMinion>(), num, 0f, player.whoAmI).originalDamage = num;
                    }
                }
            }
            if (player.AddEffect<ReaperEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "ReaperToothNecklace").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<OldScaleEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "OldDukeScales").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<OmegaBlueEffect>(Item))
            {
                modPlayer.omegaBlueSet = true;
                player.maxMinions += 2;
            }

        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueHelmet>());
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueChestplate>());
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueTentacles>());
            recipe.AddIngredient(ModContent.ItemType<OldDukeScales>());
            recipe.AddIngredient(ModContent.ItemType<ReaperToothNecklace>());
            recipe.AddIngredient(ModContent.ItemType<MutatedTruffle>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class TruffleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OmegaBlueEnchant>();
        }
        public class ReaperEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OmegaBlueEnchant>();
        }
        public class OldScaleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OmegaBlueEnchant>();
        }
        public class OmegaBlueEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OmegaBlueEnchant>();
        }
    }
}
