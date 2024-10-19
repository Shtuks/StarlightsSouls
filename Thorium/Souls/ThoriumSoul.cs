using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;

namespace ssm.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumSoul : BaseSoul
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.value = 5000000;

            Item.rare = -12;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ShtunThoriumPlayer thoriumPlayer = player.GetModPlayer<ShtunThoriumPlayer>();

            thoriumPlayer.ThoriumSoul = true;

            //MUSPELHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "MuspelheimForce").UpdateAccessory(player, hideVisual);
            //JOTUNHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "JotunheimForce").UpdateAccessory(player, hideVisual);
            //ALFHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "AlfheimForce").UpdateAccessory(player, hideVisual);
            //NIFLHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "NiflheimForce").UpdateAccessory(player, hideVisual);
            //SVARTALFHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SvartalfheimForce").UpdateAccessory(player, hideVisual);
            //MIDGARD
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "MidgardForce").UpdateAccessory(player, hideVisual);
            //VANAHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "VanaheimForce").UpdateAccessory(player, hideVisual);
            //HELHEIM
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "HelheimForce").UpdateAccessory(player, hideVisual);
            //ASGARD
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "AsgardForce").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<AbomEnergy>(10);
            recipe.AddIngredient(null, "MuspelheimForce");
            recipe.AddIngredient(null, "JotunheimForce");
            recipe.AddIngredient(null, "AlfheimForce");
            recipe.AddIngredient(null, "NiflheimForce");
            recipe.AddIngredient(null, "SvartalfheimForce");
            recipe.AddIngredient(null, "MidgardForce");
            recipe.AddIngredient(null, "VanaheimForce");
            recipe.AddIngredient(null, "HelheimForce");
            recipe.AddIngredient(null, "AsgardForce");

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}