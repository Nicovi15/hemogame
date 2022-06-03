using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter2D //: MonoBehaviour
{
    public static GameObject[] Cut(Plane plane, GameObject objectToCut)
    {
        //Get the current mesh and its verts and tris
        Mesh mesh = objectToCut.GetComponent<MeshFilter>().mesh;
        var a = mesh.GetSubMesh(0);
        Cuttable2D cuttable = objectToCut.GetComponent<Cuttable2D>();

        if (cuttable == null)
        {
            throw new NotSupportedException("Cannot slice non sliceable object, add the sliceable script to the object or inherit from sliceable to support slicing");
        }

        //Create left and right slice of hollow object
        Cuts2DMetadata slicesMeta = new Cuts2DMetadata(plane, mesh, cuttable.ShareVertices, cuttable.SmoothVertices);

        GameObject positiveObject = CreateMeshGameObject(objectToCut);
        positiveObject.name = string.Format("{0}_positive", objectToCut.name);

        GameObject negativeObject = CreateMeshGameObject(objectToCut);
        negativeObject.name = string.Format("{0}_negative", objectToCut.name);

        var positiveSideMeshData = slicesMeta.PositiveSideMesh;
        var negativeSideMeshData = slicesMeta.NegativeSideMesh;

        //positiveSideMeshData = ajoutFond(positiveSideMeshData);
        //negativeSideMeshData = ajoutFond(negativeSideMeshData);

        positiveObject.GetComponent<MeshFilter>().mesh = positiveSideMeshData;
        negativeObject.GetComponent<MeshFilter>().mesh = negativeSideMeshData;

        SetupColliders(ref positiveObject, positiveSideMeshData);
        SetupColliders(ref negativeObject, negativeSideMeshData);

        return new GameObject[] { positiveObject, negativeObject };
    }

    /// <summary>
    /// Creates the default mesh game object.
    /// </summary>
    /// <param name="originalObject">The original object.</param>
    /// <returns></returns>
    private static GameObject CreateMeshGameObject(GameObject originalObject)
    {
        var originalMaterial = originalObject.GetComponent<MeshRenderer>().materials;

        GameObject meshGameObject = new GameObject();
        Cuttable2D originalCuttable = originalObject.GetComponent<Cuttable2D>();

        meshGameObject.AddComponent<MeshFilter>();
        meshGameObject.AddComponent<MeshRenderer>();
        Cuttable2D cuttable = meshGameObject.AddComponent<Cuttable2D>();
        cuttable.ShareVertices = originalCuttable.ShareVertices;
        cuttable.SmoothVertices = originalCuttable.SmoothVertices;

        meshGameObject.GetComponent<MeshRenderer>().materials = originalMaterial;

        meshGameObject.transform.localScale = originalObject.transform.localScale;
        meshGameObject.transform.rotation = originalObject.transform.rotation;
        meshGameObject.transform.position = originalObject.transform.position;

        meshGameObject.tag = originalObject.tag;

        return meshGameObject;
    }

    /// <summary>
    /// Add mesh collider and rigid body to game object
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="mesh"></param>
    private static void SetupColliders(ref GameObject gameObject, Mesh mesh)
    {
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = ajoutFond(mesh);
        meshCollider.convex = true;
    }

    private static Mesh ajoutFond(Mesh m)
    {
        //List<Vector3> vertices;
        //List<Vector2> uv;
        //List<int> triangles;

        //vertices = new List<Vector3>();
        //uv = new List<Vector2>();
        //triangles = new List<int>();
        
        Vector3[] vertices;
        Vector2[] uv;
        int[] triangles;

        vertices = new Vector3[m.vertices.Length * 2];
        uv = new Vector2[m.uv.Length * 2];
        triangles = new int[m.triangles.Length * 2];

        //Debug.Log("init vertices : " + m.vertices.Length + " | nouveau : " + vertices.Length);
        int ik;
        for (ik = 0; ik < m.vertices.Length; ik++)
            vertices[ik] = m.vertices[ik];
            //vertices.Add(m.vertices[i]);

        for (int i = 0; i < m.uv.Length; i++)
            uv[i] = m.uv[i];
        //uv.Add(m.uv[i]);

        for (int i = 0; i < m.triangles.Length; i++)
            triangles[i] = m.triangles[i];
        //triangles.Add(m.triangles[i]);

        //ancien, nouveau
        IDictionary<int, int> memo = new Dictionary<int, int>();

        for (int i = 0; i < m.triangles.Length; i += 3)
        {
            //We need the verts in order so that we know which way to wind our new mesh triangles.
            Vector3 vert1 = m.vertices[m.triangles[i]];
            int vert1Index = m.triangles[i];
            Vector2 uv1 = m.uv[vert1Index];

            Vector3 vert2 = m.vertices[m.triangles[i+1]];
            int vert2Index = m.triangles[i+1];
            Vector2 uv2 = m.uv[vert2Index];

            Vector3 vert3 = m.vertices[m.triangles[i+2]];
            int vert3Index = m.triangles[i + 2];
            Vector2 uv3 = m.uv[vert3Index];

            if (!memo.ContainsKey(vert1Index))
            {
                vert1 += new Vector3(0, 0, 0.01f);
                vertices[ik] = vert1;
                memo.Add(vert1Index, ik);
                ik++;
            }

            if (!memo.ContainsKey(vert2Index))
            {
                vert2 += new Vector3(0, 0, 0.01f);
                vertices[ik] = vert2;
                memo.Add(vert2Index, ik);
                ik++;
            }

            if (!memo.ContainsKey(vert3Index))
            {
                vert3 += new Vector3(0, 0, 0.01f);
                vertices[ik] = vert3;
                memo.Add(vert3Index, ik);
                ik++;
            }

            triangles[m.triangles.Length + i] = memo[vert1Index];
            triangles[m.triangles.Length + i + 1] = memo[vert3Index];
            triangles[m.triangles.Length + i + 2] = memo[vert2Index];

         //   vert1 += new Vector3(0, 0, 0.5f);
         //   vert2 += new Vector3(0, 0, 0.5f);
         //   vert3 += new Vector3(0, 0, 0.5f);
         //
         //   //Debug.Log("135 : " + (m.triangles.Length + i));
         //   Debug.Log("135 : " + (ik));
         //   vertices[ik] = vert1;
         //   //Debug.Log("137 : " + (m.triangles.Length + i + 1));
         //   Debug.Log("137 : " + (ik + 1));
         //   vertices[ik + 1] = vert2;
         //   //Debug.Log("139 : " + (m.triangles.Length + i + 2));
         //   Debug.Log("139 : " + (ik + 2));
         //   vertices[ik + 2] = vert3;
         //   //vertices.Add(vert1);
         //   //vertices.Add(vert2);
         //   //vertices.Add(vert3);
         //   //
         //   //uv.Add(uv1);
         //   //uv.Add(uv2);
         //   //uv.Add(uv3);
         //
         //   triangles[ik] = m.triangles.Length + i;
         //   triangles[ik + 1] = m.triangles.Length + i + 2;
         //   triangles[ik + 2] = m.triangles.Length + i + 1;
         //
         //   ik += 3;
            //triangles.Add(m.triangles.Length + i);
            //triangles.Add(m.triangles.Length + i + 2);
            //triangles.Add(m.triangles.Length + i + 1);
        }

        //Debug.Log("init vertices : " + m.vertices.Length + " | nouveau : " + vertices.Length);
        //Debug.Log("--------------------------");
        //foreach(var x in vertices)
        //    Debug.Log(x);
        //Debug.Log("--------------------------");
        //Debug.Log("init triangles : " + m.triangles.Length + " | nouveau : " + triangles.Length);

        Mesh res = new Mesh();
        res.vertices = vertices;
        res.uv = uv;
        res.triangles = triangles;
        //res.vertices = vertices.ToArray();
        //res.uv = uv.ToArray();
        //res.triangles = triangles.ToArray();

        return res;
    }

}
