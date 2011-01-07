using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LastDryad.UI
{
    interface IMenuRenderer
    {
        Vector2 size{ get; }
        void render(SpriteBatch drawer, Vector2 position);
    }
}
