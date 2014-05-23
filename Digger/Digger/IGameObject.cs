using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Digger
{
   
	interface IGameObject
	{
        /// <summary>
        /// Funkcja odpowiedzialna za uaktualnianie obiektu.
        /// </summary>
        /// <param name="gameTime"></param>
         void Update(GameTime gameTime);

        /// <summary>
        /// Funkcja odpowiedzialna ze rysowanie obiektu na planszy.
        /// </summary>
        /// <param name="spritebratch"></param>
         void Draw(SpriteBatch spritebratch);
	}
}
