using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;
using System.Collections.Generic;


namespace SAE_DEV
{

    public class Menu : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private Texture2D _logo;
        private SpriteFont _font;
        private Vector2 _position = new Vector2(0, 0);
        private Rectangle [ ] lesBoutons;
        private Texture2D _boutonstart;
        private Texture2D _boutonoption;
        private Texture2D _boutonexit;
        private Texture2D _boutonModeCombat;
        private Game1 _game;
        private Song _song;
       

        public enum Etats {Play,Option,Quit,Combat };
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


        public Menu(Game1 game) : base(game)
        {
            _game = game;
            lesBoutons = new Rectangle[4];
            lesBoutons[0] = new Rectangle(0, 100, 450, 94);
            lesBoutons[1] = new Rectangle(0, 300, 300, 99);
            lesBoutons[3] = new Rectangle(0, 200, 680, 110);
            lesBoutons[2] = new Rectangle(0, 400, 300, 99);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _logo = Game.Content.Load<Texture2D>("Intro");
            _font = Game.Content.Load<SpriteFont>("font");
            _boutonoption= Content.Load<Texture2D>("Commandes");
            _boutonstart = Content.Load<Texture2D>("ModeHistoire");
            _boutonexit = Content.Load<Texture2D>("Exit");
            _boutonModeCombat = Content.Load<Texture2D>("ModeCombat");
            _song = Game.Content.Load<Song>("MenuSong");

            
            MediaPlayer.Play(_song);
           MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 1f;
        }
        public override void UnloadContent()
        {
            
            base.UnloadContent();   
        }
        public override void Update(GameTime gameTime)
        {

            MouseState _mouseState = Mouse.GetState();

            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < lesBoutons.Length; i++)
                {
                    // si le clic correspond à un des 3 boutons
                    if (lesBoutons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        Console.WriteLine(i);
                        // on change l'état défini dans Game1 en fonction du bouton cliqué
                        if (i == 1)
                            Etat = Etats.Option;
                        else if (i == 0)
                            Etat = Etats.Play;
                        else if (i == 3)
                            Etat = Etats.Combat;
                        else
                            Etat = Etats.Quit;
                        break;
                    }

                }
            }

            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                // Attention, l'état a été mis à jour directement par l'écran en question
                if (this.Etat == Etats.Quit)
                    _game.Exit();
                else if (this.Etat == Etats.Play)
                {
                    _game.LoadScreen7(false);
                    
                    MediaPlayer.Stop();
                    
                }
                else if (this.Etat == Etats.Option)
                {
                    _game.LoadScreen12();
                  
                }
                else if(this.Etat == Etats.Combat)
                {
                    _game.LoadScreen13();
                }


            }
        }
       
        public override void Draw(GameTime gameTime)
        {
            _game.GraphicsDevice.Clear(Color.Black);
            _game.SpriteBatch.Begin();
            _game.SpriteBatch.Draw(_logo, _position, Color.White);
            _game.SpriteBatch.Draw(_boutonstart, new Vector2(0, 100), Color.White);
            _game.SpriteBatch.Draw(_boutonoption, new Vector2(0, 300), Color.White);
            _game.SpriteBatch.Draw(_boutonexit, new Vector2(0, 400), Color.White);
            _game.SpriteBatch.Draw(_boutonModeCombat,new Vector2(0, 200),Color.White);

            Game.SpriteBatch.End();
        }

    }
}

