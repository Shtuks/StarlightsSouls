using CalamityMod.Items.SummonItems;
using ssm.Core;
using ssm.Items;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class CalPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            for (int i = 0; i < Player.inventory.Length; i++)
            {
                Item item = Player.inventory[i];
                if (item.type == ModContent.ItemType<Terminus>() && item.active)
                {
                    item.SetDefaults(ModContent.ItemType<ShtunTerminus>());
                }
            }
        }
    }
}
