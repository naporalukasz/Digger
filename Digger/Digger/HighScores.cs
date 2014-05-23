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
using System.Xml.Serialization;
using System.IO;
using System.IO.IsolatedStorage;


namespace Digger
{
    /// <summary>
    /// Struktura do przechowywania najlepszych wynikow.
    /// </summary>
    public struct Score
    {
        public string name;
        public int score;
    }

    /// <summary>
    /// Klasa sluzaca do zapisu najlepszych wynikow.
    /// </summary>
    [Serializable]
    public class HighScores
    {
        /// <summary>
        /// Lista 10 najlepszych wynikow.
        /// </summary>
        public List<Score> Players;
        /// <summary>
        /// Obecna ilosc najlepszych wynikow.
        /// </summary>
        public int Count;

        string HighScoresFilename = "highscores.dat";
      
        /// <summary>
        /// Tworzy nowy obiekt typu HighScores.
        /// </summary>
        public HighScores()
        {
            Players = new List<Score>();
        }

         /// <summary>
      /// funkcja zapisuje najlepsze wyniki do pliku xml.
      /// </summary>
      /// <param name="data">Dane do zapisu.</param>
        public static void SaveHighScores(HighScores data)//, StorageDevice device)
        {
            // Get the path of the save game
            string fullpath = "highscores.dat";
            File.Delete(fullpath);
            // Open the file, creating it if necessary
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate);
            try
            {
                // Convert the object to XML data and put it in the stream
                XmlSerializer serializer = new XmlSerializer(data.GetType());
                serializer.Serialize(stream, data);
            }
            finally
            {
                // Close the file
                stream.Close();
            }
        }

        /// <summary>
     /// Funkcja wczytuje najlepsze wyniki z pliku.
     /// </summary>
     /// <returns>Zwraca obiekt HighScores</returns>
        public static HighScores LoadHighScores()
        {
            HighScores data;

            // Get the path of the save game
            string fullpath = "highscores.dat";

            // Open the file
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                // Read the data from the file
                XmlSerializer serializer = new XmlSerializer(typeof(HighScores));
                data = (HighScores)serializer.Deserialize(stream);
            }
            finally
            {
                // Close the file
                stream.Close();
            }
            return (data);
        }

        /// <summary>
        /// funkcja dodaje nowy wynik do obecnie zapisanych. Sprawdza czy nie przekroczono liczby 10 najlepszych wynikow, sortuje wyniki malejaco.
        /// </summary>
        /// <param name="newScore">nowy wynik do dodania.</param>
        /// <returns>Uaktualnione wyniki</returns>
        public HighScores SaveHighScore(Score newScore)
        {
            // Create the data to saved\

            HighScores data = LoadHighScores();
            data.Players.Add(newScore);
            data.Players = data.Players.OrderByDescending(el => el.score).ToList();
            data.Players.Remove(Players.Last());

            SaveHighScores(data);
            return data;
        }

        /* Iterate through data if highscore is called and make the string to be saved*/
        /// <summary>
        /// worzy string ze wszystkich dostepnych wynikow.
        /// </summary>
        /// <returns>Wynikowy string</returns>
        public string makeHighScoreString()
        {

            HighScores data = LoadHighScores();
            string scoreBoardString = "Highscores:\n\n";
            foreach (var el in data.Players)
            {
                scoreBoardString = scoreBoardString + el.name + "-" + el.score.ToString() + "\n";
            }
            return scoreBoardString;
        }

        /// <summary>
        /// Wypisuje na ekran najlepsze wyniki.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            int index = 1;
            int x = 330, y = 250;
            spriteBatch.DrawString(Source.infBoxFont, "Highscores:\n", new Vector2(300, 225), Color.Black);
            foreach (var el in Players)
            {
                spriteBatch.DrawString(Source.infBoxFont, index.ToString() + ".  " + el.name + "-" + el.score.ToString(), new Vector2(x, y), Color.Black);
                index++;
                y += 25;
            }
        }
    }
}
