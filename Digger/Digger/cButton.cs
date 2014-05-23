﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Digger
{
    /// <summary>
    /// Klasa implementująca przyciski wystepujące w grze.
    /// </summary>
    class cButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color color = new Color(255, 255, 255, 255);

        public Vector2 size;
        /// <summary>
        /// Tworzy nowy obiekt typu cButton.
        /// </summary>
        /// <param name="newTexture">Teksstur dal cButton</param>
        /// <param name="graphics"></param>
        public cButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            size = new Vector2(graphics.Viewport.Width / 4, graphics.Viewport.Height / 20);
        }

        bool down;
        /// <summary>
        /// Zmienna okreslająca czy przycisk jest klikniety.
        /// </summary>
        public bool isClicked;

        /// <summary>
        /// Funkcja uaktualnia stan przycisku zależnie czy został on kikniety czy nie.
        /// </summary>
        /// <param name="mouse">Myszka</param>
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (mouseRectangle.Intersects(rectangle))
            {
                if (color.A == 255) down = false;
                if (color.A == 0) down = true;
                if (down) color.A += 3; else color.A-=3;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else if (color.A<255)
            {
                color.A += 3;
                isClicked = false;
            }
        }

        /// <summary>
        /// Ustawia przycisk w podanej pozycji.
        /// </summary>
        /// <param name="newPosition">Nowe wspolrzedne. </param>
        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        /// <summary>
        /// Rysuje przeycisk.
        /// </summary>
        /// <param name="spritBatch"></param>
        public void Draw(SpriteBatch spritBatch)
        {
            spritBatch.Draw(texture, rectangle, color);
        }
    }
}
