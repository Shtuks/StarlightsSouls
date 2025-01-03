using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static SacredTools.Utilities.SoASets;

namespace ssm.SHTUK
{
    public class ModulesPlayer : ModPlayer
    {
        public bool radarModule;
        public bool teleportModule;
        public bool ressurectionModule;
        public bool noOverloadModule;

        public int shieldModule;
        public int servoModule;
        public int magicModule;
        public int defenceModule;
        public int lifeModule;
        public int offenceModule;
        public int energyModule;
        public int summonModule;

        public override void SaveData(TagCompound tag)
        {
            tag["teleportModule"] = teleportModule;
            tag["radarModule"] = radarModule;
            tag["ressurectionModule"] = ressurectionModule;
            tag["noOverloadModule"] = noOverloadModule;

            tag["shieldModule"] = shieldModule;
            tag["servoModule"] = servoModule;
            tag["offenceModule"] = offenceModule;
            tag["defenceModule"] = defenceModule;
            tag["lifeModule"] = lifeModule;
            tag["energyModule"] = energyModule;
            tag["magicModule"] = magicModule;
            tag["summonModule"] = summonModule;
        }
        public override void LoadData(TagCompound tag)
        {
            teleportModule = tag.GetBool("teleportModule");
            radarModule = tag.GetBool("radarModule");
            noOverloadModule = tag.GetBool("noOverloadModule");
            ressurectionModule = tag.GetBool("ressurectionModule");

            shieldModule = tag.GetInt("shieldModule");
            servoModule = tag.GetInt("servoModule");
            offenceModule = tag.GetInt("offenceModule");
            defenceModule = tag.GetInt("defenceModule");
            lifeModule = tag.GetInt("lifeModule");
            energyModule = tag.GetInt("energyModule");
            magicModule = tag.GetInt("magicModule");
            summonModule = tag.GetInt("summonModule");
        }

        public override void PostUpdateMiscEffects()
        {
            if (Player.SHTUK().isAdvancedCyborg || Player.SHTUK().isShtuxiumCyborg)
            {
                if (radarModule)
                {
                    Player.accWatch = 3;
                    Player.accDepthMeter = 1;
                    Player.accCompass = 1;
                    Player.accFishFinder = true;
                    Player.accDreamCatcher = true;
                    Player.accOreFinder = true;
                    Player.accStopwatch = true;
                    Player.accCritterGuide = true;
                    Player.accJarOfSouls = true;
                    Player.accThirdEye = true;
                    Player.accCalendar = true;
                    Player.accWeatherRadio = true;
                    Player.chiselSpeed = true;
                    Player.treasureMagnet = true;
                    Player.nightVision = true;
                    Player.findTreasure = true;
                    Player.detectCreature = true;
                    Player.dangerSense = true;
                    Lighting.AddLight(Player.Center, 0.8f, 0.8f, 0);

                    Player.SHTUK().energyConsumption += 3;
                }
                if (noOverloadModule)
                {
                    Player.SHTUK().overload = 0;
                    Player.SHTUK().energyConsumption += 5;
                }

                switch (shieldModule)
                {
                    case 0: break;
                    case 1: Player.SHTUK().shieldAdd += 100; Player.SHTUK().shieldRegAdd += 5; Player.SHTUK().energyConsumption += 1; break;
                    case 2: Player.SHTUK().shieldAdd += 200; Player.SHTUK().shieldRegAdd += 10; Player.SHTUK().energyConsumption += 2; break;
                    case 3: Player.SHTUK().shieldAdd += 400; Player.SHTUK().shieldRegAdd += 20; Player.SHTUK().energyConsumption += 3; break;
                    case 4: Player.SHTUK().shieldAdd += 800; Player.SHTUK().shieldRegAdd += 40; Player.SHTUK().energyConsumption += 4; break;
                    case 5: Player.SHTUK().shieldAdd += 1600; Player.SHTUK().shieldRegAdd += 80; Player.SHTUK().energyConsumption += 5; break;
                }
                switch (servoModule)
                {
                    case 0: break;
                    case 1: Player.SHTUK().miningAdd += 1; Player.SHTUK().speedMult += 25; Player.SHTUK().energyConsumption += 1; break;
                    case 2: Player.SHTUK().miningAdd += 2; Player.SHTUK().speedMult += 50; Player.SHTUK().energyConsumption += 2; break;
                    case 3: Player.SHTUK().miningAdd += 4; Player.SHTUK().speedMult += 75; Player.SHTUK().energyConsumption += 3; break;
                    case 4: Player.SHTUK().miningAdd += 8; Player.SHTUK().speedMult += 100; Player.SHTUK().energyConsumption += 4; break;
                    case 5: Player.SHTUK().miningAdd += 16; Player.SHTUK().speedMult += 125; Player.SHTUK().energyConsumption += 5; break;
                }
                switch (offenceModule)
                {
                    case 0: break;
                    case 1: Player.SHTUK().penetrationAdd += 25; Player.SHTUK().critAdd += 10; Player.SHTUK().damageMult += 100; Player.SHTUK().energyConsumption += 1; break;
                    case 2: Player.SHTUK().penetrationAdd += 50; Player.SHTUK().critAdd += 30; Player.SHTUK().damageMult += 200; Player.SHTUK().energyConsumption += 2; break;
                    case 3: Player.SHTUK().penetrationAdd += 75; Player.SHTUK().critAdd += 50; Player.SHTUK().damageMult += 300; Player.SHTUK().energyConsumption += 3; break;
                    case 4: Player.SHTUK().penetrationAdd += 100; Player.SHTUK().critAdd += 70; Player.SHTUK().damageMult += 400; Player.SHTUK().energyConsumption += 4; break;
                    case 5: Player.SHTUK().penetrationAdd += 125; Player.SHTUK().critAdd += 100; Player.SHTUK().damageMult += 500; Player.SHTUK().energyConsumption += 5; break;
                }
                switch (defenceModule)
                {
                    case 0: break;
                    case 1: Player.SHTUK().drAdd += 10; Player.SHTUK().defenceAdd += 10; Player.SHTUK().energyConsumption += 1; break;
                    case 2: Player.SHTUK().drAdd += 20; Player.SHTUK().defenceAdd += 20; Player.SHTUK().energyConsumption += 2; break;
                    case 3: Player.SHTUK().drAdd += 30; Player.SHTUK().defenceAdd += 40; Player.SHTUK().energyConsumption += 3; break;
                    case 4: Player.SHTUK().drAdd += 40; Player.SHTUK().defenceAdd += 80; Player.SHTUK().energyConsumption += 4; break;
                    case 5: Player.SHTUK().drAdd += 50; Player.SHTUK().defenceAdd += 160; Player.SHTUK().energyConsumption += 5; break;
                }
                switch (lifeModule)
                {
                    case 0: break;
                    case 1: Player.SHTUK().regenAdd += 10; Player.SHTUK().lifeAdd += 100; Player.SHTUK().energyConsumption += 1; break;
                    case 2: Player.SHTUK().regenAdd += 20; Player.SHTUK().lifeAdd += 200; Player.SHTUK().energyConsumption += 2; break;
                    case 3: Player.SHTUK().regenAdd += 30; Player.SHTUK().lifeAdd += 300; Player.SHTUK().energyConsumption += 3; break;
                    case 4: Player.SHTUK().regenAdd += 40; Player.SHTUK().lifeAdd += 400; Player.SHTUK().energyConsumption += 4; break;
                    case 5: Player.SHTUK().regenAdd += 50; Player.SHTUK().lifeAdd += 500; Player.SHTUK().energyConsumption += 5; break;
                }
                switch (magicModule)
                {
                    case 0: break;
                    case 1: Player.SHTUK().manaAdd += 100; Player.SHTUK().manaRegAdd += 10; Player.SHTUK().energyConsumption += 1; break;
                    case 2: Player.SHTUK().manaAdd += 200; Player.SHTUK().manaRegAdd += 20; Player.SHTUK().energyConsumption += 2; break;
                    case 3: Player.SHTUK().manaAdd += 300; Player.SHTUK().manaRegAdd += 30; Player.SHTUK().energyConsumption += 3; break;
                    case 4: Player.SHTUK().manaAdd += 400; Player.SHTUK().manaRegAdd += 40; Player.SHTUK().energyConsumption += 4; break;
                    case 5: Player.SHTUK().manaAdd += 500; Player.SHTUK().manaRegAdd += 50; Player.SHTUK().energyConsumption += 5; break;
                }
                switch (summonModule)
                {
                    case 0: break;
                    case 1: Player.SHTUK().sentryAdd += 5; Player.SHTUK().minionsAdd += 5; Player.SHTUK().energyConsumption += 1; break;
                    case 2: Player.SHTUK().sentryAdd += 10; Player.SHTUK().minionsAdd += 10; Player.SHTUK().energyConsumption += 2; break;
                    case 3: Player.SHTUK().sentryAdd += 15; Player.SHTUK().minionsAdd += 15; Player.SHTUK().energyConsumption += 3; break;
                    case 4: Player.SHTUK().sentryAdd += 20; Player.SHTUK().minionsAdd += 20; Player.SHTUK().energyConsumption += 4; break;
                    case 5: Player.SHTUK().sentryAdd += 25; Player.SHTUK().minionsAdd += 25; Player.SHTUK().energyConsumption += 5; break;
                }
                switch (energyModule)
                {
                    case 0: break;
                    case 1: Player.SHTUK().energyMax2 += 10000; Player.SHTUK().energyRegen += 1; Player.SHTUK().energyRegenCharging += 10; break;
                    case 2: Player.SHTUK().energyMax2 += 30000; Player.SHTUK().energyRegen += 3; Player.SHTUK().energyRegenCharging += 15; break;
                    case 3: Player.SHTUK().energyMax2 += 50000; Player.SHTUK().energyRegen += 5; Player.SHTUK().energyRegenCharging += 20; break;
                    case 4: Player.SHTUK().energyMax2 += 70000; Player.SHTUK().energyRegen += 7; Player.SHTUK().energyRegenCharging += 25; break;
                    case 5: Player.SHTUK().energyMax2 += 100000; Player.SHTUK().energyRegen += 10; Player.SHTUK().energyRegenCharging += 30; break;
                }
            }
        }
    }
}