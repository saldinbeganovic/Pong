using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pong.Components;
using Pong.Models;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Pong;

public class Paddle
{
    public Rectangle Rect;
    public Color PlayerColor { get; set; }

    private bool _isSecondPlayer;
    private float _moveSpeed = 800f;
    private KeyboardState _kstate;

    public Paddle(int width, int height, Color color, bool isSecondPlayer = false)
    {
        int y = Globals.Height / 2 - height / 2;
        _isSecondPlayer = isSecondPlayer;
        PlayerColor = color;
        Rect = new Rectangle((_isSecondPlayer ? Globals.Width - (width + 5) : 5), y, width, height);
    }

    public void SetPower(PowerUp powerUp)
    {
        switch (powerUp.Effect)
        {
            case PowerUpEffects.SizeUp:
                Rect.Height += 50;
                break;

            case PowerUpEffects.SizeDown:
                Rect.Height -= 50;
                break;
        }
    }

    public void Update(GameTime gameTime)
    {
        _kstate = Keyboard.GetState();
        if ((_isSecondPlayer ? _kstate.IsKeyDown(Keys.Up) : _kstate.IsKeyDown(Keys.W)) && Rect.Y > 0)
        {
            Rect.Y -= (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
        if ((_isSecondPlayer ? _kstate.IsKeyDown(Keys.Down) : _kstate.IsKeyDown(Keys.S)) && Rect.Y < Globals.Height - Rect.Height)
        {
            Rect.Y += (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }

    public void Draw()
    {
        Globals.SpriteBatch.Draw(Globals.Pixel, Rect, PlayerColor);
    }
}