using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Tiled;

namespace SAE_DEV
{
    public class Boss
    {
        public int _vieBoss;
        private Vector2 _positionBoss;
        private AnimatedSprite _boss;
        private AnimatedSprite _fireBall;
        public int _vitesseFireBall = 10;
        private Vector2 _scaleFireBall;
        private Vector2 _positionFireBall;
        public bool _fireBallLancer;
        public int _tempsAttaque = 3;
        private float _chrono = 0;
        private int _sensBoss;
        private int _vitesseBoss;
        public bool _fireBallLancerDroite;
        public bool _fireBallLancerGauche;
        public bool _fireBallLancerHaut;
        public bool _fireBallLancerBas;
        private AnimatedSprite _barrevieBoss;
        public bool _fireBallLancerDroiteT;
        public bool _fireBallLancerGaucheT;
        public bool _fireBallLancerHautT;
        public bool _fireBallLancerBasT;
        private Game1 _game;
        private Vector2 _positionBarre;

        public Boss(Vector2 positionBoss)
        {
            _positionBoss = positionBoss;
        }
        

        public void Initialize(Game game)
        {
            
            _vieBoss = 120;
            _fireBallLancer = false;
            
            _positionFireBall = new Vector2(250, 250);
            _scaleFireBall = new Vector2(2);
            _sensBoss = 1;
            _vitesseBoss = 2;
            _positionBarre = new Vector2(900, 40);
            _game=(Game1)game;

        }

        public void LoadContent(Game game)
        {
            SpriteSheet spriteSheet = game.Content.Load<SpriteSheet>("Boss.sf", new JsonContentLoader());
            SpriteSheet spritesheet=game.Content.Load<SpriteSheet>("FireballAnimation.sf",new JsonContentLoader());
            _fireBall = new AnimatedSprite(spritesheet);
            SpriteSheet spriteSheet1 = game.Content.Load<SpriteSheet>("HealthBar.sf", new JsonContentLoader());
            _barrevieBoss = new AnimatedSprite(spriteSheet1);
            _boss = new AnimatedSprite(spriteSheet);
        }
        public void Update(GameTime gameTime,Personage perso,TiledMapTileLayer maplayer,Game1 game)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            DetectionDeVie();
            KeyboardState _keyboardState = Keyboard.GetState();

            Vector2 posFutureBoss = _positionBoss;
            Vector2 posFutureFireBall = _positionFireBall;

            // chrono
            _chrono = _chrono + deltaTime;

            if ((int)_chrono == _tempsAttaque)
            {
                _fireBallLancer = true;
                _fireBall.Color = Color.White;
                _boss.Play("walkingDown");
            }
            else if ((int)_chrono != _tempsAttaque && _fireBallLancer == false)
            {
                _fireBall.Color = Color.Transparent;
                _positionFireBall = _positionBoss;
                posFutureFireBall = _positionFireBall;
            }


            if (_fireBallLancer == true) // si fireball est lancé
            {
                _fireBall.Update(gameTime);

                posFutureFireBall += Vector2.Normalize(perso._positionPerso - _positionBoss) * _vitesseFireBall;
            }
            //collision Boss
            Console.WriteLine(_fireBallLancer);
            
            if (!perso.IsCollision(posFutureBoss, maplayer,game.tileWidth))
            {
                _positionBoss = posFutureBoss;
            }

            //collision FireBall

            Console.WriteLine(_vieBoss);
            if (!perso.IsCollision(posFutureFireBall, maplayer,game.tileWidth))
            {
                _positionFireBall = posFutureFireBall;
            }
            else if (perso.IsCollision(posFutureFireBall, maplayer,game.tileWidth) && _fireBallLancer == true)
            {
                _positionFireBall = _positionBoss;
                _fireBallLancer = false;
                _chrono = 0;
            }


            _positionBoss.X += _vitesseBoss * _sensBoss;

            if (_positionBoss.X < 404)
                _sensBoss *= -1;
            if (_positionBoss.X > 544)
                _sensBoss *= -1;
            // collision arme sur le boss
           
            CollisionFireball(perso);
            CollisionArmeBoss(perso);
            _barrevieBoss.Update(deltaTime);

        }

        public void CollisionFireball(Personage perso)
        {
            if (_positionFireBall.X > perso._positionPerso.X - 16 && _positionFireBall.X < perso._positionPerso.X + 16 && _positionFireBall.Y > perso._positionPerso.Y - 16 && _positionFireBall.Y < perso._positionPerso.Y + 16 && _fireBallLancer == true)
            {
                perso._viePerso -= 20;
                _positionFireBall = _positionBoss;
                _fireBallLancer = false;
                _chrono = 0;
            }
        }


        public void CollisionArmeBoss(Personage perso)
        {
            if (perso._positionArme.X > _positionBoss.X - 16 && perso._positionArme.X < _positionBoss.X + 16 && perso._positionArme.Y > _positionBoss.Y - 16 && perso._positionArme.Y < _positionBoss.Y + 16 && perso._armeLancer == true)
            {
                _vieBoss -= 20;
                perso._positionArme = perso._positionPerso;
                perso._arme.Color = Color.Transparent;
                perso._armeLancer = false;
            }
        }
        public void modalite1v1(GameTime gameTime,TiledMapTileLayer mapLayer,int tileWidth,Personage perso)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 posFutureBoss = _positionBoss;
            Vector2 posFutureFireBall = _positionFireBall;

            if (Keyboard.GetState().IsKeyUp(Keys.F) && _fireBallLancer == false)
            {
                _fireBall.Color = Color.Transparent;
                _positionFireBall = _positionBoss;
                posFutureFireBall = _positionFireBall;
            }
            DetectionDeVie();
            CollisionFireball(perso);
            _barrevieBoss.Update(deltaTime);
            CollisionArmeBoss(perso);
            _boss.Update(deltaTime);
            _fireBall.Update(deltaTime);

            // S Bas
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {

                posFutureBoss += new Vector2(0, _vitesseBoss);
                _boss.Play("walkingDown");

                _fireBallLancerBasT = true;
                _fireBallLancerDroiteT = false;
                _fireBallLancerGaucheT = false;
                _fireBallLancerHautT = false;
            }
            // Z Haut
            else if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {

                posFutureBoss += new Vector2(0, -_vitesseBoss);
                _boss.Play("walkingUp");

                _fireBallLancerHautT = true;
                _fireBallLancerDroiteT = false;
                _fireBallLancerGaucheT = false;
                _fireBallLancerBasT = false;

            }
            // Q Gauhe
            else if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {

                posFutureBoss += new Vector2(-_vitesseBoss, 0);
                _boss.Play("walkingLeft");

                _fireBallLancerGaucheT = true;
                _fireBallLancerDroiteT = false;
                _fireBallLancerHautT = false;
                _fireBallLancerBasT = false;



            }
            // D Droite
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {

                posFutureBoss += new Vector2(_vitesseBoss, 0);
                _boss.Play("walkingRight");
                _fireBallLancerDroiteT = true;
                _fireBallLancerGaucheT = false;
                _fireBallLancerHautT = false;
                _fireBallLancerBasT = false;


            }
            else
                _boss.Play("idle");
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _fireBall.Color = Color.White;
                _fireBallLancer = true;
            }

            if (_fireBallLancer == true) // si l'arme est lancé
            {
                _fireBall.Update(gameTime);

                if (_fireBallLancerDroite == true)
                {
                    posFutureFireBall += new Vector2(_vitesseFireBall, 0);
                    _fireBall.Play("lancerDroit");
                }
                //gauche
                else if (_fireBallLancerGauche == true)
                {
                    posFutureFireBall += new Vector2(-_vitesseFireBall, 0);
                    _fireBall.Play("lancerGauche");
                }
                //haut
                else if (_fireBallLancerHaut == true)
                {
                    posFutureFireBall += new Vector2(0, -_vitesseFireBall);
                    _fireBall.Play("lancerHaut");
                }
                //bas
                else if (_fireBallLancerBas == true)
                {
                    posFutureFireBall += new Vector2(0, _vitesseFireBall);
                    _fireBall.Play("lancerBas");
                }
            }
            else // si F n'est pas appuyer/l'arme n'est pas lancé
            {
                _fireBallLancerDroite = _fireBallLancerDroiteT;
                _fireBallLancerGauche = _fireBallLancerGaucheT;
                _fireBallLancerHaut = _fireBallLancerHautT;
                _fireBallLancerBas = _fireBallLancerBasT;

            }


            if (!IsCollision(posFutureFireBall, mapLayer,tileWidth))
            {
                _positionFireBall = posFutureFireBall;
            }
            else
            {
                _positionFireBall = _positionBoss;
                _fireBall.Color = Color.Transparent;
                _fireBallLancer = false;

                //dernier mouvement enrengistrer temporaire
                _fireBallLancerDroite = _fireBallLancerDroiteT;
                _fireBallLancerGauche = _fireBallLancerGaucheT;
                _fireBallLancerHaut = _fireBallLancerHautT;
                _fireBallLancerBas = _fireBallLancerBasT;
            }

            //Collision
            if (!IsCollision(posFutureBoss, mapLayer,tileWidth))
            {
                _positionBoss = posFutureBoss;
            }
        }

        public bool IsCollision(Vector2 position, TiledMapTileLayer mapLayer, int taille)
        {
            ushort posx = (ushort)(position.X / taille / 2);
            ushort posy = (ushort)(position.Y / taille / 2);
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(posx, posy, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }

        public void DetectionDeVie()
        {
            if (_vieBoss == 120)
                _barrevieBoss.Play("vie5/5");
            else if (_vieBoss == 80)
                _barrevieBoss.Play("vie4/5");
            else if (_vieBoss == 60)
                _barrevieBoss.Play("vie3/5");
            else if (_vieBoss == 40)
                _barrevieBoss.Play("vie2/5"); 
             else if (_vieBoss == 20)
                _barrevieBoss.Play("vie1/5");

            if (_vieBoss <= 0)
            {
                _barrevieBoss.Play("vie0/5");

                _game.LoadScreen11();
                _vieBoss = 120;
            }
        }


        public void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(_boss, _positionBoss, 0); //-------------------------
            _spriteBatch.Draw(_fireBall, _positionFireBall, 0,_scaleFireBall);
            _spriteBatch.Draw(_barrevieBoss, _positionBarre, 0);
        }


        
    }
}
