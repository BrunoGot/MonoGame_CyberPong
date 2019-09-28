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

    class Ball
    {

        protected Texture2D m_texture;
        protected Rectangle m_rect;
        protected Vector2 m_speed;
        protected Vector2 m_baseSpeed;
        protected int m_screenHeight;

        //attributes

        public int X
        {
            get
            {
                return m_rect.X;
            }
        }
        public int Y
        {
            get
            {
                return m_rect.Y;
            }
        }

        public int Radius
        {
            get { return m_rect.Width / 2; }
        }

        public Ball(int _x, int _y, Vector2 _speed, int _screenHeight)
        {
            m_rect = new Rectangle(_x, _y, 64, 64);
            m_speed = _speed;
            m_baseSpeed = m_speed;
            m_screenHeight = _screenHeight;
            Reset(new Vector2( _x, _y),m_baseSpeed);
        }

        public void Load(ContentManager _content)
        {
            m_texture = _content.Load<Texture2D>("Images/PongBall1A");
        }

        public void Update(GameTime _gameTime)
        {
            double delta = _gameTime.ElapsedGameTime.TotalSeconds;
            m_rect.X += (int)(m_speed.X * delta);
            m_rect.Y += (int)(m_speed.Y * delta);

            //bal detecting border part
            if (m_rect.Y < 0)
            {
                m_speed.Y = -m_speed.Y;
            }
            if (m_rect.Y > m_screenHeight - 64)
            {
                //est-ce vraiment utile ?                m_rect.Y = GraphicsDevice.Viewport.Height-64;
                m_speed.Y = -m_speed.Y;
            }
        }

        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(m_texture, m_rect, Color.White);
        }

        public void Bounce(Vector2 _paddlePos, float _objectSize)
        {
            Console.WriteLine("Bounce");
          //  Random random = new Random(35135);
           // m_speed.X = -m_speed.X;// * 0.025f;// *(float)random.Next(90,110));

            int dirX = (int)(m_speed.X / Math.Abs(m_speed.X));
            int dirY = (int)(m_speed.Y / Math.Abs(m_speed.Y));
            Vector2 speed = m_speed;
            speed.Normalize();
            float dot = Vector2.Dot(speed, Vector2.UnitY);
            Console.WriteLine("Dot = " + dot);
            if (dot > 0.75f) {
                dirY *= -1;// * 0.025f;// * (float)random.Next(90, 110));
            }
            dirX *= -1; //bounce on X each time
            m_rect.X = (int)_paddlePos.X;
            float ballY = m_rect.Y + m_rect.Height / 2;
            float val = (ballY - _paddlePos.Y) / (_objectSize / 2f);
            Console.WriteLine("ball.Y = " + ballY);
            Console.WriteLine("_paddlePos.Y = " + _paddlePos.Y);
            Console.WriteLine("ball.Y - _paddlePos.Y = " + (ballY - _paddlePos.Y));

            m_baseSpeed += Vector2.One * 25.0f; //increase the speed each bounce
            m_speed.X = (m_baseSpeed.X+(1-val)*250)*dirX;
           // Math.Min(0.75f,val);
            m_speed.Y = m_baseSpeed.Y * dirY*val;

            Console.WriteLine("ValueType = " + val);
            Console.WriteLine("ValueType = " + (1f-Math.Min(0.75f,val)));
            //m_speed.Y*=1.0-(m_rect.Y-_paddlePos.Y)
        }

        public void Reset(Vector2 _startPos, Vector2 _startSpeed)
        {
            Console.WriteLine("Restart !!");
            m_rect.X = (int)_startPos.X;
            m_rect.Y = (int)_startPos.Y;
          /*  m_baseSpeed = _startSpeed;
            m_speed = m_baseSpeed;*/
        }

    }
}
