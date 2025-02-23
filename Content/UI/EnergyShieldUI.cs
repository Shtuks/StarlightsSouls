//using Terraria;
//using Terraria.ModLoader;
//using Terraria.UI;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria.GameContent.UI.Elements;
//using System.Collections.Generic;
//using ReLogic.Graphics;
//using System;
//using Terraria.GameContent;

//namespace ssm.Content.UI
//{
//    public class ShieldBar : UIState
//    {
//        private UIText shieldText;
//        private UIElement shieldBar;
//        private UIImage shieldFill;
//        private bool dragging = false;
//        private Vector2 offset;

//        public override void OnInitialize()
//        {
//            shieldBar = new UIElement();
//            shieldBar.Left.Set(500, 0);
//            shieldBar.Top.Set(50, 0);
//            shieldBar.Width.Set(200, 0);
//            shieldBar.Height.Set(20, 0);

//            shieldFill = new UIImage(ModContent.Request<Texture2D>("ssm/Content/UI/ShieldFull"));
//            shieldFill.Width.Set(200, 0);
//            shieldFill.Height.Set(20, 0);

//            shieldText = new UIText("0/100", 0.8f);
//            shieldText.Top.Set(-20, 0);
//            shieldText.Left.Set(450, 0);

//            shieldBar.Append(shieldFill);
//            shieldBar.Append(shieldText);

//            Append(shieldBar);
//        }

//        public override void Update(GameTime gameTime)
//        {
//            Player player = Main.LocalPlayer;
//            var modPlayer = player.GetModPlayer<ShtunShield>();

//            if (Main.LocalPlayer.Shield().shieldOn)
//            {
//                float shieldRatio = (float)modPlayer.shieldCapacity / modPlayer.shieldCapacityMax2;
//                shieldFill.Width.Set(200 * shieldRatio, 0);

//                shieldText.SetText($"{modPlayer.shieldCapacity}/{modPlayer.shieldCapacityMax2}");

//                if (dragging)
//                {
//                    shieldBar.Left.Set((int)(Main.MouseScreen.X - offset.X), 0);
//                    shieldBar.Top.Set((int)(Main.MouseScreen.Y - offset.Y), 0);
//                }


//                base.Update(gameTime);
//            }
//        }

//        protected override void DrawSelf(SpriteBatch spriteBatch)
//        {
//            Vector2 mousePosition = new Vector2(Main.mouseX, Main.mouseY);

//            if (shieldBar.ContainsPoint(mousePosition))
//            {
//                Main.LocalPlayer.mouseInterface = true;

//                if (Main.mouseLeft && Main.mouseLeftRelease)
//                {
//                    dragging = true;
//                    offset = mousePosition - new Vector2(shieldBar.Left.Pixels, shieldBar.Top.Pixels);
//                }

//                if (!Main.mouseLeft)
//                {
//                    dragging = false;
//                }

//            }
//        }
//    }

//    public class ShieldBarSystem : ModSystem
//    {
//        private UserInterface shieldBarInterface;
//        private ShieldBar shieldBar;

//        public override void Load()
//        {
//            shieldBar = new ShieldBar();
//            shieldBarInterface = new UserInterface();
//            shieldBarInterface.SetState(shieldBar);
//        }

//        public override void UpdateUI(GameTime gameTime)
//        {
//            shieldBarInterface?.Update(gameTime);
//        }

//        public void DrawBar()
//        {
//            Texture2D barTexture = ModContent.Request<Texture2D>("ShieldBar").Value;
//            Texture2D fullTexture = ModContent.Request<Texture2D>("ShieldFull").Value;
//            int anchorX = Main.screenWidth / 2;
//            Player player = Main.LocalPlayer;
//            ShtunShield modPlayer = player.GetModPlayer<ShtunShield>();
//            const int barSize = 128;
//            const int padding = 4;
//            const int chargeSize = barSize - 2 * padding;
//            const int chargeHeight = 20;
//            DynamicSpriteFont font = FontAssets.MouseText.Value;
//            float puriumShieldCharge = Math.Min(modPlayer.shieldCapacityMax2, modPlayer.shieldCapacityMax2);
//            string chargeText = (int)puriumShieldCharge + "/" + (int)modPlayer.shieldCapacityMax2;
//            string maxText = "Charge: " + (int)modPlayer.shieldCapacityMax2 + "/" + (int)modPlayer.shieldCapacityMax2;
//            Vector2 maxTextSize = font.MeasureString(maxText);
//            Color textColor = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor);
//            Main.spriteBatch.DrawString(font, "Charge:", new Vector2(anchorX + barSize / 2 - maxTextSize.X / 2f, 6f), textColor, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
//            Main.spriteBatch.DrawString(font, chargeText, new Vector2(anchorX + barSize / 2 + maxTextSize.X / 2f, 6f), textColor, 0f, new Vector2(font.MeasureString(chargeText).X, 0f), 1f, SpriteEffects.None, 0f);

//            float fill = puriumShieldCharge / modPlayer.shieldCapacityMax2;
//            Main.spriteBatch.Draw(barTexture, new Vector2(anchorX, 32f), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
//            Main.spriteBatch.Draw(fullTexture, new Vector2(anchorX + padding, 32f + padding), new Rectangle(0, 0, (int)(fill * chargeSize), chargeHeight), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
//        }

//        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
//        {
//            if (Main.LocalPlayer.Shield().shieldOn)
//            {
//                int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
//                if (mouseTextIndex != -1)
//                {
//                    layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
//                        "ssm: Shield Bar",
//                        delegate
//                        {
//                            DrawBar();
//                            //shieldBarInterface.Draw(Main.spriteBatch, new GameTime());
//                            return true;
//                        },
//                        InterfaceScaleType.UI)
//                    );
//                }
//            }
//        }
//    }

//}