using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Content.Buffs.Masomode;
using ssm.Content.Projectiles;
using ssm.Content.NPCs.MutantEX;
using ssm.Systems;

namespace ssm
{
    public partial class ShtunPlayer : ModPlayer
    {
        public bool MutantSoul;
        public bool DevianttSoul;
        public int Screenshake;
        public float throwerVelocity = 1f;
        public bool CyclonicFin;
        public int CyclonicFinCD;
        public bool MonstrocityPresence;
        public bool lumberjackSet;

        //Enchants
        public bool equippedPhantasmalEnchantment;
        public bool equippedAbominableEnchantment;
        public bool equippedNekomiEnchantment;
        public bool equippedMonstrosityEnchantment;

        public override void PreUpdate()
        {
            if(ShtunUtils.BossIsAlive(ref ShtunNpcs.mutantEX, ModContent.NPCType<MutantEX>()) && Main.player[Main.myPlayer].Shtun().lumberjackSet && WorldSaveSystem.enragedMutantEX)
            {
                Player.statDefense*=0;
                Player.endurance*=0;
            }
        }
        public override void OnEnterWorld()
        {
            if (!ModLoader.TryGetMod("ThoriumRework", out Mod _) && ModLoader.TryGetMod("ThoriumMod", out Mod _))
            {
                Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoRework"), Color.LimeGreen);
            }
            //if (!ModLoader.TryGetMod("SoABardHealer", out Mod _) && ModLoader.TryGetMod("SoA", out Mod _) && ModLoader.TryGetMod("ThoriumMod", out Mod _))
            //{
            //    Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoSoABardHealer1"), Color.Purple);
            //    Main.NewText(Language.GetTextValue($"Mods.ssm.Message.NoSoABardHealer2"), Color.Purple);
            //}
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (CyclonicFin)
            {
                target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 900);
                target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 900);

                if (hit.Crit && CyclonicFinCD <= 0 && proj.type != ModContent.ProjectileType<RazorbladeTyphoonFriendly>())
                {
                    CyclonicFinCD = 360;

                    float screenX = Main.screenPosition.X;
                    if (Player.direction < 0)
                        screenX += Main.screenWidth;
                    float screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                    Vector2 spawn = new Vector2(screenX, screenY);
                    Vector2 vel = target.Center - spawn;
                    vel.Normalize();
                    vel *= 27f;
                    Projectile.NewProjectile(proj.GetSource_FromThis(), spawn, vel, ModContent.ProjectileType<SpectralFishron>(), 300, 10f, proj.owner, target.whoAmI, 300);
                }
            }
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            return !lumberjackSet && !ShtunUtils.BossIsAlive(ref ShtunNpcs.mutantEX, ModContent.NPCType<MutantEX>()) && !WorldSaveSystem.enragedMutantEX;
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            return !lumberjackSet && !ShtunUtils.BossIsAlive(ref ShtunNpcs.mutantEX, ModContent.NPCType<MutantEX>()) && !WorldSaveSystem.enragedMutantEX;
        }
        public override void ModifyScreenPosition()
        {
            if (Screenshake > 0)
                Main.screenPosition += Main.rand.NextVector2Circular(7, 7);
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            double damageMult = 1D;
            modifiers.SourceDamage *= (float)damageMult;

            if (equippedMonstrosityEnchantment)
            {
                modifiers.SetMaxDamage(1000);
            }
        }
        public override void ResetEffects()
        {
            if (Screenshake > 0)
                Screenshake--;

            equippedPhantasmalEnchantment = false;
            equippedAbominableEnchantment = false;
            equippedNekomiEnchantment = false;
            DevianttSoul = false;
            MutantSoul = false;
            throwerVelocity = 1f;
            CyclonicFin = false;
            lumberjackSet = false;
        }
    }
}
