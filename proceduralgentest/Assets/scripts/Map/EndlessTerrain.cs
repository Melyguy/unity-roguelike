using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{
    const float scale = 1f;
    public LODInfo[] detailLevels;
    public static float maxViewDist;
    const float viewerMovetoUpdate = 25f;
    const float SQRviewerMovetoUpdate = viewerMovetoUpdate * viewerMovetoUpdate;

    public Transform Viewer;
    public Material mapMaterial;

    public static Vector2 viewerPos;
    Vector2 viewerpositionOld;
    static MapGenerator mapGenerator;
    int chunkSize;
    int chunksVisibleInDist;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    static List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

    private void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();

        maxViewDist = detailLevels[detailLevels.Length - 1].visibleDstThreshold;
        chunkSize = MapGenerator.mapChunkSize -1;
        chunksVisibleInDist = Mathf.RoundToInt( maxViewDist / chunkSize);
        UpdateVisibleChunks();
    }
    private void Update()
    {
        viewerPos = new Vector2(Viewer.position.x, Viewer.position.z);
        if((viewerpositionOld-viewerPos).sqrMagnitude > SQRviewerMovetoUpdate)
        {
            viewerpositionOld = viewerPos;
            UpdateVisibleChunks();
        }
    }

    void UpdateVisibleChunks()
    {
        for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++){
            terrainChunksVisibleLastUpdate[i].setVisible(false);
        }
        terrainChunksVisibleLastUpdate.Clear();

        int currChunkCordX = Mathf.RoundToInt(viewerPos.x / chunkSize);
        int currChunkCordY = Mathf.RoundToInt(viewerPos.y / chunkSize);


        for (int YOffset = -chunksVisibleInDist; YOffset <= chunksVisibleInDist; YOffset++)
        {
            for (int XOffset = -chunksVisibleInDist; XOffset <= chunksVisibleInDist; XOffset++)
            {
                Vector2 ViewChunkCord = new Vector2(currChunkCordX + XOffset, currChunkCordY + YOffset);

                if (terrainChunkDictionary.ContainsKey(ViewChunkCord))
                {
                    terrainChunkDictionary[ViewChunkCord].UpdateTerrainChunk();

                }else
                {
                    terrainChunkDictionary.Add(ViewChunkCord, new TerrainChunk(ViewChunkCord, chunkSize, detailLevels, transform, mapMaterial));
                    


                }
            }

        }
    }

    public class TerrainChunk
    {
        GameObject meshObject;
        Vector2 position;
        Bounds bounds;

        MapData mapData;
        MeshRenderer meshRenderer;
        MeshFilter meshFilter;
        LODInfo[] detailLevels;
        LODMesh[] lodMeshes;
        bool mapDataReceived;
        int previousLODIndex = -1;

        public TerrainChunk(Vector2 coord, int size, LODInfo[] detailLevels, Transform parent, Material material)
        {
            this.detailLevels = detailLevels;

            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 PositionV3 = new Vector3(position.x, 0, position.y);

            meshObject = new GameObject("Terrain Chunk" + position);
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshRenderer.material = material;
            meshObject.transform.position = PositionV3 * scale;
            meshObject.transform.parent = parent;
            meshObject.transform.localScale = Vector3.one * scale;
            setVisible(false);

            lodMeshes = new LODMesh[detailLevels.Length];
            for(int i = 0; i < detailLevels.Length; i++)
            {
                lodMeshes[i] = new LODMesh(detailLevels[i].lod, UpdateTerrainChunk);
            }

            mapGenerator.RequestMapData(position, OnMapDataReceived);
        }

        void OnMapDataReceived(MapData mapData)
        {
            this.mapData = mapData;
            mapDataReceived = true;

            Texture2D texture = texturegen.TextureFromColorMap(mapData.colorMap, MapGenerator.mapChunkSize, MapGenerator.mapChunkSize);
            meshRenderer.material.mainTexture = texture;

            UpdateTerrainChunk();

        }

        public void UpdateTerrainChunk()
        {
            if (mapDataReceived)
            {


                float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPos));
                bool visible = viewerDstFromNearestEdge <= maxViewDist;

                if (visible)
                {
                    int lodIndex = 0;
                    for (int i = 0; i < detailLevels.Length - 1; i++)
                    {
                        if (viewerDstFromNearestEdge > detailLevels[i].visibleDstThreshold)
                        {
                            lodIndex = i + 1;


                        }
                        else
                        {
                            break;
                        }
                    }
                    if (lodIndex != previousLODIndex)
                    {
                        LODMesh lodMesh = lodMeshes[lodIndex];
                        if (lodMesh.hasMesh)
                        {
                            previousLODIndex = lodIndex;
                            Debug.Log($"Assigning mesh to chunk {position}");
                            meshFilter.mesh = lodMesh.mesh;
                        }

                        else if (!lodMesh.hasRequestedMesh)
                        {
                            lodMesh.RequestMesh(mapData);
                            Debug.Log($"Chunk location{position} {lodMesh.hasRequestedMesh}");
                        }
                    }
                    terrainChunksVisibleLastUpdate.Add(this);
                }

                setVisible(visible);


            }
        }
        public void setVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }

        public bool isVisible()
        {
            return meshObject.activeSelf;
        }

    }
    class LODMesh
    {
        public Mesh mesh;
        public bool hasRequestedMesh;
        public bool hasMesh;
        int lod;
        System.Action updateCallback;

        public LODMesh(int lod, System.Action updateCallback)
        {
            this.lod = lod;
            this.updateCallback = updateCallback;
        }
        void onMeshDataReceived(MeshData meshData)
        {
            Debug.Log($"Mesh received for LOD {lod}");
            mesh = meshData.CreateMesh();
            hasMesh = true;
            updateCallback();
        }

        public void RequestMesh(MapData mapData)
        {

            hasRequestedMesh = true;
            mapGenerator.RequestMeshData(mapData, lod, onMeshDataReceived);

        }
    }

    [System.Serializable]
    public struct LODInfo
    {
        public int lod;
        public float visibleDstThreshold;
    }
}
