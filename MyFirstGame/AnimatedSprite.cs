﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame;

public class AnimatedSprite
{
    public Texture2D Texture { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    private int currentFrame;
    private readonly int totalFrames;

    public AnimatedSprite(Texture2D texture, int rows, int columns)
    {
        Texture = texture;
        Rows = rows;
        Columns = columns;
        currentFrame = 0;
        totalFrames = Rows * Columns;
    }

    public void Update()
    {
        currentFrame++;
        if (currentFrame == totalFrames)
        {
            currentFrame = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        int width = Texture.Width / Columns;
        int height = Texture.Height / Rows;
        int row = currentFrame / Columns;
        int col = currentFrame % Columns;

        var source = new Rectangle(x: width * col, y: height * row, width, height);
        var destination = new Rectangle(x: (int)location.X, y: (int)location.Y, width, height);

        spriteBatch.Begin();
        spriteBatch.Draw(Texture, destinationRectangle: destination, sourceRectangle: source, Color.White);
        spriteBatch.End();
    }
}