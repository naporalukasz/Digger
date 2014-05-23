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
    /// Klasa implementuje postac gracza
    /// </summary>
	class Tony:Characters
	{
        /// <summary>
        /// Timer odliczajacy czas do załadowania sie pistoletu
        /// </summary>
        static System.Windows.Forms.Timer gunTimer = new System.Windows.Forms.Timer();
        /// <summary>
        /// Timer odliczajacy czas do zaladowania dzidy
        /// </summary>
        static System.Windows.Forms.Timer laserTimer = new System.Windows.Forms.Timer();

        int weapon;
        int LoandingPist;
        int LoandingDzid;
        int numLife;
        bool shot = false;
        bool click = false;
        /// <summary>
        /// okresla stan gracza
        /// </summary>
        public static bool dead = false;
        /// <summary>
        /// Okresla obecna pozycje gracza
        /// </summary>
        public static Vector2 currPosition;
        /// <summary>
        /// Stan broni.
        /// </summary>
        public  string ready = "load";
        Bullet oBullet;
       /// <summary>
       /// Okresle obeca bron
       /// </summary>
        public int Weapon
        {
            get
            {
                return weapon;
            }
            set
            {
                weapon = value;
            }
        }
    /// <summary>
    /// Stan zycia bohatera
    /// </summary>
        public int NumLife
        {
            get { return numLife; }
            set { numLife = value; }
        }

        /// <summary>
        /// Tworzy obiekt typu Tony
        /// </summary>
        /// <param name="newTexture">Textura Tonego</param>
        /// <param name="newPosition">Nowa pozycja</param>
        public Tony(Texture2D newTexture, Vector2 newPosition)
            : base(newTexture, newPosition)
        {
            currPosition = newPosition;
            speed = 4;
            Weapon = 1;
            LoandingPist = 1;
            LoandingDzid = 1;
            NumLife = 3;
            gunTimer.Tick += new EventHandler(TimerPistolet);
            gunTimer.Interval = 3000;
            gunTimer.Start();
            laserTimer.Tick += new EventHandler(TimerLaser);
            laserTimer.Interval = 10000;
        }

        /// <summary>
        /// Uaktualnia pozycje gracza
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            int x = (int)(position.X+speed) / 48;
            int y = (int)(position.Y+speed) / 48;
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && position.X + 24 < 816)
            {
                if (Map.map[x, y].isEmpty != StateField.trap)
                    position.X += speed;
                else
                    position.X = ((x) * 48) - 24;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && position.X - 24 > 0)
            {
                if (Map.map[(x - 1 <= 0) ? 0 : x - 1, y].isEmpty != StateField.trap)
                    position.X -= speed;
                else
                    position.X = ((x ) * 48) + 24; 
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up) && position.Y - 24 > 0)
            {
                if (Map.map[x, (y-1<=0)?0:y-1].isEmpty != StateField.trap)
                    position.Y -= speed;
                else
                    position.Y = ((y ) * 48) + 24;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && position.Y + 24 < 624)
            {
                if (Map.map[x, y].isEmpty != StateField.trap)
                    position.Y += speed;
                else
                    position.Y = ((y - 1) * 48) + 24;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt)) click = true;
            if (Keyboard.GetState().IsKeyUp(Keys.LeftAlt) && click)
            {
                if (Weapon == 1)
                {
                    weapon = 2;
                    gunTimer.Stop();
                    laserTimer.Start();
                    ready = LoandingDzid == 2 ? "Ready" : "Load";
                }
                else
                {
                    weapon = 1;
                    gunTimer.Start();
                    laserTimer.Stop();
                    ready = LoandingPist == 2 ? "Ready" : "Load";
                }
                click = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && LoandingPist==2)
            {
                shot = true;
                oBullet = new Bullet(position,weapon);
                LoandingPist = 1;
                ready = "load";
                gunTimer.Start();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && LoandingDzid == 2)
            {
                shot = true;
                oBullet = new Bullet(position, weapon);
                LoandingDzid = 1;
                ready = "load";
                laserTimer.Start();
            }

            if(shot)
                oBullet.Update(gameTime);
            currPosition=position;
        }

        /// <summary>
        /// Rysuje gracza na planszy
        /// </summary>
        /// <param name="spriteBratch"></param>
        public override void Draw(SpriteBatch spriteBratch)
        {
            if(!dead)
                spriteBratch.Draw(texture, new Vector2(position.X - 24, position.Y + 24), Color.White);
            if (shot)
                oBullet.Draw(spriteBratch);
            
        }

        /// <summary>
        /// zmienia stan gotowosci broni pistolet
        /// </summary>
        /// <param name="myObject"></param>
        /// <param name="myEventArgs"></param>
        private  void TimerPistolet(Object myObject, EventArgs myEventArgs)
        {
            gunTimer.Stop();
            LoandingPist = 2;
            ready = "Ready";
        }

        /// <summary>
        /// Zmienia stan gotowosci broni laserowa dzida
        /// </summary>
        /// <param name="myObject"></param>
        /// <param name="myEventArgs"></param>
        private void TimerLaser(Object myObject, EventArgs myEventArgs)
        {
            laserTimer.Stop();
            LoandingDzid = 2;
            ready = "Ready";
        }


    
	}
}
