using Microsoft.Xna.Framework;
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
    public class Pont : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public TiledMapTileLayer mapLayer;
        private TiledMapTileLayer mapLayer1;
        private TiledMapTileLayer mapLayer2;
        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;
        public Matrix tileMapMatrix;
        private Personage _personage;
        private Game1 _game;
        public const float SCALE = 2;
        private Vector2 spawnpont = new Vector2(35, 259);
        private Vector2 spawnChateau = new Vector2( 899,259);
        private Vector2 spawnPerso;

        public Pont(Game1 game, bool depuisDehors) : base(game) {
            _game = game;
            if (depuisDehors) spawnPerso = spawnChateau;
            else spawnPerso = spawnpont;


        }
        public override void Initialize()
        {

            _personage = new Personage(spawnPerso);

            _personage.Initialize(Game);

            

        }

        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("lac");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("contact");
            mapLayer1 = _tiledMap.GetLayer<TiledMapTileLayer>("chateau");
            mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("foret");
            base.LoadContent();
            _personage.LoadContenent(Game);
            tileMapMatrix = Matrix.CreateScale(SCALE);


        }

        public override void Update(GameTime gameTime)
        {

            if (_personage.IsCollision(_personage.posFuture, mapLayer2, _game.tileWidth))
            {
                _game.LoadScreen4(true);
            }
            if (_personage.IsCollision(_personage.posFuture, mapLayer1, _game.tileWidth))
            {
                _game.LoadScreen6();
            }

            _tiledMapRenderer.Update(gameTime);
            _personage.Update(gameTime, mapLayer);
            
           


        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw(viewMatrix: tileMapMatrix);
            Game.SpriteBatch.Begin();
            _personage.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
        }

    }
}
