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

		public override void UpdateEquip(Item Item, Player player)
		{
			if (Item.type == ModContent.ItemType<StyxChestplate>())
			{
				player.statDefense = player.statDefense += 20;
				player.GetDamage(DamageClass.Generic) += 10 / 100f;
				player.maxMinions += 10;
			}
			if (Item.type == ModContent.ItemType<StyxChestplate>())
			{
				player.statDefense = player.statDefense += 20;
				player.GetDamage(DamageClass.Generic) += 10 / 100f;
			}
			if (Item.type == ModContent.ItemType<StyxLeggings>())
			{
				player.statDefense = player.statDefense += 20;
				player.GetDamage(DamageClass.Generic) += 10 / 100f;
			}
		}
	}	
}