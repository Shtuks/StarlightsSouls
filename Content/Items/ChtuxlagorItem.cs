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
    public abstract class ChtuxlagorItem : ModItem
    {
        public virtual List<string> Articles => new List<string> { "The" };

        public virtual void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
        }

        public virtual string glowmaskstring => Texture.Remove(0, "ssm/".Length) + "_glow";

        public virtual int NumFrames => 1;

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
                itemNameLine.OverrideColor = ssm.ChtuxlagorColor();

                itemNameLine.ArticlePrefixAdjustment(Item.prefix, Articles.ToArray());
            }

            SafeModifyTooltips(tooltips);
        
            tooltips.Add(new TooltipLine(Mod, $"{Mod.Name}:Chtuxlagor", Language.GetTextValue($"Mods.ssm.Tooltip.Chtuxlagor")));
        }
    }
}