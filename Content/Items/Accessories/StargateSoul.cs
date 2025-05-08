using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using ssm.CrossMod.CraftingStations;
using ssm.Content.Items.Consumables;
using Terraria.DataStructures;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class StargateSoul : AnticheatItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Type] = true;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 9));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            //ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(int.MaxValue, 20f, 5f);
        }
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.value = int.MaxValue;
            Item.rare = 11;
            Item.accessory = true;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            player.wingsLogic = ArmorIDs.Wing.LongTrailRainbowWings;
            ascentWhenFalling = 0.85f;
            if (player.HasEffect<FlightMasteryGravity>())
                ascentWhenFalling *= 1.5f;
            ascentWhenRising = 0.25f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 1.75f;
            constantAscend = 0.135f;
            if (player.controlUp)
            {
                ascentWhenFalling *= 6f;
                ascentWhenRising *= 6f;
                constantAscend *= 6f;
            }
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 18f;
            acceleration = 0.75f;
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((line.Mod == "Terraria" && line.Name == "ItemName") || line.Name == "FlavorText")
            {
                Main.spriteBatch.End(); 
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(42, 66, 99));
                shader.TrySetParameter("secondaryColor", Main.DiscoColor);
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End(); 
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }

        void PassiveEffect(Player player)
        {
            BionomicCluster.PassiveEffect(player, Item);
            AshWoodEnchant.PassiveEffect(player);

            player.AddEffect<AmmoCycleEffect>(Item);

            player.FargoSouls().WoodEnchantDiscount = true;

            //cell phone
            player.accWatch = 3;
            player.accDepthMeter = 1;
            player.accCompass = 1;
            player.accFishFinder = true;
            player.accDreamCatcher = true;
            player.accOreFinder = true;
            player.accStopwatch = true;
            player.accCritterGuide = true;
            player.accJarOfSouls = true;
            player.accThirdEye = true;
            player.accCalendar = true;
            player.accWeatherRadio = true;
        }
        public override void UpdateInventory(Player player) => PassiveEffect(player);
        public override void UpdateVanity(Player player) => PassiveEffect(player);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            PassiveEffect(player);

            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                {
                    player.buffImmune[index] = true;
                    if (ModLoader.TryGetMod("CalamityMod", out Mod kal))
                    {
                        player.buffImmune[ModContent.Find<ModBuff>("CalamityMod", "RageMode").Type] = false;
                        player.buffImmune[ModContent.Find<ModBuff>("CalamityMod", "AdrenalineMode").Type] = false;
                    }
                }
            }

            ModContent.Find<ModItem>(Mod.Name, "EternityForce").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(Mod.Name, "MacroverseSoul").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(Mod.Name, "CyclonicFin").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "EternitySoul").UpdateAccessory(player, false);

            if (ModCompatibility.Calamity.Loaded)
            {
                ModContent.Find<ModItem>(Mod.Name, "CalamitySoul").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(Mod.Name, "AddonsForce").UpdateAccessory(player, false);
            }

            if (ModCompatibility.Crossmod.Loaded)
            {
                ModContent.Find<ModItem>(ModCompatibility.Crossmod.Name, "BrandoftheBrimstoneWitch").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(ModCompatibility.Crossmod.Name, "VagabondsSoul").UpdateAccessory(player, false);
            }

            if (ModCompatibility.SoulsCompat.Loaded)
            {
                ModContent.Find<ModItem>(ModCompatibility.SoulsCompat.Name, "SoulOfTmod").UpdateAccessory(player, false);
            }

            if (ModCompatibility.Spooky.Loaded)
            {
                ModContent.Find<ModItem>(Mod.Name, "HorrorForce").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(Mod.Name, "TerrorForce").UpdateAccessory(player, false);
            }

            if (ModCompatibility.Polarities.Loaded)
            {
                ModContent.Find<ModItem>(Mod.Name, "SpacetimeForce").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(Mod.Name, "WildernessForce").UpdateAccessory(player, false);
                // ModContent.Find<ModItem>(Mod.Name, "OppositionSoul").UpdateAccessory(player, false);
            }

            if (ModCompatibility.BeekeeperClass.Loaded)
            {
                ModContent.Find<ModItem>(Mod.Name, "BeekeeperSoul").UpdateAccessory(player, false);
            }

            if (ModCompatibility.WrathoftheGods.Loaded)
            {
                ModContent.Find<ModItem>(Mod.Name, "SolynsSigil").UpdateAccessory(player, false);
            }

            if (ModCompatibility.SacredTools.Loaded)
            {
                ModContent.Find<ModItem>(Mod.Name, "SoASoul").UpdateAccessory(player, false);
            }

            if (ModCompatibility.Redemption.Loaded)
            {
                //ModContent.Find<ModItem>(Mod.Name, "RedemptionSoul").UpdateAccessory(player, false);
            }

            if (ModCompatibility.Spirit.Loaded)
            {
                //ModContent.Find<ModItem>(Mod.Name, "SpiritSoul").UpdateAccessory(player, false);
            }

            if (ModCompatibility.Orchid.Loaded)
            {
                //ModContent.Find<ModItem>(Mod.Name, "OrchidSoul").UpdateAccessory(player, false);
            }

            if (ModCompatibility.Homeward.Loaded)
            {
                //ModContent.Find<ModItem>(Mod.Name, "HomewardSoul").UpdateAccessory(player, false);
            }

            if (ModCompatibility.Thorium.Loaded)
            {
                ModContent.Find<ModItem>(Mod.Name, "ThoriumSoul").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(Mod.Name, "BardSoul").UpdateAccessory(player, false);
                ModContent.Find<ModItem>(Mod.Name, "GuardianAngelsSoul").UpdateAccessory(player, false);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);

            recipe.AddIngredient<EternityForce>(1);
            recipe.AddIngredient<EternitySoul>(1);
            recipe.AddIngredient<MacroverseSoul>(1);
            recipe.AddIngredient<CyclonicFin>(1);
            //recipe.AddIngredient<ModdedSoul>(1);

            recipe.AddIngredient<Sadism>(30);
            //recipe.AddIngredient<tModLoadiumBar>(30);
            //recipe.AddIngredient<ShardOfStarlight>(30);

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}
