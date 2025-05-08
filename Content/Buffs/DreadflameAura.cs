using FargowiltasSouls;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;
using static ssm.SoA.Enchantments.DreadfireEnchant;

namespace ssm.Content.Buffs
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class DreadflameAura : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Terraria.ID.BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage<SummonDamageClass>() += player.ForceEffect<DreadfireEffect>() ? 0.3f : 0.2f;
            player.GetDamage<MagicDamageClass>() += player.ForceEffect<DreadfireEffect>() ? 0.3f : 0.2f;
        }
    }
}
