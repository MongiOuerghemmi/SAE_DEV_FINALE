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
    public class Chateau : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public TiledMapTileLayer mapLayer;
        private TiledMapTileLayer mapLayer1;
        private TiledMapTileLayer mapLayer2;
        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;
        public Matrix tileMapMatrix;
        public const float SCALE = 2;
        private Personage _personage;
        private Zombie[] _zombies;
        private Game1 _game;
        private Random random=new Random();
        private bool dansLeChateau;
        
        public Chateau(Game1 game) : base(game) { _game = game; }
        public override void Initialize()
        {
           
            
            _zombies=new Zombie[10];
            _personage = new Personage(new Vector2(34, 112 * SCALE));
            for(int i = 0; i < _zombies.Length; i++)
            {
                _zombies[i] = new Zombie(new Vector2(random.Next(144 * 2, 480 * 2), random.Next(80 * 2, 214 * 2)));
                _zombies[i].Initialize(Game);
                //Console.WriteLine(_zombies[i]._positionzombie);
               
            }
            _personage.Initialize(Game);
          


        }

        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("chateau");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("contact");
            mapLayer1 = _tiledMap.GetLayer<TiledMapTileLayer>("foret");
            mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("entree");

            base.LoadContent();
            _personage.LoadContenent(Game);
            for (int i = 0; i < _zombies.Length; i++)
            {
                _zombies[i].LoadContent(Game);
            }
            tileMapMatrix = Matrix.CreateScale(SCALE);


        }

        public override void Update(GameTime gameTime)
        {
            

            _tiledMapRenderer.Update(gameTime);
            _personage.Update(gameTime, mapLayer);
            _personage.PerteVieZombie(_zombies);

            if (_personage.IsCollision(_personage.posFuture, mapLayer1, _game.tileWidth))
            {
               
                _game.LoadScreen9(true);
            }

            if (_personage.IsCollision(_personage.posFuture, mapLayer2, _game.tileWidth))
                _game.LoadScreen8();
            for (int i = 0; i < _zombies.Length; i++)
            {
                _zombies[i]?.Update(mapLayer,_personage,_game.deltaTime, _zombies, i);
                
            }
            _personage.ToucheParZombie(_zombies);
              


        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw(viewMatrix: tileMapMatrix);
            Game.SpriteBatch.Begin();
            _personage.Draw(Game.SpriteBatch);
            for (int i = 0; i < _zombies.Length; i++)
            {
                _zombies[i]?.Draw(Game.SpriteBatch);
            }
            Game.SpriteBatch.End();
        }

    }
}
