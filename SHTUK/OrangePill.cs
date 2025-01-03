using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ssm.SHTUK
{
    public class OrangePill : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 30;
            Item.rare = 11;
            Item.useStyle = 2;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.consumable = true;
            Item.UseSound = SoundID.Item3;
            Item.value = Item.sellPrice(0, 10, 0, 0);
        }

        public override bool? UseItem(Player player)
        {
            if (player.SHTUK().isCyborg)
            {
                player.SHTUK().isCyborg = false;
                player.SHTUK().isAdvancedCyborg = false;
                player.SHTUK().isShtuxiumCyborg = false;
                
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " rejected boundless power in favor of human body."), 745745745, 0);
            }
            return true;
        }
    }
}
