using FargowiltasSouls;
using System.Collections.Generic;
using FargowiltasSouls.Content.Items.Armor;
using Terraria;
using Terraria.ModLoader;
using ssm;

namespace ssm.Reworks
{
	public class StyxArmor : GlobalItem
	{
		public override string IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == ModContent.ItemType<StyxCrown>() && body.type == ModContent.ItemType<StyxChestplate>() && legs.type == ModContent.ItemType<StyxLeggings>() ? "NewStyx" : "";
		}

        public override void SetDefaults(Item item)
        {
            if (item.type == ModContent.ItemType<StyxCrown>())
            {
                item.defense = 35;
            }
            if (item.type == ModContent.ItemType<StyxChestplate>())
            {
                item.defense = 45;
            }
            if (item.type == ModContent.ItemType<StyxLeggings>())
            {
                item.defense = 40;
            }
        }

        public override void UpdateEquip(Item Item, Player player)
		{
			if (Item.type == ModContent.ItemType<StyxCrown>())
			{
				Item.defense = 35;
				player.GetDamage(DamageClass.Generic) += 10 / 100f;
				player.maxMinions += 5;
            }
			if (Item.type == ModContent.ItemType<StyxChestplate>())
			{
                Item.defense = 45;
                player.GetDamage(DamageClass.Generic) += 10 / 100f;
			}
			if (Item.type == ModContent.ItemType<StyxLeggings>())
			{
                Item.defense = 40;
                player.GetDamage(DamageClass.Generic) += 10 / 100f;
			}
		}
	}	
}