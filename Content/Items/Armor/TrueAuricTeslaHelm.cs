using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using ssm.Content.Buffs;
using Terraria.Localization;

namespace ssm.Content.Items.Armor
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [AutoloadEquip(EquipType.Head)]
    public class TrueAuricTeslaHelm : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExtraContent;
        }
        private readonly Mod FargoCross = Terraria.ModLoader.ModLoader.GetMod("FargowiltasCrossmod");
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        private readonly Mod Calamity = Terraria.ModLoader.ModLoader.GetMod("CalamityMod");

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
            if (Terraria.ModLoader.ModLoader.GetMod("CalamityMod") != null)
            {
                /*ModContent.Find<ModItem>(this.Calamity.Name, "AuricTeslaBodyArmor").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Calamity.Name, "AuricTeslaCuisses").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Calamity.Name, "GodSlayerChestplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Calamity.Name, "GodSlayerLeggins").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Calamity.Name, "SilvaArmor").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Calamity.Name, "SilvaLeggins").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Calamity.Name, "BloodflareBodyArmor").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Calamity.Name, "BloodflareCuisses").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Calamity.Name, "TarragonBreastplace").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Calamity.Name, "TarragonLeggins").UpdateArmorSet(player);*/

                //ModContent.Find<ModItem>(this.Calamity.Name, "AuricTeslaPlumedHelm").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.Calamity.Name, "AuricTeslaRoyalHelm").UpdateArmorSet(player);
                //ModContent.Find<ModItem>(this.Calamity.Name, "AuricTeslaWiredHemmedVisage").UpdateArmorSet(player);
                //ModContent.Find<ModItem>(this.Calamity.Name, "AuricTeslaHoodedFacemask").UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
        }
    }
}
