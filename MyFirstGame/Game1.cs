using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Drawing resources
        private Texture2D shuttle;
        private float shuttleAngle = 0;
        private Texture2D earth;

        // Font resources
        private SpriteFont font;
        private int score;

        // Animation resource
        private AnimatedSprite animatedSprite;

        // Additive Blending resources
        private Texture2D blue;
        private Texture2D green;
        private Texture2D red;

        // These variables will allow us to move our sprites around in circles.The first three, the angles, are going to store what angle each of the sprites are located at around the circle.
        // The next three, the speeds, are going to store how fast the sprites are moving in the circle.They are each different values, so you can see them as they overlap.
        // The last one, the distance, is the radius of the circle that our sprites will travel around.
        private float blueAngle = 0;
        private float greenAngle = 0;
        private float redAngle = 0;
        private float blueSpeed = 0.025f;
        private float greenSpeed = 0.017f;
        private float redSpeed = 0.022f;
        private float distance = 100;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            shuttle = Content.Load<Texture2D>("shuttle");
            earth = Content.Load<Texture2D>("earth");
            font = Content.Load<SpriteFont>("Score");

            var texture = Content.Load<Texture2D>("SmileyWalk");
            animatedSprite = new AnimatedSprite(texture, 4, 4);

            blue = Content.Load<Texture2D>("blue");
            green = Content.Load<Texture2D>("green");
            red = Content.Load<Texture2D>("red");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            score++;
            shuttleAngle += 0.01f;
            animatedSprite.Update();

            // Blending
            blueAngle += blueSpeed;
            greenAngle += greenSpeed;
            redAngle += redSpeed;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //_spriteBatch.Draw(earth, new Vector2(400, 240), Color.White);
            //DrawRotatingShuttle();
            //_spriteBatch.DrawString(font, "Score: " + score, new Vector2(100, 100), Color.Black);
            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            DrawBlendingResources();
            _spriteBatch.End();

            //animatedSprite.Draw(_spriteBatch, new Vector2(400, 200));
            base.Draw(gameTime);
        }

        private void DrawBlendingResources()
        {
            // Calculate the actual locations of the sprites from their angles (from polar to Cartesian coordinates)
            var bluePosition = new Vector2((float)Math.Cos(blueAngle) * distance, (float)Math.Sin(blueAngle) * distance);
            var greenPosition = new Vector2((float)Math.Cos(greenAngle) * distance, (float)Math.Sin(greenAngle) * distance);
            var redPosition = new Vector2((float)Math.Cos(redAngle) * distance, (float)Math.Sin(redAngle) * distance);

            var center = new Vector2(300, 140);

            _spriteBatch.Draw(blue, center + bluePosition, Color.White);
            _spriteBatch.Draw(green, center + greenPosition, Color.White);
            _spriteBatch.Draw(red, center + redPosition, Color.White);
        }

        private void DrawRotatingShuttle()
        {
            var location = new Vector2(400, 240); // Place where to draw the sprite.
            var sourceRectangle = new Rectangle(0, 0, shuttle.Width, shuttle.Height); // What part of the texture we want to draw. The whole texture in this case.
            // ReSharper disable PossibleLossOfFraction
            var origin = new Vector2(shuttle.Width / 2, shuttle.Height / 2); // Location of the origin of the rotation.

            _spriteBatch.Draw(shuttle, location, sourceRectangle, Color.White, shuttleAngle, origin, 1.0f, SpriteEffects.None, 1);
        }
    }
}
