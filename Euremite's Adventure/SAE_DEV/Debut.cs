using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV
{
    public class Debut : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private Personage _personage;
        private SpriteFont _font;
        public TiledMapTileLayer mapLayer;
        private TiledMapTileLayer mapLayer1;
        private TiledMapTileLayer mapLayer2;
        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;
        public Matrix tileMapMatrix;
        public const float SCALE = 2;
        public Game1 _game;
       
       
        




        public Debut(Game1 game) : base(game) {
            _game = game;
        }

        public override void Initialize()
        {
            _personage = new Personage(new Vector2(560,140));
            _personage.Initialize(Game);
            base.Initialize();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
        }

        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("principale");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _font = Game.Content.Load<SpriteFont>("font");
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Maison");
            mapLayer1 = _tiledMap.GetLayer<TiledMapTileLayer>("maisoneure");
            mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("foret");

            tileMapMatrix = Matrix.CreateScale(SCALE);
           
            _personage.LoadContenent(Game);
           
           
            base.LoadContent();

        }

       


        public override void Update(GameTime gameTime)
        {

            _tiledMapRenderer.Update(gameTime);
            _personage.Update(gameTime,mapLayer);
            if (_personage.IsCollision(_personage.posFuture, mapLayer1, _game.tileWidth)){
                _game.LoadScreen7(true);
            }
            if (_personage.IsCollision(_personage.posFuture, mapLayer2, _game.tileWidth))
            {
                _game.LoadScreen4(false);
            }


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
