using FargowiltasSouls.Content.Items;
using Terraria.ID;
using Terraria;
using ssm.Content.DamageClasses;

namespace ssm.Content.Items.Tools
{
    public class CSOCP : SoulsItem
    {
        int mode;
        public override void SetDefaults()
        {
            Item.damage = int.MaxValue / 100;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 1;
            Item.useTime = 1;
            Item.width = 72;
            Item.height = 72;
            Item.rare = ItemRarityID.Gray;
            Item.value = int.MaxValue;
            Item.DamageType = ShtuxianDamage.Instance;
            Item.autoReuse = true;

            switch (mode)
            {
            case 0: Item.pick = int.MaxValue / 100; Item.tileBoost = 100; break;
            case 1: Item.axe = int.MaxValue / 100; Item.tileBoost = 20; break;
            case 3: Item.hammer = int.MaxValue / 100; Item.tileBoost = 20; break;
            default: mode = 0; goto case 0;
            }
        }

        public override void HoldItem(Player player)
        {
            if(Main.mouseRightRelease && Main.mouseRight)
            {
                mode = (mode+ 1) % 3;
                UpdateStats();
            }
        }

        public void UpdateStats()
        {
            switch (mode)
            {
                case 0:
                    Item.axe = 0;
                    Item.hammer = 0;
                    Item.pick = int.MaxValue / 100; 
                    Item.tileBoost = 100; 
                    break;
                case 1:
                    Item.axe = int.MaxValue / 100;
                    Item.hammer = 0;
                    Item.pick = 0;
                    Item.tileBoost = 20;
                    break;
                case 2:
                    Item.axe = 0;
                    Item.hammer = int.MaxValue / 100;
                    Item.pick = 0;
                    Item.tileBoost = 20;
                    break;
            }
        }
    }
}