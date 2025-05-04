using CalamityMod.Buffs.Summon;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.GodSlayer;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using ssm.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Weapons.Summon;
using ssm.Content.SoulToggles;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class GodSlayerEnchant : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        private readonly Mod calamitybardhealer = ModLoader.GetMod("CalamityBardHealer");

        private readonly Mod ragnarok = ModLoader.GetMod("RagnarokMod");

        public override Color nameColor => new(100, 108, 156);

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<MechwormEffect>(Item))
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source_ItemUse = player.GetSource_ItemUse(base.Item);

                    if (player.FindBuffIndex(ModContent.BuffType<Mechworm>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<Mechworm>(), 3600);
                    }

                    int headIndex = -1;

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<MechwormHead>()] < 1)
                    {
                        int num = player.ApplyArmorAccDamageBonusesTo(140f);
                        Projectile head = Projectile.NewProjectileDirect(source_ItemUse, player.Center, -Vector2.UnitY, ModContent.ProjectileType<MechwormHead>(), num, 0f, player.whoAmI);
                        head.originalDamage = num;
                        headIndex = head.whoAmI; 
                    }

                    int previousSegmentIndex = headIndex;
                    for (int i = 0; i < 15; i++)
                    {
                        if (player.ownedProjectileCounts[ModContent.ProjectileType<MechwormBody>()] < 15)
                        {
                            int num = player.ApplyArmorAccDamageBonusesTo(140f);
                            Vector2 position = player.Center + new Vector2(0, -20 * (i + 1));
                            Projectile body = Projectile.NewProjectileDirect(source_ItemUse, position, -Vector2.UnitY, ModContent.ProjectileType<MechwormBody>(), num, 0f, player.whoAmI);
                            body.originalDamage = num;
                            body.ai[0] = previousSegmentIndex; 
                            previousSegmentIndex = body.whoAmI;
                        }
                    }

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<MechwormTail>()] < 1)
                    {
                        int num = player.ApplyArmorAccDamageBonusesTo(140f);
                        Vector2 tailPosition = player.Center + new Vector2(0, -20 * 16);
                        Projectile tail = Projectile.NewProjectileDirect(source_ItemUse, tailPosition, -Vector2.UnitY, ModContent.ProjectileType<MechwormTail>(), num, 0f, player.whoAmI);
                        tail.originalDamage = num;
                        tail.ai[0] = previousSegmentIndex; 
                    }
                }
            }
            if (player.AddEffect<OmniSpeakerEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamitybardhealer.Name, "OmniSpeaker").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<VeneratedEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "VeneratedLocket").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<UniversalHeadsetEffect>(Item))
            {
                ModContent.Find<ModItem>(this.ragnarok.Name, "UniversalHeadset").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DimSoulEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "DimensionalSoulArtifact").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "GodSlayerHeadMelee").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "GodSlayerHeadRanged").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "GodSlayerHeadRogue").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "GodSlayerDeathsingerCowl").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "GodSlayerHeadBard").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "GodSlayerChestplate").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "GodSlayerLeggings").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GodSlayerHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<GodSlayerHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<GodSlayerHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<GodSlayerChestplate>());
            recipe.AddIngredient(ModContent.ItemType<GodSlayerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<VeneratedLocket>());
            recipe.AddIngredient(ModContent.ItemType<DimensionalSoulArtifact>());
            recipe.AddIngredient(ModContent.ItemType<StaffoftheMechworm>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class OmniSpeakerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GodSlayerEnchant>();
        }
        public class VeneratedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GodSlayerEnchant>();
        }
        public class MechwormEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GodSlayerEnchant>();
        }
        public class UniversalHeadsetEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GodSlayerEnchant>();
        }
        public class DimSoulEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GodSlayerEnchant>();
        }
    }
}
