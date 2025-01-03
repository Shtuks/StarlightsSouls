using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm;
using ssm.SHTUK;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

internal class EnergyBar : UIElement
{
    private float _width;

    private float _height;

    private int _frameTimer;

    private int _frameTimer2;

    private UIText _text;

    private Rectangle _barDestination;

    private Vector2 _drawPosition;

    private Color _gradientA;

    private Color _gradientB;

    public Texture2D texture;

    private static List<float> _AverageEnergy = new List<float>();

    public EnergyBar(int height, int width)
    {
        _width = width;
        _height = height;
    }

    public override void OnInitialize()
    {
        Height.Set(_height, 0f);
        Width.Set(_width, 0f);
        _gradientA = new Color(0, 0, 0);
        _gradientB = new Color(0, 0, 0);
        _text = new UIText("0/0");
        _text.Width.Set(_width, 0f);
        _text.Height.Set(_height, 0f);
        _text.Top.Set(_height / 2f + 10f, 0f);
        _text.Left.Set(_width - 60f, 0f);
        Append(_text);
        _barDestination = new Rectangle(20, 0, (int)_width, (int)_height);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        SHTUKPlayer player = Main.LocalPlayer.GetModPlayer<SHTUKPlayer>();
        float quotient = 1f;
        float averageEnergy = (float)Math.Floor(_AverageEnergy.Sum() / 15f);
        quotient = averageEnergy / (int)Math.Ceiling(player.energyMax * player.energyMaxMult + player.energyMax2);
        quotient = Utils.Clamp(quotient, 0f, 1f);
        Rectangle hitbox = GetInnerDimensions().ToRectangle();
        hitbox.X += _barDestination.X;
        hitbox.Y += _barDestination.Y;
        hitbox.Width = _barDestination.Width;
        hitbox.Height = _barDestination.Height;
        int left = hitbox.Left;
        int right = hitbox.Right;
        int steps = (int)((float)(right - left) * quotient);
        for (int i = 0; i < steps; i++)
        {
            float percent = (float)i / (float)(right - left);
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(_gradientA, _gradientB, percent));
        }
        _frameTimer++;
        if (_frameTimer >= 20)
        {
            _frameTimer = 0;
        }
        int frameHeight = ShtunRegs.energyBar.Height() / 4;
        int frame = _frameTimer / 5;
        texture = (Texture2D)ShtunRegs.energyBar;
        _drawPosition = new Vector2(hitbox.X - 36, hitbox.Y - 4);
        Rectangle sourceRectangle = default(Rectangle);
        spriteBatch.Draw(sourceRectangle: new Rectangle(0, frameHeight * frame, ShtunRegs.energyBar.Width(), frameHeight), texture: texture, position: _drawPosition, color: Color.White);
        _frameTimer2 += 3;
        if (_frameTimer2 >= 15)
        {
            _frameTimer2 = 0;
        }
    }

    public override void Update(GameTime gameTime)
    {
        SHTUKPlayer player = Main.LocalPlayer.GetModPlayer<SHTUKPlayer>();

        _AverageEnergy.Add(player.energy);
        if (_AverageEnergy.Count > 15)
        {
            _AverageEnergy.RemoveRange(0, _AverageEnergy.Count - 15);
        }
        int averageEnergy = (int)Math.Floor(_AverageEnergy.Sum() / 15f);
        _text.SetText("Ki:" + averageEnergy + " / " + (int)Math.Ceiling(player.energyMax * player.energyMaxMult + player.energyMax2));
    }
}

internal class BarState : UIState
{
    public EnergyBar bar;

    public static bool visible;

    private Vector2 _offset;

    public bool dragging;

    public override void OnInitialize()
    {
        bar = new EnergyBar(20, 100);
        bar.Left.Set(515f, 0f);
        bar.Top.Set(49f, 0f);
        bar.OnLeftMouseDown += DragStart;
        bar.OnLeftMouseUp += DragEnd;
        Append(bar);
    }

    private void DragStart(UIMouseEvent evt, UIElement listeningElement)
    {
        _offset = new Vector2(evt.MousePosition.X - bar.Left.Pixels, evt.MousePosition.Y - bar.Top.Pixels);
        dragging = true;
    }

    private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
    {
        Vector2 end = evt.MousePosition;
        dragging = false;
        bar.Left.Set(end.X - _offset.X, 0f);
        bar.Top.Set(end.Y - _offset.Y, 0f);
        Recalculate();
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        Vector2 mousePosition = default;
        mousePosition = new Vector2(Main.mouseX, Main.mouseY);
        if (bar.ContainsPoint(mousePosition))
        {
            Main.LocalPlayer.mouseInterface = true;
        }
        if (dragging)
        {
            bar.Left.Set(mousePosition.X - _offset.X, 0f);
            bar.Top.Set(mousePosition.Y - _offset.Y, 0f);
            Recalculate();
        }
    }
}
