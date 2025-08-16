using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [Header("Rock Settings")]
    public GameObject treePrefab;
    public int numberOfRocks = 100;
    public int randomSeed = 0; // Set to 0 to auto-generate a seed
    public int storedSeed = 0; // This will store the actual seed used

    public int randomRotation = 0;

    [Header("Mesh Settings")]
    public float surfaceOffset = 0.1f;

    private void Start()
    {
        SpawnRocks();
    }

    void SpawnRocks()
    {
        if (treePrefab == null)
        {
            Debug.LogWarning("Rock prefab not assigned!");
            return;
        }

        Mesh mesh = GetComponent<MeshFilter>()?.mesh;
        if (mesh == null)
        {
            Debug.LogWarning("No mesh found on this GameObject.");
            return;
        }

        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        // If no seed provided, generate one
        if (randomSeed == 0)
        {
            randomSeed = UnityEngine.Random.Range(1, int.MaxValue);
        }

        // Store the seed used
        storedSeed = randomSeed;

        System.Random rand = new System.Random(randomSeed);

        for (int i = 0; i < numberOfRocks; i++)
        {
            int index = rand.Next(vertices.Length);
            Vector3 localPos = vertices[index];
            Vector3 worldPos = transform.TransformPoint(localPos);
            Vector3 normal = transform.TransformDirection(normals[index]);



            // Random Y-axis rotation
            float yAngle = (float)(rand.NextDouble() * 360.0);
            Quaternion yRotation = Quaternion.Euler(0, yAngle, 0);

            // Apply rotation to the prefab
            Instantiate(treePrefab, worldPos + normal * surfaceOffset, yRotation, this.transform);

        }
    }
}
