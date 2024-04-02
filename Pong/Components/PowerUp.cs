using Microsoft.Xna.Framework;
using Pong.Models;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Pong.Components;

public class PowerUp : Component
{
    public PowerUpType Type { get; set; }
    public PowerUpEffects Effect { get; set; }

    public Rectangle Bounds { get; set; }
    private Random _random = new Random();
    private Color _color;

    public PowerUp(PowerUpType type, PowerUpEffects effect)
    {
        Type = type;
        Effect = effect;
        SetSpawnLocation();
        SetColor();
    }

    private void SetColor()
    {
        switch (Effect)
        {
            case PowerUpEffects.None:
                _color = Color.WhiteSmoke;
                break;

            case PowerUpEffects.InverseControls:
                _color = Color.Yellow;
                break;

            case PowerUpEffects.SizeUp:
                _color = Color.AliceBlue;
                break;

            case PowerUpEffects.SizeDown:
                _color = Color.DarkRed;
                break;

            case PowerUpEffects.Freeze:
                _color = Color.DeepSkyBlue;
                break;

            case PowerUpEffects.ChangeBallSpeed:
                _color = Color.Orange;
                break;

            case PowerUpEffects.SlowDown:
                _color = Color.LightSlateGray;
                break;

            case PowerUpEffects.SpeedUp:
                _color = Color.Green;
                break;

            case PowerUpEffects.BlackHole:
                _color = Color.DarkMagenta;
                break;

            case PowerUpEffects.Spikes:
                _color = Color.Gray;
                break;
        }
    }

    private void SetSpawnLocation()
    {
        int x = _random.Next(100, Globals.Width - 100);
        int y = _random.Next(100, Globals.Height - 100);
        Bounds = new Rectangle(x, y, 30, 30);
    }

    public override void Draw(GameTime gameTime)
    {
        Globals.SpriteBatch.Draw(Globals.Pixel, Bounds, _color);
    }

    public override void Update(GameTime gameTime)
    {
        // throw new System.NotImplementedException();
    }
}