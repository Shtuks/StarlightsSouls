using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.Content.Items.Consumables
{
    public class UltimateHealingPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.rare = 11;
            Item.value = Item.buyPrice(gold: 10);

            Item.healLife = 10000;
            Item.potion = true;
        }
        public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
            healValue = player.statLifeMax2;
        }
    }
}