using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LastDryad.Gameplay
{
    public class GameState
    {
        Player player;
        Game1 game;
        Texture2D playersprite;
        public GameState(Game1 game)
        {
            player = new Player(game);
            this.game = game;
            playersprite = game.Content.Load<Texture2D>("menuselector");
        }

        public void update(GamePadState padstate, GameTime time)
        {

            HandleMovement(padstate, time);
            if (!game.InputLocked)
            {
                HandleButtonPresses(padstate, time);
            }
        }

        private void HandleMovement(GamePadState padstate, GameTime time)
        {
            Vector2 movement = padstate.ThumbSticks.Left * (time.ElapsedGameTime.Milliseconds * Player.speed);
            movement.Y = -movement.Y;
            player.Position = player.Position + movement;
        }

        public void HandleButtonPresses(GamePadState padstate, GameTime time)
        {
            if (padstate.Buttons.Start == ButtonState.Pressed)
            {
                game.OpenMenu(new UI.MainMenu(game));
            }
            if (padstate.Buttons.X == ButtonState.Pressed)
            {
                UI.RadialMenu magic = new UI.RadialMenu(game, player.Position, 150);
                magic.addOption(new UI.ExitOption(game));
                magic.addOption(new UI.ExitOption(game));
                magic.addOption(new UI.ExitOption(game));
                magic.addOption(new UI.NewGameOption(game));
                game.OpenMenu(magic);
            }
            
        }
        public void draw(Game1 game, GameTime time)
        {
            Vector2 playersize = new Vector2(playersprite.Width, playersprite.Height);
            game.MenuBatch.Draw(playersprite, player.Position-playersize/2, Color.White);
        }
    }
}
