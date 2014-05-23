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
    /// Klasa odpowiedzialna za zmiane pozionow.
    /// </summary>
	public class Logic
	{
        Level oLevel;
        /// <summary>
        /// Obececy rozgrywany poziom.
        /// </summary>
        public static int numLevel = 0;
 
        GraphicsDevice oGraphics;
        int point = 0;
        int startPoint = 0;
        int Count = 0;

        /// <summary>
        /// Przechowuje obecny stan punktow.
        /// </summary>
        public int Points
        {
            get
            {
                return point;
            }
            set
            {
                point = value;
            }
        }
        /// <summary>
        /// Przechowuje stan punktow na poczatku levelu.
        /// </summary>
        public int StartPoints
        {
            get
            {
                return startPoint;
            }
            set
            {
                startPoint = value;
            }
        }

        /// <summary>
        /// Tworzy nowy obiekt typu Logic.
        /// </summary>
        /// <param name="graphics"></param>
        public Logic(GraphicsDevice graphics)
        {
            numLevel+=1;
            oGraphics = graphics;

        }

        /// <summary>
        /// Startuje rozgrywke.
        /// </summary>
        public void startLevel()
        {
            oLevel = new Level(oGraphics, 0);
        }

        /// <summary>
        /// Startuje rozgrywke
        /// </summary>
        /// <param name="point">Ilos punktow</param>
        /// <param name="counter">ilosc zyc</param>
        public void startLevel(int point,int counter)
        {
            Count++;
            oLevel = new Level(oGraphics, point);
            Tony.dead = false;
            oLevel.player.NumLife-=counter;
        }

        /// <summary>
        /// Uaktualnia poziomu, sprawdza czy gracz wygral lub przegral.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (Tony.dead)
            {
                if (Count == 2)
                    Game.CurrentGameState = GameState.Loss;
                else
                    Game.CurrentGameState = GameState.Dead;
           
            }

                oLevel.Update(gameTime);
                point = Level.points;
                if (oLevel.levelEnd)
                {
                    if (numLevel == 5 || numLevel == 10 || numLevel == 15)
                    {
                        Game.CurrentGameState = GameState.WinGame;
                    }
                    numLevel++;
                    startPoint = Level.points;
                    oLevel = new Level(oGraphics, point);
                }
            
        }

        /// <summary>
        /// Rysuje obiekty obecnego levelu. 
        /// </summary>
        /// <param name="spriteBratch"></param>
        public  void Draw(SpriteBatch spriteBratch)
        {
                oLevel.Draw(spriteBratch);           
        }

	}
}
