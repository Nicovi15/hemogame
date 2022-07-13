using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCircle : MonoBehaviour
{
    [SerializeField]
    CircleCameraTrajectory CCT;

    [SerializeField]
    GameObject cam;

    public Transform target;

    public float theta = 0;

    public float speed;

    public float minX;
    public float maxX;

    public float minZ;
    public float maxZ;

    public float scrollSpeed = 3;
    public float zoom;

    public float zoomMin;
    public float zoomMax;

    public float minDistCam = 2;

    bool isMoving;

    public float rotateSpeed = 1;

    [SerializeField]
    DialogueUI dialogueUI;

    // Start is called before the first frame update
    void Start()
    {
        target = CCT.center;
        zoom = zoomMin;

        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueUI.IsOpen || Time.timeScale < 0.5f)
        {
            return;
        }

        if (!isMoving)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                zoom += scrollSpeed * Time.deltaTime;
                zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                zoom -= scrollSpeed * Time.deltaTime;
                zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
            }

            cam.transform.localPosition = new Vector3(0, 0, zoom);

            Ray testCam = new Ray(cam.transform.position, cam.transform.forward);

            if (Physics.Raycast(testCam, out RaycastHit hitInfos))
            {
                Vector3 differenceDirection = cam.transform.forward;
                float difference = Vector3.Dot(differenceDirection, hitInfos.point - cam.transform.position);
                if (difference < minDistCam)
                {
                    //float difference = Vector3.Dot(differenceDirection, hitInfos.point - cam.transform.position);
                    float t = minDistCam - difference;

                    cam.transform.position -= differenceDirection * t;
                    zoom = Mathf.Clamp(cam.transform.localPosition.z, zoomMin, zoomMax);


                    //Vector3 t = (hitInfos.point - cam.transform.position);
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                //theta -= Time.deltaTime * speed;
                /*
                CCT.transform.RotateAround(CCT.transform.position, Vector3.up, -90);
                theta += Mathf.PI / 2;
                theta = theta % (2 * Mathf.PI);
                */
                StartCoroutine(rotateRight(theta + Mathf.PI / 2));
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                //theta += Time.deltaTime * speed;

                /*
                CCT.transform.RotateAround(CCT.transform.position, Vector3.up, 90);
                theta -= Mathf.PI / 2;
                theta = theta % (2 * Mathf.PI);

                */

                StartCoroutine(rotateLeft(theta - Mathf.PI / 2));
            }

            if (Input.GetButton("Left"))
            {
                //CCT.transform.position += - CCT.transform.right * speed * Time.deltaTime;3
                Vector3 newPos = CCT.transform.position - (CCT.transform.rotation * Vector3.right * speed * Time.deltaTime);
                CCT.transform.position = checkLimit(newPos);

            }

            if (Input.GetButton("Right"))
            {
                //CCT.transform.position += CCT.transform.right * speed * Time.deltaTime;
                Vector3 newPos = CCT.transform.position + CCT.transform.rotation * Vector3.right * speed * Time.deltaTime;
                CCT.transform.position = checkLimit(newPos);
            }

            if (Input.GetButton("Up"))
            {
                //CCT.transform.position += CCT.transform.forward * speed * Time.deltaTime;
                //CCT.transform.position += cam.transform.forward * speed * Time.deltaTime;
                Vector3 newPos = CCT.transform.position + CCT.transform.rotation * Vector3.forward * speed * Time.deltaTime;
                CCT.transform.position = checkLimit(newPos);
            }
            if (Input.GetButton("Down"))
            {
                //CCT.transform.position += -CCT.transform.forward * speed * Time.deltaTime;
                //CCT.transform.positionCCT.transform.position  += -cam.transform.forward * sp(CCT.transform.rotation * Vector3.forward * speed * Time.deltaTime);eed * Time.deltaTime;
                Vector3 newPos = CCT.transform.position - (CCT.transform.rotation * Vector3.forward * speed * Time.deltaTime);
                CCT.transform.position = checkLimit(newPos);
            }
        }
        
        float x = CCT.radius * Mathf.Cos(theta);
        float z = CCT.radius * Mathf.Sin(theta);
        this.transform.position = CCT.transform.position + new Vector3(x, 0, z);

        this.transform.LookAt(target);
    }

    public Vector3 checkLimit(Vector3 pos)
    {
        return new Vector3(Mathf.Clamp(pos.x, minX, maxX), pos.y, Mathf.Clamp(pos.z, minZ, maxZ));
    }
    
    IEnumerator rotateLeft(float t)
    {
        //CCT.transform.RotateAround(CCT.transform.position, Vector3.up, 90);
        isMoving = true;
        while (theta > t)
        {
            theta -= Time.deltaTime * rotateSpeed;
            yield return null;
        }
        theta = t % (2 * Mathf.PI);
        CCT.transform.RotateAround(CCT.transform.position, Vector3.up, 90);
        isMoving = false;
    }

    IEnumerator rotateRight(float t)
    {
        //CCT.transform.RotateAround(CCT.transform.position, Vector3.up, 90);
        isMoving = true;
        while (theta < t)
        {
            theta += Time.deltaTime * rotateSpeed;
            yield return null;
        }
        theta = t % (2 * Mathf.PI);
        CCT.transform.RotateAround(CCT.transform.position, Vector3.up, -90);
        isMoving = false;
    }
}
