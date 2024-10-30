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
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Fargowiltas.Items.Tiles;
using Terraria.Localization;
using Terraria.DataStructures;
using FargowiltasSouls.Core.Toggler;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Content.Items.Accessories;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Content.Items.Accessories
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    public class ChtuxlagorHeart : SoulsItem
    {
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExtraContent;
        }

        public override void SetStaticDefaults() => ItemID.Sets.ItemNoGravity[this.Type] = true;

        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(1000, 0, 0, 0);
            this.Item.rare = ItemRarityID.Purple;
            this.Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<FargoSoulsPlayer>().MutantEyeCD > 0) { player.GetModPlayer<FargoSoulsPlayer>().MutantEyeCD--; }
            player.GetModPlayer<ShtunPlayer>().ChtuxlagorHeart = true;
            player.buffImmune[ModContent.BuffType<Buffs.ShtuxibusCurse>()] = true;
            if (player.AddEffect<ChtuxlagorHeartEffect>(Item)) { }
        }

        public class ChtuxlagorHeartHead : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChtuxlagorHeartHeader>();
            public override int ToggleItemType => ModContent.ItemType<ChtuxlagorHeart>();
        }

        public class ChtuxlagorHeartEffect : ChtuxlagorHeartHead
        {
            public override int ToggleItemType => ModContent.ItemType<ChtuxlagorHeart>();
            public override bool IgnoresMutantPresence => true;
        }
    }
}
