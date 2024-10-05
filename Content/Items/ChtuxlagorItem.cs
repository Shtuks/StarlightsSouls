using ssm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.Content.Items
{
    /// <summary>
    /// Abstract class extended by the items of this mod. <br />
    /// Contains useful code for boilerplate reduction.
    /// </summary>
    public abstract class ChtuxlagorItem : ModItem
    {
        /// <summary>
        /// If it is, the item's text color will automatically be set to a custom color (can manually be overriden) and "Chtuxlagor" will be added to the end of the item's tooltips. <br />
        /// </summary>
        //public virtual bool Infinity => false;

        /// <summary>
        /// A list of articles that this item may begin with depending on localization. <br />
        /// Used for the prefix-article fix.
        /// </summary>
        public virtual List<string> Articles => new List<string> { "The" };

        /// <summary>
        /// Allows you to modify all the tooltips that display for this item. <br />
        /// Called directly after the code in <see cref="SafeModifyTooltips(List{TooltipLine})"/>.
        /// </summary>
        /// <param name="tooltips"></param>
        public virtual void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
        }

        /// <summary>
        /// The location of the item's glowmask texture, defaults to the item's internal texture name with _glow
        /// </summary>
        public virtual string glowmaskstring => Texture.Remove(0, "ssm/".Length) + "_glow";

        /// <summary>
        /// The amount of frames in the item's animation. <br />
        /// </summary>
        public virtual int NumFrames => 1;

        /// <summary>
        /// Allows you to draw things in front of this item. This method is called even if PreDrawInWorld returns false. <br />
        /// Runs directly after the code for PostDrawInWorld in SoulsItem.
        /// </summary>
        public virtual void SafePostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) { }

        public sealed override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (Mod.RequestAssetIfExists(glowmaskstring, out Asset<Texture2D> glow))
            {
                Item item = Main.item[whoAmI];
                Texture2D texture = ssm.Instance.Assets.Request<Texture2D>(glowmaskstring, AssetRequestMode.ImmediateLoad).Value;
                int height = texture.Height / NumFrames;
                int width = texture.Width;
                int frame = (NumFrames > 1) ? (height * Main.itemFrame[whoAmI]) : 0;
                SpriteEffects flipdirection = item.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Rectangle Origin = new Rectangle(0, frame, width, height);
                Vector2 DrawCenter = new Vector2(item.Center.X, item.position.Y + item.height - height / 2);
                Main.EntitySpriteDraw(texture, DrawCenter - Main.screenPosition, Origin, Color.White, rotation, Origin.Size() / 2, scale, flipdirection, 0);
            }
            SafePostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
        }

        public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (tooltips.TryFindTooltipLine("ItemName", out TooltipLine itemNameLine))
            {
                // If this item is exclusive to e-mode, give it a custom item "rarity" (not an actual rarity, wait for 1.4).
                // This is often overridden.
                // if (Chtuxlagor)
                itemNameLine.OverrideColor = ssm.ChtuxlagorColor();

                // Call the artcle-prefix adjustment method.
                // This automatically handles fixing item names that begin with an article.
                itemNameLine.ArticlePrefixAdjustment(Item.prefix, Articles.ToArray());
            }

            SafeModifyTooltips(tooltips);

            // Add the Chtuxlagor toolip after tooltip modification in order to be displayed underneath any manual tooltips (i.e. SoE cycling).

            tooltips.Add(new TooltipLine(Mod, $"{Mod.Name}:Chtuxlagor", Language.GetTextValue($"Mods.ssm.Tooltip.Chtuxlagor")));
        }
    }
}