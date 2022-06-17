using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFleur : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    public string titre;

    public Camera cam;

    [SerializeField]
    LayerMask doigtLayer;

    public Vector3 worldPosition;

    public float y;

    public GameObject currentDoigt;

    public List<GameObject> doigts;

    public bool canValide = false;

    public Vector3 centreImage;

    public float maxDist;

    [SerializeField]
    public GameObject image;

    public bool canMove = true;

    public List<GameObject> decoupes;

    public List<string> decoupesOutils;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return;

        Ray ray;
        ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {

            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 9000, doigtLayer))
            {
                currentDoigt = hit.collider.gameObject;
            }


        }

        if (Input.GetMouseButton(0) && currentDoigt != null)
        {

            Plane plane = new Plane(Vector3.up, y);
            float distance;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }

            Vector3 curPos = worldPosition;

            if(Vector3.Distance(curPos, centreImage) < maxDist)
                currentDoigt.transform.position = curPos + new Vector3(0, 0.01f, 0);

            //curPos + new Vector3(0, 0.01f, 0)
        }

        if (Input.GetMouseButtonUp(0) && currentDoigt != null)
        {
            currentDoigt = null;

        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(!doigts.Contains(other.gameObject))
            doigts.Add(other.gameObject);
        canValide = doigts.Count == 2;
    }

    private void OnTriggerExit(Collider other)
    {
        doigts.Remove(other.gameObject);
        canValide = doigts.Count == 2;
    }
}
