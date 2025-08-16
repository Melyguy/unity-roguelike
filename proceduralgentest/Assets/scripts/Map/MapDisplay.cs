using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    // Drag your cel-shader material here in the inspector
    public Material celMaterial;

    // Make sure this matches the texture property used in your cel shader
    [SerializeField] private string textureProperty = "_MainTex";

    public void DrawTexture(Texture2D texture)
    {
        if (texture == null) return;

        // If you want the quad/plane preview with the cel material too:
        var mat = new Material(celMaterial != null ? celMaterial : textureRender.sharedMaterial);
        mat.SetTexture(textureProperty, texture);
        textureRender.sharedMaterial = mat;

        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();

        if (texture != null)
        {
            var mat = new Material(celMaterial != null ? celMaterial : meshRenderer.sharedMaterial);
            mat.SetTexture(textureProperty, texture);
            meshRenderer.sharedMaterial = mat;
        }

        meshCollider.sharedMesh = meshFilter.sharedMesh;
    }
}
