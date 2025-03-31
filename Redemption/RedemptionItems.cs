using FargowiltasSouls;
using Redemption.Items.Accessories.PostML;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionItems : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if(item.type == ModContent.ItemType<MutagenMelee>())
            {
                player.GetDamage(DamageClass.Melee) *= 0.20f;
                player.GetCritChance(DamageClass.Melee) += 10f;
                player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
                player.kbGlove = true;
                player.autoReuseGlove = true;
            }
            if (item.type == ModContent.ItemType<MutagenMagic>())
            {
                player.GetDamage(DamageClass.Magic) *= 0.20f;
                player.GetCritChance(DamageClass.Magic) += 10f;
                player.statManaMax2 += 100;
            }
            if (item.type == ModContent.ItemType<MutagenSummon>())
            {
                player.GetDamage(DamageClass.Summon) *= 0.20f;
                player.GetAttackSpeed(DamageClass.Summon) += 10f;
                player.maxMinions += 2;
            }
            if (item.type == ModContent.ItemType<MutagenRanged>())
            {
                player.GetDamage(DamageClass.Ranged) *= 0.20f;
                player.GetCritChance(DamageClass.Ranged) += 10f;
                player.FargoSouls().RangedEssence = true;
            }
        }
    }
}
