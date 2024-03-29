using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pong.Components;
using System;
using System.Collections.Generic;
using Component = Pong.Components.Component;

namespace Pong.States;

public class MenuState : State
{
    private List<Component> _components;
    private Paddle _playerOne;
    private Paddle _playerTwo;

    public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
        _playerOne = new Paddle(40, 200, Color.Wheat);
        _playerTwo = new Paddle(40, 200, Color.Wheat, true);
        AddComponents(graphicsDevice);
    }

    private void AddComponents(GraphicsDevice graphicsDevice)
    {
        int centerX = Globals.Width / 2;
        int centerY = Globals.Height / 2;
        int marginBottom = 20;
        Color foreground = Color.Black;
        Color background = Color.White;

        Button customizeButton = new Button("CUSTOMIZE", new Rectangle(centerX - 100, Globals.Height - 60 - marginBottom, 200, 60), foreground, background);
        Button startButton = new Button("START GAME", new Rectangle(centerX - 100, customizeButton.Bounds.Y - 60 - marginBottom, 200, 60), foreground, background);

        ColorPicker colorPicker = new ColorPicker(graphicsDevice);
        colorPicker.IsVisible = false;
        startButton.Clicked += Start_Clicked;
        customizeButton.Clicked += CustomizeButton_Clicked;

        TextBlock title = new TextBlock("-:PONG:-", new Vector2(Globals.Width / 2, 80));
        TextBlock playerOne = new TextBlock("P1", new Vector2(_playerOne.Rect.Width / 2 + 5, _playerOne.Rect.Top - marginBottom));
        TextBlock playerTwo = new TextBlock("P2", new Vector2(Globals.Width - _playerTwo.Rect.Width / 2 - 5, _playerTwo.Rect.Top - marginBottom));

        _components = new List<Component>()
      {
            colorPicker,
            customizeButton,
            startButton,
            title,
            playerOne,
            playerTwo,
      };
    }

    private void CustomizeButton_Clicked(object sender, EventArgs e)
    {
        // throw new NotImplementedException();
    }

    private void Start_Clicked(object sender, EventArgs e)
    {
        _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
    }

    public override void Draw(GameTime gameTime)
    {
        _playerOne.Draw();
        _playerTwo.Draw();
        foreach (Component component in _components)
            component.Draw(gameTime);
    }

    public override void PostUpdate(GameTime gameTime)
    {
        // throw new NotImplementedException();
    }

    public override void Update(GameTime gameTime)
    {
        foreach (Component component in _components)
            component.Update(gameTime);
    }
}