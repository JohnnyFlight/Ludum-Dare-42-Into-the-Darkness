using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Creates a mesh that follows the player and updates texture coordinates to show movement
public class Floor : MonoBehaviour
{
    private Vector2 _position;

    public Vector2 CellSize = new Vector2(1f, 1f);
    public Vector2Int GridSize = new Vector2Int(50, 50);

    public float noiseScale = 0.2f;

    public Material Texture;

    public GameObject Track;

    // Use this for initialization
    void Start()
    {
        _position = new Vector2(0f, 0f);

        CreateMesh(CellSize, GridSize);
    }
    
    //  TODO: Have a game object you can attach to track automatically?
    public void UpdatePosition(Vector2 pos)
    {    
        this.transform.position = new Vector2(Track.transform.position.x - (Track.transform.position.x % CellSize.x) + CellSize.x / 2, Track.transform.position.y - (Track.transform.position.y % CellSize.y) + CellSize.y / 2);
        //this.transform.position = new Vector2(Track.transform.position.x, Track.transform.position.y);

        GetComponent<MeshFilter>().mesh.uv = GenerateUVs(new Vector2(FloorNegativeFix(Track.transform.position.x), FloorNegativeFix(Track.transform.position.y)), GridSize);

        Debug.Log($"original: {Track.transform.position.x} rounded: {Mathf.Floor(Track.transform.position.x)}");
    }

    float FloorNegativeFix(float x)
    {
        if (x < 0f) return Mathf.Floor(x) + 1;

        return Mathf.Floor(x);
    }

    //  From: https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    Vector2[] GenerateUVs(Vector2 position, Vector2Int gridSize)
    {
        List<Vector2> uvs = new List<Vector2>();

        //  For each cell create 4 verts
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                float noise = Mathf.PerlinNoise(((float)i + position.x) / gridSize.x / noiseScale, ((float)j + position.y) / gridSize.y / noiseScale);
                Vector2 uvBase = NoiseToUV(noise);
                
                //  Bottom left
                uvs.Add(uvBase);

                //  Bottom right
                uvs.Add(uvBase + new Vector2(0.5f, 0));

                //  Top right
                uvs.Add(uvBase + new Vector2(0.5f, 0.5f));

                //  Top left
                uvs.Add(uvBase + new Vector2(0, 0.5f));
            }
        }

        return uvs.ToArray();
    }

    private void CreateMesh(Vector2 cellSize, Vector2Int gridSize)
    {
        //  Attach new Mesh filter
        Mesh mesh = new Mesh();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
        meshRenderer.material = Texture;

        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<Vector3> normals = new List<Vector3>();
        List<int> tris = new List<int>();

        Vector2 offset = new Vector2(cellSize.x * gridSize.x, cellSize.y * gridSize.y) / -2;

        //  For each cell create 4 verts
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                float noise = Mathf.PerlinNoise((float)i / gridSize.x / noiseScale, (float)j / gridSize.y / noiseScale);
                Vector2 uvBase = NoiseToUV(noise);
                
                //  Bottom left
                verts.Add(new Vector3(i * cellSize.x, j * cellSize.y) + (Vector3)offset);
                uvs.Add(uvBase);

                //  Bottom right
                verts.Add(new Vector3((i + 1) * cellSize.x, j * cellSize.y) + (Vector3)offset);
                uvs.Add(uvBase + new Vector2(0.5f, 0));

                //  Top right
                verts.Add(new Vector3((i + 1) * cellSize.x, (j + 1) * cellSize.y) + (Vector3)offset);
                uvs.Add(uvBase + new Vector2(0.5f, 0.5f));

                //  Top left
                verts.Add(new Vector3(i * cellSize.x, (j + 1) * cellSize.y) + (Vector3)offset);
                uvs.Add(uvBase +  new Vector2(0, 0.5f));

                //  Add normals
                normals.Add(new Vector3(0, 0, -1));
                normals.Add(new Vector3(0, 0, -1));
                normals.Add(new Vector3(0, 0, -1));
                normals.Add(new Vector3(0, 0, -1));

                //  Add triangles
                tris.Add(i * gridSize.x * 4 + j * 4 + 1);
                tris.Add(i * gridSize.x * 4 + j * 4 + 0);
                tris.Add(i * gridSize.x * 4 + j * 4 + 2);

                tris.Add(i * gridSize.x * 4 + j * 4 + 3);
                tris.Add(i * gridSize.x * 4 + j * 4 + 2);
                tris.Add(i * gridSize.x * 4 + j * 4 + 0);
            }
        }

        mesh.vertices = verts.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.normals = normals.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Track == null) return;

        UpdatePosition(Track.transform.position);
    }

    Vector2 NoiseToUV(float n)
    {
        if (n < 0.25f) return new Vector2(0f, 0f);
        if (n < 0.5f) return new Vector2(0f, 0.5f);
        if (n < 0.75f) return new Vector2(0.5f, 0f);
        return new Vector2(0.5f, 0.5f);
    }
}
