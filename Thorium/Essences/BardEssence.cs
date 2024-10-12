using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;
using ssm.Core;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Souls;

namespace ssm.Thorium.Essences
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BardEssence : BaseSoul
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
            BardEffect(player);
        }

        private void BardEffect(Player player)
        {
            //ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //thoriumPlayer.symphonicDamage += 0.18f;
            //thoriumPlayer.symphonicSpeed += .05f;
            //thoriumPlayer.symphonicCrit += 5;
        }
    }
}
