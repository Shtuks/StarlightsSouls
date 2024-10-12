using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Souls;

namespace ssm.Thorium.Essences
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class HealerEssence : BaseSoul
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 4;
            Item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            HealEffect(player);
        }

        private void HealEffect(Player player)
        {
            //ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //thoriumPlayer.radiantBoost += 0.18f;
            //thoriumPlayer.radiantSpeed -= 0.05f;
            //thoriumPlayer.healingSpeed += 0.05f;
            //thoriumPlayer.radiantCrit += 5;
        }
    }
}
