using System.Collections;
using UnityEngine;

public static class Noise
{
    public static float[,] GerenateNoiseMap(int mapWidth, int mapHeight, float scale)
    {
        float[,] noiseMap = new float[mapHeight, mapWidth];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float sapmleY = y / scale;
                float sampleX = x / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sapmleY);
                noiseMap[x, y] = perlinValue;
            }
        }
        return noiseMap;
    }
}
