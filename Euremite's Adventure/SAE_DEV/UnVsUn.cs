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
    public class UnvsUn : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private Personage _perso;
        private Texture2D _logo;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer maplayer;
        private Vector2 _position = new Vector2(169, 271);
        private Boss _boss;
        private Matrix tileMapMatrix;
        private Game1 _game;
        public UnvsUn(Game1 game) : base(game) { _game = game; }

        public override void Initialize()
        {
          
            base.Initialize();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _perso = new Personage(_position);
            _perso.Initialize(Game);
            _boss = new Boss(new Vector2(814,271));
            _boss.Initialize(Game);
            tileMapMatrix = Matrix.CreateScale(2);
        }

        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("1v1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            maplayer = _tiledMap.GetLayer<TiledMapTileLayer>("contact");
            _perso.LoadContenent(Game);
            _boss.LoadContent(Game);
            Console.WriteLine(_boss._vieBoss);
            
            



            base.LoadContent();

        }

        public override void Update(GameTime gameTime)
        {
            _perso.Update(gameTime,maplayer);
            _boss.modalite1v1(gameTime, maplayer, _game.tileWidth,_perso);
            
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw(viewMatrix: tileMapMatrix);
            Game.SpriteBatch.Begin();
            _perso.Draw(Game.SpriteBatch);

            _boss.Draw(Game.SpriteBatch);

            Game.SpriteBatch.End();
        }
    }
}
