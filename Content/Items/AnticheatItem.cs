using Terraria.DataStructures;
using Terraria.ModLoader.IO;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Items
{
    public abstract class AnticheatItem : ModItem
    {
        //internal bool legitObtained = false;
        //public override void OnCreated(ItemCreationContext context)
        //{
        //    if (context is EntitySource_Loot || context is EntitySource_ItemUse)
        //    {
        //        legitObtained = true;
        //    }
        //}

        //public override void SaveData(TagCompound tag)
        //{
        //    tag["Legit"] = legitObtained;
        //}

        //public override void LoadData(TagCompound tag)
        //{
        //    legitObtained = tag.GetBool("Legit");
        //}

        //public override void UpdateInventory(Player player)
        //{
        //    if (!legitObtained)
        //    {
        //        Item.TurnToAir();

        //        if (player.whoAmI == Main.myPlayer)
        //        {
        //            Main.NewText("This item can't be aquired by cheating!", Microsoft.Xna.Framework.Color.Red);
        //        }
        //    }
        //}
    }
}
