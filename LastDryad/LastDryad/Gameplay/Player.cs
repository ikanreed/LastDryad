using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LastDryad.Gameplay
{
    class Player
    {
        public const float speed = 0.5f;
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        
        private Game1 game;

        public Player(Game1 game)
        {
            // TODO: Complete member initialization
            this.game = game;
            position = new Vector2(0, 0);
        }
    }
}
