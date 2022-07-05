using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RondeRecre : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float speedAnim;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
            if(transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).tag != "sousRonde")
                transform.GetChild(i).GetChild(0).GetComponent<Animator>().SetFloat("vitesse", speedAnim);
    }

    // Update is called once per frame
    void Update()
    {
        float y = transform.rotation.eulerAngles.y;
        y += speed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, y, 0);
    }
}
