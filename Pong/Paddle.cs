using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Pong
{
    public class Paddle
    {
        protected Texture2D m_texture;
        protected Rectangle m_rect;
        protected int m_speed;
        protected int m_height;

        public int X { get { return m_rect.X; } }
        public int Y { get { return m_rect.Y; } }
        public int Width { get { return m_rect.Width; } }
        public int Height { get { return m_rect.Height; } }

        public Paddle(int _x, int _y, int _speed, int _height)
        {
            m_rect = new Rectangle(_x, _y, 32, 128);
            this.m_speed = _speed;
            m_height = _height;
        }

        public void Load(ContentManager _content)
        {
            m_texture = _content.Load<Texture2D>("Images\\PongBar1A2");
        }

        public virtual void Update(GameTime _gameTime)
        {
            //keyboard input part
            double delta = _gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Space)) //special key to speed up ;)
            {
                delta *= 3.0f;
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                m_rect.Y += (int)(m_speed * delta);
            }
            if (ks.IsKeyDown(Keys.Up))
            {
                m_rect.Y -= (int)(m_speed * delta);
            }
            //block the bar
            if (m_rect.Y < 0)
            {
                m_rect.Y = 0;
            }
            if (m_rect.Y > m_height - m_rect.Height)
            {
                m_rect.Y = m_height - m_rect.Height;
            }

        }

        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(m_texture, m_rect, Color.White);
        }
    }
}
