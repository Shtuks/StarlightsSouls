using CalamityMod.Items.Armor.Vanity;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ssm;
using ssm.Core;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class SCalSet : GlobalItem
    {
        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<SCalMask>() && body.type == ModContent.ItemType<SCalRobes>() && legs.type == ModContent.ItemType<SCalBoots>() ? "NewSCalSet" : "";
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            player.setBonus = "All vanilla prefixes on accessories are twice as effective\nInflicts Vulnerability Hex on all hits";
            //ShtunPlayer modPlayer = player.GetModPlayer<ShtunPlayer>();
            //modPlayer.InflictHex = true;
            for (int index = 3; index < 8 + player.extraAccessorySlots; ++index)
            {
                Item obj = player.armor[index];
                if (obj.prefix > 0)
                {
                    int type = obj.type;
                    player.GrantPrefixBenefits(obj);
                    obj.type = type;
                }
            }
        }

        public override void UpdateEquip(Item Item, Player player)
        {
            if (Item.type == ModContent.ItemType<SCalMask>())
            {
                player.statDefense = player.statDefense += 30;
                player.GetDamage(DamageClass.Magic) += 10 / 100f;
                player.GetDamage(DamageClass.Summon) += 10 / 100f;
                player.GetCritChance<MagicDamageClass>() += 7f;
                ++player.maxMinions;
                Item.vanity = false;
            }
            if (Item.type == ModContent.ItemType<SCalRobes>())
            {
                player.statDefense = player.statDefense += 40;
                player.GetDamage(DamageClass.Magic) += 15 / 100f;
                player.GetDamage(DamageClass.Summon) += 15 / 100f;
                player.GetCritChance<MagicDamageClass>() += 10f;
                player.maxMinions += 2;
                Item.vanity = false;
            }
            if (Item.type != ModContent.ItemType<SCalBoots>())
                return;
            player.statDefense = player.statDefense += 35;
            player.GetDamage(DamageClass.Magic) += 10 / 100f;
            player.GetDamage(DamageClass.Summon) += 10 / 100f;
            player.GetCritChance<MagicDamageClass>() += 8f;
            ++player.maxMinions;
            Item.vanity = false;
        }

        public override void ModifyTooltips(Item Item, List<TooltipLine> tooltips)
        {
            if (Item.type == ModContent.ItemType<SCalMask>())
            {
                TooltipLine tooltipLine = new TooltipLine(((ModType)this).Mod, "ScalMaskThing", "30 Defense\nIncreases minion and magic damage by 10%\nIncreases magic crit chance by 7%\nIncreases max minions by 1");
                tooltips.Add(tooltipLine);
            }
            if (Item.type == ModContent.ItemType<SCalRobes>())
            {
                TooltipLine tooltipLine = new TooltipLine(((ModType)this).Mod, "ScalRobesThing", "40 Defense\nIncreases minion and magic damage by 15%\nIncreases magic crit chance by 10%\nIncreases max minions by 2");
                tooltips.Add(tooltipLine);
            }
            if (Item.type != ModContent.ItemType<SCalBoots>())
                return;
            TooltipLine tooltipLine1 = new TooltipLine(((ModType)this).Mod, "ScalBootsThing", "20 Defense\nIncreases minion and magic damage by 10%\nIncreases magic crit chance by 8%\nIncreases max minions by 1");
            tooltips.Add(tooltipLine1);
        }
    }
}