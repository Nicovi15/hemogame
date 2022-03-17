using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCircle : MonoBehaviour
{
    [SerializeField]
    CircleCameraTrajectory CCT;

    public Transform target;

    public float theta = 0;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = CCT.center;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Left"))
        {
            theta -= Time.deltaTime * speed;
            theta = theta % (2*Mathf.PI);
        }

        if (Input.GetButton("Right"))
        {
            theta += Time.deltaTime * speed;
            theta = theta % (2 * Mathf.PI);
        }

        float x = CCT.radius * Mathf.Cos(theta);
        float z = CCT.radius * Mathf.Sin(theta);
        this.transform.position = CCT.center.position + new Vector3(x, CCT.transform.position.y, z);

        this.transform.LookAt(target);
    }
}
