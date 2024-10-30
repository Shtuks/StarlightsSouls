using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using System.Collections.Generic;
using Terraria.Localization;
using ThoriumMod.Items.HealerItems;
using ssm.Core;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using ThoriumMod.Items.Terrarium;
using ThoriumMod.Projectiles.Healer;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Thorium.Enchantments;
using FargowiltasSouls.Core.Toggler.Content;
using static ssm.Thorium.Enchantments.CyberPunkEnchant;

namespace ssm.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class GuardianAngelsSoul : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 750000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.thorium.Name, "GraveGoods").UpdateAccessory(player, hideVisual);
            Thorium(player);
        }

        private void Thorium(Player player)
        {
            //general
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            player.GetDamage<HealerDamage>() += 0.30f;
            player.GetCritChance<HealerDamage>() += 0.15f;
            player.GetAttackSpeed<HealerDamage>() += 0.20f;
            player.GetAttackSpeed((DamageClass)ThoriumDamageBase<HealerTool>.Instance) += 0.20f;
            player.GetModPlayer<ThoriumPlayer>().healBonus += 20;

            //support stash
            thoriumPlayer.accSupportSash = true;
            //ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SupportSash").UpdateAccessory(player, true);

            //saving grace
            //thoriumPlayer.crossHeal = true;
            //thoriumPlayer.healBloom = true;

            //soul guard
            thoriumPlayer.graveGoods = true;

            //for (int i = 0; i < 255; i++)
            //{
            //    Player player2 = Main.player[i];
            //    if (player2.active && player2 != player && Vector2.Distance(player2.Center, player.Center) < 400f)
            //    {
                    //player2.AddBuff(thorium.BuffType("AegisAura"), 30, false);
            //    }
            //}

            //archdemon's curse
            thoriumPlayer.darkAura = true;

            //archangels heart
            thoriumPlayer.healBonus += 20;

            //medical bag
            thoriumPlayer.medicalAcc = true;


            //head mirror arrow 
            if (player.AddEffect<GuardianEffect>(Item))
            {
                ModContent.Find<ModItem>(this.thorium.Name, "MedicalBag").UpdateAccessory(player, true);
                /*float num = 0f;
                int num2 = player.whoAmI;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && Main.player[i] != player && !Main.player[i].dead && (Main.player[i].statLifeMax2 - Main.player[i].statLife) > num)
                    {
                        num = (Main.player[i].statLifeMax2 - Main.player[i].statLife);
                        num2 = i;
                    }
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<HealerSymbol>()] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<HealerSymbol>(), 0, 0f, player.whoAmI, 0f, 0f);
                }
                for (int j = 0; j < 1000; j++)
                {
                    Projectile projectile = Main.projectile[j];
                    if (projectile.active && projectile.owner == player.whoAmI && projectile.type == ModContent.ProjectileType<HealerSymbol>())
                    {
                        projectile.timeLeft = 2;
                        projectile.ai[1] = num2;
                    }
                }*/
            }
        }

        public class GuardianEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<GuardianAngelsSoul>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(null, "HealerEssence");
            recipe.AddIngredient<AbomEnergy>(10);
            recipe.AddIngredient<SupportSash>();
            recipe.AddIngredient<SavingGrace>();
            recipe.AddIngredient<SoulGuard>();
            recipe.AddIngredient<ArchDemonCurse>();
            recipe.AddIngredient<ArchangelHeart>();
            recipe.AddIngredient<MedicalBag>();
            recipe.AddIngredient<TeslaDefibrillator>();
            recipe.AddIngredient<MoonlightStaff>();
            recipe.AddIngredient<TerrariumHolyScythe>();
            recipe.AddIngredient<TerraScythe>();
            recipe.AddIngredient<PhoenixStaff>();
            recipe.AddIngredient<ShieldDroneBeacon>();
            recipe.AddIngredient<LifeAndDeath>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
