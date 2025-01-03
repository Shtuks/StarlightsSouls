using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.SHTUK.Modules
{
    public class OffenceModule : ModItem
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
            Item.value = Item.sellPrice(10, 0, 0, 0);
        }

        public override bool? UseItem(Player player)
        {
            if (player.SHTUK().isAdvancedCyborg)
            {
                player.Modules().offenceModule = 1;
            }
            return true;
        }
    }
}
