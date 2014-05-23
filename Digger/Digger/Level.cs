using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Digger
{
    /// <summary>
    /// Klasa zarzadzajaca obecnym levelem.
    /// </summary>
    class Level
    {
        /// <summary>
        /// Obecny gracz.
        /// </summary>
        public Tony player;
        
        /// <summary>
        /// Timer odliczający czas do pojawienia sie kolejnego przeciwnika
        /// </summary>
        static System.Windows.Forms.Timer enemyTimer = new System.Windows.Forms.Timer();

        /// <summary>
        /// Okresla zakonczenie obecnego levelu.
        /// </summary>
        public bool levelEnd = false;
      
        /// <summary>
        /// Punkty zdobyte podczas rozgrywki.
        /// </summary>
        public static int points = 0;
      
        int numRobak = 1;
        int numSuperR = 0;
        public List<Bonus> bonus;
        /// <summary>
        /// Lista przeciwnikow typu Robak.
        /// </summary>
        public static List<Robak> enemy;
        /// <summary>
        /// Lista przeciwnikow typu SuperRobak.
        /// </summary>
        public static List<SuperRobak> enemySuper;
        /// <summary>
        /// Lista zawierajaca pulapki.
        /// </summary>
        public static List<Trap> trap;
        /// <summary>
        /// Obecna mapa dla levelu.
        /// </summary>
        public Map map;


        /// <summary>
        /// Tworzy nowy obiekt typu Level
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="currpoint">Punkty od ktorych ma sie zaczac level</param>
        public Level( GraphicsDevice graphics, int currpoint)
        {
            bonus = new List<Bonus>();
            enemy = new List<Robak>();
            enemySuper = new List<SuperRobak>();
            trap = new List<Trap>();
            points = currpoint;

           // size = new Vector2(graphics.Viewport.Width, graphics.Viewport.Height );
            map = new Map();           
            player = new Tony(Source.tony, Map.startPosition);
            countEnemy();
            enemy.Add(new Robak(Source.robak, Map.endPosition, player));
            enemyTimer.Tick += new EventHandler(AddEnemy);
            enemyTimer.Interval = 8000;
            enemyTimer.Start();
            levelEnd = false;
            Tony.dead = false;
        }
        
        /// <summary>
        /// Funkcja odpowiedzialna za uaktualnianie obiektow zwiazanych z levelem.
        /// </summary>
        /// <param name="gameTimer"></param>
        public void Update(GameTime gameTimer)
        {
           
                player.Update(gameTimer);
                foreach (var el in enemy)
                    el.Update(gameTimer);
                foreach (var el in enemySuper)
                    el.Update(gameTimer);
                foreach (var el in trap)
                    el.Update(gameTimer);
                Level.trap.RemoveAll(delegate(Trap en)
                {
                    return en.draw == false;

                });
                map.Update(gameTimer, ref player);
                if (map.clear)
                    levelEnd = true;

                //points += player.Points;
                //player.Points = 0;//TODO: zmienic na lepsze np dodac dziedziczenie
            
        }

        /// <summary>
        /// Rysowanie obiektow zwiazanych z levelem.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {          
                map.Draw(spriteBatch);
                player.Draw(spriteBatch);

                foreach (var el in enemy)
                    el.Draw(spriteBatch);
                foreach (var el in enemySuper)
                    el.Draw(spriteBatch);
                foreach (var el in trap)
                    el.Draw(spriteBatch);

                spriteBatch.Draw(Source.tloScore, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(Source.infBoxFont, "Points:" + points.ToString(), new Vector2(40, 13), Color.Red);
                spriteBatch.DrawString(Source.infBoxFont, "Life:" + player.NumLife.ToString(), new Vector2(200, 13), Color.Red);
                spriteBatch.DrawString(Source.infBoxFont, "Level:" + Logic.numLevel.ToString(), new Vector2(350, 13), Color.Blue);
                if (player.Weapon == 1)
                    spriteBatch.Draw(Source.weapon1, new Vector2(500, 0), Color.White);
                else
                    spriteBatch.Draw(Source.weapon2, new Vector2(500, 0), Color.White);

                spriteBatch.DrawString(Source.infBoxFont, "Weapon state:" + player.ready, new Vector2(550, 13), Color.Red);           
        }

        /// <summary>
        /// Funkcja dodaje przeciwnikow do levelu.
        /// </summary>
        /// <param name="myObject"></param>
        /// <param name="myEventArgs"></param>
        private void AddEnemy(Object myObject, EventArgs myEventArgs)
        {
            Random random = new Random();
            int tmp = random.Next(1, 3);
            if (numRobak > enemy.Count &&tmp==1)
                enemy.Add(new Robak(Source.robak, Map.endPosition, player));
            else if (numSuperR > enemySuper.Count && tmp == 2)
                enemySuper.Add(new SuperRobak(Source.robakS, Map.endPosition, player));
        }

        /// <summary>
        /// Funkcja wylicza ilosc przeciwnikow w zaleznosci od obecnego levelu
        /// </summary>
        private void countEnemy()
        {
            switch (Logic.numLevel)
            {
                case 1:
                      numRobak = 2;
                      numSuperR = 0;
                    break;
                case 2:
                    numRobak = 3;
                      numSuperR = 0;
                    break;
                case 3:
                    numRobak = 3;
                      numSuperR = 1;
                    break;
                case 4:
                     numRobak = 4;
                      numSuperR = 1;
                    break;
                case 5:
                     numRobak = 5;
                      numSuperR = 2;
                    break;
                case 6://medium
                    numRobak = 3;
                      numSuperR = 0;
                    break;
                case 7:
                     numRobak = 4;
                      numSuperR = 0;
                    break;
                case 8:
                     numRobak = 4;
                      numSuperR = 1;
                    break;
                case 9:
                     numRobak = 5;
                      numSuperR = 2;
                    break;
                case 10:
                     numRobak = 6;
                      numSuperR = 2;
                    break;
                case 11://hard
                     numRobak = 4;
                      numSuperR = 0;
                    break;
                case 12:
                     numRobak = 5;
                      numSuperR = 0;
                    break;
                case 13:
                     numRobak = 5;
                      numSuperR = 1;
                    break;
                case 14:
                     numRobak = 6;
                      numSuperR = 2;
                    break;
                case 15:
                     numRobak = 7;
                      numSuperR = 3;
                    break;
            }
        }
    }
}
