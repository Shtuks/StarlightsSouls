using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Microsoft.Xna.Framework.Graphics;
using SacredTools.Content.Items.Armor.Prairie;
using SacredTools.Items.Weapons;
using SacredTools.Content.Items.Armor.Quasar;
using SacredTools.Items.Weapons.Primordia;
using ssm.Core;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class QuasarEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SoAEnchantments;
        }
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 300000;
        }

        public override Color nameColor => new(69, 95, 109);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (player.AddEffect<QuasarEffect>(Item))
            {
                //set bonus
                modPlayer.NovaSetEffect = true;
            }
        }

        public class QuasarEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<QuasarEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<NovaHelmet>();
            recipe.AddIngredient<NovaBreastplate>();
            recipe.AddIngredient<NovaLegs>();
            recipe.AddIngredient<Ainfijarnar>();
            recipe.AddIngredient<NovaknifePack>();
            recipe.AddIngredient<NovaLance>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
