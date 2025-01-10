﻿using Terraria;
using Terraria.ModLoader;

namespace ssm.SHTUK
{
    public class InfinitieEnergyDebug : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = 10000;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.SHTUK().energyMax2 += int.MaxValue - 1000000;
            player.SHTUK().energyRegen += 1000000;
        }
    }
}