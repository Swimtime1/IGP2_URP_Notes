using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralPyramid : MonoBehaviour
{
    [SerializeField] protected int gridWidth = 256;
    [SerializeField] protected int gridHeight = 256;

    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private Vector3[] vertices;
    private Vector2[] uvs;
    private int[] triangles;

    public void TransformToPyramid()
    {
        mesh = new Mesh();

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        vertices = new Vector3[16];
        triangles = new int[36];
        uvs = new Vector2[16];

        // Assigns each vertex
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(gridWidth/2.0f, 0, 0);
        vertices[2] = new Vector3(0, 0, gridWidth/2.0f);
        vertices[3] = new Vector3(gridWidth/2.0f, 0, gridWidth/2.0f);
        vertices[4] = new Vector3(0, 0, 0);
        vertices[5] = new Vector3(gridWidth/4.0f, gridWidth/2.0f, gridWidth/4.0f);
        vertices[6] = new Vector3(gridWidth/2.0f, 0, 0);
        vertices[7] = new Vector3(gridWidth/2.0f, 0, 0);
        vertices[8] = new Vector3(gridWidth/4.0f, gridWidth/2.0f, gridWidth/4.0f);
        vertices[9] = new Vector3(gridWidth/2.0f, 0, gridWidth/2.0f);
        vertices[10] = new Vector3(0, 0, gridWidth/2.0f);
        vertices[11] = new Vector3(gridWidth/4.0f, gridWidth/2.0f, gridWidth/4.0f);
        vertices[12] = new Vector3(0, 0, 0);
        vertices[13] = new Vector3(gridWidth/2.0f, 0, gridWidth/2.0f);
        vertices[14] = new Vector3(gridWidth/4.0f, gridWidth/2.0f, gridWidth/4.0f);
        vertices[15] = new Vector3(0, 0, gridWidth/2.0f);

        // sets the texture for the base
        uvs[0] = new Vector2(0, 1);
        uvs[1] = new Vector2(1, 1);
        uvs[2] = new Vector2(0, 0);
        uvs[3] = new Vector2(1, 0);

        // sets the texture for every side
        for(int i = 4; i < 16; i++)
        {
            // assigns bottom left
            if((i % 3) == 1) { uvs[i] = new Vector2(0, 0); }

            // assigns top
            else if((i % 3) == 2) { uvs[i] = new Vector2(0.5f, 0.5f); }

            // assigns bottom right
            else { uvs[i] = new Vector2(1, 0); }
        }

        // Assigns each triangle for the base
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 3;
        triangles[4] = 2;
        triangles[5] = 1; 
        
        // Assigns each triangle for the sides
        for(int i = 6; i < 18; i++) { triangles[i] = (i - 2); }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
