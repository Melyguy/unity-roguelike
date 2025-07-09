using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    public void DrawTexture(Texture2D texture)
    {
        if (texture != null)
        {
            Material material = new Material(textureRender.sharedMaterial);
            material.mainTexture = texture;
            textureRender.sharedMaterial = material;
            textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
        }
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        if (texture != null)
        {
            Material material = new Material(meshRenderer.sharedMaterial);
            material.mainTexture = texture;
            meshRenderer.sharedMaterial = material;
        }
        meshCollider.sharedMesh = meshFilter.sharedMesh;
    }
}
