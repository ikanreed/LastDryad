using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace LastDryad.UI
{
    class RadialMenu :IMenu
    {
        private List<IMenuOption> options;
        private Vector2 center;
        private int selectedindex;
        private Texture2D highlighter;
        protected Vector2 Center
        {
            get { return center; }
        }
        private float radius;
        protected Game1 game;
        public RadialMenu(Game1 game, Vector2 center, float radius)
        {
            selectedindex = -1;
            this.game = game;
            options = new List<IMenuOption>();
            this.radius = radius;
            this.center = center;
            highlighter = game.Content.Load<Texture2D>("randomstandin");
        }
        public void addOption(IMenuOption option)
        {
            this.options.Add(option);
        }
        public void render()
        {
            float angle = 0.0f;
            int count = 0;
            foreach (IMenuOption option in options)
            {
                IMenuRenderer renderer = option.Renderer;
                
                Vector2 shift = new Vector2( 
                    (float)Math.Cos(angle) * radius, 
                    (float)Math.Sin(angle) * radius);
                Vector2 target = center+shift-(renderer.size/2);
                
                if (count == selectedindex)
                {
                    Vector2 highlighttarget = center + shift - new Vector2(highlighter.Width, highlighter.Height) / 2;
                    game.MenuBatch.Draw(highlighter, highlighttarget, Color.White);
                }
                renderer.render(game.MenuBatch, target);


                angle += (float)(Math.PI * 2 / options.Count);
                count += 1;
            }
        }
        
        public void handleInput(GamePadState state, GameTime time)
        {
            if (!game.InputLocked)
            {
                HandleButtons(state);
            }
            selectedindex=getSelectedIndex(state);
        }

        private void HandleButtons(GamePadState state)
        {
            if (MenuButtonPressed(state))
            {
                game.closeMenu();
            }
            else if (state.IsButtonDown(Buttons.A))
            {
                IMenuOption selected = getSelectedOption(state);
                if (selected != null)
                {
                    selected.action();
                }
                game.closeMenu();
            }
        }

        private IMenuOption getSelectedOption(GamePadState state)
        {
            int index=getSelectedIndex(state);
            if(index !=-1)
                return options[index];
            return null;

        }

        private int getSelectedIndex(GamePadState state)
        {
            int result = -1;
            Vector2 stick = state.ThumbSticks.Left;
            if (stick.Length() > 0.5f)
            {
                
                float angle = (float)(Math.Atan2(-stick.Y , stick.X));
                if (angle < 0)
                {
                    angle = (float)(angle + Math.PI * 2);
                }
                float scaled = angle * options.Count / (float)(Math.PI * 2);
                result = ((int)Math.Round(scaled)) % options.Count;
            }
            return result;
        }

        private static bool MenuButtonPressed(GamePadState state)
        {
            return state.Buttons.X == ButtonState.Pressed ||
                            state.Buttons.Y == ButtonState.Pressed ||
                            state.Buttons.B == ButtonState.Pressed;
        }
    }
}
