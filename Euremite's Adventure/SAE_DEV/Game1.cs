using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Sprites;
using System.Collections.Generic;
using SAE_DEV;
using MonoGame.Extended.Tiled;
using System.Windows.Input;

namespace SAE_DEV
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch { get; private set; }
        public readonly ScreenManager _screenManager;
        public const int TAILLE_FENETRE_X = 960;
        public const int TAILLE_FENETRE_Y = 588;
        public int tileWidth = 16;
        public float deltaTime;
        public const float Scale = 2;
        public enum Etats {Menu};
        private Etats etat;


        public Etats Etat
        {
            get
            {
                return this.etat;
            }

            set
            {
                this.etat = value;
            }
        }





        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //_graphics.IsFullScreen = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

           
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = TAILLE_FENETRE_X;
            _graphics.PreferredBackBufferHeight =TAILLE_FENETRE_Y;

            _graphics.ApplyChanges();
            LoadScreen1();

            base.Initialize();
           
            
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
             deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                if (this.Etat == Etats.Menu)
                    LoadScreen2();
            }
            base.Update(gameTime);
        }
        public void LoadScreen1()
        {
            _screenManager.LoadScreen(new Intro(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadScreen2()
        {
            _screenManager.LoadScreen(new Menu(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadScreen3()
        {
            _screenManager.LoadScreen(new Debut(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadScreen4(bool foret)
        {
            _screenManager.LoadScreen(new Foret(this,foret), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadScreen5()
        {
            _screenManager.LoadScreen(new Village(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadScreen6()
        {
            _screenManager.LoadScreen(new Chateau(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadScreen7(bool depuisDehors)
        {
            _screenManager.LoadScreen(new Maison(this, depuisDehors), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadScreen8()
        {
            _screenManager.LoadScreen(new InterieurC(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadScreen9(bool chateau)
        {
            _screenManager.LoadScreen(new Pont(this,chateau), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadScreen10()
        {
            _screenManager.LoadScreen(new GameOver(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadScreen11()
        {
            _screenManager.LoadScreen(new Win(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadScreen12()
        {
            _screenManager.LoadScreen(new Option(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadScreen13()
        {
            _screenManager.LoadScreen(new UnvsUn(this), new FadeTransition(GraphicsDevice, Color.Black));
        }






        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
           
            base.Draw(gameTime);
        }
        //public static void GetMapLayers(out TiledMapLayer contact)
    }
}