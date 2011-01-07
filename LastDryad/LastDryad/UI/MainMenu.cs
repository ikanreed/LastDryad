using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace LastDryad.UI
{
    class MainMenu:IMenu
    {
        List<IMenuOption> options;
        Game1 game;
        Texture2D selectorIcon;
        const float spacing = 35.0f;
        const float leftpadding = 150;
        const float toppadding = 100;
        const float icondistance = 35;
        int selectedIndex;
        private bool moved;
        public MainMenu(Game1 game)
        {

            options = new List<IMenuOption>();
            this.game=game;
            if (game.State != null)
            {
                this.options.Add(new ContinueOption(game));
            }
            this.options.Add(new NewGameOption(game));
            this.options.Add(new ExitOption(game));
            selectorIcon = game.Content.Load<Texture2D>("menuselector");
            selectedIndex = 0;
            moved = false;
        }
        public void render()
        {
            for (int y = 0; y < options.Count; y++)
            {
                if (y == selectedIndex)
                {
                    game.MenuBatch.Draw(
                        selectorIcon,
                        new Vector2(leftpadding-icondistance,toppadding+spacing*y)
                        ,Color.White
                        );
                }
                options[y].Renderer.render(
                    game.MenuBatch,
                    new Vector2(leftpadding,toppadding+spacing*y)
                    );
                
            }
        }
        const float stickthreshold = 0.1f;
        public void handleInput(GamePadState state, GameTime time)
        {
            if (game.InputLocked)
            {
                moved = true;
                return;
                
            }
            float thumby=state.ThumbSticks.Left.Y;
            if (state.Buttons.A == ButtonState.Pressed)
            {
                this.options[selectedIndex].action();
            }
            else if (Math.Abs(thumby) > stickthreshold)
            {
                if (!moved)
                {
                    moved = true;
                    if (thumby > 0)
                    {
                        selectedIndex = (selectedIndex + options.Count-1) % options.Count;
                    }
                    else
                    {
                        selectedIndex = (selectedIndex + 1) % options.Count;
                    }
                }
            }
            else
            {
                moved = false;
            }
        }

    }


    class ExitOption : IMenuOption
    {
        Game1 game;
        IMenuRenderer renderer;
        public ExitOption(Game1 game)
        {
            this.game = game;
            renderer = new TextMenuRenderer(game, "LeafyStencil", "EXIT");
        }
        public IMenuRenderer Renderer
        {
            get { return renderer; }
        }

        public void action()
        {
            game.Exit();
        }
    }

    class NewGameOption : IMenuOption
    {
        Game1 game;
        IMenuRenderer renderer;
        public NewGameOption(Game1 game)
        {
            this.game = game;
            renderer = new TextMenuRenderer(game, "LeafyStencil", "NEW GAME");
        }

        public IMenuRenderer Renderer
        {
            get { return renderer; }
        }

        public void action()
        {
            game.State = new Gameplay.GameState(game) ;
            game.closeMenu();
        }
    }

    class ContinueOption : IMenuOption
    {
        Game1 game;
        IMenuRenderer renderer;
        public ContinueOption(Game1 game)
        {
            this.game = game;
            renderer = new TextMenuRenderer(game, "LeafyStencil", "RESUME");
        }

        public IMenuRenderer Renderer
        {
            get { return renderer; }
        }

        public void action()
        {
            //game.State = new Gameplay.GameState(game) ;
            game.closeMenu();
        }
    }
}
