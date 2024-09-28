using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralTriangle : MonoBehaviour
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

        vertices = new Vector3[3];
        triangles = new int[3];
        uvs = new Vector2[3];

        vertices[0] = new Vector3(0, gridWidth/2.0f, 0);
        vertices[1] = new Vector3(gridWidth/2.0f, gridWidth/2.0f, 0);
        vertices[2] = new Vector3(0, gridWidth/2.0f, gridHeight/2.0f);

        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(gridWidth/2.0f, 0);
        uvs[2] = new Vector2(0, gridHeight/2.0f);

        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
