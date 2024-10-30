using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using ThoriumMod;
using ThoriumMod.Projectiles;
using ThoriumMod.Buffs;
using ThoriumMod.Projectiles.Thrower;
using ThoriumMod.Projectiles.Bard;
using ThoriumMod.Projectiles.Healer;
using ThoriumMod.Dusts;
using ThoriumMod.NPCs;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]

    public partial class ShtunThoriumPlayer : ModPlayer
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public bool FungusEnchant;
        public bool WarlockEnchant;
        public bool SacredEnchant;
        public bool LivingWoodEnchant;
        public bool DepthEnchant;
        public bool KnightEnchant;
        public bool IllumiteEnchant;
        public bool JesterEnchant;
        public bool WhiteDwarfEnchant;
        public bool YewEnchant;
        public bool CryoEnchant;
        public bool TideHunterEnchant;
        public bool BronzeEnchant;
        public bool TideTurnerEnchant;
        public bool AssassinEnchant;
        public bool PyroEnchant;
        public bool ThoriumEnchant;
        public bool SpiritTrapperEnchant;
        public bool LifeBloomEnchant;
        public bool LichEnchant;
        public bool DemonBloodEnchant;
        public bool MixTape;
        public bool ConduitEnchant;
        public bool ThoriumSoul;

        public void ThoriumResetEffects()
        {
            FungusEnchant = false;
            WarlockEnchant = false;
            SacredEnchant = false;
            LivingWoodEnchant = false;
            DepthEnchant = false;
            KnightEnchant = false;
            IllumiteEnchant = false;
            JesterEnchant = false;
            WhiteDwarfEnchant = false;
            YewEnchant = false;
            CryoEnchant = false;
            TideHunterEnchant = false;
            BronzeEnchant = false;
            TideTurnerEnchant = false;
            AssassinEnchant = false;
            PyroEnchant = false;
            ThoriumEnchant = false;
            SpiritTrapperEnchant = false;
            LifeBloomEnchant = false;
            LichEnchant = false;
            DemonBloodEnchant = false;
            MixTape = false;
            ConduitEnchant = false;
            ThoriumSoul = false;
        }

        private void ThoriumPostUpdate()
        {
            if (SpiritTrapperEnchant && Player.ownedProjectileCounts[ModContent.ProjectileType<SpiritTrapperVisual>()] >= 5)
            {
                Player.statLifeMax2 += 10;
                Player.HealEffect(10, true);
                for (int num23 = 0; num23 < 5; num23++)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<SpiritTrapperVisual>(), 0, 0, Player.whoAmI, (float)num23, 0f);
                }
                for (int num24 = 0; num24 < 1000; num24++)
                {
                    Projectile projectile3 = Main.projectile[num24];
                    if (projectile3.active && projectile3.type == ModContent.ProjectileType<SpiritTrapperVisual>())
                    {
                        projectile3.Kill();
                    }
                }
            }
        }

        private void ThoriumModifyProj(Projectile proj, NPC target, int damage, bool crit)
        {
            ThoriumPlayer thoriumPlayer = Player.GetModPlayer<ThoriumPlayer>();

            //if (FungusEnchant && !ThoriumSoul && Main.rand.Next(5) == 0)
                //target.AddBuff(ModContent.BuffType<Mycelium>(120);

            if (proj.type == ModContent.ProjectileType<PyroBurst>() || proj.type == ModContent.ProjectileType<LightStrike>() || proj.type == ModContent.ProjectileType<WhiteFlare>() || proj.type == ModContent.ProjectileType<MixtapeNote>())
            {
                return;
            }

            if (TideTurnerEnchant)
            {
                //tide turner daggers
                if (/*SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.TideDaggers)*/ Player.ownedProjectileCounts[ModContent.ProjectileType<TideDagger>()] < 24 && proj.type != ModContent.ProjectileType<ThrowingGuideFollowup>() && proj.type != ModContent.ProjectileType<TideDagger>() && target.type != NPCID.TargetDummy && Main.rand.Next(5) == 0)
                {
                    //ShtunThoriumProjectile.XWay(4, Player.position, ModContent.ProjectileType<TideDagger>(), 3, (int)(proj.damage * 0.75), 3);
                    //Main.PlaySound(SoundID.Item, (int)Player.position.X, (int)Player.position.Y, 43, 1f, 0f);
                }
                //mini crits
                if (thoriumPlayer.tideOrb > 0 && !crit)
                {
                    float num = 30f;
                    int num2 = 0;
                    while ((float)num2 < num)
                    {
                        Vector2 vector = Vector2.UnitX * 0f;
                        vector += -Utils.RotatedBy(Vector2.UnitY, (double)((float)num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(12f, 12f);
                        vector = Utils.RotatedBy(vector, (double)Utils.ToRotation(target.velocity), default(Vector2));
                        int num3 = Dust.NewDust(target.Center, 0, 0, 176, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num3].scale = 1.5f;
                        Main.dust[num3].noGravity = true;
                        Main.dust[num3].position = target.Center + vector;
                        Main.dust[num3].velocity = target.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                        int num4 = num2;
                        num2 = num4 + 1;
                    }
                    crit = true;
                    damage = (int)((double)damage * 0.75);
                    thoriumPlayer.tideOrb--;
                }
            }

            if (AssassinEnchant)
            {
                //assassin duplicate damage
                //if (SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.AssassinDamage) && Utils.NextFloat(Main.rand) < 0.1f)
                //{
                    //Main.PlaySound(SoundID.Item, (int)target.position.X, (int)target.position.Y, 92, 1f, 0f);
                    //Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasmaDamage"), (int)((float)proj.damage * 1.15f), 0f, Main.myPlayer, 0f, 0f);
                    //Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasma"), 0, 0f, Main.myPlayer, 0f, 0f);
                //}

                //insta kill
                if (target.type != NPCID.TargetDummy && target.lifeMax < 1000000 && Utils.NextFloat(Main.rand) < 0.05f)
                {
                    if ((target.boss || NPCID.Sets.BossHeadTextures[target.type] > -1) && target.life < target.lifeMax * 0.05)
                    {
                        CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), new Color(135, 255, 45), "ERADICATED", false, false);
                        target.life = 0;
                        //Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasmaDamage"), (int)(target.lifeMax * 1.25f), 0f, Main.myPlayer, 0f, 0f);
                        //Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasma"), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                    else if (NPCID.Sets.BossHeadTextures[target.type] < 0)
                    {
                        CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), new Color(135, 255, 45), "ERADICATED", false, false);
                        target.life = 0;
                        //Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasmaDamage"), (int)(target.lifeMax * 1.25f), 0f, Main.myPlayer, 0f, 0f);
                        //Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasma"), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            if (PyroEnchant)
            {
                //pyro
                target.AddBuff(24, 300, true);
                target.AddBuff(ModContent.BuffType<Singed>(), 300, true);

                if (/*SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.PyromancerBursts &&*/ proj.type != ModContent.ProjectileType<PyroBurst>())
                {
                    Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<PyroBurst>(), 100, 1f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<PyroExplosion2>(), 100, 1f, Main.myPlayer, 0f, 0f);
                }
            }

            if (BronzeEnchant /*&& SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.BronzeLightning*/ && Main.rand.Next(5) == 0 && proj.type != ModContent.ProjectileType<LightStrike>() && proj.type != ModContent.ProjectileType<ThrowingGuideFollowup>())
            {
                target.immune[proj.owner] = 5;
                Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<LightStrike>(), proj.damage / 4, 1f, Main.myPlayer, 0f, 0f);
            }

            //white dwarf
            if (WhiteDwarfEnchant /*&& SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.WhiteDwarf)*/ && crit)
            {
                //Main.PlaySound(SoundID.Item, (int)target.position.X, (int)target.position.Y, 92, 1f, 0f);
                Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<WhiteFlare>(), (int)(target.lifeMax * 0.001), 0f, Main.myPlayer, 0f, 0f);
            }

            //jew wood
            if (YewEnchant /*&& SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.YewCrits)*/ && !crit)
            {
                thoriumPlayer.yewChargeTimer = 120;
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<YewVisual>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<YewVisual>(), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                if (thoriumPlayer.yewCharge < 4)
                {
                    thoriumPlayer.yewCharge++;
                }
                else
                {
                    crit = true;
                    damage = (int)((double)damage * 0.75);
                    thoriumPlayer.yewCharge = 0;
                }
            }

            //cryo
            if (CryoEnchant /*&& SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.CryoDamage)*/)
            {
                //target.AddBuff(ModContent.BuffType<EnemyFrozen>(), 120, false);
                Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<ReactionNitrogen>(), 0, 0f, Main.myPlayer, 0f, 0f);
            }

            if (WarlockEnchant/* && SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.WarlockWisps) */ && !(proj.ModProjectile is ThoriumProjectile /*&& ((ThoriumProjectile)proj.ModProjectile).radiant)*/))
            {
                //warlock
                if (crit && Player.ownedProjectileCounts[ModContent.ProjectileType<ShadowWisp>()] < 15)
                {
                    Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<ShadowWisp>(), (int)(proj.damage * 0.75), 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        private void ThoriumModifyNPC(NPC target, Item Item, int damage, bool crit)
        {
            ThoriumPlayer thoriumPlayer = Player.GetModPlayer<ThoriumPlayer>();

            //if (FungusEnchant && !ThoriumSoul && Main.rand.Next(5) == 0)
            //    target.AddBuff(thorium.BuffType("Mycelium"), 120);

            if (TideTurnerEnchant)
            {
                //tide turner daggers
                //if (SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.TideDaggers) && Player.ownedProjectileCounts[thorium.ProjectileType("TideDagger")] < 24 && target.type != NPCID.TargetDummy && Main.rand.Next(5) == 0)
                //{
                //    FargoDLCGlobalProjectile.XWay(4, Player.position, thorium.ProjectileType("TideDagger"), 3, (int)(Item.damage * 0.75), 3);
                //    Main.PlaySound(SoundID.Item, (int)Player.position.X, (int)Player.position.Y, 43, 1f, 0f);
                //}
                //mini crits
                if (thoriumPlayer.tideOrb > 0 && !crit)
                {
                    float num = 30f;
                    int num2 = 0;
                    while ((float)num2 < num)
                    {
                        Vector2 vector = Vector2.UnitX * 0f;
                        vector += -Utils.RotatedBy(Vector2.UnitY, (double)((float)num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(12f, 12f);
                        vector = Utils.RotatedBy(vector, (double)Utils.ToRotation(target.velocity), default(Vector2));
                        int num3 = Dust.NewDust(target.Center, 0, 0, 176, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num3].scale = 1.5f;
                        Main.dust[num3].noGravity = true;
                        Main.dust[num3].position = target.Center + vector;
                        Main.dust[num3].velocity = target.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                        int num4 = num2;
                        num2 = num4 + 1;
                    }
                    crit = true;
                    damage = (int)((double)damage * 0.75);
                    thoriumPlayer.tideOrb--;
                }
            }

            if (AssassinEnchant)
            {
                //assassin duplicate damage
                //if (SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.AssassinDamage) && Utils.NextFloat(Main.rand) < 0.1f)
                //{
                //    Main.PlaySound(SoundID.Item, (int)target.position.X, (int)target.position.Y, 92, 1f, 0f);
                    //Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasmaDamage"), (int)((float)Item.damage * 1.15f), 0f, Main.myPlayer, 0f, 0f);
                    //Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasma"), 0, 0f, Main.myPlayer, 0f, 0f);
                //}
                //insta kill
                if (target.type != NPCID.TargetDummy && target.lifeMax < 1000000 && Utils.NextFloat(Main.rand) < 0.05f)
                {
                    if ((target.boss || NPCID.Sets.BossHeadTextures[target.type] > -1) && target.life < target.lifeMax * 0.05)
                    {
                        CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), new Color(135, 255, 45), "ERADICATED", false, false);
                        target.life = 0;
                        //Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasmaDamage"), (int)(target.lifeMax * 1.25f), 0f, Main.myPlayer, 0f, 0f);
                        //Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasma"), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                    else if (NPCID.Sets.BossHeadTextures[target.type] < 0)
                    {
                        CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), new Color(135, 255, 45), "ERADICATED", false, false);
                        target.life = 0;
                        //Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasmaDamage"), (int)(target.lifeMax * 1.25f), 0f, Main.myPlayer, 0f, 0f);
                        //Projectile.NewProjectile(((int)target.Center.X), ((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("MeteorPlasma"), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            if (PyroEnchant)
            {
                //pyro
                target.AddBuff(24, 300, true);
                target.AddBuff(ModContent.BuffType<Singed>(), 300, true);
                Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<PyroBurst>(), 100, 1f, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<PyroExplosion2>(), 100, 1f, Main.myPlayer, 0f, 0f);
            }

             if (BronzeEnchant /*&& SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.BronzeLightning*/ && Main.rand.Next(5) == 0)
            {
                target.immune[Player.whoAmI] = 5;
                Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<LightStrike>(), (int)(Player.GetWeaponDamage(Player.HeldItem) / 4), 1f, Main.myPlayer, 0f, 0f);
            }

            //white dwarf
            if (WhiteDwarfEnchant /*&& SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.WhiteDwarf)*/ && crit)
            {
                //Main.PlaySound(SoundID.Item, (int)target.position.X, (int)target.position.Y, 92, 1f, 0f);
                Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<WhiteFlare>(), (int)(target.lifeMax * 0.001), 0f, Main.myPlayer, 0f, 0f);
            }

            //jew wood
            if (YewEnchant /*&& SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.YewCrits)*/ && !crit)
            {
                thoriumPlayer.yewChargeTimer = 120;
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<YewVisual>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<YewVisual>(), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                if (thoriumPlayer.yewCharge < 4)
                {
                    thoriumPlayer.yewCharge++;
                }
                else
                {
                    crit = true;
                    damage = (int)((double)damage * 0.75);
                    thoriumPlayer.yewCharge = 0;
                }
            }

            //cryo
            if (CryoEnchant /*&& SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.CryoDamage)*/)
            {
                //target.AddBuff(ModContent.BuffType<EnemyFrozen>(), 120, false);
                Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<ReactionNitrogen>(), 0, 0f, Main.myPlayer, 0f, 0f);
            }

            if (WarlockEnchant/* && SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.WarlockWisps)*/)
            {
                //warlock
                if (crit && Player.ownedProjectileCounts[ModContent.ProjectileType<ShadowWisp>()] < 15)
                {
                    Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<ShadowWisp>(), (int)(Player.GetWeaponDamage(Player.HeldItem) * 0.75), 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        private void ThoriumHitProj(Projectile proj, NPC target, int damage, bool crit)
        {
            ThoriumPlayer thoriumPlayer = Player.GetModPlayer<ThoriumPlayer>();

            if (SpiritTrapperEnchant && /*SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.SpiritTrapperWisps) &&*/ !proj.minion)
            {
                if (target.life < 0 && target.value > 0f)
                {
                    Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<SpiritTrapperVisual>(), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                if (target.boss || NPCID.Sets.BossHeadTextures[target.type] > -1)
                {
                    thoriumPlayer.setSpiritTrapperHit++;
                }
            }

            //tide hunter
            if (TideHunterEnchant && /*SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.TideFoam) &&*/ crit)
            {
                for (int n = 0; n < 10; n++)
                {
                    int num10 = Dust.NewDust(target.position, target.width, target.height, 217, (float)Main.rand.Next(-4, 4), (float)Main.rand.Next(-4, 4), 100, default(Color), 1f);
                    Main.dust[num10].noGravity = true;
                    Main.dust[num10].noLight = true;
                }
                for (int num11 = 0; num11 < 200; num11++)
                {
                    NPC npc = Main.npc[num11];
                    if (npc.active && npc.FindBuffIndex(ModContent.BuffType<Oozed>()) < 0 && !npc.friendly && Vector2.Distance(npc.Center, target.Center) < 80f)
                    {
                        npc.AddBuff(ModContent.BuffType<Oozed>(), 90, false);
                    }
                }
            }

            if (LichEnchant && target.life <= 0)
            {
                //Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, thorium.ProjectileType("SoulFragment"), 0, 0f, proj.owner, 0f, 0f);
                for (int num26 = 0; num26 < 5; num26++)
                {
                    int num27 = Dust.NewDust(proj.position, proj.width, proj.height, 55, (float)Main.rand.Next(-4, 4), (float)Main.rand.Next(-4, 4), 150, default(Color), 0.75f);
                    Main.dust[num27].noGravity = true;
                }
                for (int num28 = 0; num28 < 5; num28++)
                {
                    int num29 = Dust.NewDust(proj.position, proj.width, proj.height, ModContent.DustType<HarbingerDust>(), (float)Main.rand.Next(-3, 3), (float)Main.rand.Next(-3, 3), 100, default(Color), 1f);
                    Main.dust[num29].noGravity = true;
                }
            }

            //life bloom
            if (LifeBloomEnchant && target.type != NPCID.TargetDummy && Main.rand.Next(4) == 0 && thoriumPlayer.setLifeBloomMax < 50)
            {
                for (int l = 0; l < 10; l++)
                {
                    int num7 = Dust.NewDust(target.position, target.width, target.height, 44, (float)Main.rand.Next(-5, 5), (float)Main.rand.Next(-5, 5), 0, default(Color), 1f);
                    Main.dust[num7].noGravity = true;
                }
                int num8 = Main.rand.Next(1, 4);
                Player.statLife += num8;
                Player.HealEffect(num8, true);
                thoriumPlayer.setLifeBloomMax += num8;
            }

            //demon blood
            if (DemonBloodEnchant && target.type != NPCID.TargetDummy /*&& !thoriumPlayer.bloodChargeExhaust*/)
            {
                thoriumPlayer.bloodCharge++;
                //thoriumPlayer.bloodc bloodChargeTimer = 120;
                //if (Player.ownedProjectileCounts[ModContent.ProjectileType<DemonBlood>()] < 1)
                //{
                //    Projectile.NewProjectile(Player.Center.X, Player.Center.Y, 0f, 0f, thorium.ProjectileType("DemonBloodVisual"), 0, 0f, Player.whoAmI, 0f, 0f);
                //}
                if (thoriumPlayer.bloodCharge >= 5)
                {
                    Player.statLife += damage / 5;
                    Player.HealEffect(damage / 5, true);
                    Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<BloodBoom>(), 0, 0f, Main.myPlayer, 0f, 0f);
                    damage = (int)((float)damage * 2f);
                    //Player.AddBuff(ModContent.BuffType<DemonBloodE>(), 600, true);
                    thoriumPlayer.bloodCharge = 0;
                }
            }

            if (/*proj.type == thorium.ProjectileType("MeteorPlasmaDamage") ||*/ proj.type == ModContent.ProjectileType<PyroBurst>() || proj.type == ModContent.ProjectileType<LightStrike>() || proj.type == ModContent.ProjectileType<WhiteFlare>() || proj.type == ModContent.ProjectileType<MixtapeNote>() || proj.type == ModContent.ProjectileType<DragonPulse>())
            {
                return;
            }

            //mixtape
            if (MixTape &&/* SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.MixTape) &&*/ crit && proj.type != ModContent.ProjectileType<MixtapeNote>())
            {
                int num23 = Main.rand.Next(3);
                //Main.PlaySound(SoundID.Item, (int)target.position.X, (int)target.position.Y, 73, 1f, 0f);
                for (int n = 0; n < 5; n++)
                {
                    //Projectile.NewProjectile(target.GetSource_FromThis(), target.Center, Vector2 Utils.NextFloat(Main.rand, -5f, 5f), Utils.NextFloat(Main.rand, -5f, 5f), ModContent.ProjectileType<MixtapeNote>(), (int)((float)proj.damage * 0.25f), 2f, proj.owner, (float)num23, 0f);
                }
            }

            //if (ThoriumEnchant /*&& SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.ThoriumDivers)*/ && NPC.CountNPCS(ModContent.NPCType<Diverman>()) < 5 && Main.rand.Next(20) == 0)
            //{
            //    int diver = NPC.NewNPC((int)target.Center.X, (int)target.Center.Y, ModContent.NPCType<Diverman>());
            //    Main.npc[diver].AddBuff(BuffID.ShadowFlame, 9999999);
            //    Main.npc[diver].AddBuff(BuffID.CursedInferno, 9999999);
            //}
        }

        private void ThoriumHitNPC(NPC target, Item Item, bool crit)
        {
            ThoriumPlayer thoriumPlayer = Player.GetModPlayer<ThoriumPlayer>();

            if (SpiritTrapperEnchant /*&& SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.SpiritTrapperWisps) && !Item.summon */)
            {
                if (target.life < 0 && target.value > 0f)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<SpiritTrapperVisual>(), 0, 0, Player.whoAmI, 0f, 0f);
                }
                if (target.boss || NPCID.Sets.BossHeadTextures[target.type] > -1)
                {
                    thoriumPlayer.setSpiritTrapperHit++;
                }
            }

            //tide hunter
            if (/*TideHunterEnchant && SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.TideFoam) &&*/ crit)
            {
                for (int n = 0; n < 10; n++)
                {
                    int num10 = Dust.NewDust(target.position, target.width, target.height, 217, (float)Main.rand.Next(-4, 4), (float)Main.rand.Next(-4, 4), 100, default(Color), 1f);
                    Main.dust[num10].noGravity = true;
                    Main.dust[num10].noLight = true;
                }
                for (int num11 = 0; num11 < 200; num11++)
                {
                    NPC npc = Main.npc[num11];
                    if (npc.active && npc.FindBuffIndex(ModContent.BuffType<Oozed>()) < 0 && !npc.friendly && Vector2.Distance(npc.Center, target.Center) < 80f)
                    {
                        npc.AddBuff(ModContent.BuffType<Oozed>(), 90, false);
                    }
                }
            }

            if (LichEnchant && target.life <= 0)
            {
                //Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, thorium.ProjectileType("SoulFragment"), 0, 0f, Player.whoAmI, 0f, 0f);
                for (int num26 = 0; num26 < 5; num26++)
                {
                    int num27 = Dust.NewDust(target.position, target.width, target.height, 55, (float)Main.rand.Next(-4, 4), (float)Main.rand.Next(-4, 4), 150, default(Color), 0.75f);
                    Main.dust[num27].noGravity = true;
                }
                for (int num28 = 0; num28 < 5; num28++)
                {
                    int num29 = Dust.NewDust(target.position, target.width, target.height, ModContent.DustType<HarbingerDust>(), (float)Main.rand.Next(-3, 3), (float)Main.rand.Next(-3, 3), 100, default(Color), 1f);
                    Main.dust[num29].noGravity = true;
                }
            }

            //life bloom
            if (LifeBloomEnchant && target.type != NPCID.TargetDummy && Main.rand.Next(4) == 0 && thoriumPlayer.setLifeBloomMax < 50)
            {
                for (int l = 0; l < 10; l++)
                {
                    int num7 = Dust.NewDust(target.position, target.width, target.height, 44, (float)Main.rand.Next(-5, 5), (float)Main.rand.Next(-5, 5), 0, default(Color), 1f);
                    Main.dust[num7].noGravity = true;
                }
                int num8 = Main.rand.Next(1, 4);
                Player.statLife += num8;
                Player.HealEffect(num8, true);
                thoriumPlayer.setLifeBloomMax += num8;
            }

            //demon blood
            if (DemonBloodEnchant && target.type != NPCID.TargetDummy //&& !thoriumPlayer.bloodChargeExhaust
                )
            {
                thoriumPlayer.bloodCharge++;
                //thoriumPlayer.timer bloodChargeTimer = 120;
                //if (Player.ownedProjectileCounts[thorium.ProjectileType("DemonBloodVisual")] < 1)
                //{
                    //Projectile.NewProjectile(Player.Center.X, Player.Center.Y, 0f, 0f, thorium.ProjectileType("DemonBloodVisual"), 0, 0f, Player.whoAmI, 0f, 0f);
                //}
                if (thoriumPlayer.bloodCharge >= 5)
                {
                    Player.statLife += Item.damage / 5;
                    Player.HealEffect(Item.damage / 5, true);
                    //Projectile.NewProjectile((float)((int)target.Center.X), (float)((int)target.Center.Y), 0f, 0f, thorium.ProjectileType("BloodBoom"), 0, 0f, Main.myPlayer, 0f, 0f);
                    Item.damage = (int)((float)Item.damage * 2f);
                   // Player.AddBuff(thorium.BuffType("DemonBloodExhaust"), 600, true);
                    thoriumPlayer.bloodCharge = 0;
                }
            }

            //mixtape
            //if (MixTape && SoulConfig.Instance.GetValue(SoulConfig.Instance.thoriumToggles.MixTape) && crit)
            //{
                //int num23 = Main.rand.Next(3);
                //Main.PlaySound(SoundID.Item, (int)target.position.X, (int)target.position.Y, 73, 1f, 0f);
                //for (int n = 0; n < 5; n++)
                //{
                   // Projectile.NewProjectile(target.Center.X, target.Center.Y, Utils.NextFloat(Main.rand, -5f, 5f), Utils.NextFloat(Main.rand, -5f, 5f), thorium.ProjectileType("MixtapeNote"), (int)((float)Item.damage * 0.25f), 2f, Player.whoAmI, (float)num23, 0f);
                //}
            //}
        }

        /*private void ThoriumDamage(float dmg)
        {
            ThoriumPlayer thoriumPlayer = Player.GetModPlayer<ThoriumPlayer>();
            thoriumPlayer.radiantBoost += dmg;
            thoriumPlayer.symphonicDamage += dmg;
        }

        private void ThoriumCrit(int crit)
        {
            ThoriumPlayer thoriumPlayer = Player.GetModPlayer<ThoriumPlayer>();
            thoriumPlayer.radiantCrit += crit;
            thoriumPlayer.symphonicCrit += crit;
        }

        private void ThoriumCritEquals(int crit)
        {
            ThoriumPlayer thoriumPlayer = Player.GetModPlayer<ThoriumPlayer>();
            thoriumPlayer.radiantCrit = crit;
            thoriumPlayer.symphonicCrit = crit;*/
    }
}