using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV
{
    public class Village : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public TiledMapTileLayer mapLayer;
        public TiledMapTileLayer mapLayer1;
        public TiledMap _tiledMap;
        private Texture2D _dialogue;
        public TiledMapRenderer _tiledMapRenderer;
        public Matrix tileMapMatrix;
        public const float SCALE = 2;
        private Personage _personage;
        private Game1 _game;

        public Village(Game1 game) : base(game) { _game = game; }
        public override void Initialize()
        {
            _personage = new Personage(new Vector2(Game1.TAILLE_FENETRE_X - 150, 144 * SCALE));
            _personage.Initialize(Game);
          


        }

        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("principalapres");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Maison");
            mapLayer1 = _tiledMap.GetLayer<TiledMapTileLayer>("foret");
            _dialogue = Content.Load<Texture2D>("Village");
            base.LoadContent();
            _personage.LoadContenent(Game);
            tileMapMatrix = Matrix.CreateScale(SCALE);


        }

        public override void Update(GameTime gameTime)
        {

            _tiledMapRenderer.Update(gameTime);
            _personage.Update(gameTime, mapLayer);
            if (_personage.IsCollision(_personage.posFuture, mapLayer1, _game.tileWidth))
            {
                _game.LoadScreen4(false);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw(viewMatrix: tileMapMatrix);
            Game.SpriteBatch.Begin();
            if (_personage._positionPerso.X <= 780)
            {
                Game.SpriteBatch.Draw(_dialogue,new Vector2(320,500),Color.White);
            }
            _personage.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
        }

    }
}
