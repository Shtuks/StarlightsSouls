using Terraria;
using Terraria.ModLoader;
using CalamityMod.Items.Accessories;
using ssm.Core;
using SacredTools.Content.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ID;
using System;

namespace ssm.CrossMod.Shields
{
    /*
        * Progression look like this:
        * frozen shield + deific amulet
        * celestial shield
        * shield of reflection
        * rampart
    */

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class RampartShieldRecepies : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Shields;
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // frozen shield + delific amulet to celestial
                if (recipe.HasResult(ModContent.ItemType<CelestialShield>()) && recipe.HasIngredient(ItemID.AnkhShield))
                {
                    recipe.RemoveIngredient(ItemID.AnkhShield);
                    recipe.RemoveIngredient(ItemID.PaladinsShield);
                    recipe.AddIngredient(ItemID.FrozenShield , 1);
                    recipe.AddIngredient<DeificAmulet>(1);
                }
                // celestial to reflection
                // soa code
                // reflections to rampart
                if (recipe.HasResult(ModContent.ItemType<RampartofDeities>()) && recipe.HasIngredient(ItemID.FrozenShield))
                {
                    recipe.RemoveIngredient(ModContent.ItemType<DeificAmulet>());
                    recipe.RemoveIngredient(ItemID.FrozenShield);
                    recipe.AddIngredient<ReflectionShield>(1);
                }
                //rampart to colossus (if no cal dlc)
                if (recipe.HasResult(ModContent.ItemType<ColossusSoul>()) && recipe.HasIngredient(3997))
                {
                    recipe.RemoveIngredient(3997);
                    recipe.AddIngredient<RampartofDeities>(1);
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class RampartShieldEffects : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Shields;
        }
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<CelestialShield>()
                || Item.type == ModContent.ItemType<ReflectionShield>()
                || Item.type == ModContent.ItemType<RampartofDeities>()
                || Item.type == ModContent.ItemType<ColossusSoul>())
            {
                if ((double)player.statLife <= (double)player.statLifeMax2 * 0.5){player.AddBuff(62, 5);}
                player.noKnockback = true;
                if (!((float)player.statLife > (float)player.statLifeMax2 * 0.25f)){return;}
                player.hasPaladinShield = true;
                if (player.whoAmI == Main.myPlayer || player.miscCounter % 10 != 0){return;}
                int myPlayer = Main.myPlayer;
                if (Main.player[myPlayer].team == player.team && player.team != 0){
                    float num = player.position.X - Main.player[myPlayer].position.X;
                    float num2 = player.position.Y - Main.player[myPlayer].position.Y;
                    if ((float)Math.Sqrt(num * num + num2 * num2) < 800f)
                    {Main.player[myPlayer].AddBuff(43, 20);}}
            }
            if (Item.type == ModContent.ItemType<ReflectionShield>()
                || Item.type == ModContent.ItemType<RampartofDeities>()
                || Item.type == ModContent.ItemType<ColossusSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "CelestialShield").UpdateAccessory(player, false);
            }
            if (Item.type == ModContent.ItemType<RampartofDeities>()
                || Item.type == ModContent.ItemType<ColossusSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SacredTools.Name, "ReflectionShiels").UpdateAccessory(player, false);
            }
            if (Item.type == ModContent.ItemType<ColossusSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Calamity.Name, "RampartofDeities").UpdateAccessory(player, false);
            }
        }
    }

}

