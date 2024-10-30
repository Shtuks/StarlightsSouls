using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ID;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Fargowiltas.Items.Tiles;
using Terraria.Localization;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Content.Items.Accessories;
using ssm.Core;
using ssm;
using ssm.Content.Buffs.Minions;
using FargowiltasSouls.Core.Toggler.Content;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Content.Items.Accessories
{
    public class CelestialEnchant : BaseEnchant
    {
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");

        public override void SetStaticDefaults() => ItemID.Sets.ItemNoGravity[this.Type] = true;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExtraContent;
        }

        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(1, 0, 0, 0);
            this.Item.rare = 10;
            this.Item.accessory = true;
        }

        public override Color nameColor => new(255, 255, 255);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<CelestialEffect>(Item))
            {
                player.GetModPlayer<ShtunPlayer>().CelestialPower = true;
                player.AddBuff(ModContent.BuffType<CelestialPowerBuff>(), 2);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe(1);
            recipe.AddIngredient(3456, 15);
            recipe.AddIngredient(3457, 15);
            recipe.AddIngredient(3458, 15);
            recipe.AddIngredient(3459, 15);
            recipe.AddIngredient(3467, 10);

            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();

            if (ModLoader.TryGetMod("CalamityMod", out Mod kal))
            {
                CreateRecipe()
                    .AddIngredient(kal.Find<ModItem>("GalacticaSingularity").Type, 10)
                    .AddIngredient(kal.Find<ModItem>("AstralBar").Type, 10)
                    .AddIngredient(3467, 25)
                    .AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"))
                    .Register();
            }
        }

        public class CelestialEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<CosmoHeader>();
            public override int ToggleItemType => ModContent.ItemType<CelestialEnchant>();
        }
    }
}
