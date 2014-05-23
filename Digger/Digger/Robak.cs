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
    /// Klasa implementyjaca przeciwnika typu Robak.
    /// </summary>
    class Robak:Characters
    {
        Tony oTony;

        public  int x = 0, y = 0;
        /// <summary>
        /// Tworzy nowy obiekt typu Robak
        /// </summary>
        /// <param name="newTexture">textura dla Robaka</param>
        /// <param name="newPosition">now pozycja</param>
        /// <param name="newTony">obiekt gracza</param>
        public Robak(Texture2D newTexture, Vector2 newPosition,Tony newTony)
            : base(newTexture, newPosition) { oTony = newTony; }

        /// <summary>
        /// Uaktualnia stan robaka- poruszanie, kolizje
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            move();
            hit();
        }
        /// <summary>
        /// porusza robakiem
        /// </summary>
        private void move()
        {
           // int x = 0, y = 0;
            x = (int)position.X / 48;
            y = (int)position.Y / 48;
            if (position.X - oTony.position.X < 0)
            {
                //if (x >= 1) x = 1;
                if (y >= 12) y = 12;
                if (position.X + 24 < 816)
                    if(Map.map[x,y].isEmpty == StateField.empty)
                        position.X += speed;
                
            }
            else if (position.X - oTony.position.X > 0)
            {
                if (x >= 15) x = 15;
                if (y >= 12) y = 12;
                if (position.X - 24 > 0)
                    if(Map.map[x , y].isEmpty == StateField.empty)
                        position.X -= speed;
                   
            }

            else if (position.Y - oTony.position.Y < 0)
            {
                if (x >= 16) x = 16;
                if (y >= 11) y = 11;
                if (position.Y + 24 < 624)
                    if (Map.map[x, y].isEmpty == StateField.empty)
                        position.Y += speed;
                    

            }
            else if (position.Y - oTony.position.Y > 0)
            {
                if (x >= 16) x = 16;
                if (y >= 11) y = 11;
                if (position.Y - 24 > 48)
                    if (Map.map[x, y].isEmpty == StateField.empty)
                        position.Y -= speed;
                   
            }
        }
        /// <summary>
        /// Rysuje robaka na planszy
        /// </summary>
        /// <param name="spriteBratch"></param>
        public override void Draw(SpriteBatch spriteBratch)
        {
            spriteBratch.Draw(texture, new Vector2(position.X - 24, position.Y + 24), Color.White);
        }
        /// <summary>
        /// Sprawdza czy Robak nie zabil gracza
        /// </summary>
        public  void hit() 
        {
            int tx = (int)(oTony.position.X) / 48;
            int ty = (int)(oTony.position.Y) / 48;
            int mx = (int)(position.X) / 48;
            int my = (int)(position.Y) / 48;
            if (tx == mx && ty == my)
            {
                Tony.dead = true;
            }
        }

    }
}
