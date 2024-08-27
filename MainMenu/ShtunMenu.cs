/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.MainMenu
{
  public class ShtunMenu : ModMenu
  {
    private const string menuAssetPath = "ssm/MainMenu";

    public override Asset<Texture2D> Logo
    {
      get => ModContent.Request<Texture2D>("ssm/MainMenu/ModLogo", (AssetRequestMode) 2);
    }

    public override Asset<Texture2D> SunTexture
    {
      get => ModContent.Request<Texture2D>("ssm/MainMenu/EclipseSun", (AssetRequestMode) 2);
    }

    public override Asset<Texture2D> MoonTexture
    {
      get => ModContent.Request<Texture2D>("ssm/MainMenu/EclipseSun", (AssetRequestMode) 2);
    }

    public override int Music => 91;

    public override ModSurfaceBackgroundStyle MenuBackgroundStyle
    {
      get => (ModSurfaceBackgroundStyle) ModContent.GetInstance<SurfaceBackgroundStyle>();
    }

    public override string DisplayName => "Shtundex";

    public override void OnSelected()
    {
      SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(), (SoundUpdateCallback) null);
    }

    public override bool PreDrawLogo(
      SpriteBatch spriteBatch,
      ref Vector2 logoDrawCenter,
      ref float logoRotation,
      ref float logoScale,
      ref Color drawColor)
    {
      this.ModBackgroundDraw(ModContent.Request<Texture2D>("ssm/MainMenu/ModBackground", (AssetRequestMode) 2).Value, spriteBatch);
      return true;
    }

    private void ModBackgroundDraw(Texture2D texture2D, SpriteBatch spriteBatch)
    {
      Vector2 zero = Vector2.Zero;
      float num1 = (float) Main.screenWidth / (float) texture2D.Width;
      float num2 = (float) Main.screenHeight / (float) texture2D.Height;
      float num3 = num1;
      if ((double) num1 != (double) num2)
      {
        if ((double) num2 > (double) num1)
        {
          num3 = num2;
          zero.X -= (float) (((double) texture2D.Width * (double) num3 - (double) Main.screenWidth) * 0.5);
        }
        else
          zero.Y -= (float) (((double) texture2D.Height * (double) num3 - (double) Main.screenHeight) * 0.5);
      }
      spriteBatch.Draw(texture2D, zero, new Rectangle?(), Color.White, 0.0f, Vector2.Zero, num3, (SpriteEffects) 0, 0.0f);
    }
  }
}*/
