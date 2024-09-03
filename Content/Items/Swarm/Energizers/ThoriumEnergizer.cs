using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ssm.Content.Items.Swarm.Energizers
{
    public class ThoriumEnergizer : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod) {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.rare = 1;
            Item.value = 1000000;
        }
    }
}