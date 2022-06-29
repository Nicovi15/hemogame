using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mouse : MonoBehaviour
{
    public Vector3 startingPos;
    public Vector3 endingPos;
    public Camera cam;
    public LineRenderer LR;

    public Plane plan;

    public GameObject image;

    public Ray r;
    public Vector3 enterPoint;

    public List<GameObject> gos = new List<GameObject>();

    public MeshCollider mc;

    public float distCut;

    public Vector3 worldPosition;

    public float y;

    [SerializeField]
    LayerMask pinLayer;

    Pin p1 = null;
    Pin p2 = null;

    public bool coupe = false;

    Plane pDebug;

    public
    GameObject t = null;

    public float largeurCoupe = 0.001f;

    public bool canCoupe = true;

    [SerializeField]
    SystemeDecoupe SD;

    public float timeToReachTarget = 4f;

    AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        plan = new Plane();
        r = new Ray();
        enterPoint = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        //float width = LR.startWidth;
        //LR.material.mainTextureScale = new Vector2(1f / width, 1.0f);
        //Renderer rend = GetComponent<Renderer>();
        //rend.material.mainTextureScale =
        //    new Vector2(Vector2.Distance(LR.GetPosition(0), LR.GetPosition(LR.positionCount - 1)) / LR.widthMultiplier,
        //        1);

        if (!canCoupe)
            return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.y = cam.transform.position.y;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        //Debug.Log(mousePos);

        RaycastHit hit;
        Ray ray;
        ray = cam.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out hit))
        if (Physics.Raycast(ray, out hit, 9000, pinLayer))
        {
            Pin p = hit.collider.GetComponent<Pin>();
            p.highlight();
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Plane plane = new Plane(Vector3.up, y);
            //float distance;
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (plane.Raycast(ray, out distance))
            //{
            //    worldPosition = ray.GetPoint(distance);
            //}
            //
            //startingPos = worldPosition;

            ray = cam.ScreenPointToRay(Input.mousePosition);

            //if (Physics.Raycast(ray, out hit))
            if (Physics.Raycast(ray, out hit, 9000, pinLayer))
            {
                Pin p = hit.collider.GetComponent<Pin>();
                p.select();
                startingPos = p.getPinPos();
                p1 = p;
                LR.positionCount = 1;
                LR.SetPosition(0, startingPos + new Vector3(0, 0.01f, 0));
            }

            
            //startingPos = mousePos;
            //startingPos.y = 0;
            //LR.positionCount = 1;
            //LR.SetPosition(0, startingPos + new Vector3(0, 0.01f, 0));
        }

        if (Input.GetMouseButton(0))
        {
            if (p1 == null)
                return;
            Plane plane = new Plane(Vector3.up, y);
            float distance;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }

            Vector3 curPos = worldPosition;
            //Vector3 curPos = mousePos;
            //curPos.y = 6;
            LR.positionCount = 2;
            LR.SetPosition(0, startingPos + new Vector3(0, 0.01f, 0));
            LR.SetPosition(1, curPos + new Vector3(0, 0.01f, 0));
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (p1 == null)
                return;

            Plane plane = new Plane(Vector3.up, y);
            float distance;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }

            endingPos = worldPosition;
            //endingPos = mousePos;
            //endingPos.z = 0;

            ray = cam.ScreenPointToRay(Input.mousePosition);

            //if (Physics.Raycast(ray, out hit))
            if (Physics.Raycast(ray, out hit, 9000, pinLayer))
            {
                Pin p = hit.collider.GetComponent<Pin>();
                p.select();
                endingPos = p.getPinPos();
                p2 = p;
            }
            else
            {
                LR.positionCount = 0;
                p1.unselect();
                p1 = null;
                return;

            }


            LR.positionCount = 2;
            LR.SetPosition(0, startingPos + new Vector3(0, 0.01f, 0));
            LR.SetPosition(1, endingPos + new Vector3(0, 0.01f, 0));

            //Creation du plan
            //Vector3 side1 = cam.transform.position - startingPos;
            //Vector3 side2 = cam.transform.position - endingPos;
            Vector3 side1 = (startingPos + new Vector3(0, 1, 0)) - startingPos;
            Vector3 side2 = (startingPos + new Vector3(0, 1, 0)) - endingPos;

            Vector3 normal = Vector3.Cross(side1, side2).normalized;

           //Vector3 transformedNormal = ((Vector3)(image.transform.localToWorldMatrix.transpose * normal)).normalized;
           //
           //Vector3 transformedStartingPoint = new Vector3();
           //
           //RaycastHit hit;
           //r = new Ray(startingPos, (endingPos - startingPos).normalized);
           //if (Physics.Raycast(r, out hit, Mathf.Infinity))
           //{
           //    transformedStartingPoint = image.transform.InverseTransformPoint(hit.point);
           //    Debug.Log("Did Hit " + hit.point);
           //    enterPoint = transformedStartingPoint;
           //
           //}
           //
           ////plan = new Plane(transformedNormal, transformedStartingPoint);
           ////plan = new Plane(normal, startingPos);
           ////plan = new Plane(transformedNormal, startingPos);

            mc = this.gameObject.AddComponent<MeshCollider>();
            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[8];
            int[] triangles = new int[12];

            //vertices[0] = startingPos + new Vector3(0, 0, 0.5f) - new Vector3(0, 0.01f, 0);
            //vertices[1] = startingPos - new Vector3(0, 0, 0.5f) - new Vector3(0, 0.01f, 0);
            //vertices[2] = endingPos + new Vector3(0, 0, 0.5f) - new Vector3(0, 0.01f, 0);
            //vertices[3] = endingPos - new Vector3(0, 0, 0.5f) - new Vector3(0, 0.01f, 0);
            //
            //vertices[4] = startingPos + new Vector3(0, 0, 0.5f) + new Vector3(0,0.01f, 0);
            //vertices[5] = startingPos - new Vector3(0, 0, 0.5f) + new Vector3(0, 0.01f, 0); 
            //vertices[6] = endingPos + new Vector3(0, 0, 0.5f) + new Vector3(0, 0.01f, 0); 
            //vertices[7] = endingPos - new Vector3(0, 0, 0.5f) + new Vector3(0, 0.01f, 0); 

            //vertices[0] = startingPos + new Vector3(0, 0.5f,0) - new Vector3(0, 0, largeurCoupe);
            //vertices[1] = startingPos - new Vector3(0, 0.5f, 0) - new Vector3(0,  0, largeurCoupe);
            //vertices[2] = endingPos + new Vector3(0, 0.5f, 0) - new Vector3(0,  0, largeurCoupe);
            //vertices[3] = endingPos - new Vector3(0, 0.5f, 0) - new Vector3(0,  0, largeurCoupe);
            //
            //vertices[4] = startingPos + new Vector3(0, 0.5f, 0) + new Vector3(0,  0, largeurCoupe);
            //vertices[5] = startingPos - new Vector3(0, 0.5f, 0) + new Vector3(0,  0, largeurCoupe);
            //vertices[6] = endingPos + new Vector3(0, 0.5f, 0) + new Vector3(0, 0, largeurCoupe);
            //vertices[7] = endingPos - new Vector3(0, 0.5f, 0) + new Vector3(0,  0, largeurCoupe);

            vertices[0] = startingPos + (side1.normalized * largeurCoupe) - new Vector3(0, 0, largeurCoupe);
            vertices[1] = startingPos - (side1.normalized * largeurCoupe) - new Vector3(0, 0, largeurCoupe);
            vertices[2] = endingPos + (side1.normalized * largeurCoupe) - new Vector3(0, 0, largeurCoupe);
            vertices[3] = endingPos - (side1.normalized * largeurCoupe) - new Vector3(0, 0, largeurCoupe);

            vertices[4] = startingPos + (side1.normalized * largeurCoupe) + new Vector3(0, 0, largeurCoupe);
            vertices[5] = startingPos - (side1.normalized * largeurCoupe) + new Vector3(0, 0, largeurCoupe);
            vertices[6] = endingPos + (side1.normalized * largeurCoupe) + new Vector3(0, 0, largeurCoupe);
            vertices[7] = endingPos - (side1.normalized * largeurCoupe) + new Vector3(0, 0, largeurCoupe);

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 1;
            triangles[4] = 3;
            triangles[5] = 2;

            triangles[6] = 6;
            triangles[7] = 5;
            triangles[8] = 4;
            triangles[9] = 6;
            triangles[10] = 7;
            triangles[11] = 5;

            mesh.vertices = vertices;
            mesh.triangles = triangles;


            //LR.BakeMesh(mesh, true);
            mc.sharedMesh = mesh;
            mc.convex = true;
            mc.isTrigger = true;

            //GameObject[] cuts = Cutter2D.Cut(plan, image);
            //Destroy(image);

            StartCoroutine(decoupe(normal));

        }

        //DrawPlane(startingPos, pDebug.normal);

    }

    private void OnMouseDown()
    {
        startingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        endingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    IEnumerator decoupe(Vector3 normal, bool auto = false)
    {
        yield return new WaitForSeconds(0.1f);

        while(!coupe)
            yield return null;

        if (gos.Count <= 0)
        {
            LR.positionCount = 0;
            p1.unselect();
            p2.unselect();
            p1 = null;
            p2 = null;
            Destroy(mc);
            yield break;
        }

        SD.setCanChange(false);

        AS.Play();
        foreach (var x in gos)
        {
            if (x.CompareTag("Doigts"))
            {
                SD.doigtsCoupe.Add(x.GetComponent<DoigtsDissec>());
            }
            else if (x.CompareTag("CollisionDoigt"))
            {
                
            }
            else
            {
                Vector3 transformedNormal = ((Vector3)(x.transform.localToWorldMatrix.transpose * normal)).normalized;
                Vector3 transformedStartingPoint = new Vector3();

                RaycastHit hit;
                r = new Ray(startingPos, (endingPos - startingPos).normalized);
                //Physics.Raycast(r, out hit, Mathf.Infinity)
                //Physics.SphereCast(r, 1f, out hit, Mathf.Infinity)
                if (Physics.Raycast(r, out hit, Mathf.Infinity))
                {
                    transformedStartingPoint = x.transform.InverseTransformPoint(hit.point);
                    //Debug.Log("Did Hit " + hit.point);
                    enterPoint = transformedStartingPoint;
                }

                Plane plan = new Plane(transformedNormal, transformedStartingPoint);

                GameObject[] test = Cutter2D.Cut(plan, x);
                test[0].transform.SetParent(image.transform);
                test[1].transform.SetParent(image.transform);

                //test[0].transform.position += normal * distCut;
                //test[1].transform.position -= normal * distCut;
                if (test[0].GetComponent<MeshFilter>().mesh.triangles.Length == 0)
                    Destroy(test[0]);
                if (test[1].GetComponent<MeshFilter>().mesh.triangles.Length == 0)
                    Destroy(test[1]);

                Destroy(x);
            }

        }

        gos.Clear();
        Destroy(mc);
        yield return new WaitForSeconds(0.1f);

        Vector3 side1 = (startingPos + new Vector3(0,1,0)) - startingPos;
        Vector3 side2 = (startingPos + new Vector3(0, 1, 0)) - endingPos;

        Vector3 normal2 = Vector3.Cross(side1, side2).normalized;

        Plane p = new Plane(normal2, startingPos);
        pDebug = p;
        //Plane p = new Plane(transformedNormal, transformedStartingPoint);

        for (int i = 0; i < image.transform.childCount; i++)
        {
            Transform child = image.transform.GetChild(i);
            //Vector3 y = child.TransformPoint(child.GetComponent<MeshFilter>().mesh.bounds.center);
            Vector3 y = child.GetComponent<MeshCollider>().bounds.center;
            //Vector3 y = child.GetComponent<Renderer>().bounds.center;
            Vector3 y1 = y + normal2 * 5;
            Vector3 y2 = y - normal2 * 5;
            //Debug.Log(i +" " + p.GetDistanceToPoint(y));
            //Debug.Log(i + " " + y1);
            //Debug.Log(i + " " + y2);
            //Debug.Log(i+" " + (p.GetSide(y)));
            //Debug.Log(i + " " + (p.GetSide(y1)));
            //Debug.Log(i + " " + (p.GetSide(y2)));

            //if(p.GetDistanceToPoint(y) != 0)
            if (p.GetDistanceToPoint(y) > 0.001 || p.GetDistanceToPoint(y) < -0.001)
            {
                if (p.GetSide(y))
                    child.position += normal2 * distCut;
                else
                    child.position -= normal2 * distCut;

                child.position = new Vector3(child.position.x, image.transform.position.y, child.position.z);
            }
            else
            {
                child.position += normal2 * distCut;
                child.GetComponent<Cuttable2D>().correctionNormal(normal2, distCut);
            }
            

            //if (p.GetSide(child.TransformPoint(child.GetComponent<MeshFilter>().mesh.bounds.center)))
            //    child.position += normal * distCut;
            //else
            //    child.position -= normal * distCut;
            //
            //child.position = new Vector3(child.position.x, image.transform.position.y, child.position.z);
        }

        if (auto)
        {
            LR.positionCount = 0;
            StartCoroutine(SD.proceedCoupeAuto());
        }
        else
        {
            StartCoroutine(SD.proceedCoupe(p1.gameObject, p2.gameObject));
            LR.positionCount = 0;
            p1.unselect();
            p2.unselect();
            p1 = null;
            p2 = null;
        }
    }

    void DrawPlane(Vector3 position, Vector3 normal)
    {

        Vector3 v3;

        if (normal.normalized != Vector3.forward)
            v3 = Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude;
        else
            v3 = Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude; ;

        var corner0 = position + v3;
        var corner2 = position - v3;
        var q = Quaternion.AngleAxis(90.0f, normal);
        v3 = q * v3;
        var corner1 = position + v3;
        var corner3 = position - v3;

        Debug.DrawLine(corner0, corner2, Color.green);
        Debug.DrawLine(corner1, corner3, Color.green);
        Debug.DrawLine(corner0, corner1, Color.green);
        Debug.DrawLine(corner1, corner2, Color.green);
        Debug.DrawLine(corner2, corner3, Color.green);
        Debug.DrawLine(corner3, corner0, Color.green);
        Debug.DrawRay(position, normal, Color.red);

        if(t != null)
        {
            Debug.Log(pDebug.GetSide(t.GetComponent<MeshCollider>().bounds.center));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(r);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 13)
            gos.Add(other.gameObject);
    }


    public void decoupeAuto(Vector3 startingPos, Vector3 endingPos)
    {
        this.startingPos = startingPos;
        this.endingPos = endingPos;

        //Creation du plan
        //Vector3 side1 = cam.transform.position - startingPos;
        //Vector3 side2 = cam.transform.position - endingPos;
        Vector3 side1 = (startingPos + new Vector3(0, 1, 0)) - startingPos;
        Vector3 side2 = (startingPos + new Vector3(0, 1, 0)) - endingPos;

        Vector3 normal = Vector3.Cross(side1, side2).normalized;

        //Vector3 transformedNormal = ((Vector3)(image.transform.localToWorldMatrix.transpose * normal)).normalized;
        //
        //Vector3 transformedStartingPoint = new Vector3();
        //
        //RaycastHit hit;
        //r = new Ray(startingPos, (endingPos - startingPos).normalized);
        //if (Physics.Raycast(r, out hit, Mathf.Infinity))
        //{
        //    transformedStartingPoint = image.transform.InverseTransformPoint(hit.point);
        //    Debug.Log("Did Hit " + hit.point);
        //    enterPoint = transformedStartingPoint;
        //
        //}
        //
        ////plan = new Plane(transformedNormal, transformedStartingPoint);
        ////plan = new Plane(normal, startingPos);
        ////plan = new Plane(transformedNormal, startingPos);

        mc = this.gameObject.AddComponent<MeshCollider>();
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[8];
        int[] triangles = new int[12];

        //vertices[0] = startingPos + new Vector3(0, 0, 0.5f) - new Vector3(0, 0.01f, 0);
        //vertices[1] = startingPos - new Vector3(0, 0, 0.5f) - new Vector3(0, 0.01f, 0);
        //vertices[2] = endingPos + new Vector3(0, 0, 0.5f) - new Vector3(0, 0.01f, 0);
        //vertices[3] = endingPos - new Vector3(0, 0, 0.5f) - new Vector3(0, 0.01f, 0);
        //
        //vertices[4] = startingPos + new Vector3(0, 0, 0.5f) + new Vector3(0,0.01f, 0);
        //vertices[5] = startingPos - new Vector3(0, 0, 0.5f) + new Vector3(0, 0.01f, 0); 
        //vertices[6] = endingPos + new Vector3(0, 0, 0.5f) + new Vector3(0, 0.01f, 0); 
        //vertices[7] = endingPos - new Vector3(0, 0, 0.5f) + new Vector3(0, 0.01f, 0); 

        //vertices[0] = startingPos + new Vector3(0, 0.5f,0) - new Vector3(0, 0, largeurCoupe);
        //vertices[1] = startingPos - new Vector3(0, 0.5f, 0) - new Vector3(0,  0, largeurCoupe);
        //vertices[2] = endingPos + new Vector3(0, 0.5f, 0) - new Vector3(0,  0, largeurCoupe);
        //vertices[3] = endingPos - new Vector3(0, 0.5f, 0) - new Vector3(0,  0, largeurCoupe);
        //
        //vertices[4] = startingPos + new Vector3(0, 0.5f, 0) + new Vector3(0,  0, largeurCoupe);
        //vertices[5] = startingPos - new Vector3(0, 0.5f, 0) + new Vector3(0,  0, largeurCoupe);
        //vertices[6] = endingPos + new Vector3(0, 0.5f, 0) + new Vector3(0, 0, largeurCoupe);
        //vertices[7] = endingPos - new Vector3(0, 0.5f, 0) + new Vector3(0,  0, largeurCoupe);

        vertices[0] = startingPos + (side1.normalized * largeurCoupe) - new Vector3(0, 0, largeurCoupe);
        vertices[1] = startingPos - (side1.normalized * largeurCoupe) - new Vector3(0, 0, largeurCoupe);
        vertices[2] = endingPos + (side1.normalized * largeurCoupe) - new Vector3(0, 0, largeurCoupe);
        vertices[3] = endingPos - (side1.normalized * largeurCoupe) - new Vector3(0, 0, largeurCoupe);

        vertices[4] = startingPos + (side1.normalized * largeurCoupe) + new Vector3(0, 0, largeurCoupe);
        vertices[5] = startingPos - (side1.normalized * largeurCoupe) + new Vector3(0, 0, largeurCoupe);
        vertices[6] = endingPos + (side1.normalized * largeurCoupe) + new Vector3(0, 0, largeurCoupe);
        vertices[7] = endingPos - (side1.normalized * largeurCoupe) + new Vector3(0, 0, largeurCoupe);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        triangles[6] = 6;
        triangles[7] = 5;
        triangles[8] = 4;
        triangles[9] = 6;
        triangles[10] = 7;
        triangles[11] = 5;

        mesh.vertices = vertices;
        mesh.triangles = triangles;


        //LR.BakeMesh(mesh, true);
        mc.sharedMesh = mesh;
        mc.convex = true;
        mc.isTrigger = true;

        //GameObject[] cuts = Cutter2D.Cut(plan, image);
        //Destroy(image);

        StartCoroutine(decoupe(normal, true));
    }

    public IEnumerator traceAuto(Vector3 startingPos, Vector3 endingPos)
    {
        LR.positionCount = 1;
        LR.SetPosition(0, startingPos + new Vector3(0, 0.01f, 0));
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / timeToReachTarget;
            LR.positionCount = 2;
            LR.SetPosition(0, startingPos + new Vector3(0, 0.01f, 0));
            LR.SetPosition(1, Vector3.Lerp(startingPos, endingPos, t) + new Vector3(0, 0.01f, 0));
            yield return null;
        } 

        LR.positionCount = 2;
        LR.SetPosition(0, startingPos + new Vector3(0, 0.01f, 0));
        LR.SetPosition(1, endingPos + new Vector3(0, 0.01f, 0));

        decoupeAuto(startingPos, endingPos);
        yield return null;
    }

}
