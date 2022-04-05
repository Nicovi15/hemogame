using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerFPMovement : MonoBehaviour
{
    [Header("Player config")]

    [SerializeField]
    CharacterController controller;

    [SerializeField]
    Camera cameraPlayer;

    [Header("Movement parameters")]

    [SerializeField]
    float speed = 12f;

    [SerializeField]
    float gravity = -9.81f;

    [Header("Ground config")]

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    float groundDist = 0.4f;

    [SerializeField]
    LayerMask groundMask;

    bool isGrounded;

    Vector3 velocity;

    [Header("Pick Up config")]

    [SerializeField]
    LayerMask pickUpMask;

    [SerializeField]
    Transform rightHand;

    [SerializeField]
    Transform leftHand;

    [SerializeField]
    Transform dropPos;

    //Vector3 originalHandPos;

    //[SerializeField]
    //float shakeMagnitude;

    [SerializeField]
    float pickUpRange;

    Rigidbody pickedObjectBody;
    Collider pickedObjectCollider;

    [SerializeField]
    float dropForceForward;

    [SerializeField]
    float dropForceUpward;

    [SerializeField]
    float regularRadius;

    [SerializeField]
    float holdingRadius;

    [SerializeField]
    float regularHeight;

    [SerializeField]
    float holdintHeight;

    public GameObject hoverObject;
    PickableObject hoverObjectScript;

    [SerializeField]
    TextMeshProUGUI hudText;

    [SerializeField]
    GameObject hudParler;

    [SerializeField]
    GameObject hudPrendre;

    [SerializeField]
    GameObject hudInteract;

    Vector3 initialLHPos;
    Vector3 initialRHPos;

    public float amount;
    public float maxAmount;
    public float smoothAmount;

    public float cdPickUpMax = 0.1f;
    float cdPickUp;

    [SerializeField]
    LayerMask goalMask;

    [SerializeField]
    LayerMask triggerMask;

    [SerializeField]
    LayerMask interMask;

    [SerializeField]
    LayerMask characterMask;

    [SerializeField]
    DialogueUI dialogueUI;

    [SerializeField]
    TransiMEP transi;

    public bool interact = false;

    public DialogueUI DialogueUI => dialogueUI;

    //public IInteractable Interactable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //originalHandPos = rightHand.transform.localPosition;
        hudText.text = "";
        initialLHPos = leftHand.localPosition;
        initialRHPos = rightHand.localPosition;

        cdPickUp = cdPickUpMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueUI.IsOpen || !transi.isOut)
        {
            hudText.text = "";
            hudParler.SetActive(false);

            return;
        }
            

        cdPickUp -= Time.deltaTime;

        #region Movement

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        /*
        if(Vector3.Distance(move, Vector3.zero) > 0.1f)
        {
            float xs = Random.Range(-1f, 1f) * shakeMagnitude;
            float ys = Random.Range(-1f, 1f) * shakeMagnitude;

            rightHand.transform.localPosition = originalHandPos + new Vector3(xs, ys, 0);
        }
        */

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.A))
            leftHand.gameObject.SetActive(!leftHand.gameObject.activeSelf);

        if (leftHand.gameObject.activeSelf)
            leftHandMov();

        rightHandMov();

        #endregion

        interact = false;

        Ray testGoal = new Ray(cameraPlayer.transform.position, cameraPlayer.transform.forward);
        if (Physics.Raycast(testGoal, out RaycastHit hitInfos2, pickUpRange, goalMask))
        {
            //hudText.text = hitInfos2.collider.GetComponent<GoalObjectif>().getActionDesc();
            //hudInteract.SetActive(true);

            if (hitInfos2.collider.GetComponent<GoalObjectif>().MR.enabled && Input.GetKeyDown(KeyCode.E))
            {
                if (pickedObjectBody)
                {
                    Vector3 pos = this.transform.position;
                    //pickedObjectBody.transform.position = hitInfos2.collider.transform.position;
                    //Debug.Log("Objet placer");
                    //GameObject o = pickedObjectBody.gameObject;
                    //dropObject();
                    hitInfos2.collider.GetComponent<GoalObjectif>().placerObjectif(pickedObjectBody.gameObject);
                    poserObject();
                    //hitInfos2.collider.GetComponent<GoalObjectif>().placerObjectif(o);
                    controller.radius = regularRadius;
                    controller.Move(this.transform.position - pos);
                }
            }
        }
        else if (!hoverObject)
        {
            hudText.text = "";
            //hudInteract.SetActive(false);
        }
            

        Ray testTrigger = new Ray(cameraPlayer.transform.position, cameraPlayer.transform.forward);
        if (Physics.Raycast(testTrigger, out RaycastHit hitInfos3, pickUpRange + 1.5f, triggerMask))
        {
            if (hitInfos3.collider.GetComponent<TriggerObjectif>().MR.enabled)
            {
                hudInteract.SetActive(true);
                hudText.text = hitInfos3.collider.GetComponent<TriggerObjectif>().getActionDesc();
                interact = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (pickedObjectBody)
                    {
                        GameObject o = pickedObjectBody.gameObject;
                        poserObject();
                        hitInfos3.collider.GetComponent<TriggerObjectif>().triggerObj(o);
                        controller.radius = regularRadius;
                    }
                }
            }

        }

        /*
        else if(!hoverObject && !interact)
        {
            hudText.text = "";
            hudInteract.SetActive(false);
        }
        */

        Ray testInter = new Ray(cameraPlayer.transform.position, cameraPlayer.transform.forward);
        if (Physics.Raycast(testInter, out RaycastHit hitInfos4, pickUpRange, interMask))
        {
            hudInteract.SetActive(true);
            hudText.text = hitInfos4.collider.GetComponent<Interactable>().getDesc();
            interact = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                hitInfos4.collider.GetComponent<Interactable>().interact();
                return;

            }
        }

        /*
        else if(!hoverObject)
        {
            hudText.text = "";
            hudInteract.SetActive(false);
        }
        */

        if (!interact)
        {
            hudInteract.SetActive(false);
        }

        Ray testCharacter = new Ray(cameraPlayer.transform.position, cameraPlayer.transform.forward);
        if (Physics.Raycast(testCharacter, out RaycastHit hitInfos5, pickUpRange, characterMask))
        {
            hudParler.SetActive(true);
            hudText.text = "Parler";

            if (Input.GetKeyDown(KeyCode.E))
            {
                hitInfos5.collider.GetComponent<IInteractable>().Interact(this);
                return;
            }
        }
        else if (!hoverObject && !interact)
        {
            hudParler.SetActive(false);
            hudText.text = "";
        }
            
        //hudText.text = "";


        #region PickUp Objects

        Ray testPickUp = new Ray(cameraPlayer.transform.position, cameraPlayer.transform.forward);

        if(Physics.Raycast(testPickUp, out RaycastHit hitInfos, pickUpRange, pickUpMask) && !pickedObjectBody){

            if(hoverObject == null)
            {
                hoverObject = hitInfos.collider.gameObject;
                hoverObjectScript = hoverObject.GetComponent<PickableObject>();
                hoverObjectScript.hover();
                hudText.text = "Prendre\n" + hoverObjectScript.data.nameObject;
                hudPrendre.SetActive(true);
            }
            else if(hoverObject != hitInfos.collider.gameObject)
            {
                hoverObjectScript.unhover();
                hoverObject = hitInfos.collider.gameObject;
                hoverObjectScript = hoverObject.GetComponent<PickableObject>();
                hoverObjectScript.hover();
                hudText.text = "Prendre\n" + hoverObjectScript.data.nameObject;
                hudPrendre.SetActive(true);
            }
        }
        else if(hoverObject)
        {
            hudPrendre.SetActive(false);
            hudText.text = "";
            hoverObjectScript.unhover();
            hoverObject = null;
            hoverObjectScript = null;
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pickedObjectBody)
            {
                dropObject();
            }
            else if(cdPickUp < 0 && Physics.Raycast(testPickUp, out RaycastHit hitInfo, pickUpRange, pickUpMask))
            {
                {
                    pickObject(hitInfo);
                }
            }

        }

        #endregion

       

    }

    private void pickObject(RaycastHit hitInfo)
    {
        controller.radius = holdingRadius;
        //controller.center = new Vector3(0, holdintHeight, 0);

        pickedObjectBody = hitInfo.rigidbody;
        pickedObjectCollider = hitInfo.collider;

        pickedObjectBody.isKinematic = true;
        //pickedObjectCollider.enabled = false;

        pickedObjectBody.transform.SetParent(rightHand);
        pickedObjectBody.transform.localPosition = Vector3.zero;

        controller.radius = pickedObjectBody.GetComponent<PickableObject>().hold();
        ObjectDescription data = pickedObjectBody.GetComponent<PickableObject>().getData();
        dropPos.localPosition = new Vector3(0, 0.5f, data.dropPosZ);
        //pickedObjectBody.transform.rotation = new Quaternion();
    }

    private void dropObject()
    {
        controller.radius = regularRadius;
        //controller.center = new Vector3(0, regularHeight, 0);

        pickedObjectBody.transform.SetParent(null);

        pickedObjectBody.transform.position = dropPos.position;

        pickedObjectBody.isKinematic = false;
        pickedObjectCollider.enabled = true;

        //pickedObjectBody.transform.position = dropPos.position;
        //pickedObjectBody.MovePosition(dropPos.position);

        pickedObjectBody.GetComponent<PickableObject>().drop();

        pickedObjectBody.AddForce(cameraPlayer.transform.forward * dropForceForward, ForceMode.Impulse);
        pickedObjectBody.AddForce(cameraPlayer.transform.up * dropForceUpward, ForceMode.Impulse);

        pickedObjectBody = null;
        pickedObjectCollider = null;

        cdPickUp = cdPickUpMax;
    }

    private void poserObject()
    {
        pickedObjectBody.transform.SetParent(null);
        pickedObjectBody.GetComponent<PickableObject>().poser();
        pickedObjectCollider.enabled = false;
        pickedObjectBody = null;
        pickedObjectCollider = null;
        cdPickUp = cdPickUpMax;

    }

    private void OnDisable()
    {
        if(pickedObjectBody)
            dropObject();
        if (hoverObject)
        {
            hudText.text = "";
            hoverObjectScript.unhover();
            hoverObject = null;
            hoverObjectScript = null;
        }
    }

    private void leftHandMov()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPos = new Vector3(movementX, movementY, 0);
        leftHand.localPosition = Vector3.Lerp(leftHand.localPosition, finalPos + initialLHPos, Time.deltaTime * smoothAmount);
    }

    private void rightHandMov()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPos = new Vector3(movementX, movementY, 0);
        rightHand.localPosition = Vector3.Lerp(rightHand.localPosition, finalPos + initialRHPos, Time.deltaTime * smoothAmount);
    }

}
