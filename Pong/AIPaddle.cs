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
    class AIPaddle:Paddle
    {
        private Ball m_ball;

        public AIPaddle(int _x, int _y, int _speed, int _screenHeight, Ball _ball):base(_x,_y, _speed, _screenHeight)
        {
            m_ball = _ball;
        }

        public override void Update(GameTime _gameTime)
        {
            double delta = _gameTime.ElapsedGameTime.TotalSeconds;

            if (m_ball.Y < m_rect.Y + m_rect.Height / 4)
            {
                m_rect.Y -= (int)(m_speed * delta);
            }
            if (m_ball.Y > m_rect.Y + m_rect.Height*3 / 4)
            {
                m_rect.Y += (int)(m_speed * delta);
            }
        }

        
    }
}
