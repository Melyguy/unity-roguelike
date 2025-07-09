using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class noise
{

    public enum NormalizeMode {Local, Global};
    public static float[,] generateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset, NormalizeMode normalizeMode)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        float maxPossibleHeight = 0f;
        float amp = 1;
        float freq = 1;

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) - offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);

            maxPossibleHeight += amp;
            amp *= persistance;
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfwidth = mapWidth / 2f;
        float halfheigth = mapHeight / 2f;


        for (int y = 0; y < mapHeight; y ++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                amp = 1;
                freq = 1;
                float noiseheight = 0;
                for (int i =0; i < octaves; i++)
                {


                    float sampleX = (x- halfwidth + octaveOffsets[i].x) /scale * freq;
                    float sampleY = (y- halfheigth + octaveOffsets[i].y) /scale * freq;
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2  - 1;
                    noiseheight += perlinValue * amp;

                    amp *= persistance;
                    freq *= lacunarity;
                }
                if(noiseheight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseheight;
                }else if (noiseheight <minNoiseHeight)
                {
                    minNoiseHeight = noiseheight;
                }
                noiseMap[x, y] = noiseheight;
            }
        }


        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if(normalizeMode == NormalizeMode.Local)
                {
                    noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
                }
                else
                {
                    float normalizedHeight = (noiseMap[x, y] + 1f) / (2f * maxPossibleHeight/2f);
                    noiseMap[x, y] = Mathf.Clamp(normalizedHeight,0,int.MaxValue);
                }
            }
        }     
                return noiseMap;
    }

        
}
