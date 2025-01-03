using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.SHTUK
{
    public class FullModulesDebug : ModItem
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
            player.Modules().defenceModule = 5;
            player.Modules().energyModule = 5;
            player.Modules().magicModule = 5;
            player.Modules().offenceModule = 5;
            player.Modules().servoModule = 5;
            player.Modules().teleportModule = true;
            player.Modules().radarModule = true;
            player.Modules().ressurectionModule = true;
            player.Modules().noOverloadModule = true;
            player.Modules().lifeModule = 5;
            player.Modules().summonModule = 5;
            return true;
        }
    }
}