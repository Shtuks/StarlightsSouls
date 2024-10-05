using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class TrueMonstrositySuit : ModItem
    {
        private readonly Mod calamity = Terraria.ModLoader.ModLoader.GetMod("CalamityMod");

        public override void SetDefaults()
        {
            ((Entity)this.Item).width = 18;
            ((Entity)this.Item).height = 18;
            this.Item.rare = 11;
            this.Item.expert = true;
            this.Item.value = Item.sellPrice(10, 0, 0, 0);
            this.Item.defense = 100;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.6f;
            player.GetCritChance(DamageClass.Generic) += 2f;
            player.statLifeMax2 += 1000;
            player.statManaMax2 += 1000;
            player.endurance += 0.5f;
            player.lifeRegen += 7;
            player.lifeRegenCount += 7;
            player.lifeRegenTime += 7;
        }

        public override void AddRecipes()
        {
        }
    }
}
