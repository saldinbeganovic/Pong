using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Pong.Components;

public class Button : Component
{
    public string Text { get; set; }
    public Color Foreground { get; set; } = Color.White;
    public Color Background { get; set; } = Color.Transparent;

    public Rectangle Bounds { get; private set; }

    public event EventHandler Clicked;

    private bool isHovered;
    private bool isPressed;

    public Button(string text, Rectangle bounds, Color foreground, Color background)
    {
        Text = text;
        Bounds = bounds;
        Foreground = foreground;
        Background = background;
    }

    public override void Draw(GameTime gameTime)
    {
        Globals.SpriteBatch.Draw(Globals.Pixel, Bounds, Background);

        // Draw the button text
        Vector2 textSize = Globals.Font.MeasureString(Text);
        Vector2 textPosition = new Vector2(Bounds.Center.X - textSize.X / 2, Bounds.Center.Y - textSize.Y / 2);
        Globals.SpriteBatch.DrawString(Globals.Font, Text, textPosition, Foreground);
    }

    public override void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();
        // Check if mouse is hovering over the button
        isHovered = Bounds.Contains(mouseState.Position);

        // Check if the left mouse button is pressed
        if (isHovered && mouseState.LeftButton == ButtonState.Pressed)
        {
            isPressed = true;
        }
        else
        {
            if (isPressed && mouseState.LeftButton == ButtonState.Released)
            {
                // If the button was previously pressed and is now released,
                // raise the Clicked event
                Clicked?.Invoke(this, EventArgs.Empty);
                isPressed = false;
            }
        }
    }
}