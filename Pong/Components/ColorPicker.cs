using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong.Components;

public class ColorPicker : Component
{
    private Texture2D colorWheelTexture;
    private Color selectedColor;

    public Color SelectedColor => selectedColor;

    public ColorPicker(GraphicsDevice graphicsDevice)
    {
        // Load color wheel texture
        colorWheelTexture = GenerateColorWheelTexture(graphicsDevice, 200);
        selectedColor = Color.White;
    }

    public override void Draw(GameTime gameTime)
    {
        // Draw color wheel
        Globals.SpriteBatch.Draw(colorWheelTexture, new Vector2(Globals.Width / 2 - 100, Globals.Height / 2 - 100), Color.White);

        // Draw selected color preview
        Globals.SpriteBatch.Draw(Globals.Pixel, new Rectangle(Globals.Width / 2 - 50, Globals.Height - 100, 100, 50), selectedColor);
    }

    public override void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();
        int invertedY = Globals.Height - mouseState.Y;
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            Vector2 center = new Vector2(Globals.Width / 2, Globals.Height / 2);
            Vector2 mousePosition = new Vector2(mouseState.X, invertedY); // Use inverted y-coordinate
            Vector2 relativePosition = mousePosition - center;

            float angle = MathHelper.ToDegrees((float)Math.Atan2(relativePosition.Y, relativePosition.X));
            if (angle < 0)
                angle += 360;
            float distance = relativePosition.Length();

            if (distance <= 100)
            {
                float hue = angle / 360f;
                selectedColor = ColorFromHSV(hue, 1f, 1f);
            }
        }
    }

    private Texture2D GenerateColorWheelTexture(GraphicsDevice graphicsDevice, int diameter)
    {
        Texture2D texture = new Texture2D(graphicsDevice, diameter, diameter);
        Color[] colorData = new Color[diameter * diameter];

        Vector2 center = new Vector2(diameter / 2f, diameter / 2f);

        for (int y = 0; y < diameter; y++)
        {
            for (int x = 0; x < diameter; x++)
            {
                Vector2 position = new Vector2(x, y);
                Vector2 offset = position - center;

                // Adjust the angle calculation to flip horizontally
                float angle = (float)(Math.Atan2(offset.Y, -offset.X) + Math.PI);

                float hue = (float)(angle / (2 * Math.PI));
                float distanceFromCenter = offset.Length();

                // Adjust the saturation calculation based on distance from the center
                float maxDistance = diameter / 2f;
                float saturation = Math.Min(1f, distanceFromCenter / maxDistance);

                colorData[y * diameter + x] = ColorFromHSV(hue, saturation, 1f);
            }
        }

        texture.SetData(colorData);
        return texture;
    }

    private Color ColorFromHSV(float hue, float saturation, float value)
    {
        int hi = (int)(hue * 6);
        float f = hue * 6 - hi;
        float p = value * (1 - saturation);
        float q = value * (1 - f * saturation);
        float t = value * (1 - (1 - f) * saturation);
        float r = 0, g = 0, b = 0;
        switch (hi)
        {
            case 0: r = value; g = t; b = p; break;
            case 1: r = q; g = value; b = p; break;
            case 2: r = p; g = value; b = t; break;
            case 3: r = p; g = q; b = value; break;
            case 4: r = t; g = p; b = value; break;
            case 5: r = value; g = p; b = q; break;
        }
        return new Color((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
    }
}