using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.SHTUK
{
    public class CyborgDebug : ModItem
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
            string v1 = player.SHTUK().isCyborg.ToString();
            string v2 = player.SHTUK().isAdvancedCyborg.ToString();
            string v3 = player.SHTUK().isShtuxiumCyborg.ToString();
            string v4 = player.SHTUK().generalMult.ToString();
            string v14 = player.SHTUK().energyConsumption.ToString();
            string v15 = player.SHTUK().energy.ToString();
            string v16 = player.SHTUK().energyMax2.ToString();
            string v17 = player.SHTUK().energyRegenCharging.ToString();
            string v19 = player.SHTUK().energyRegen.ToString();
            string v5 = player.Modules().defenceModule.ToString();
            string v6 = player.Modules().energyModule.ToString();
            string v7 = player.Modules().magicModule.ToString();
            string v8 = player.Modules().offenceModule.ToString();
            string v9 = player.Modules().servoModule.ToString();
            string v10 = player.Modules().teleportModule.ToString();
            string v11 = player.Modules().radarModule.ToString();
            string v12 = player.Modules().noOverloadModule.ToString();
            string v13 = player.Modules().lifeModule.ToString();
            string v18 = player.Modules().ressurectionModule.ToString();

            ShtunUtils.DisplayLocalizedText("/ S.H.T.U.K /", Color.White);
            ShtunUtils.DisplayLocalizedText("Is cyborg:               " + v1, Color.Red);
            ShtunUtils.DisplayLocalizedText("Is advanced:             " + v2, Color.Yellow);
            ShtunUtils.DisplayLocalizedText("Is shtuxium:             " + v3, Color.Green);
            ShtunUtils.DisplayLocalizedText("General Mutliplier:      " + v4, Color.Blue);
            ShtunUtils.DisplayLocalizedText("Consumption:             " + v14, Color.Violet);
            ShtunUtils.DisplayLocalizedText("Energy:                  " + v15, Color.CadetBlue);
            ShtunUtils.DisplayLocalizedText("Energy Max:              " + v16, Color.DarkSalmon);
            ShtunUtils.DisplayLocalizedText("Charging:                " + v17, Color.Brown);
            ShtunUtils.DisplayLocalizedText("Regen:                   " + v19, Color.DarkCyan);
            ShtunUtils.DisplayLocalizedText("/ Modules /", Color.White);
            ShtunUtils.DisplayLocalizedText("Defence:                 " + v5, Color.Purple);
            ShtunUtils.DisplayLocalizedText("Energy:                  " + v6, Color.Cyan);
            ShtunUtils.DisplayLocalizedText("Magic:                   " + v7, Color.YellowGreen);
            ShtunUtils.DisplayLocalizedText("Offence:                 " + v8, Color.Lime);
            ShtunUtils.DisplayLocalizedText("Servo:                   " + v9, Color.Teal);
            ShtunUtils.DisplayLocalizedText("Teleport:                " + v10, Color.Azure);
            ShtunUtils.DisplayLocalizedText("Radar:                   " + v11, Color.BlueViolet);
            ShtunUtils.DisplayLocalizedText("Overload:                " + v12, Color.Chocolate);
            ShtunUtils.DisplayLocalizedText("Life:                    " + v13, Color.Crimson);
            ShtunUtils.DisplayLocalizedText("Ressurection:            " + v18, Color.DarkGray);

            player.SHTUK().addEnergy(null);

            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            player.Modules().defenceModule = 0;
            player.Modules().energyModule = 0;
            player.Modules().magicModule = 0;
            player.Modules().offenceModule = 0;
            player.Modules().servoModule = 0;
            player.Modules().teleportModule = false;
            player.Modules().radarModule = false;
            player.Modules().noOverloadModule = false;
            player.Modules().lifeModule = 0;
            return true;
        }
    }
}