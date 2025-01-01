using System;
using Terraria.ModLoader;
using Terraria;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.HealerItems;
using ssm.Core;
using ThoriumMod;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class PostSetupContentThorium
    {
        public static void PostSetupContent_Thorium()
        {
            double Damage(DamageClass damageClass) => Math.Round(Main.LocalPlayer.GetTotalDamage(damageClass).Additive * Main.LocalPlayer.GetTotalDamage(damageClass).Multiplicative * 100 - 100);
            int Crit(DamageClass damageClass) => (int)Main.LocalPlayer.GetTotalCritChance(damageClass);

            int bardItem = ModContent.ItemType<GoldBugleHorn>();
            Func<string> bardDamage = () => $"Bard Damage: {Damage(ModContent.GetInstance<BardDamage>())}%";
            Func<string> bardCrit = () => $"Bard Critical: {Crit(ModContent.GetInstance<BardDamage>())}%";
            ModCompatibility.MutantMod.Mod.Call("AddStat", bardItem, bardDamage);
            ModCompatibility.MutantMod.Mod.Call("AddStat", bardItem, bardCrit);

            int healerItem = ModContent.ItemType<PalmCross>();
            Func<string> healerDamage = () => $"Healer Damage: {Damage(ModContent.GetInstance<HealerDamage>())}%";
            Func<string> healerCrit = () => $"Healer Critical: {Crit(ModContent.GetInstance<HealerDamage>())}%";
            ModCompatibility.MutantMod.Mod.Call("AddStat", healerItem, healerDamage);
            ModCompatibility.MutantMod.Mod.Call("AddStat", healerItem, healerCrit);
        }
    }
}
