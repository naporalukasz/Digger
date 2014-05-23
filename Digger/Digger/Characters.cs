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
    /// Szablon klasy dla postaci wystepujacych w grze.
    /// </summary>
     class Characters:IGameObject
    {
  
         public Texture2D texture;
         public int speed;
         /// <summary>
         /// Okresla obecna pozycje postaci.
         /// </summary>
        public  Vector2 position;
         

         /// <summary>
         /// Tworzy nowy obiekt typu Charakters.
         /// </summary>
         /// <param name="newTexture">Tekstura dla Charakters.</param>
         /// <param name="newPosition">Pozycja startowa dla Charakters.</param>
        public Characters(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = new Vector2(newPosition.X+24,newPosition.Y+24);
            speed = 1;          
        }

         /// <summary>
         /// Uaktualnia stan Charakters.
         /// </summary>
         /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            
        }

         /// <summary>
         /// Rysuje Charakters na planszy.
         /// </summary>
         /// <param name="spriteBratch"></param>
        public virtual void Draw(SpriteBatch spriteBratch)
        {
            
        }

    }

}
