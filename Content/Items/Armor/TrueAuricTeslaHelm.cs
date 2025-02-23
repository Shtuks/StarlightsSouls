using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using ssm.Content.Buffs;
using Terraria.Localization;
using CalamityMod;

namespace ssm.Content.Items.Armor
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [AutoloadEquip(EquipType.Head)]
    public class TrueAuricTeslaHelm : ModItem
    {
        public override void SetDefaults()
        {
            ((Entity)this.Item).width = 18;
            ((Entity)this.Item).height = 18;
            this.Item.rare = 11;
            this.Item.expert = true;
            this.Item.value = Item.sellPrice(10, 0, 0, 0);
            this.Item.defense = 45;
        }

        public static string GetSetBonusString()
        {
            return Language.GetTextValue($"Mods.ssm.SetBonus.TrueAuric");
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.3f;
            player.GetAttackSpeed(DamageClass.Generic) += 3f;
            player.GetArmorPenetration(DamageClass.Generic) += 50f;
            player.GetCritChance(DamageClass.Generic) += 2f;
            player.maxMinions += 10;
            player.maxTurrets += 10;
            player.manaCost -= 0.4f;
            player.ammoCost75 = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TrueAuricTeslaBody>() && legs.type == ModContent.ItemType<TrueAuricTeslaLegs>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 5f;
            player.GetDamage(DamageClass.Generic) += 0.10f;
            player.GetArmorPenetration(DamageClass.Generic) += 700f;
            player.GetAttackSpeed(DamageClass.Generic) += 5f;
            player.statDefense = player.statDefense += 50;
            player.AddBuff(ModContent.BuffType<YharimBuff>(), 2, true);
            player.Calamity().auricSet = true;
            player.Calamity().bloodflareSet = true;
            player.Calamity().silvaSet = true;
            player.Calamity().godSlayer = true;
            player.Calamity().tarraSet = true;

            player.Calamity().bloodflareMelee = true;
            player.Calamity().bloodflareMage = true;
            player.Calamity().bloodflareRanged = true;
            player.Calamity().bloodflareThrowing = true;
            player.Calamity().tarraMage = true;
            player.Calamity().tarraMelee = true;
            player.Calamity().tarraRanged = true;
            player.Calamity().tarraThrowing = true;
            player.Calamity().godSlayerRanged = true;
            player.Calamity().godSlayerThrowing = true;
            player.Calamity().silvaMage = true;
        }

        public override void AddRecipes()
        {
        }
    }
}
