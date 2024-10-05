using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using ssm;

namespace ssm.Content.Projectiles.Shtuxibus
{
    public class Shtuxibus : ModProjectile
    {
        public override string Texture => "ssm/Content/NPCs/Shtuxibus/Shtuxibus";
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public string trailTexture => "Content/NPCs/Shtuxibus/ShtuxibusSoul";
        public int npcType => ModContent.NPCType<NPCs.Shtuxibus.Shtuxibus>();
        public bool auraTrail;
        const int auraFrames = 19;
        //const int lightningFrames = 20;
        public bool sansEye;
        public float SHADOWMUTANTREAL;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mutant");
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 50;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if (Projectile.hide)
                behindProjectiles.Add(index);
        }

        public override void AI()
        {
            NPC npc = ShtunUtils.NPCExists(Projectile.ai[1], npcType);
            if (npc != null)
            {
                Projectile.Center = npc.Center;
                Projectile.alpha = npc.alpha;
                Projectile.direction = Projectile.spriteDirection = npc.direction;
                Projectile.timeLeft = 30;
                auraTrail = npc.localAI[3] >= 3;

                /*switch((int)npc.ai[0]) //draw behind whenever holding a weapon
                {
                    case 0:
                    case 4:
                    case 5:
                    case 6:
                    case 13:
                    case 14:
                    case 15:
                    case 21:
                    case 22:
                    case 23:
                    case 25:
                    case 36:
                    case 41:
                        Projectile.hide = true;
                        break;

                    default:
                        Projectile.hide = false;
                        break;
                }*/

                Projectile.hide =
                    Main.player[Projectile.owner].ownedProjectileCounts[ModContent.Find<ModProjectile>(fargosouls.Name, "MutantSpearAim").Type] > 0
                    || Main.player[Projectile.owner].ownedProjectileCounts[ModContent.Find<ModProjectile>(fargosouls.Name, "MutantSpearDash").Type] > 0
                    || Main.player[Projectile.owner].ownedProjectileCounts[ModContent.Find<ModProjectile>(fargosouls.Name, "MutantSpearSpin").Type] > 0
                    || Main.player[Projectile.owner].ownedProjectileCounts[ModContent.Find<ModProjectile>(fargosouls.Name, "MutantSlimeRain").Type] > 0;

                sansEye =
                    (npc.ai[0] == 10 && npc.ai[1] > 150)
                    || (npc.ai[0] == -5 && npc.ai[2] > 420 - 90 && npc.ai[2] < 420);

                if (npc.ai[0] == 10)
                {
                    SHADOWMUTANTREAL += 0.03f;
                    if (SHADOWMUTANTREAL > 0.75f)
                        SHADOWMUTANTREAL = 0.75f;
                }

                Projectile.localAI[1] = sansEye ? MathHelper.Lerp(Projectile.localAI[1], 1f, 0.05f) : 0; //for rotation of sans eye
                Projectile.ai[0] = sansEye ? Projectile.ai[0] + 1 : 0;

                if ((npc.ai[0] >= 11 || npc.ai[0] < 0))
                {
                    sansEye = true;
                    Projectile.ai[0] = -1;
                }

                if (!Main.dedServ)
                    Projectile.frame = (int)(npc.frame.Y / (float)(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]));

                if (npc.frameCounter == 0)
                {
                    if (++Projectile.localAI[0] >= auraFrames)
                        Projectile.localAI[0] = 0;
                }
            }
            else
            {
                sansEye = false;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.Kill();
                return;
            }

            SHADOWMUTANTREAL -= 0.01f;
            if (SHADOWMUTANTREAL < 0)
                SHADOWMUTANTREAL = 0;
        }

        public override void OnKill(int timeLeft)
        {
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Texture2D texture2D14 = ssm.Instance.Assets.Request<Texture2D>(trailTexture, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * Projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Texture2D aura = ssm.Instance.Assets.Request<Texture2D>("NPCs/Shtuxibus/MutantAura", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            int auraFrameHeight = aura.Height / auraFrames;
            int auraY = auraFrameHeight * (int)Projectile.localAI[0];
            Rectangle auraRectangle = new Rectangle(0, auraY, aura.Width, auraFrameHeight);

            Color color26 = Projectile.GetAlpha(Projectile.hide && Main.netMode == NetmodeID.MultiplayerClient ? Lighting.GetColor((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16) : lightColor);

            SpriteEffects effects = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            float scale = (Main.mouseTextColor / 200f - 0.35f) * 0.4f + 0.9f;
            scale *= Projectile.scale;

            if (auraTrail || SHADOWMUTANTREAL > 0)
                Main.EntitySpriteDraw(texture2D14, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Color.White * Projectile.Opacity, Projectile.rotation, origin2, scale, effects, 0);

            if (auraTrail)
            {
                Color color25 = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB) * Projectile.Opacity;
                color25.A = 200;

                for (float i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i += 0.25f)
                {
                    Color color27 = color25 * 0.5f;
                    color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];
                    int max0 = (int)i - 1;//Math.Max((int)i - 1, 0);
                    if (max0 < 0)
                        max0 = 0;
                    float num165 = Projectile.oldRot[max0];
                    Vector2 center = Vector2.Lerp(Projectile.oldPos[(int)i], Projectile.oldPos[max0], 1 - i % 1);
                    center += Projectile.Size / 2;
                    Main.EntitySpriteDraw(texture2D14, center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, Projectile.scale, effects, 0);
                }

                Main.EntitySpriteDraw(aura, -16 * Vector2.UnitY + Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(auraRectangle), color25, Projectile.rotation, auraRectangle.Size() / 2f, scale, effects, 0);
            }
            else
            {
                for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
                {
                    Color color27 = color26;
                    color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];
                    Vector2 value4 = Projectile.oldPos[i];
                    float num165 = Projectile.oldRot[i];
                    Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, Projectile.scale, effects, 0);
                }
            }

            color26 = Color.Lerp(color26, Color.Black, SHADOWMUTANTREAL);
            Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, Projectile.rotation, origin2, Projectile.scale, effects, 0);

            if (sansEye)
            {
                Color color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);

                bool forcedMasoEye = Projectile.ai[0] == -1;

                const int maxTime = 120;
                float effectiveTime = Projectile.ai[0];
                float rotation = MathHelper.TwoPi * Projectile.localAI[1];
                float modifier = Math.Min(1f, (float)Math.Sin(Math.PI * effectiveTime / maxTime) * 2f);
                float opacity =
                    forcedMasoEye
                    ? 1f
                    : Math.Min(1f, modifier * 2f);
                float sansScale =
                    forcedMasoEye
                    ? Projectile.scale * Main.cursorScale * 0.8f * Main.rand.NextFloat(0.75f, 1.25f)
                    : Projectile.scale * modifier * Main.cursorScale * 1.25f;

                Texture2D star = ssm.Instance.Assets.Request<Texture2D>("LifeStar", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                Rectangle rect = new Rectangle(0, 0, star.Width, star.Height);
                Vector2 origin = new Vector2((star.Width / 2) + sansScale, (star.Height / 2) + sansScale);

                Vector2 drawPos = Projectile.Center;
                drawPos.X += 8 * Projectile.spriteDirection;
                drawPos.Y -= 11;

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

                Main.spriteBatch.Draw(star, drawPos - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(rect), color * opacity, rotation, origin, sansScale, SpriteEffects.None, 0);
                Main.spriteBatch.Draw(star, drawPos - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(rect), new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB) * opacity * 0.75f, rotation, origin, sansScale, SpriteEffects.None, 0);

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            }
            return false;
        }
    }
}