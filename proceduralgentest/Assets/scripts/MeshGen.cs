using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGen
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float HeightMultiplier, AnimationCurve heightMapCurve, int levelOfDetail)
    {
        AnimationCurve heightCurve = new AnimationCurve(heightMapCurve.keys); 
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        float topleftX = (width - 1) / -2f;
        float topleftZ = (width - 1) / 2f;
        int meshSimplificationIncrement = (levelOfDetail == 0)?1:levelOfDetail * 2;
        int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;


        MeshData Meshdata = new MeshData(width, height);
        int vertexIndex = 0;

        for (int y = 0; y < height; y += meshSimplificationIncrement)
        {
            for (int x = 0; x < width; x += meshSimplificationIncrement)
            {
                lock (heightCurve)
                {
                    Meshdata.vertices[vertexIndex] = new Vector3(topleftX + x, heightMapCurve.Evaluate(heightMap[x, y]) * HeightMultiplier, topleftZ - y);
                }
                    Meshdata.UVS[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                if(x < width -1 && y <height -1)
                {
                    Meshdata.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    Meshdata.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);    
                }
                vertexIndex++;
            }
        }
        return Meshdata;
    }
}


public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] UVS;
    int triangleIndex;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        UVS = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    } 
    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;

    }
    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = UVS;
        mesh.RecalculateNormals();
        return mesh;
        
    }
}