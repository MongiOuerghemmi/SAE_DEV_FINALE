using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
    public class Maison : GameScreen
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
      
        private Vector2 spawnMaison = new Vector2(590, 165);
        private Vector2 spawnMaisonPorte = new Vector2(480, 430);
        private Vector2 spawnPerso;
        private Texture2D _dialoguemere;
        
        private Game1 _game;

        public Maison(Game1 game, bool depuisDehors) : base(game)
        {
            _game = game;
            if (depuisDehors) spawnPerso = spawnMaisonPorte;
            else spawnPerso = spawnMaison;
        }
        public override void Initialize()
        {
            Random random = new Random();
            _personage = new Personage(spawnPerso);
           
            Variables.dialogueZombie = false;

        


            _personage.Initialize(Game);



        }

        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("maisoneuremite");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("contact");
            mapLayer1 = _tiledMap.GetLayer<TiledMapTileLayer>("sortie");
            mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("mere");
            _dialoguemere = Content.Load<Texture2D>("Maisson");
          

            
           

            base.LoadContent();
            _personage.LoadContenent(Game);
            _personage._viePerso = 120;
            tileMapMatrix = Matrix.CreateScale(SCALE);


        }

        public override void Update(GameTime gameTime)
        {

       
            _tiledMapRenderer.Update(gameTime);
            if (_personage.IsCollision(_personage._positionPerso, mapLayer1, _game.tileWidth))
                _game.LoadScreen3();

            
            _personage.Update(gameTime, mapLayer);

            Console.WriteLine("salut M.....");

        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw(viewMatrix: tileMapMatrix);
            Game.SpriteBatch.Begin();
            if (_personage.IsCollision(_personage._positionPerso, mapLayer2, _game.tileWidth))
                Game.SpriteBatch.Draw(_dialoguemere, new Vector2(320, 500), Color.White);
                _personage.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
        }

    }
}
