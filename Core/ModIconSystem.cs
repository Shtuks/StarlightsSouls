using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;

// OYFDYIWHFOIWJFPOWFJP HOW IT SHOULD WORK
namespace ssm
{
    public partial class ssm : Mod
    {
        //private Texture2D overlayTexture;
        //private void DrawHook(object self, SpriteBatch spriteBatch)
        //{
        //    // Access the private _mod field to check the current mod
        //    FieldInfo modField = self.GetType().GetField("_mod", BindingFlags.Instance | BindingFlags.NonPublic);
        //    Mod currentMod = (Mod)modField.GetValue(self);

        //    // Check if this is our mod and the texture is loaded
        //    if (currentMod.Name == this.Name && overlayTexture != null)
        //    {
        //        // Access the private _modIcon field to get the icon's dimensions
        //        FieldInfo modIconField = self.GetType().GetField("_modIcon", BindingFlags.Instance | BindingFlags.NonPublic);
        //        UIElement modIcon = (UIElement)modIconField.GetValue(self);
        //        var dimensions = modIcon.GetDimensions();

        //        // Calculate the center position for the overlay
        //        Vector2 position = dimensions.Position();
        //        position.X += dimensions.Width / 2f;
        //        position.Y += dimensions.Height / 2f;

        //        // Draw the overlay texture centered on the mod icon
        //        spriteBatch.Draw(overlayTexture, position, null, Color.White, 0f, overlayTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
        //    }
        //}
    }
}
