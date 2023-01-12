using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV
{
    public class GameOver : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        private Texture2D _logo;

        private Vector2 _position = new Vector2(0, 0);
        private float _timer = 2.5f;
        private Song _song;
        public GameOver(Game1 game) : base(game) { }

        public override void LoadContent()
        {
            base.LoadContent();

            _logo = Game.Content.Load<Texture2D>("GameOver");
            _song = Game.Content.Load<Song>("GamOver");
            MediaPlayer.Play(_song);
            MediaPlayer.Volume = 1f;
        }

        public override void Update(GameTime gameTime)
        {
            if (_timer > 0)
            {
                float deltatime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                _timer -= deltatime;

                if (_timer <= 0)
                {
                    Game.LoadScreen2();
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(_logo, _position, Color.White);

            Game.SpriteBatch.End();
        }
    }
}
