using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ShtuxibusSuit : ModItem
    {
        public override void Load()
        {
            if (Main.netMode == 2)
                return;
            EquipLoader.AddEquipTexture(((ModType)this).Mod, "ssm/Content/Items/Armor/ShtuxibusSuit_Neck", (EquipType)11, (ModItem)this, (string)null, (EquipTexture)null);
            EquipLoader.AddEquipTexture(((ModType)this).Mod, "ssm/Content/Items/Armor/ShtuxibusSuit_Back", (EquipType)5, (ModItem)this, (string)null, (EquipTexture)null);
        }
        public override void SetDefaults()
        {
            ((Entity)this.Item).width = 42;
            ((Entity)this.Item).height = 38;
            this.Item.rare = 11;
            this.Item.expert = true;
            this.Item.value = Item.sellPrice(10, 0, 0, 0);
        }

        public override void AddRecipes()
        {
        }
    }
}
