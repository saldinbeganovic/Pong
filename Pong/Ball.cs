using Microsoft.Xna.Framework;
using Pong.Components;
using Pong.Models;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Pong;

public class Ball
{
    private Rectangle _rect;
    private int _right = 1, _top = 1, _moveSpeed = 1000;
    private int _deltaSpeed;
    private Size _ballSize;
    private Color _ballColor;
    private Paddle? _lastHit = null;
    private Paddle? _enemy = null;

    public Ball(int width, int height)
    {
        _ballSize = new Size(width, height);
        _ballColor = Color.White;
        _rect = new Rectangle(Globals.Width / 2 - width / 2, Globals.Height / 2 - height / 2, width, height);
    }

    public void Update(GameTime gameTime, Paddle player1, Paddle player2, List<PowerUp> powerUps)
    {
        _deltaSpeed = (int)(_moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        _rect.X += _right * _deltaSpeed;
        _rect.Y += _top * _deltaSpeed;

        CheckPlayerColision(player1, player2);
        CheckPowerUpColision(powerUps);
        CheckWindowColision();
    }

    private void CheckPlayerColision(Paddle player1, Paddle player2)
    {
        if (player1.Rect.Right > _rect.Left && _rect.Top > player1.Rect.Top && _rect.Bottom < player1.Rect.Bottom)
        {
            _right = 1;
            _ballColor = player1.PlayerColor;
            _lastHit = player1;
            _enemy = player2;
        }
        if (player2.Rect.Left < _rect.Right && _rect.Top > player2.Rect.Top && _rect.Bottom < player2.Rect.Bottom)
        {
            _right = -1;
            _ballColor = player2.PlayerColor;
            _lastHit = player2;
            _enemy = player1;
        }
    }

    private void CheckPowerUpColision(List<PowerUp> powerUps)
    {
        if (_lastHit == null) return;
        foreach (PowerUp powerUp in powerUps)
        {
            // Check if the ball's bounds intersect with the power-up's bounds
            if (_rect.Intersects(powerUp.Bounds))
            {
                // Handle the collision here
                // For example, you can apply the power-up effect to the game

                switch (powerUp.Type)
                {
                    case Models.PowerUpType.Friendly:
                        _lastHit.SetPower(powerUp);
                        break;

                    case Models.PowerUpType.Lethal:
                        _enemy.SetPower(powerUp);
                        break;
                }

                // Optionally, remove the power-up from the list since it has been collected
                powerUps.Remove(powerUp);
                CreateNewPowerUp(powerUps);

                // Exit the loop since we've found a collision
                return;
            }
        }
    }

    private void CreateNewPowerUp(List<PowerUp> powerUps)
    {
        Random rnd = new Random();
        int a = rnd.Next(1, 9);
        int b = rnd.Next(1, 2);
        PowerUp powerUp = new PowerUp((PowerUpType)b, (PowerUpEffects)a);
        powerUps.Add(powerUp);
    }

    private void CheckWindowColision()
    {
        if (_rect.Y < 0)
        {
            _top *= -1;
        }
        if (_rect.Y > Globals.Height - _rect.Height)
        {
            _top *= -1;
        }
        if (_rect.X < 0)
        {
            Globals.Player2_score += 1;
            ResetGame();
            //_moveSpeed += 10;
        }
        if (_rect.X > Globals.Width - _rect.Width)
        {
            Globals.Player1_score += 1;
            ResetGame();
            // _moveSpeed += 10;
        }
    }

    public void ResetGame()
    {
        _rect.X = Globals.Width / 2 - _ballSize.Width;
        _rect.Y = Globals.Height / 2 - _ballSize.Height;
        _ballColor = Color.White;
        _lastHit = null;
        _enemy = null;
    }

    public void Draw()
    {
        Globals.SpriteBatch.Draw(Globals.Pixel, _rect, _ballColor);
    }
}