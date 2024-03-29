using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Metadata;

namespace Pong;

public class Globals
{
    public static SpriteBatch SpriteBatch;
    public static int Width = 1920;
    public static int Height = 1080;
    public static Texture2D Pixel;
    public static int Player1_score = 0;
    public static int Player2_score = 0;
    public static SpriteFont Font;
}