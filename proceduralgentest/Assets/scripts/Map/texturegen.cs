using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class texturegen
{
    private static string textureSavePath = "heightmap_texture.png";

    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();

        // Save the texture
        SaveTexture(texture);

        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        // Try to load existing texture first
        Texture2D loadedTexture = LoadTexture();
        if (loadedTexture != null)
        {
            return loadedTexture;
        }

        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }
        return TextureFromColorMap(colorMap, width, height);
    }

    private static void SaveTexture(Texture2D texture)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, textureSavePath);
        byte[] textureData = texture.EncodeToPNG();
        File.WriteAllBytes(fullPath, textureData);
    }

    private static Texture2D LoadTexture()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, textureSavePath);
        if (File.Exists(fullPath))
        {
            byte[] fileData = File.ReadAllBytes(fullPath);
            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            if (texture.LoadImage(fileData))
            {
                texture.filterMode = FilterMode.Point;
                texture.wrapMode = TextureWrapMode.Clamp;
                return texture;
            }
        }
        return null;
    }
}
