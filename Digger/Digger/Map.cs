using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
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
    /// Okresla stan pola na planszy.
    /// </summary>
    enum StateField
    {
        empty,
        rubin,
        diamond,
        fill, 
        trap
    }

    /// <summary>
    /// Struktura pojedynczego pola na planszy.
    /// </summary>
    struct Field
    {
        public Vector2 position;
        public StateField isEmpty;
        public Texture2D texture;
    }

    /// <summary>
    /// Klasa odpowiedzialna zatworzenie mapy rozgrywki.
    /// </summary>
    class Map
    {
        int HIGHT = 17;
        int WIDTH = 13;
        /// <summary>
        /// okresla czy gracz zakonczyl dony poziom.
        /// </summary>
        public bool clear = false;
        int numRubin;
        int numTrap;
        bool isdiamond = false;

        /// <summary>
        /// Tablic pol mapy.
        /// </summary>
        public static Field[,] map = new Field[17, 13];
        /// <summary>
        /// Pozycja poczatka startowego tunelu.
        /// </summary>
        public static Vector2 startPosition ;
        /// <summary>
        /// Pozycja konca poczatkowego tunelu.
        /// </summary>
        public static Vector2 endPosition ;
        /// <summary>
        ///Tworzy nowy obiekt typu Map.
        /// </summary>
        public Map()
        {
         
            for (int i = 0; i < HIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    map[i, j].position = new Vector2(i * 48, (j+1) * 48);
                    map[i, j].isEmpty = StateField.fill;
                }
            }
            firstTunnelrend();
            setRubin();
            setTrap();
        }

        /// <summary>
        /// Ustawia rubiny na mapie.
        /// </summary>
        private void setRubin()
        {
            countRubin();
            Random random = new Random(); 
            int count=0;
            int x = 0, y = 0;
            while (count!=numRubin)
            {
                x = random.Next(0, 16);
                y = random.Next(0, 12);
                if (map[x, y].isEmpty == StateField.fill)
                {
                    map[x, y].isEmpty = StateField.rubin;
                    count++;
                }
            }
        }

        /// <summary>
        /// Ustawia pulapki na mapie.
        /// </summary>
        private void setTrap()
        {
            Random random = new Random();
            int count = 0;
            int x = 0, y = 0;
            while (count < numTrap)
            {
                x = random.Next(1, 16);
                y = random.Next(1, 12);
                if (map[x, y].isEmpty == StateField.fill && map[x, y].isEmpty != StateField.rubin)
                {
                    map[x, y].isEmpty = StateField.trap;
                    Level.trap.Add(new Trap(new Vector2((48*(x)),(48*(y+1)))));
                    count++;
                }
            }
            var yj = Level.trap;
        }

        /// <summary>
        /// Wylicza ileos rubinow i pulapek zaleznei od pozimu.
        /// </summary>
        private void countRubin()
        {
            switch (Logic.numLevel)
            {
                case 1:
                    numRubin = 10;
                    numTrap = 3;
                    break;
                case 2:
                    numRubin = 15;
                    numTrap = 4;
                    break;
                case 3:
                    numRubin = 15;
                    numTrap = 5;
                    break;
                case 4:
                    numRubin = 15;
                    numTrap = 5;
                    break;
                case 5:
                    numRubin = 20;
                    numTrap = 5;
                    break;
                case 6:
                    numRubin = 13;
                    numTrap = 2;
                    break;
                case 7:
                    numRubin = 17;
                    numTrap = 3;
                    break;
                case 8:
                    numRubin = 17;
                    numTrap = 4;
                    break;
                case 9:
                    numRubin = 17;
                    numTrap = 4;
                    break;
                case 10:
                    numRubin = 23;
                    numTrap = 4;
                    break;
                case 11:
                    numRubin = 15;
                    numTrap = 2;
                    break;
                case 12:
                    numRubin = 20;
                    numTrap = 2;
                    break;
                case 13:
                    numRubin = 20;
                    numTrap = 3;
                    break;
                case 14:
                    numRubin = 20;
                    numTrap = 3;
                    break;
                case 15:
                    numRubin = 25;
                    numTrap = 4;
                    break;
            }
        }

        /// <summary>
        /// Tworzy pierwszy losowy tunel
        /// </summary>
        private void firstTunnelrend()
        {
            Random random = new Random();            
            var startPoint = new Point(random.Next(1, 17), random.Next(1, 13));
            startPosition = new Vector2(startPoint.X * 48, startPoint.Y * 48);
            map[startPoint.X, startPoint.Y].isEmpty = StateField.empty;
            var len = random.Next(11 , 21);
            var last = 0;
            int i=0;
           while(i<=len)
           {
                var kier = random.Next(0, 3);
                if (kier%2 == last%2 )
                    if(kier != last  )
                        switch (kier)
                        {
                            case 0:
                                kier = 2;
                                break;
                            case 1:
                                kier = 3;
                                break;
                            case 2:
                                kier = 0;
                                break;
                            case 3:
                                kier = 1;
                                break;
                        }
                switch (kier)
                {
                    case 0:
                        if (startPoint.X + 1 <= HIGHT - 1)
                        {
                            startPoint.X++;
                            last = 0;
                        }
                        break;
                    case 1:
                        if (startPoint.Y + 1 <= WIDTH-1)
                        {
                            startPoint.Y++;
                            last = 1;
                        }
                        break;
                    case 2:
                        if (startPoint.X - 1 >= 0)
                        {
                            startPoint.X--;
                            last = 2;
                        }
                        break;
                    case 3:
                        if (startPoint.Y - 1 >= 0)
                        {
                            startPoint.Y--;
                            last = 3;
                        }
                        break;
                }
                map[startPoint.X, startPoint.Y].isEmpty = StateField.empty;
                i++;
            }
            endPosition = new Vector2(startPoint.X * 48, startPoint.Y * 48);
        }

        /// <summary>
        /// Uaktualnia obiekty na mapie.
        /// </summary>
        /// <param name="gameTimer"></param>
        /// <param name="player">Obikt gracza</param>
        public void Update(GameTime gameTimer, ref Tony player)
        {
            //float x = Tony.position.X;

            Vector2 ob = new Vector2(player.position.X / 48, player.position.Y / 48);
           // map[(int)ob.X, (int)ob.Y].isEmpty = StateField.empty;
            if (map[(int)ob.X, (int)ob.Y].isEmpty == StateField.rubin)
            {
                numRubin--;
                Level.points += 30;
                if (numRubin == 0 && isdiamond == false)
                    setDiamond();
            }
            else if (map[(int)ob.X, (int)ob.Y].isEmpty == StateField.diamond)
            {
                Level.points += 100;
                clear = true;
            }
            if( map[(int)ob.X, (int)ob.Y].isEmpty != StateField.trap)
                map[(int)ob.X, (int)ob.Y].isEmpty = StateField.empty;            
        }

        /// <summary>
        /// Ustawia diament po zebraniu wszystkich rubinow.
        /// </summary>
        private void setDiamond()
        {
            Random random = new Random();
            int x = 0, y = 0;
                       
            x = random.Next(0, 17);
            y = random.Next(0, 13);
          //  if (map[x, y].isEmpty == StateField.fill)
           // {
                map[x, y].isEmpty = StateField.diamond;                
            //}
            isdiamond = true;
        }

        /// <summary>
        /// Rysuje obiekty na mapie.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //player.Draw(spriteBatch);
            for (int i = 0; i < HIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if(map[i,j].isEmpty == StateField.fill || map[i,j].isEmpty == StateField.trap)
                        spriteBatch.Draw(Source.pole, map[i, j].position, Color.White);
                    if(map[i,j].isEmpty == StateField.rubin)
                        spriteBatch.Draw(Source.rubin, map[i, j].position, Color.White);
                    if(map[i,j].isEmpty == StateField.diamond)
                        spriteBatch.Draw(Source.diament, map[i, j].position, Color.White);
                }
            }
            //spriteBatch.Draw(texture, Color.White);
            //player.Draw(spriteBatch);
        }
    }
}
