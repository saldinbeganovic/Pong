using Microsoft.Xna.Framework;

namespace Pong.Components;

public class TextBlock : Component
{
    public string Text { get; set; }
    public Color Foreground { get; set; }
    public Color Background { get; set; }
    public Vector2 Position { get; set; }

    public TextBlock(string text, Vector2 position)
    {
        Text = text;
        Position = position;
        Foreground = Color.White;
        Background = Color.Transparent;
    }

    public override void Draw(GameTime gameTime)
    {
        Vector2 textSize = Globals.Font.MeasureString(Text);
        Vector2 textPosition = new Vector2(Position.X - textSize.X / 2, Position.Y - textSize.Y / 2);
        Globals.SpriteBatch.DrawString(Globals.Font, Text, textPosition, Foreground);
    }

    public override void Update(GameTime gameTime)
    {
        // throw new NotImplementedException();
    }
}