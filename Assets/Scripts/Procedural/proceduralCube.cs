using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralCube : MonoBehaviour
{
    [SerializeField] protected int gridWidth = 256;
    [SerializeField] protected int gridHeight = 256;

    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private Vector3[] vertices;
    private Vector2[] uvs;
    private int[] triangles;

    void Start()
    {
        mesh = new Mesh();

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        vertices = new Vector3[24];
        triangles = new int[36];
        uvs = new Vector2[24];

        // Assigns each vertex
        vertices[0] = new Vector3(gridWidth/2.0f, 0, gridWidth/2.0f);
        vertices[1] = new Vector3(0, 0, gridWidth/2.0f);
        vertices[2] = new Vector3(gridWidth/2.0f, gridWidth/2.0f, gridWidth/2.0f);
        vertices[3] = new Vector3(0, gridWidth/2.0f, gridWidth/2.0f);
        vertices[4] = new Vector3(gridWidth/2.0f, gridWidth/2.0f, 0);
        vertices[5] = new Vector3(0, gridWidth/2.0f, 0);
        vertices[6] = new Vector3(gridWidth/2.0f, 0, 0);
        vertices[7] = new Vector3(0, 0, 0);
        vertices[8] = new Vector3(gridWidth/2.0f, gridWidth/2.0f, gridWidth/2.0f);
        vertices[9] = new Vector3(0, gridWidth/2.0f, gridWidth/2.0f);
        vertices[10] = new Vector3(gridWidth/2.0f, gridWidth/2.0f, 0);
        vertices[11] = new Vector3(0, gridWidth/2.0f, 0);
        vertices[12] = new Vector3(gridWidth/2.0f, 0, 0);
        vertices[13] = new Vector3(gridWidth/2.0f, 0, gridWidth/2.0f);
        vertices[14] = new Vector3(0, 0, gridWidth/2.0f);
        vertices[15] = new Vector3(0, 0, 0);
        vertices[16] = new Vector3(0, 0, gridWidth/2.0f);
        vertices[17] = new Vector3(0, gridWidth/2.0f, gridWidth/2.0f);
        vertices[18] = new Vector3(0, gridWidth/2.0f, 0);
        vertices[19] = new Vector3(0, 0, 0);
        vertices[20] = new Vector3(gridWidth/2.0f, 0, 0);
        vertices[21] = new Vector3(gridWidth/2.0f, gridWidth/2.0f, 0);
        vertices[22] = new Vector3(gridWidth/2.0f, gridWidth/2.0f, gridWidth/2.0f);
        vertices[23] = new Vector3(gridWidth/2.0f, 0, gridWidth/2.0f);

        // sets the texture for every vertex
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(0, 1);
        uvs[3] = new Vector2(1, 1);
        uvs[4] = new Vector2(0, 1);
        uvs[5] = new Vector2(1, 1);
        uvs[6] = new Vector2(0, 1);
        uvs[7] = new Vector2(1, 1);
        uvs[8] = new Vector2(0, 0);
        uvs[9] = new Vector2(1, 0);
        uvs[10] = new Vector2(0, 0);
        uvs[11] = new Vector2(1, 0);
        uvs[12] = new Vector2(0, 0);
        uvs[13] = new Vector2(0, 1);
        uvs[14] = new Vector2(1, 1);
        uvs[15] = new Vector2(1, 0);
        uvs[16] = new Vector2(0, 0);
        uvs[17] = new Vector2(0, 1);
        uvs[18] = new Vector2(1, 1);
        uvs[19] = new Vector2(1, 0);
        uvs[20] = new Vector2(0, 0);
        uvs[21] = new Vector2(0, 1);
        uvs[22] = new Vector2(1, 1);
        uvs[23] = new Vector2(1, 0);

        // Assigns each triangle        
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 3;
        triangles[3] = 0;
        triangles[4] = 3;
        triangles[5] = 1;
        triangles[6] = 8;
        triangles[7] = 4;
        triangles[8] = 5;
        triangles[9] = 8;
        triangles[10] = 5;
        triangles[11] = 9;
        triangles[12] = 10;
        triangles[13] = 6;
        triangles[14] = 7;
        triangles[15] = 10;
        triangles[16] = 7;
        triangles[17] = 11;
        triangles[18] = 12;
        triangles[19] = 13;
        triangles[20] = 14;
        triangles[21] = 12;
        triangles[22] = 14;
        triangles[23] = 15;
        triangles[24] = 16;
        triangles[25] = 17;
        triangles[26] = 18;
        triangles[27] = 16;
        triangles[28] = 18;
        triangles[29] = 19;
        triangles[30] = 20;
        triangles[31] = 21;
        triangles[32] = 22;
        triangles[33] = 20;
        triangles[34] = 22;
        triangles[35] = 23;

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
