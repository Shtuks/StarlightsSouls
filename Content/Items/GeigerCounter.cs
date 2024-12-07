using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items
{
    public class GeigerCounter : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 100;
            Item.value = 10000;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.accessory = true;
        }

        public override void UpdateInventory(Player player)
        {
            player.Shtun().geiger = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shtun().geiger = true;
        }

        public override bool? UseItem(Player player)
        {
            string v1 = player.Radiation().radiation.ToString();
            string v2 = player.Radiation().radiationResistance.ToString();
            string v3 = player.Radiation().lethalRad.ToString();
            string v4 = player.Radiation().irradiated.ToString();
            string v5 = player.Radiation().irradiationSpeed.ToString();
            string v6 = player.Radiation().antiradRegen.ToString();

            ShtunUtils.DisplayLocalizedText("RAD:                    " + v1, Color.Red);
            ShtunUtils.DisplayLocalizedText("RAD resistance:         " + v2, Color.Yellow);
            ShtunUtils.DisplayLocalizedText("Lethal RAD:             " + v3, Color.Blue);
            ShtunUtils.DisplayLocalizedText("Is Irradiated:          " + v4, Color.Green);
            ShtunUtils.DisplayLocalizedText("Irradiation speed:      " + v5, Color.Blue);
            ShtunUtils.DisplayLocalizedText("Antirad speed:          " + v6, Color.Blue);
            return true;
        }
    }
}