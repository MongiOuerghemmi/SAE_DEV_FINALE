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
using Microsoft.Xna.Framework.Input;



namespace SAE_DEV
{
    public class Foret : GameScreen
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
        private Game1 _game;
        private Zombie [] _zombie;
        private Texture2D _dialogueForet;
        private bool dansLaForet;
        private Vector2 spawnForet = new Vector2(64, 288);
        private Vector2 spawnApresPont = new Vector2(913, 201);
        private Vector2 spawnPerso;
        private bool _afficherDialogue;

        public Foret(Game1 game,bool dansLePont) : base(game) 
        { 
            _game = game;
            if (dansLePont) spawnPerso = spawnApresPont;
            else spawnPerso = spawnForet;
        }
    
        public override void Initialize()
        {
            _zombie = new Zombie[1];
            _personage = new Personage(spawnPerso);
            for (int i = 0; i < _zombie.Length; i++)
            {
                _zombie[i] = new Zombie(new Vector2(272*SCALE,200));
                _zombie[i].Initialize(Game);
                //Console.WriteLine(_zombies[i]._positionzombie);

            }
            _personage.Initialize(Game);
           
            
        }

        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("foret");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("contact");
            mapLayer1 = _tiledMap.GetLayer<TiledMapTileLayer>("village");
            mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("pont");
            _dialogueForet = Content.Load<Texture2D>("Forret");
            base.LoadContent();
            _personage.LoadContenent(Game);
            for (int i = 0; i < _zombie.Length; i++)
            {
                _zombie[i].LoadContent(Game);
            }
            tileMapMatrix = Matrix.CreateScale(SCALE);

            _afficherDialogue = !Variables.dialogueZombie;
        }

        public override void Update(GameTime gameTime)
        {
            
            _tiledMapRenderer.Update(gameTime);
            _personage.Update(gameTime, mapLayer);
            _personage.PerteVieZombie(_zombie);
            if (_personage._positionPerso.Y < 210) {

                for (int i = 0; i < _zombie.Length; i++)
                {
                    _zombie[i]?.Update(mapLayer, _personage, _game.deltaTime, _zombie, i);
                   
                }
                _personage.ToucheParZombie(_zombie);
                 


            }
            if (_personage.IsCollision(_personage.posFuture,mapLayer1,_game.tileWidth))
                _game.LoadScreen5();
            if (_personage.IsCollision(_personage.posFuture,mapLayer2,_game.tileWidth))
                _game.LoadScreen9(false);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);

            _tiledMapRenderer.Draw(viewMatrix: tileMapMatrix);
            
            Game.SpriteBatch.Begin();
            _personage.Draw(Game.SpriteBatch);
            for (int i = 0; i < _zombie.Length; i++)
            {
                _zombie[i]?.Draw(Game.SpriteBatch);
            }
            if(_personage._positionPerso.Y <210 && _afficherDialogue)
            {
                _game.SpriteBatch.Draw(_dialogueForet, new Vector2(320, 500), Color.White);
                Variables.dialogueZombie = true;
            }
            
            Game.SpriteBatch.End();
        }

}
}

