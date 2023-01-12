using MonoGame.Extended.Sprites;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics.Metrics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Media;
namespace SAE_DEV
{
    public class Personage
    {
        public AnimatedSprite _arme;
        private AnimatedSprite _perso;
        private AnimatedSprite _barrevieperso;
        public Vector2 _positionPerso;
        private int _vitessePerso;
        public int _viePerso;
        public Vector2 posFuture;
        private Game1 _game;
        private Vector2 _scaleArme;
        private float _damagetimer;
        public Vector2 _positionArme;
        public bool _armeLancer;
        public bool _armeLancerDroite;
        public bool _armeLancerGauche;
        public bool _armeLancerHaut;
        public bool _armeLancerBas;

        public bool _armeLancerDroiteT;
        public bool _armeLancerGaucheT;
        public bool _armeLancerHautT;
        public bool _armeLancerBasT;
        

        public const float SCALE = 2;
        private Vector2 _scaleperso;
        
        private float _vitesseArme;

        public Personage(Vector2 position)
        {
            _positionPerso = position;
            
        }

        public void Initialize(Game game)
        {
            _vitessePerso = 3;
            _scaleperso = new Vector2((SCALE - 1), (float)(SCALE -1));
            _viePerso = Variables.vie;
            _game = (Game1)game;
            _scaleArme = _scaleperso;
            _positionArme = _positionPerso;
            _vitesseArme = 4;
           
        


        }
        public void LoadContenent(Game game)
        {
            SpriteSheet spriteSheet = game.Content.Load<SpriteSheet>("PersoAnimation.sf", new JsonContentLoader());
           SpriteSheet spriteSheet1=game.Content.Load<SpriteSheet>("HealthBar.sf",new JsonContentLoader());
            SpriteSheet spriteSheetArme = game.Content.Load<SpriteSheet>("ArmeAnimation.sf", new JsonContentLoader());
            _arme = new AnimatedSprite(spriteSheetArme);
            _perso = new AnimatedSprite(spriteSheet);
            
            _barrevieperso = new AnimatedSprite(spriteSheet1);   
            MediaPlayer.Stop();
            



        }
        public void Update(GameTime gameTime, TiledMapTileLayer maplayer)
        {
            Vector2 posFutureArme = _positionArme;
            Console.WriteLine(_viePerso);
           


            
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_damagetimer > 0)
                {
                    _damagetimer -= deltaTime;
                }


               


                posFuture = _positionPerso;

                if (Keyboard.GetState().IsKeyUp(Keys.F) && _armeLancer == false)
                {
                    _arme.Color = Color.Transparent;
                    _positionArme = _positionPerso;
                    posFutureArme = _positionArme;

                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {

                    posFuture += new Vector2(0, _vitessePerso);
                    _perso.Play("walkingDown");
                    _armeLancerBasT = true;
                    _armeLancerHautT = false;
                    _armeLancerDroiteT = false;
                    _armeLancerGaucheT = false;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {

                    posFuture += new Vector2(0, -_vitessePerso);
                    _perso.Play("walkingUp");
                    _armeLancerHautT = true;
                    _armeLancerDroiteT = false;
                    _armeLancerBasT = false;
                    _armeLancerGaucheT = false;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {

                    posFuture += new Vector2(-_vitessePerso, 0);
                    _perso.Play("walkingLeft");

                    _armeLancerGaucheT = true;
                    _armeLancerDroiteT = false;
                    _armeLancerHautT = false;
                    _armeLancerBasT = false;



                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {

                    posFuture += new Vector2(_vitessePerso, 0);
                    _perso.Play("walkingRight");
                    _armeLancerDroiteT = true;
                    _armeLancerGaucheT = false;
                    _armeLancerHautT = false;
                    _armeLancerBasT = false;


                }
                else
                    _perso.Play("idle");

                Console.WriteLine(_positionPerso);


                if (Keyboard.GetState().IsKeyDown(Keys.F))
                {
                    _arme.Color = Color.White;
                    _armeLancer = true;
                }

                if (_armeLancer == true) // si l'arme est lancé
                {
                    _arme.Play("animation0");
                    _arme.Update(gameTime);

                    if (_armeLancerDroite == true)
                    {
                        posFutureArme += new Vector2(_vitesseArme, 0);

                    }
                    //gauche
                    else if (_armeLancerGauche == true)
                    {
                        posFutureArme += new Vector2(-_vitesseArme, 0);

                    }
                    //haut
                    else if (_armeLancerHaut == true)
                    {
                        posFutureArme += new Vector2(0, -_vitesseArme);

                    }
                    //bas
                    else if (_armeLancerBas == true)
                    {
                        posFutureArme += new Vector2(0, _vitesseArme);

                    }
                }
                else // si F n'est pas appuyer/l'arme n'est pas lancé
                {
                    _armeLancerDroite = _armeLancerDroiteT;
                    _armeLancerGauche = _armeLancerGaucheT;
                    _armeLancerHaut = _armeLancerHautT;
                    _armeLancerBas = _armeLancerBasT;
                }
                if (!IsCollision(posFutureArme, maplayer, 16))
                {
                    _positionArme = posFutureArme;
                }
                else
                {
                    _positionArme = _positionPerso;
                    _arme.Color = Color.Transparent;
                    _armeLancer = false;

                    //dernier mouvement enrengistrer temporaire
                    _armeLancerDroite = _armeLancerDroiteT;
                    _armeLancerGauche = _armeLancerGaucheT;
                    _armeLancerHaut = _armeLancerHautT;
                    _armeLancerBas = _armeLancerBasT;
                }


                if (!IsCollision(posFuture, maplayer, 16))
                {
                    _positionPerso = posFuture;
                }

                DetectionDeVie();
                _barrevieperso.Update(deltaTime);

                _perso.Update(deltaTime); // time écoul

                Variables.vie = _viePerso;
            }
            
            
            
            
        
        public void Draw(SpriteBatch _spriteBatch)
        {
            
            _spriteBatch.Draw(_perso,_positionPerso,0,_scaleperso);
            _spriteBatch.Draw(_barrevieperso, new Vector2(50, 40)) ;
            _spriteBatch.Draw(_arme, _positionArme, 0, _scaleArme);
        }


        

        public bool IsCollision(Vector2 position,TiledMapTileLayer mapLayer,int taille)
        {
            ushort posx = (ushort)(position.X / taille / SCALE);
            ushort posy = (ushort)(position.Y / taille / SCALE);
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(posx, posy, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
        public RectangleF GetHitbox()
        {
            return GetHitbox(_positionPerso);
        }
        public RectangleF GetHitboxArme()
        {
            return GetHitbox(_positionArme);
        }

        public RectangleF GetHitbox(Vector2 pos)
        {
            return new RectangleF(pos, new Vector2(24,32));
        }
        public RectangleF GetHitboxArme(Vector2 pos)
        {
            return new RectangleF(pos, new Vector2(13, 15));
        }

        public bool IntersectionZombie(Zombie[] zombie)
        {
            for (int i = 0; i < zombie.Length; i++)
            {
                if (!zombie[i].estMort && GetHitbox().Intersects(zombie[i].GetHitbox()))
                {
                    return true;
                }
            }
            return false;
        }
        public void ToucheParZombie(Zombie[] zombie)
        {
            if (IntersectionZombie(zombie)&&_damagetimer<=0)
            {
                _viePerso -= 20;
                _damagetimer = 1;
            }
            
        }
        public void DetectionDeVie()
        {
            if (_viePerso == 120)
                _barrevieperso.Play("vie5/5");
            if (_viePerso == 80)
                _barrevieperso.Play("vie4/5");
            else if (_viePerso == 60)
                _barrevieperso.Play("vie3/5");
            else if (_viePerso == 40)
                _barrevieperso.Play("vie2/5");
            else if (_viePerso == 20)
                _barrevieperso.Play("vie1/5");

            if (_viePerso <= 0)
            {
                _barrevieperso.Play("vie0/5");
                _game.LoadScreen10();
                _viePerso = 120;
            }
        }
    
    public bool CollisionArmeZombie(Zombie zombie)
        {
            return !zombie.estMort && _armeLancer && GetHitboxArme().Intersects(zombie.GetHitbox());
        }
        public void PerteVieZombie(Zombie[]zombies)
        {
            for(int i = 0; i < zombies.Length; i++) { 
                if (CollisionArmeZombie(zombies[i]))
                {
                     zombies[i].viezombie -= 10; 
                    _positionArme = _positionPerso;
                    _armeLancer = false;
                    Console.WriteLine("touche");
                }
            }
        }
         





}
}