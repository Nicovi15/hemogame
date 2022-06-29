using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleveRebondRecre : MonoBehaviour
{

    public float speed;
    public float moveSpeedMax;
    public float minDist;

    public float currentSpeed;

    [SerializeField]
    Animator anim;

    [SerializeField]
    bool firstDirIsRandom = false;
    public Vector3 dir;

    [SerializeField]
    LayerMask obstaclesLayers;

    [SerializeField]
    float distanceRaycast;

    [SerializeField]
    Transform raycastPos;


    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;

        if (firstDirIsRandom)
            dir = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vitesse", currentSpeed * 2);

        if (currentSpeed > 0.25 * moveSpeedMax)
            anim.SetFloat("MarcheMultiplier", 2);
        else
            anim.SetFloat("MarcheMultiplier", 1);

        if (currentSpeed > 0.75 * moveSpeedMax)
            anim.SetFloat("CourseMultiplier", 1.5f);
        else
            anim.SetFloat("CourseMultiplier", 1);

        transform.LookAt(this.transform.position + dir * 3);

        if (Physics.Raycast(raycastPos.position, transform.forward, distanceRaycast, obstaclesLayers))
            currentSpeed = 0;
        else
            currentSpeed = speed;

        this.transform.position += dir * currentSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        dir = Vector3.Reflect(dir, collision.contacts[0].normal).normalized;
    }

}
