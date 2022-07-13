using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerRecre : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    public float minSpeed;

    [SerializeField]
    public float maxSpeed;

    [SerializeField]
    public float maxSpeed1;

    [SerializeField]
    public float maxSpeed2;

    [SerializeField]
    public float maxSpeed3;

    [SerializeField]
    PlayerRecre PR;

    [SerializeField]
    public float minDistance;

    [SerializeField]
    float speed;

    [SerializeField]
    Animator anim;

    Rigidbody rb;

    Vector3 newPos;

    Vector3 pastFollowerPosition;
    Vector3 pastTargetPosition;

    public bool mustFollow;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        newPos = transform.position;
        pastFollowerPosition = transform.position;
        pastTargetPosition = (transform.position - target.position).normalized * minDistance + target.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) < minDistance)
        {
            anim.SetFloat("vitesse", 0);
            return;
        }
            

        anim.SetFloat("vitesse", PR.currentMoveSpeed);

        if (PR.currentMoveSpeed > 0.25 * PR.moveSpeedMax)
            anim.SetFloat("MarcheMultiplier", 2);
        else
            anim.SetFloat("MarcheMultiplier", 1);

        if (PR.currentMoveSpeed > 0.75 * PR.moveSpeedMax)
            anim.SetFloat("CourseMultiplier", 1.5f);
        else
            anim.SetFloat("CourseMultiplier", 1);

        if (PR.currentMoveSpeed > 0.75 * PR.moveSpeedMax)
            speed = maxSpeed3;
        else if (PR.currentMoveSpeed > 0.5 * PR.moveSpeedMax)
            speed = maxSpeed2;
        else if (PR.currentMoveSpeed > 0.25 * PR.moveSpeedMax)
            speed = maxSpeed1;
        else 
            speed = maxSpeed;

        newPos = SmoothApproach(pastFollowerPosition, pastTargetPosition, (transform.position - target.position).normalized * minDistance + target.position, speed);
        pastFollowerPosition = transform.position;
        pastTargetPosition = (transform.position - target.position).normalized * minDistance + target.position;

        //if (!float.IsNaN(newPos.x))
        //    transform.position = newPos;

    }

    private void FixedUpdate()
    {
        if (!float.IsNaN(newPos.x) && mustFollow)
            rb.MovePosition(newPos);
    }

    Vector3 SuperSmoothLerp(Vector3 pastPosition, Vector3 pastTargetPosition, Vector3 targetPosition, float time, float speed)
    {
        Vector3 f = pastPosition - pastTargetPosition + (targetPosition - pastTargetPosition) / (speed * time);
        return targetPosition - (targetPosition - pastTargetPosition) / (speed * time) + f * Mathf.Exp(-speed * time);
    }

    Vector3 SmoothApproach(Vector3 pastPosition, Vector3 pastTargetPosition, Vector3 targetPosition, float speed)
    {
        float t = Time.smoothDeltaTime * speed;
        Vector3 v = (targetPosition - pastTargetPosition) / t;
        Vector3 f = pastPosition - pastTargetPosition + v;
        return targetPosition - v + f * Mathf.Exp(-t);
    }
}
