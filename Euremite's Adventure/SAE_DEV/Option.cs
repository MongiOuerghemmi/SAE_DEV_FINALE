using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV
{
    public class Option : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        private Texture2D _logo;
        private Game1 _game;

        private Vector2 _position = new Vector2(0, 0);
        private float _timer = 2.5f;
        public Option(Game1 game) : base(game) { 
            _game = game;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _logo = Game.Content.Load<Texture2D>("Commande");
           
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _game.LoadScreen2();
            }  

        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(_logo, _position, Color.White);

            Game.SpriteBatch.End();
        }
    }
}
