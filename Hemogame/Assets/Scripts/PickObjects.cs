using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickObjects : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    LayerMask pickMask;

    [SerializeField]
    LayerMask ignoreRayMask;

    [SerializeField]
    LayerMask allWithoutIgnore;

    [SerializeField]
    Vector3 offSet;

    Ray ray;
    RaycastHit hit;

    GameObject pickedObject;
    Rigidbody pickedObjectBody;
    float initialMass;
    int initialLayer;

    Vector3 mOffset;
    float mZCoord;

    [SerializeField]
    GameObject target;

    [SerializeField]
    Vector3 scaleTargetInit;

    [SerializeField]
    float coefScale = 1;

    [SerializeField]
    float highSpeed;

    [SerializeField]
    float lowSpeed;

    [SerializeField]
    float veryLowSpeed;

    float speed;

    [SerializeField]
    float minDistance;

    [SerializeField]
    float lowDistance;

    [SerializeField]
    float highDistance;

    Vector3 dir;

    GameObject objectOver;

    public GameObject prefabPanel;

    public GameObject panel;

    public Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        target.SetActive(false);
        speed = lowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //ray = cam.ScreenPointToRay(Input.mousePosition);
        //Physics.Raycast(ray, out hit);

        //Debug.Log(hit.point);
        //Debug.Log(cam.ScreenToWorldPoint(Input.mousePosition));
        
        if (!pickedObject)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (pickMask == (pickMask | (1 << hit.collider.gameObject.layer)))
                {
                    Debug.Log(hit.collider.name);
                    if (objectOver == null)
                    {
                        objectOver = hit.collider.gameObject;
                        objectOver.GetComponent<PickableObject>().hover();
                        panel = Instantiate(prefabPanel, canvas);
                        panel.SendMessage("setData", objectOver.GetComponent<PickableObject>().data);
                        panel.SendMessage("showOnlyName");
                    }
                    else if (objectOver != hit.collider.gameObject)
                    {
                        Destroy(panel);
                        objectOver.GetComponent<PickableObject>().unhover();
                        objectOver = hit.collider.gameObject;
                        objectOver.GetComponent<PickableObject>().hover();
                        panel = Instantiate(prefabPanel, canvas);
                        panel.SendMessage("setData", objectOver.GetComponent<PickableObject>().data);
                        panel.SendMessage("showOnlyName");
                    }

                    hit.collider.gameObject.GetComponent<PickableObject>().mouseOver();
                    if (Input.GetMouseButtonDown(0))
                    {
                        Destroy(panel);
                        target.SetActive(true);
                        target.transform.position = hit.point;

                        pickedObject = hit.collider.gameObject;
                        pickedObjectBody = pickedObject.GetComponent<Rigidbody>();
                        pickedObject.GetComponent<PickableObject>().pick();
                        initialLayer = pickedObject.layer;
                        initialMass = pickedObjectBody.mass;

                        pickedObjectBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                        pickedObjectBody.interpolation = RigidbodyInterpolation.Extrapolate;
                        pickedObjectBody.useGravity = false;
                        pickedObjectBody.constraints = RigidbodyConstraints.FreezeRotation;
                        pickedObject.layer = 2;
                        pickedObjectBody.mass = 1e-7f;

                        BoxCollider bc = pickedObject.GetComponent<BoxCollider>();
                        
                        target.transform.localScale = new Vector3(Mathf.Max(bc.bounds.size.x * scaleTargetInit.x * coefScale, 0.25f), scaleTargetInit.y, Mathf.Max(bc.bounds.size.z * scaleTargetInit.z * coefScale, 0.25f));
                        //mZCoord = cam.WorldToScreenPoint(pickedObject.transform.position).z;
                        //mOffset = pickedObject.transform.position - getMouseWorldPos();
                    }
                        
                }
                else if (objectOver)
                {
                    objectOver.GetComponent<PickableObject>().unhover();
                    objectOver = null;
                    Destroy(panel);
                }
                    
            }
        }
        else
        {
            Destroy(panel);
            ray = cam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 10000, ~ignoreRayMask);
            Debug.Log(hit.collider.name);
            pickedObjectBody.velocity = Vector3.zero;
            pickedObjectBody.angularVelocity = Vector3.zero;
            //pickedObject.transform.position = hit.point + offSet;
            target.transform.position = hit.point;

            float dist = Vector3.Distance(pickedObject.transform.position, hit.point + offSet);

            if (dist > minDistance)
            {
                dir = (hit.point + offSet - pickedObject.transform.position).normalized;

                if (dist > highDistance)
                    speed = highSpeed;
                else if (dist > lowDistance)
                    speed = lowSpeed;
                else
                    speed = veryLowSpeed;
            }
            else
                dir = Vector3.zero;

            //pickedObject.transform.position = getMouseWorldPos() + mOffset;

            if (Input.GetMouseButtonUp(0))
            {
                pickedObjectBody.mass = initialMass;
                pickedObject.layer = initialLayer;
                pickedObjectBody.constraints = RigidbodyConstraints.None;
                pickedObjectBody.useGravity = true;
                pickedObjectBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
                pickedObjectBody.interpolation = RigidbodyInterpolation.None;
                pickedObject = null;
                pickedObjectBody = null;

                target.SetActive(false);
            }
        }
        
    }

    Vector3 getMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mZCoord;

        return cam.ScreenToWorldPoint(mousePos);
    }

    private void FixedUpdate()
    {
        if (pickedObject && dir != Vector3.zero)
            //pickedObject.GetComponent<Rigidbody>().MovePosition(hit.point + offSet);
            //pickedObjectBody.AddForce(dir * speed * Time.deltaTime);
            pickedObjectBody.MovePosition(pickedObject.transform.position + dir * speed * Time.deltaTime);
    }

}
