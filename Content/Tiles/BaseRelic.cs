using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;

namespace ssm.Content.Tiles
{
    // Copied from fargos. No idea why, since there only one relic in mod.
    public abstract class BaseRelic : ModTile
    {
        public const int FrameWidth = 54;

        public const int FrameHeight = 72;

        public const int HorizontalFrames = 1;

        public const int VerticalFrames = 1;

        private const string path = "ssm/Content/Tiles/";

        public Asset<Texture2D> RelicTexture;

        protected abstract int ItemType { get; }

        protected string RelicTextureName => "ssm/Content/Tiles/" + Name;

        public override string Texture => "ssm/Content/Tiles/RelicPedestal";

        public override void Load()
        {
            if (!Main.dedServ)
            {
                RelicTexture = ModContent.Request<Texture2D>(RelicTextureName);
            }
        }

        public override void Unload()
        {
            RelicTexture = null;
        }

        public override void SetStaticDefaults()
        {
            Main.tileShine[base.Type] = 400;
            Main.tileFrameImportant[base.Type] = true;
            TileID.Sets.InteractibleByNPCs[base.Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newTile.StyleHorizontal = false;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(1);
            TileObjectData.addTile(base.Type);
            AddMapEntry(new Color(233, 207, 94), Language.GetText("MapObject.Relic"));
        }

        public override bool CanDrop(int i, int j)
        {
            return false;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int num = frameX / 54;
            int num2 = 0;
            if (num == 0)
            {
                num2 = ItemType;
            }

            if (num2 > 0)
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, num2);
            }
        }

        public override bool CreateDust(int i, int j, ref int type)
        {
            return false;
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            tileFrameX %= 54;
            tileFrameY %= 144;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            if (drawData.tileFrameX % 54 == 0 && drawData.tileFrameY % 72 == 0)
            {
                Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
            }
        }

        public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 vector = new Vector2(Main.offScreenRange);
            if (Main.drawToScreen)
            {
                vector = Vector2.Zero;
            }

            Point p = new Point(i, j);
            Tile tile = Main.tile[p.X, p.Y];
            if (!(tile == null) && tile.HasTile)
            {
                Texture2D value = RelicTexture.Value;
                int frameY = tile.TileFrameX / 54;
                Rectangle rectangle = value.Frame(1, 1, 0, frameY);
                Vector2 origin = rectangle.Size() / 2f;
                Vector2 vector2 = p.ToWorldCoordinates(24f, 64f);
                Color color = Lighting.GetColor(p.X, p.Y);
                SpriteEffects effects = ((tile.TileFrameY / 72 != 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
                float num = (float)Math.Sin(Main.GlobalTimeWrappedHourly * (MathF.PI * 2f) / 5f);
                Vector2 vector3 = vector2 + vector - Main.screenPosition + new Vector2(0f, -40f) + new Vector2(0f, num * 4f);
                spriteBatch.Draw(value, vector3, rectangle, color, 0f, origin, 1f, effects, 0f);
                float num2 = (float)Math.Sin(Main.GlobalTimeWrappedHourly * (MathF.PI * 2f) / 2f) * 0.3f + 0.7f;
                Color color2 = color;
                color2.A = 0;
                color2 = color2 * 0.1f * num2;
                for (float num3 = 0f; num3 < 1f; num3 += 1f / 6f)
                {
                    spriteBatch.Draw(value, vector3 + (MathF.PI * 2f * num3).ToRotationVector2() * (6f + num * 2f), rectangle, color2, 0f, origin, 1f, effects, 0f);
                }
            }
        }
    }
}