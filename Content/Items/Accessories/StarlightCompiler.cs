using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Items.Accessories
{
    public class StarlightCompiler : ModItem
    {

        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(10000, 0, 0, 0);
            this.Item.rare = 10;
            this.Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shtun().trueDevSets = true;
        }
    }
}
