using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    Mesh mesh;
    void Start()
    {
        var meshFilter = gameObject.AddComponent<MeshFilter>();
        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = Resources.Load<Material>("Test");
        this.mesh = new Mesh();
        meshFilter.mesh = this.mesh;

        // https://catlikecoding.com/unity/tutorials/procedural-meshes/creating-a-mesh/
        // mesh.vertices = new Vector3[] {
        //     Vector3.zero,
        //     Vector3.up,
        //     Vector3.right,
        //     Vector3.up + Vector3.right,
        // };
        // mesh.triangles = new int[] {
        //     0, 1, 2,
        //     1, 3, 2,
        // };
        // mesh.normals = new Vector3[] {
        //     Vector3.back,
        //     Vector3.back,
        //     Vector3.back,
        //     Vector3.back,
        // };
        // mesh.uv = new Vector2[] {
        //     Vector2.zero,
        //     Vector2.up,
        //     Vector2.right,
        //     Vector3.up + Vector3.right,
        // };

        var vertices = new List<Vector3>(1000*1000);
        var triangles = new List<int>(1000*1000/2);
        var normals = new List<Vector3>(1000*1000);
        var uvs = new List<Vector2>(1000*1000);
        for (int x = 0; x < 1000; x++)
        {
            for (int y = 0; y < 1000; y++)
            {
                vertices.Add(new Vector3(x*0.1f, y*0.1f, Random.Range(0.0f, 1.0f)));
                normals.Add(Vector3.back);
                uvs.Add(new Vector3(x*0.1f/1000f, y*0.1f/1000f));
                if (y % 2 == 0 && x < 999 && y < 999)
                {
                    triangles.AddRange(new int[] {
                        TriangleIndexAt(x, y),
                        TriangleIndexAt(x+1, y),
                        TriangleIndexAt(x, y+1),
                    });
                    triangles.AddRange(new int[] {
                        TriangleIndexAt(x+1, y),
                        TriangleIndexAt(x+1, y+1),
                        TriangleIndexAt(x, y+1),
                    });
                }
            }
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
    }

    int TriangleIndexAt(int x, int y)
    {
        return y + x*1000;
    }

    void Update()
    {

    }
}
