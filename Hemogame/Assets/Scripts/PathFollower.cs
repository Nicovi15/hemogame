using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public List<Transform> path;
    public Transform currentDest;

    public float speed;
    public float minDist;

    float currentSpeed;

    public Vector3 dir;
    public int index;

    GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
        index = 0;
        updateDest();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(currentDest);

        this.transform.position += dir * currentSpeed * Time.deltaTime;

        if(Vector3.Distance(transform.position, currentDest.position) < minDist)
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

    private void OnMouseEnter()
    {
        if (GM.findingPhase)
        {
            currentSpeed = 0;
        }
    }

    private void OnMouseExit()
    {
        if (GM.findingPhase)
        {
            currentSpeed = speed;
        }
    }
}
