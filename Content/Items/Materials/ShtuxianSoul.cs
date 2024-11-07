using System;
using CalamityHunt;
using CalamityHunt.Common.Players;
using CalamityHunt.Common.Utilities;
using CalamityHunt.Content.Items.Misc.AuricSouls;
using CalamityHunt.Content.Items.Rarities;
using CalamityHunt.Content.NPCs.Bosses.GoozmaBoss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Content.Items.Materials
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Goozma.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Goozma.Name)]
    public class ShtuxianSoul : ModItem
    {
        public static Texture2D trailTexture;

        public LoopingSound heartbeatSound;

        public LoopingSound droneSound;

        public int breathSoundCounter;

        public override void Load()
        {
            trailTexture = AssetDirectory.Textures.AuricSouls.GoozmaSoulTrail.Value;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[base.Type] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[base.Type] = true;
            ItemID.Sets.IgnoresEncumberingStone[base.Type] = true;
            Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[base.Type] = true;
            ItemID.Sets.ItemIconPulse[base.Type] = true;
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[base.Type] = true;
        }

        public override void SetDefaults()
        {
            base.Item.width = 16;
            base.Item.height = 16;
            base.Item.rare = ModContent.RarityType<VioletRarity>();
            if (ModLoader.HasMod("CalamityMod"))
            {
                Mod calamity = ModLoader.GetMod("CalamityMod");
                calamity.TryFind<ModRarity>("Violet", out var r);
                base.Item.rare = r.Type;
            }
        }

        public override bool OnPickup(Player player)
        {
            for (int i = 0; i < 150; i++)
            {
                Dust soul = Dust.NewDustPerfect(base.Item.Center, 264, Main.rand.NextVector2Circular(10f, 10f), 0, this.GetAlpha(Color.White).Value, Main.rand.NextFloat(2f));
                soul.noGravity = true;
            }
            player.GetModPlayer<AuricSoulPlayer>().goozmaSoul = true;
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color glowColor = new GradientColor(SlimeUtils.GoozColors, 0.5f, 0.5f).ValueAt(Main.GlobalTimeWrappedHourly * 30f - 10f);
            Color secColor = Color.SaddleBrown;
            return Color.Lerp(glowColor, secColor, 0.3f + MathF.Sin(Main.GlobalTimeWrappedHourly * 0.1f) * 0.1f);
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            //IL_0145: Unknown result type (might be due to invalid IL or missing references)
            if (this.heartbeatSound == null)
            {
                this.heartbeatSound = new LoopingSound(AssetDirectory.Sounds.Souls.GoozmaSoulHeartbeat, new HUtils.ItemAudioTracker(base.Item).IsActiveAndInGame);
            }
            this.heartbeatSound.PlaySound(() => base.Item.position, () => 1f, () => 0f);
            if (this.droneSound == null)
            {
                this.droneSound = new LoopingSound(AssetDirectory.Sounds.Souls.GoozmaSoulDrone, new HUtils.ItemAudioTracker(base.Item).IsActiveAndInGame);
            }
            this.droneSound.PlaySound(() => base.Item.position, () => 1.5f, () => 0f);
            if (this.breathSoundCounter-- <= 0)
            {
                SoundEngine.PlaySound(in AssetDirectory.Sounds.Souls.GoozmaSoulBreathe, base.Item.Center);
                this.breathSoundCounter = Main.rand.Next(300, 500);
            }
            if (Main.rand.NextBool(5))
            {
                Dust soul = Dust.NewDustDirect(base.Item.Center - new Vector2(15f), 30, 30, 264, 0f, 0f - Main.rand.NextFloat(1f, 2f), 0, this.GetAlpha(Color.White).Value, Main.rand.NextFloat(2f));
                soul.noGravity = true;
            }
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D glowTexture = AssetDirectory.Textures.Glow[1].Value;
            Texture2D eyeTexture = AssetDirectory.Textures.ChromaticSoulEye.Value;
            Color glowColor = this.GetAlpha(Color.White).Value;
            glowColor.A = 0;
            float soulScale = scale;
            float fastScale = 1f + MathF.Sin(Main.GlobalTimeWrappedHourly * 15f % ((float)Math.PI * 2f)) * 0.1f;
            scale = 1f + MathF.Sin(Main.GlobalTimeWrappedHourly % ((float)Math.PI * 2f)) * 0.1f;
            spriteBatch.Draw(glowTexture, base.Item.Center - Main.screenPosition, glowTexture.Frame(), Color.Black * 0.2f, 0f, glowTexture.Size() * 0.5f, soulScale * 3f, SpriteEffects.None, 0f);
            VertexStrip strip = new VertexStrip();
            VertexStrip strip2 = new VertexStrip();
            int count = 500;
            Vector2[] offs = new Vector2[count];
            Vector2[] offs2 = new Vector2[count];
            float[] offRots = new float[count];
            float[] offRots2 = new float[count];
            float time = Main.GlobalTimeWrappedHourly * 0.5f;
            for (int i = 0; i < count; i++)
            {
                Vector2 x = new Vector2(60f * scale + MathF.Sin(time * 5f + (float)i / (float)count * 76f) * 7f, 0f).RotatedBy((float)Math.PI * 2f / (float)count * (float)i - time);
                x.X *= 1f + MathF.Cos(time * 2f) * 0.1f;
                offs[i] = x.RotatedBy(time);
                Vector2 y = new Vector2(30f + MathF.Sin(time * 8f + (float)i / (float)count * 76f) * 3f, 0f).RotatedBy((float)Math.PI * 2f / (float)count * (float)i - time + 0.9f);
                y.X *= 1f + MathF.Cos(time * 3f) * 0.2f;
                offs2[i] = y.RotatedBy(time);
            }
            offRots[0] = offs[0].AngleTo(offs[1]);
            offRots2[0] = offs2[0].AngleTo(offs2[1]);
            for (int i = 1; i < count; i++)
            {
                offRots[i] = offs[i - 1].AngleTo(offs[i]);
                offRots2[i] = offs2[i - 1].AngleTo(offs[i]);
            }
            strip.PrepareStrip(offs, offRots, StripColor, StripWidth, base.Item.Center - Main.screenPosition, offs.Length, includeBacksides: true);
            strip2.PrepareStrip(offs2, offRots2, StripColor, StripWidth, base.Item.Center - Main.screenPosition, offs2.Length, includeBacksides: true);
            Effect effect = AssetDirectory.Effects.CrystalLightning.Value;
            effect.Parameters["uTransformMatrix"].SetValue(Main.GameViewMatrix.NormalizedTransformationmatrix);
            effect.Parameters["uTexture"].SetValue(GoozmaSoul.trailTexture);
            effect.Parameters["uGlow"].SetValue(TextureAssets.Extra[197].Value);
            effect.Parameters["uColor"].SetValue(Vector3.One);
            effect.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly * 0.2f % 1f);
            effect.CurrentTechnique.Passes[0].Apply();
            strip.DrawTrail();
            strip2.DrawTrail();
            for (int i = 0; i < 4; i++)
            {
                Color sparkleColor = new GradientColor(SlimeUtils.GoozColors, 0.5f, 0.5f).ValueAt(Main.GlobalTimeWrappedHourly * 30f + (float)i) * 0.7f;
                sparkleColor.A = 0;
                float length = 4f;
                if (i % 2 == 1)
                {
                    length *= 0.5f;
                }
                this.DrawSideSparkle(spriteBatch, base.Item.Center + new Vector2(10f + length * 15f, 0f).RotatedBy((float)Math.PI / 2f * (float)i) - Main.screenPosition, (float)Math.PI / 2f * (float)i, sparkleColor, length);
            }
            spriteBatch.Draw(eyeTexture, base.Item.Center - Main.screenPosition, eyeTexture.Frame(), this.GetAlpha(Color.White).Value * 0.3f, -(float)Math.PI / 4f, eyeTexture.Size() * 0.5f, scale * 0.8f, SpriteEffects.None, 0f);
            spriteBatch.Draw(eyeTexture, base.Item.Center - Main.screenPosition, eyeTexture.Frame(), new Color(255, 255, 255, 0), -(float)Math.PI / 4f, eyeTexture.Size() * 0.5f, scale * 0.8f, SpriteEffects.None, 0f);
            spriteBatch.Draw(eyeTexture, base.Item.Center - Main.screenPosition, eyeTexture.Frame(), glowColor, -(float)Math.PI / 4f, eyeTexture.Size() * 0.5f, scale * 0.9f, SpriteEffects.None, 0f);
            spriteBatch.Draw(glowTexture, base.Item.Center - Main.screenPosition, glowTexture.Frame(), glowColor * 0.4f, 0f, glowTexture.Size() * 0.5f, 0.2f + fastScale * 0.4f, SpriteEffects.None, 0f);
            spriteBatch.Draw(glowTexture, base.Item.Center - Main.screenPosition, glowTexture.Frame(), glowColor * 0.2f, 0f, glowTexture.Size() * 0.5f, fastScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(glowTexture, base.Item.Center - Main.screenPosition, glowTexture.Frame(), glowColor * 0.6f, 0f, glowTexture.Size() * 0.5f, 0.1f + fastScale * 0.1f, SpriteEffects.None, 0f);
            return false;
            static Color StripColor(float p)
            {
                Color final = new GradientColor(SlimeUtils.GoozColors, 0.5f, 0.5f).ValueAt(p * 20f - 10f + Main.GlobalTimeWrappedHourly * 30f);
                return final * 0.8f;
            }
            static float StripWidth(float p)
            {
                return 15f * MathHelper.Clamp(MathF.Sin(p * 20f) + 0.4f, 0f, 1f);
            }
        }

        private void DrawSideSparkle(SpriteBatch spriteBatch, Vector2 position, float rotation, Color color, float length)
        {
            Texture2D sparkTexture = AssetDirectory.Textures.Sparkle.Value;
            Vector2 t = new Vector2(0.2f, length * 0.7f);
            spriteBatch.Draw(scale: new Vector2(0.6f, length + 0.2f), texture: sparkTexture, position: position, sourceRectangle: sparkTexture.Frame(), color: color * 0.1f, rotation: rotation + (float)Math.PI / 2f, origin: sparkTexture.Size() * 0.5f, effects: SpriteEffects.None, layerDepth: 0f);
            spriteBatch.Draw(sparkTexture, position, sparkTexture.Frame(), color * 1.5f, rotation + (float)Math.PI / 2f, sparkTexture.Size() * 0.5f, t, SpriteEffects.None, 0f);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = TextureAssets.Item[base.Type].Value;
            Texture2D glowTexture = AssetDirectory.Textures.Glow[1].Value;
            spriteBatch.Draw(texture, position, frame, this.GetAlpha(Color.White).Value, 0f, frame.Size() * 0.5f, scale + 0.2f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, position, frame, new Color(200, 200, 200, 0), 0f, frame.Size() * 0.5f, scale + 0.2f, SpriteEffects.None, 0f);
            Color glowColor = this.GetAlpha(Color.White).Value;
            glowColor.A = 0;
            spriteBatch.Draw(glowTexture, position, glowTexture.Frame(), glowColor * 0.7f, 0f, glowTexture.Size() * 0.5f, scale * 0.2f, SpriteEffects.None, 0f);
            return false;
        }
    }

}
