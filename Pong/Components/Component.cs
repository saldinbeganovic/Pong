using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Components;

public abstract class Component
{
    public bool IsVisible { get; set; }

    public abstract void Draw(GameTime gameTime);

    public abstract void Update(GameTime gameTime);
}