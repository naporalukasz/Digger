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
    /// Klasa implementujaca bonusy wystepujace w grze
    /// </summary>
    class Bonus:IGameObject
    {
        public Texture2D texture;
        public  Vector2 position;

        /// <summary>
        /// Tworzy nowy obiekt typu Bonus.
        /// </summary>
        /// <param name="newTexture">Tekstura dla Bonusa.</param>
        /// <param name="newPosition">Pozycja startowa.</param>
        public Bonus(Texture2D newTexture, Vector2 newPosition)
        {
                
        }

        /// <summary>
        /// Uaktualnia stan Bunusu. 
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            
        }
        
        /// <summary>
        /// Ryzuje Bonus na planszy.
        /// </summary>
        /// <param name="spriteBratch"></param>
        public virtual void Draw(SpriteBatch spriteBratch)
        {
            
        }

    }
}
