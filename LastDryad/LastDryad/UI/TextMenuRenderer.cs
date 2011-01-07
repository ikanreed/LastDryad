using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LastDryad.UI
{
    class TextMenuRenderer:IMenuRenderer
    {
        Game game;
        SpriteFont font;
        string text;
        public TextMenuRenderer(Game game, string fontname, string text)
        {
            this.game = game;
            font = game.Content.Load<SpriteFont>(fontname);
            this.text = text;
        }
        public void render(SpriteBatch drawer, Vector2 position)
        {
            drawer.DrawString(font,text,position,Color.Green);
        }
        public Vector2 size
        {
            get { return font.MeasureString(text); }
        }
    }
}
