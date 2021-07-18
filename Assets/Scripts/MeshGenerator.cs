using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    public int index = 0;

    Mesh mesh;
    Vector3[] vertices;
    Color[] colors;
    int[] triangles;
    public Gradient gradient;

    public int xSize = 20;

    public int zSize = 20;

    int maxDist = 0;

    GameObject player;

    float minHeight;

    float maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        if (zSize == xSize)
        {
            maxDist = xSize * 4;
        }
        else
        {
            if (zSize > xSize)
            {
                maxDist = zSize * 4;
            }
            else
            {
                maxDist = xSize * 4;
            }
        }

        mesh = new Mesh();

        player = GameObject.Find("Player").gameObject;

        GetComponent<MeshFilter>().mesh = mesh;

        if (TryGetComponent<MeshCollider>(out MeshCollider meshCollider))
        {
            meshCollider.sharedMesh = mesh;
        }

        CreatShape();

        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distToPlayer >= maxDist)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distToPlayer >= maxDist)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void UpdateColors()
    {
        colors = new Color[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minHeight, maxHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }
    }
    public void CreatShape()
    {
        if (mesh == null)
        {
            mesh = new Mesh();

            GetComponent<MeshFilter>().mesh = mesh;

            if (TryGetComponent<MeshCollider>(out MeshCollider meshCollider))
            {
                meshCollider.sharedMesh = mesh;
            }
        }
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise((x + transform.position.x) * .3f, (z + transform.position.z) * .3f) * 2;
                vertices[i] = new Vector3(x, y, z);
                i++;
                if (y > maxHeight)
                {
                    maxHeight = y;
                }
                if (y < minHeight)
                {
                    minHeight = y;
                }
            }
        }

        int vert = 0;

        int tris = 0;

        triangles = new int[xSize * zSize * 6];

        for (int z = 0; z < zSize; z++)
        {

            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }

        UpdateColors();

        UpdatdMesh();
    }

    void UpdatdMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;

        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        mesh.colors = colors;

        if (TryGetComponent<MeshCollider>(out MeshCollider meshCollider))
        {
            meshCollider.sharedMesh = mesh;
        }
    }

    private void OnDrawGizmos()
    {
        if (vertices != null)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.color = Color.red;

                Gizmos.DrawSphere(new Vector3(vertices[i].x + transform.position.x, vertices[i].y, vertices[i].z + transform.position.z), .1f);
            }
        }
    }
}
