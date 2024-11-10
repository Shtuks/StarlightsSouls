using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ID;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Fargowiltas.Items.Tiles;
using Terraria.Localization;
using Terraria.DataStructures;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Content.Items.Accessories;
using ssm.Core;
using CalamityMod.Items.Accessories;
using ssm.Content.Tiles;


namespace ssm.Calamity.Souls
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class CalamitySoul : BaseSoul
    {
        private readonly Mod FargoCross = Terraria.ModLoader.ModLoader.GetMod("FargowiltasCrossmod");
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");

        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(10, 0, 0, 0);
            this.Item.rare = 11;
            this.Item.accessory = true;
            this.Item.defense = 90;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.FargoCross.Name, "ExplorationForce").UpdateAccessory(player, false);
            ModContent.Find<ModItem>(this.FargoCross.Name, "BrandoftheBrimstoneWitch").UpdateAccessory(player, false);
            if (player.AddEffect<ShatteredCommunityEffect>(Item))
                ModContent.GetInstance<ShatteredCommunity>().UpdateAccessory(player, hideVisual);

            player.buffImmune[ModContent.Find<ModBuff>(this.FargoCross.Name, "CalamitousPresenceBuff").Type] = true;
        }

        public override void AddRecipes()
        {
          Recipe recipe = this.CreateRecipe(1);
            recipe.AddIngredient<ShatteredCommunity>();

            recipe.AddIngredient(this.FargoCross, "ExplorationForce", 1);
            recipe.AddIngredient(this.FargoCross, "BrandoftheBrimstoneWitch", 1);
            recipe.AddIngredient(this.FargoSoul, "AbomEnergy", 10);
            
            recipe.AddTile<DemonshadeWorkbenchTile>();

            recipe.Register();
        }

        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        [ExtendsFromMod(ModCompatibility.Calamity.Name)]
        public abstract class CalamitySoulEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<CalamitySoulHeader>();
        }

        public class ShatteredCommunityEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<ShatteredCommunity>();
            public override bool MutantsPresenceAffects => true;
        }
    }
}
