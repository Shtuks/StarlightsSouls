using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using Terraria.Localization;

namespace ssm.Content.Items.Armor
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [AutoloadEquip(EquipType.Head)]
    public class TrueMonstrosityMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = 11;
            Item.expert = true;
            Item.value = Item.sellPrice(10, 0, 0, 0);
            Item.defense = 60;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.5f;
            player.GetArmorPenetration(DamageClass.Generic) += 70f;
            player.GetCritChance(DamageClass.Generic) += 2f;
            player.maxMinions += 20;
            player.maxTurrets += 20;
            player.manaCost -= 0.4f;
            player.ammoCost75 = true;
        }

        public static string GetSetBonusString()
        {
            return Language.GetTextValue($"Mods.ssm.SetBonus.Monstrosity");
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TrueMonstrositySuit>() && legs.type == ModContent.ItemType<TrueMonstrosityPants>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 5f;
            player.GetDamage(DamageClass.Generic) += 2f;
            player.thorns = 1f;
            player.GetArmorPenetration(DamageClass.Generic) += 700f;
            player.GetAttackSpeed(DamageClass.Generic) += 5f;
            player.longInvince = true;
            player.endurance += 50f;
            player.lavaImmune = true;
            player.manaFlower = true;
            player.manaMagnet = true;
            player.magicCuffs = true;
            player.ignoreWater = true;
            player.pStone = true;
            player.findTreasure = true;
            player.noKnockback = true;
            player.lavaImmune = true;
            player.noFallDmg = true;
            /*player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("YharimPower"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("GraxDefense"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("AbyssalDivingSuitPlates"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("DraconicSurgeBuff"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("TyrantsFury"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("TriumphBuff"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("ArmorShattering"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("TitanScale"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("KamiBuff"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("AbyssalWeapon"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("HolyWrathBuff"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("ProfanedRageBuff"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("XerocRage"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("AstrageldonBuff"), 2, true);
            player.AddBuff(Terraria.ModLoader.ModLoader.GetMod("CalamityMod").BuffType("Revivify"), 2, true);*/
            if (Terraria.ModLoader.ModLoader.GetMod("CalamityMod") != null)
            {
                ModContent.Find<ModItem>("CalamityMod", "DemonshadeHelm").UpdateArmorSet(player);
                ModContent.Find<ModItem>("CalamityMod", "DemonshadeBreastplate").UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
        }
    }
}
