using ssm.Content.Buffs;
using Terraria.GameInput;
using Terraria.ModLoader;
using ssm.Content.Items.Accessories;
using Terraria;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Buffs.Masomode;
using ssm.Content.NPCs.StarlightCat;

namespace ssm
{
    public partial class ShtunHit : ModPlayer
    {
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            double damageMult = 1D;
            modifiers.SourceDamage *= (float)damageMult;

            if (Player.Shtun().ChtuxlagorHeart)
            {
                modifiers.SetMaxDamage(1000);
            }
            if (Player.HasBuff<MutantDesperationBuff>())
            {
                Player.AddBuff(ModContent.BuffType<TimeFrozenBuff>(),9999);
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (Main.expertMode && NPC.AnyNPCs(ModContent.NPCType<StarlightCatBoss>()))
            {
                Player.Shtun().ChtuxlagorHits++;
            }
            if (Player.Shtun().ChtuxlagorHits > 10)
            {
                Player.Shtun().ERASE(Player);
                Player.Shtun().ChtuxlagorHits = 0;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<StarlightCatBoss>()))
            {
                if (Main.zenithWorld)
                {
                    Player.statLife = 0;
                    Player.AddBuff(ModContent.BuffType<ChtuxlagorInfernoEX>(), 5400);
                }
                else
                {
                    Player.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
                    Player.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
                    Player.AddBuff(ModContent.BuffType<MutantFangBuff>(), 5400);
                    Player.AddBuff(ModContent.BuffType<GodEaterBuff>(), 5400);
                    Player.AddBuff(ModContent.BuffType<AbomFangBuff>(), 5400);
                    Player.AddBuff(ModContent.BuffType<PurifiedBuff>(), 5400);
                    Player.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 5400);
                    Player.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 5400);
                    Player.AddBuff(ModContent.BuffType<ChtuxlagorInferno>(), 5400);
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.Shtun().equippedPhantasmalEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "MutantFangBuff").Type, 1000, false);
            if (Player.Shtun().equippedNekomiEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "DeviPresenceBuff").Type, 1000, false);
            if (Player.Shtun().equippedAbominableEnchantment)
                target.AddBuff(ModContent.Find<ModBuff>(this.FargoSoul.Name, "AbomFangBuff").Type, 1000, false);
            if (Player.Shtun().ChtuxlagorHeart)
                target.AddBuff(ModContent.Find<ModBuff>("ssm", "ChtuxlagorInferno").Type, 1000, false);
        }
    }

    public partial class ShtunNPCHit : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            
        }
    }
}
