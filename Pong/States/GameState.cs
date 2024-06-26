﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pong.Components;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.States;

public class GameState : State
{
    private Paddle _playerOne;
    private Paddle _playerTwo;
    private Ball _ball;
    private List<PowerUp> _powerUps;

    public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
        _playerOne = new Paddle(40, 200, Color.Red);
        _playerTwo = new Paddle(40, 200, Color.Blue, true);
        _ball = new Ball(40, 40);
        _powerUps = new List<PowerUp>();
        PowerUp powerUp = new PowerUp(Models.PowerUpType.Friendly, Models.PowerUpEffects.SizeUp);
        _powerUps.Add(powerUp);
    }

    public override void Draw(GameTime gameTime)
    {
        Globals.SpriteBatch.DrawString(Globals.Font, Globals.Player1_score.ToString(), new Vector2(100, 50), Color.White);
        Globals.SpriteBatch.DrawString(Globals.Font, Globals.Player2_score.ToString(), new Vector2(Globals.Width - 112, 50), Color.White);

        _playerOne.Draw();
        _playerTwo.Draw();
        foreach (PowerUp powerUp in _powerUps)
        {
            powerUp.Draw(gameTime);
        }
        _ball.Draw();
    }

    public override void PostUpdate(GameTime gameTime)
    {
        // throw new NotImplementedException();
    }

    public override void Update(GameTime gameTime)
    {
        _playerOne.Update(gameTime);
        _playerTwo.Update(gameTime);
        foreach (PowerUp powerUp in _powerUps)
        {
            powerUp.Update(gameTime);
        }
        _ball.Update(gameTime, _playerOne, _playerTwo, _powerUps);
    }
}