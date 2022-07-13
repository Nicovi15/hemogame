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
    LayerMask interMask;

    [SerializeField]
    LayerMask characterMask;

    [SerializeField]
    LayerMask ignoreRayMask;

    [SerializeField]
    LayerMask allWithoutIgnore;

    [SerializeField]
    Vector3 offSet;

    Ray ray;
    RaycastHit hit;

    public GameObject pickedObject;
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

    [SerializeField]
    DialogueUI dialogueUI;

    public GameObject cursor;

    cursorFollow CF;

    public DialogueUI DialogueUI => dialogueUI;

    // Start is called before the first frame update
    void Start()
    {
        CF = cursor.GetComponent<cursorFollow>();
        target.SetActive(false);
        speed = lowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueUI.IsOpen || Time.timeScale < 0.5f)
        {
            cursor.SetActive(false);

            if(panel)
                Destroy(panel);
            return;
        }
            
        if(!cursor.activeSelf)
            cursor.SetActive(true);
        //ray = cam.ScreenPointToRay(Input.mousePosition);
        //Physics.Raycast(ray, out hit);

        //Debug.Log(hit.point);
        //Debug.Log(cam.ScreenToWorldPoint(Input.mousePosition));

        if (!pickedObject)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            
            //if (Physics.Raycast(ray, out hit))
            if(Physics.Raycast(ray, out hit, 9000, allWithoutIgnore))
            {
                if (pickMask == (pickMask | (1 << hit.collider.gameObject.layer)))
                {
                    
                    if (objectOver == null)
                    {
                        objectOver = hit.collider.gameObject;
                        objectOver.GetComponent<PickableObject>().hover();
                        if (!panel)
                            panel = Instantiate(prefabPanel, canvas);
                        panel.SendMessage("setData", objectOver.GetComponent<PickableObject>().data);
                        panel.SendMessage("showOnlyName");
                        CF.setPrendre();
                    }
                    else if (objectOver != hit.collider.gameObject)
                    {
                        //Destroy(panel);
                        objectOver.GetComponent<PickableObject>().unhover();
                        objectOver = hit.collider.gameObject;
                        objectOver.GetComponent<PickableObject>().hover();
                        if (!panel)
                            panel = Instantiate(prefabPanel, canvas);
                        panel.SendMessage("setData", objectOver.GetComponent<PickableObject>().data);
                        panel.SendMessage("showOnlyName");
                        CF.setPrendre();
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
                        CF.hide();
                    }
                        
                }
                else if (objectOver)
                {
                    objectOver.GetComponent<PickableObject>().unhover();
                    objectOver = null;
                    Destroy(panel);
                    CF.setNormal();
                }
                else if (interMask == (interMask | (1 << hit.collider.gameObject.layer)))
                {
                    if (!panel)
                        panel = Instantiate(prefabPanel, canvas);
                    panel.SendMessage("setMessage", hit.collider.GetComponent<Interactable>().getDesc());
                    CF.setInteract();

                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.collider.GetComponent<Interactable>().interact();
                    }
                }
                else if (characterMask == (characterMask | (1 << hit.collider.gameObject.layer)))
                {
                    if (!panel)
                        panel = Instantiate(prefabPanel, canvas);
                    panel.SendMessage("setMessage", "Parler");
                    CF.setParler();
                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.collider.GetComponent<IInteractable>().Interact(this);
                    }
                }
                /*
                else
                    Destroy(panel);
                */
            }
            else
            {
                if (objectOver)
                {
                    objectOver.GetComponent<PickableObject>().unhover();
                    objectOver = null;
                }
                Destroy(panel);
                CF.setNormal();
            }
                

            }
        else
        {
            Destroy(panel);
            CF.hide();
            ray = cam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 10000, ~ignoreRayMask);
            //Debug.Log(hit.collider.name);
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
                pickedObject.GetComponent<PickableObject>().unpick();
                pickedObjectBody.mass = initialMass;
                pickedObject.layer = initialLayer;
                pickedObjectBody.isKinematic = false;
                pickedObjectBody.constraints = RigidbodyConstraints.None;
                pickedObjectBody.useGravity = true;
                pickedObjectBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
                pickedObjectBody.interpolation = RigidbodyInterpolation.None;
                pickedObject = null;
                pickedObjectBody = null;

                target.SetActive(false);
                CF.setNormal();
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

    private void OnDisable()
    {
        if (!pickedObject)
            return;

        pickedObject.GetComponent<PickableObject>().unpick();
        pickedObject.GetComponent<PickableObject>().unhover();
        pickedObjectBody.mass = initialMass;
        pickedObject.layer = initialLayer;
        pickedObjectBody.isKinematic = false;
        pickedObjectBody.constraints = RigidbodyConstraints.None;
        pickedObjectBody.useGravity = true;
        pickedObjectBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        pickedObjectBody.interpolation = RigidbodyInterpolation.None;
        pickedObject = null;
        pickedObjectBody = null;

        target.SetActive(false);
        CF.setNormal();
    }

}
