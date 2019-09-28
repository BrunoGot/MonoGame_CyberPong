using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //perso
        /*  private Texture2D m_ball;
          private Rectangle m_ballRect;*/
        private Ball m_ball;
        private Paddle m_leftPaddle;
        private AIPaddle m_AIPaddle;
        //do a background class
        private Rectangle m_bgRect;
        private Texture2D m_bgTexture;
//        private int m_ballSpeedX = 300;
  //      private int m_ballSpeedY = 300;

        private int m_barSpeed = 300;
        private int m_ballSpeed = 300;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m_ball = new Ball(100, 100, new Vector2(m_ballSpeed, m_ballSpeed), GraphicsDevice.Viewport.Height);
            m_leftPaddle = new Paddle(0, 200, m_barSpeed, GraphicsDevice.Viewport.Height);
            m_AIPaddle = new AIPaddle(GraphicsDevice.Viewport.Width-32, 200, m_barSpeed, GraphicsDevice.Viewport.Height,m_ball);
            m_bgRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Console.WriteLine("size w/h = " + GraphicsDevice.Viewport.Width + " / " + GraphicsDevice.Viewport.Height);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // m_ball = Content.Load<Texture2D>("Images\\PongBall");
            m_ball.Load(Content);
//             m_ballRect = new Rectangle(100, 100, 64, 64);

            m_leftPaddle.Load(Content);
            m_AIPaddle.Load(Content);

            m_bgTexture = Content.Load<Texture2D>("Images\\BackGround");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            m_ball.Update(gameTime);
            m_leftPaddle.Update(gameTime);
            m_AIPaddle.Update(gameTime);
            BallPaddleCollision();
            base.Update(gameTime);
        }

        private void BallPaddleCollision()
        {
            Vector2 paddlePos = Vector2.Zero;
            //detect left paddle collision
            if (m_ball.X < m_leftPaddle.Width)
            {
                if (m_ball.Y + m_ball.Radius >= m_leftPaddle.Y && m_ball.Y + m_ball.Radius <= m_leftPaddle.Y + m_leftPaddle.Height)
                {
                    //bounce
                    paddlePos = new Vector2(m_leftPaddle.Width, m_leftPaddle.Y + m_leftPaddle.Height / 2);
                    m_ball.Bounce(paddlePos, m_leftPaddle.Height);
                }
                else //left paddle loose !
                {
                    Vector2 pos = new Vector2(m_leftPaddle.Width + m_ball.Radius*4.0f, GraphicsDevice.Viewport.Height / 2.0f);
                    Vector2 speed = new Vector2(m_ballSpeed, 0);
                    m_ball.Reset(Vector2.One*300,speed);
                }
            }
            //detect rightPaddle collision
            if (m_ball.X+m_ball.Radius*2.0 > m_AIPaddle.X)
            {
                if (m_ball.Y >= m_AIPaddle.Y && m_ball.Y + m_ball.Radius<= m_AIPaddle.Y + m_AIPaddle.Height)
                {
                    //bounce
                    paddlePos = new Vector2(m_AIPaddle.X- m_ball.Radius * 2.0f, m_AIPaddle.Y + m_AIPaddle.Height / 2);
                    m_ball.Bounce(paddlePos, m_AIPaddle.Height);
                }
                else //RightPaddle loose ! 
                {
                    Vector2 pos = new Vector2(m_AIPaddle.X - m_ball.Radius*3.0f, GraphicsDevice.Viewport.Height / 2.0f);
                    Vector2 speed = new Vector2(-m_ballSpeed, 0);
                    m_ball.Reset(pos,speed);
                }
            }

            

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            // spriteBatch.Draw(m_ball, m_ballRect, Color.White);
            spriteBatch.Draw(m_bgTexture, m_bgRect, Color.White);
            m_ball.Draw(gameTime, spriteBatch);
            m_leftPaddle.Draw(gameTime, spriteBatch);
            m_AIPaddle.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
