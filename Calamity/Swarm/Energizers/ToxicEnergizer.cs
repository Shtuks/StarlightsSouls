using Terraria.ModLoader;

namespace ssm.Calamity.Swarm.Energizers
{
    public class ToxicEnergizer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.rare = 1;
            Item.value = 100000;
        }
    }
}