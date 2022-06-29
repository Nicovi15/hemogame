using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerRecre : MonoBehaviour
{
    public List<Transform> path;
    public Transform currentDest;

    public float speed;
    public float moveSpeedMax;
    public float minDist;

    public float currentSpeed;

    [SerializeField]
    Animator anim;

    public Vector3 dir;
    public int index;

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
        index = 0;
        updateDest();
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

        transform.LookAt(currentDest);

        if (Physics.Raycast(raycastPos.position, transform.forward, distanceRaycast, obstaclesLayers))
            currentSpeed = 0;
        else
            currentSpeed = speed;

        this.transform.position += dir * currentSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, currentDest.position) < minDist)
            nextDest();
    }

    void nextDest()
    {
        index++;
        updateDest();
    }

    void updateDest()
    {
        if (index < 0)
            index = path.Count - 1;
        else if (index == path.Count)
            index = 0;

        currentDest = path[index];
        dir = (currentDest.position - transform.position).normalized;
    }
}
