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
    public class InterieurC : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public TiledMapTileLayer mapLayer;
        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;
        public Matrix tileMapMatrix;
        private Personage _personage;
        private Game1 _game;
        public const float SCALE = 2;
        private Boss _boss;
        

        public InterieurC(Game1 game) : base(game) { _game = game; }
        public override void Initialize()
        {
            
            _personage = new Personage(new Vector2(470,524));
            _boss = new Boss(new Vector2(470, 169));
            _boss.Initialize(Game);
            _personage.Initialize(Game);



        }

        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("interchateau");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("contact");
            base.LoadContent();
            _personage.LoadContenent(Game);
            _boss.LoadContent(Game);
            tileMapMatrix = Matrix.CreateScale(SCALE);


        }

        public override void Update(GameTime gameTime)
        {


            _tiledMapRenderer.Update(gameTime);
            _personage.Update(gameTime, mapLayer);
            _boss.Update(gameTime,_personage,mapLayer,_game);



        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw(viewMatrix: tileMapMatrix);
            Game.SpriteBatch.Begin();
            _personage.Draw(Game.SpriteBatch);
            _boss.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
        }

    }
}
