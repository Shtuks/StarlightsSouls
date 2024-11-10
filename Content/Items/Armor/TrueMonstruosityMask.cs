using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using Terraria.Localization;
using CalamityMod.CalPlayer;
using CalamityMod;
using FargowiltasSouls.Content.Items.Armor;
using ssm.Content.Items.Materials;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Armor.GemTech;
using ssm.Content.Tiles;

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
            Item.defense = 100;
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
            player.endurance += 5f;
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
            if (Terraria.ModLoader.ModLoader.GetMod("CalamityMod") != null)
            {
                ModContent.Find<ModItem>("CalamityMod", "DemonshadeHelm").UpdateArmorSet(player);
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.tarraSet = true;
                calamityPlayer.GemTechSet = true;
                calamityPlayer.bloodflareSet = true;
                calamityPlayer.godSlayer = true;
                calamityPlayer.auricSet = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<MonstrosityMask>();

            recipe.AddIngredient<DemonshadeHelm>();
            recipe.AddIngredient<GemTechHeadgear>();
            recipe.AddRecipeGroup("ssm:Auric");

            recipe.AddTile<DemonshadeWorkbenchTile>();
        }
    }
}
