using Fargowiltas.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Content.Items
{
    public class ShieldCheckDebug : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 100;
            Item.value = 10000;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)
        {
            string v1 = player.Shield().shieldCapacity.ToString();
            string v2 = player.Shield().shieldCapacityMax.ToString();
            string v3 = player.Shield().shieldRegenSpeed.ToString();
            string v4 = player.Shield().shieldOn.ToString();
            string v5 = player.Shield().shieldHits.ToString();
            string v6 = player.Shield().shieldHitsCap.ToString();
            string v7 = player.Shield().shieldHitsRegen.ToString();
            string v8 = player.Shield().drawShield.ToString();
            string v9 = player.Shield().noShieldHitsCap.ToString();

            ShtunUtils.DisplayLocalizedText("/ Shield /", Color.White);
            ShtunUtils.DisplayLocalizedText("Capacity:             " + v1, Color.Red);
            ShtunUtils.DisplayLocalizedText("Max Capacity:         " + v2, Color.Yellow);
            ShtunUtils.DisplayLocalizedText("Regen Speed:          " + v3, Color.Green);
            ShtunUtils.DisplayLocalizedText("Shield:               " + v4, Color.Blue);
            ShtunUtils.DisplayLocalizedText("/ Hits Cap /", Color.White);
            ShtunUtils.DisplayLocalizedText("Hits:                 " + v5, Color.Purple);
            ShtunUtils.DisplayLocalizedText("Hits Cap:             " + v6, Color.Cyan);
            ShtunUtils.DisplayLocalizedText("Hits Regen:           " + v7, Color.Lime);
            ShtunUtils.DisplayLocalizedText("No Hits Cap:          " + v9, Color.Pink);
            ShtunUtils.DisplayLocalizedText("/ Other /", Color.White);
            ShtunUtils.DisplayLocalizedText("Draw shield:          " + v8, Color.Orange);
            return true;
        }
    }
}