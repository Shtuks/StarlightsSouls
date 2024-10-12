using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    public class SadismEX : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[this.Type] = true;
            Main.buffNoSave[this.Type] = false;
            Main.persistentBuff[this.Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                {
                    player.buffImmune[index] = true;
                    if (ModLoader.TryGetMod("CalamityMod", out Mod kal))
                    {
                        player.buffImmune[ModContent.Find<ModBuff>("CalamityMod", "RageMode").Type] = false;
                        player.buffImmune[ModContent.Find<ModBuff>("CalamityMod", "AdrenalineMode").Type] = false;
                    }
                    //player.buffImmune[this.mod.BuffType("ChtuxlagorInferno")] = false;
                }
            }
        }
    }
}
