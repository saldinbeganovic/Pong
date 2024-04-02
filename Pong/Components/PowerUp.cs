﻿using Microsoft.Xna.Framework;
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

    public PowerUp(PowerUpType type, PowerUpEffects effect)
    {
        Type = type;
        Effect = effect;
        SetSpawnLocation();
    }

    private void SetSpawnLocation()
    {
        int x = _random.Next(100, Globals.Width - 100);
        int y = _random.Next(100, Globals.Height - 100);
        Bounds = new Rectangle(x, y, 30, 30);
    }

    public override void Draw(GameTime gameTime)
    {
        Globals.SpriteBatch.Draw(Globals.Pixel, Bounds, Color.HotPink);
    }

    public override void Update(GameTime gameTime)
    {
        // throw new System.NotImplementedException();
    }
}