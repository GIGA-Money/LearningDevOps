using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;


/*
 creative notes:
 random water noices
 maybe some lizard people textures.

 make into a biome

 ask how to genearte a biome in console.
 */


class Program
{
    static void Main()
    {
        WorleyNoise worleyNoise = new WorleyNoise();
        worleyNoise.GenerateBitmap(512, 512, @"C:\worley_noise.bmp");
    }
}

public class WorleyNoise
{
    private List<Vector2> points;

    public WorleyNoise()
    {
        var random = new Random();
        points = new List<Vector2>();

        for (int i = 0; i < 250; i++)
        {
            points.Add(new Vector2((float)random.NextDouble(), (float)random.NextDouble()));
        }
    }

    public float Noise(float x, float y)
    {
        float minDistance = float.MaxValue;

        foreach (var point in points)
        {
            float dx = x - point.X;
            float dy = y - point.Y;
            float distance = dx * dx + dy * dy;

            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }

        return (float)Math.Sqrt(minDistance) / (float)Math.Sqrt(2);
    }

    public void GenerateBitmap(int width, int height, string path)
    {
        Bitmap bitmap = new Bitmap(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noiseValue = Noise((float)x / width, (float)y / height);
                int colorValue = 255 - (int)(255 * noiseValue);
                bitmap.SetPixel(x, y, Color.FromArgb(colorValue, colorValue, colorValue));
            }
        }

        bitmap.Save(path);
    }
}
