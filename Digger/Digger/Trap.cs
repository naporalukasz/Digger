using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Digger
{
    /// <summary>
    /// Klasa implementuje pulapke
    /// </summary>
    class Trap:IGameObject
    {
        /// <summary>
        /// Pozycja pulapki
        /// </summary>
         public  Vector2 position;
         int speed;
         public bool draw;
         bool act = false;
         int x = 0, y = 0;
        /// <summary>
        /// timer odliczajacy czas do aktywacji pulapki
        /// </summary>
         System.Windows.Forms.Timer trapTimer = new System.Windows.Forms.Timer();

        /// <summary>
        /// Tworzy nowy obiekt typu Trap
        /// </summary>
        /// <param name="startPosition">Pozycja startowa</param>
        public Trap(Vector2 startPosition)
        {
            position = startPosition;
            speed = 2;   
            draw = true;
            trapTimer.Tick += new EventHandler(activate);
            trapTimer.Interval = 2000;
            
        }

        /// <summary>
        /// Uaktualnia pulapke
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {       
            x = (int)(position.X )/ 48;
            y = (int)(position.Y )/ 48;
            if (y >= 13)
            {
                y = 12;
                draw = false;
            }
            if (Map.map[x , y].isEmpty == StateField.empty && !act)
            {              
                trapTimer.Start();
            }
            if (act)
            {
                if (Map.map[x, y ].isEmpty == StateField.empty)
                {              
                    position.Y += speed;
                    checkHit();
                }
                else
                {
                    draw = false;
                    
                    Level.points += 50;
                }
            }
        }
        /// <summary>
        /// uruchamia pulapke
        /// </summary>
        /// <param name="myObject"></param>
        /// <param name="myEventArgs"></param>
        private void activate(Object myObject, EventArgs myEventArgs)
        {
            Map.map[x, y - 1].isEmpty = StateField.fill;
            trapTimer.Stop();
            act = true;

        }
        /// <summary>
        /// Sprawdza czy pulapka trafila w cos, dodaje punkty za pulapke 
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
                return (en.x == x && en.y == y);

            });
            if ((x) == (int)Tony.currPosition.X / 48 && y == (int)Tony.currPosition.Y / 48 )
                Tony.dead = true;
                
            if (ile != Level.enemy.Count() && draw)
            {
                Level.points += (100*(ile - Level.enemy.Count()));
                draw = false;
            }
            else if (iles != Level.enemySuper.Count() && draw)
            {
                Level.points += (200 * (iles - Level.enemySuper.Count()));
                draw= false;
            }
        }

        public  void Draw(SpriteBatch spriteBratch)
        {
            if (draw)
                spriteBratch.Draw(Source.glaz, new Vector2(position.X,position.Y), Color.White);
            
        }
    }
}
