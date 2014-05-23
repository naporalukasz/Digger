using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Digger
{
    /// <summary>
    /// klasa odpowiedzialna ze tworzenie, uaktualnianie i rysowanie pociskow.
    /// </summary>
    class Bullet:IGameObject
    {
        Vector2 position;
        int speed;
        bool draw;
        int weapon;
        int x = 0, y = 0;
        List<int> road;
        
        /// <summary>
        /// Tworzy nowy obiekt typu Bullet.
        /// </summary>
        /// <param name="startPosition">Pozycja startowa dla Bullet.</param>
        /// <param name="type">Określa rodzaj Bullet, dostepne sa dwa typy.</param>
        public Bullet(Vector2 startPosition, int type)
        {
            position = startPosition;
            position.Y = startPosition.Y + 32;
            speed = 3;
            weapon = type;
            draw = true;
            road = new List<int>();
        }

        /// <summary>
        /// Uaktualnia stan Bullet,sprawdza kolizje z przeciwnikami i przeszkodami.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {       
            x = (int)(position.X )/ 48;
            y = (int)(position.Y -32)/ 48;
            if (weapon == 1)
            {
                if (x < 17 && y < 13)
                {
                    if (Map.map[x, y].isEmpty == StateField.empty && weapon == 1)
                    {
                        position.X += speed;
                    }
                    else if (weapon == 2)
                        position.X += speed;
                    else
                        draw = false;
                }
            }
            else if (weapon == 2)
            {
                while (x <= 16)
                {
                    road.Add((int)x * 48);
                    x++;
                }
            }
            if (weapon == 1)
                checkHit();
            else 
                checHit2();
        }

        /// <summary>
        /// Funkcja sprawdza czy pocisk drugiego typu trafil w przeciwnika, przeszkode. Dodaje punkty za trafienia.
        /// </summary>
        private void checHit2()
        {
            int ile = Level.enemy.Count();
            int iles = Level.enemySuper.Count();
            Level.enemy.RemoveAll(delegate(Robak en)
            {
                return (en.x == x );

            });
            Level.enemySuper.RemoveAll(delegate(SuperRobak en)
            {
                return (en.x == x );

            });
            if (ile != Level.enemy.Count() && draw)
            {

                Level.points += (100*(ile-Level.enemy.Count()));
                draw = false;
            }
            else if (iles != Level.enemySuper.Count() && draw)
            {
                Level.points += (200*(iles-Level.enemySuper.Count()));
                draw = false;
            }
        }

        /// <summary>
        /// Funkcja sprawdza czy pocisk pierwszego typu trafil w przeciwnika, przeszkode. Dodaje punkty za trafienia.
        /// </summary>
        private void checkHit()
        {
            int ile = Level.enemy.Count();
            int iles = Level.enemySuper.Count();
            Level.enemy.RemoveAll(delegate(Robak en)
            {
                return (en.x == x && en.y == y);

            });
            Level.enemySuper.RemoveAll(delegate(SuperRobak en)
            {
                return (en.x == x);// && en.y == y);

            });
            if (ile != Level.enemy.Count() && draw)
            {
                Level.points += 100;
                draw = false;
            }
            else if (iles != Level.enemySuper.Count() && draw)
            {
                Level.points += 200;
                draw= false;
            }
        }

        /// <summary>
        /// Rysuje Bullet na planszy.
        /// </summary>
        /// <param name="spriteBratch"></param>
        public  void Draw(SpriteBatch spriteBratch)
        {
            if (draw && weapon ==1)
                spriteBratch.Draw(Source.pocisk1, position, Color.White);
            else if (draw && weapon == 2)
            {
                foreach (var el in road)
                    spriteBratch.Draw(Source.pocisk2, new Vector2(el, position.Y), Color.White);
                draw = false;
            }
        }
    }
}
