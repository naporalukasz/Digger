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
using Microsoft.Xna.Framework.Storage;

namespace Digger
{
    /// <summary>
    /// Struktura sluzaca do zapisu gry.
    /// </summary>
    public struct SaveGame
    {
       public int currLife;
       public int currpoint;
       public int currlevel;
    }

    /// <summary>
    /// klasa odpowiedzialna za zapisywanie obecnego ztanu gry.
    /// </summary>
    [Serializable]
	public class LoadGame
	{
 
        string filename = "mysave.dat";

        /// <summary>
        /// Zapisuje obecny stan gry.
        /// </summary>
        /// <param name="data">Dane do zapisu.</param>
        public static void SaveGame(SaveGame data)//, StorageDevice device)
        {
            // Get the path of the save game
            string fullpath = "mysave.dat";
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
        /// Funkcja wczytuje zapisana gre.
        /// </summary>
        /// <returns>Dane dotyczace zapisu gry</returns>
        public static SaveGame LoadGames()
        {
            SaveGame data;

            // Get the path of the save game
            string fullpath = "mysave.dat";

            // Open the file
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                // Read the data from the file
                XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
                data = (SaveGame)serializer.Deserialize(stream);
            }
            finally
            {
                // Close the file
                stream.Close();
            }


            return (data);
        }
	}
}
