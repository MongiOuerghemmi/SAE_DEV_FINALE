using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV
{
    public class Zombie
    {
        private AnimatedSprite _zombie;
        public Vector2 _positionzombie;
        public int viezombie;
        public const float SCALE = 2;
        private int _zombievitesse = 35;
        private int sens_deplacement_zombie_horizontale;
        private int sens_deplacemet_zombie_verticale;
        public bool estMort=false;


        public Zombie(Vector2 position)
        {
            _positionzombie = position;
            

        }

        public void Initialize(Game game)
        {

            viezombie = 20;

        }

        public void LoadContent(Game game)
        {
            SpriteSheet spriteSheet = game.Content.Load<SpriteSheet>("ZombieAnimation.sf", new JsonContentLoader());
            _zombie = new AnimatedSprite(spriteSheet);
            _zombie.Play("idle");
        }
        public void Update(TiledMapTileLayer maplayer,Personage perso,float deltaTime, Zombie[] zombies, int zombiePos)
        {
            if (viezombie <= 0)
            {
                estMort = true;
                return;
            }
               

               Suivre(perso,deltaTime,maplayer,16, zombies, zombiePos);
            Console.WriteLine(viezombie);
            
        }
       

        public void Suivre(Personage perso,float deltaTime, TiledMapTileLayer mapLayer, int taille, Zombie[] zombies, int zombiePos) {
            sens_deplacement_zombie_horizontale = 0;
            sens_deplacemet_zombie_verticale = 0;
            Vector2 posFuture = _positionzombie;


            if (perso._positionPerso.X == _positionzombie.X)
            {
                sens_deplacement_zombie_horizontale = 0;
              

            }
            else if (perso._positionPerso.X > _positionzombie.X)
            {
                sens_deplacement_zombie_horizontale = 1;

            }
            else if (perso._positionPerso.X < _positionzombie.X)
            {
                sens_deplacement_zombie_horizontale = -1;

            }

            if (perso._positionPerso.Y == _positionzombie.Y)
            {
                sens_deplacemet_zombie_verticale = 0;

            }
            else if (perso._positionPerso.Y > _positionzombie.Y)
            {
                sens_deplacemet_zombie_verticale = 1;

            }
            else if (perso._positionPerso.Y < _positionzombie.Y)
            {
                sens_deplacemet_zombie_verticale = -1;

            }

            posFuture.X += sens_deplacement_zombie_horizontale * _zombievitesse * deltaTime;
            posFuture.Y += sens_deplacemet_zombie_verticale * _zombievitesse * deltaTime;
            posFuture.X = MathF.Round(posFuture.X);
            posFuture.Y = MathF.Round(posFuture.Y);

            // Test collision zombie
            for(int j = 0; j < zombies.Length; j++)
            {
                if (j == zombiePos) continue;
                if (GetHitbox(posFuture).Intersects(zombies[j].GetHitbox())) return;
            }

            if (!IsCollision(posFuture,mapLayer, 16))
            {
                _positionzombie = posFuture;
            }

        }

        public RectangleF GetHitbox()
        {
            return GetHitbox(_positionzombie);
        }

        public RectangleF GetHitbox(Vector2 pos)
        {
            return new RectangleF(pos, new Vector2(32));
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            //controler si il est mort
           if (estMort)
               return;
           _spriteBatch.Draw(_zombie,_positionzombie);
             
        }
        public bool IsCollision(Vector2 position, TiledMapTileLayer mapLayer, int taille)
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

    }
}
