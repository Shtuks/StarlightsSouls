using Terraria.ModLoader;
using Terraria;
using CalValEX.Items.Plushies;
using ssm.Core;

namespace ssm.Content.Items.ShtuxibusPlush
{
    [ExtendsFromMod("CalValEX")]
    [JITWhenModsEnabled("CalValEX")]
    public class ShtuxibusFumo : ModItem
    {
        public override string Texture => "ssm/Content/Items/ShtuxibusPlush/ShtuxibusPlush";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.rare = -1;
            Item.value = 40;
            Item.maxStack = 99;
        }

        public override void UpdateInventory(Player player)
        {
            Item.SetDefaults(PlushManager.PlushItems["Shtuxibus"]);
        }
    }
}