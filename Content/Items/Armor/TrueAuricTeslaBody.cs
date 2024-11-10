using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Content.Items.Armor
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [AutoloadEquip(EquipType.Body)]
    public class TrueAuricTeslaBody : ModItem
    {
        private readonly Mod calamity = Terraria.ModLoader.ModLoader.GetMod("CalamityMod");

        public override void SetDefaults()
        {
            ((Entity)this.Item).width = 18;
            ((Entity)this.Item).height = 18;
            this.Item.rare = 11;
            this.Item.expert = true;
            this.Item.value = Item.sellPrice(10, 0, 0, 0);
            this.Item.defense = 50;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.4f;
            player.GetCritChance(DamageClass.Generic) += 2f;
            player.statLifeMax2 += 300;
            player.statManaMax2 += 300;
            player.endurance += 0.5f;
            player.lifeRegen += 5;
            player.lifeRegenCount += 5;
            player.lifeRegenTime += 5;
        }

        public override void AddRecipes()
        {
        }
    }
}
