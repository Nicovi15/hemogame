using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecre : MonoBehaviour
{
    public bool enCours = false;

    [Header("Health settings")]
    [SerializeField]
    public float vieMax;

    public float currentVie;

    [Header("Movement settings")]
    [SerializeField]
    float rotaSpeed;

    [SerializeField]
    public float moveSpeedinit;

    [SerializeField]
    public float moveSpeedMax;

    [SerializeField]
    public float currentMoveSpeedMax;

    public float currentMoveSpeed;

    [SerializeField]
    float acceleration;

    [SerializeField]
    float deccelerationAuto;

    [SerializeField]
    float deccelerationManu;

    [SerializeField]
    Animator anim;

    [Header("Collision obstacle settings")]
    [SerializeField]
    float speedAccident;

    [SerializeField]
    float distanceRaycast;

    [SerializeField]
    LayerMask obstaclesLayers;

    Rigidbody rb;

    [SerializeField]
    float dureeColli;

    [SerializeField]
    float dureeBonk;

    float cdRun;

    bool canRun = true;

    [Header("Sprites")]
    [SerializeField]
    SpriteRenderer goutteDir;

    [SerializeField]
    SpriteRenderer crossCollision;

    [SerializeField]
    float clignotementRateColli;

    [SerializeField]
    float clignotementRate;

    [SerializeField]
    Color couleur1;

    [SerializeField]
    Color couleur2;

    [SerializeField]
    Color couleur3;

    [SerializeField]
    Color couleur4;

    [SerializeField]
    GameManagerRecre GM;

    [SerializeField]
    GestionJaugeHemo jauges;

    [SerializeField]
    int limitePhysique;

    [SerializeField]
    Transform raycastT;

    [SerializeField]
    FollowerRecre FR;

    [SerializeField]
    GameObject canvasMess;

    [SerializeField]
    string followerName;

    [SerializeField]
    Color followerColor;

    [SerializeField]
    List<string> messagesFeedback;

    public bool blesse = false;

    RigidbodyConstraints previousConstraints;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //currentVie = vieMax;
        goutteDir.enabled = false;
        crossCollision.enabled = false;

        currentMoveSpeedMax = moveSpeedMax;
        FR.mustFollow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enCours)
            return;

        anim.SetFloat("vitesse", currentMoveSpeed);

        if(currentMoveSpeed > 0.25 * moveSpeedMax)
            anim.SetFloat("MarcheMultiplier", 2);
        else
            anim.SetFloat("MarcheMultiplier", 1);

        if (currentMoveSpeed > 0.75 * moveSpeedMax)
            anim.SetFloat("CourseMultiplier", 1.5f);
        else
            anim.SetFloat("CourseMultiplier", 1);

        updateGoutteCouleur();

        //if (cdRun < dureeBonk)
        //{
        //    cdRun += Time.deltaTime;
        //    if (cdRun > dureeBonk)
        //        canRun = true;
        //}
            


        if (canRun)
        {
            float rota = Input.GetAxisRaw("Horizontal");
            transform.Rotate(0, rota * rotaSpeed * 10 * Time.deltaTime, 0);
            SpeedControl();

            if (Input.GetAxisRaw("Vertical") != 0)
            {
                if (Input.GetAxisRaw("Vertical") > 0 && !Physics.Raycast(raycastT.position, transform.forward, distanceRaycast, obstaclesLayers))
                {
                    if (currentMoveSpeed < currentMoveSpeedMax)
                        currentMoveSpeed += acceleration * Time.deltaTime;

                    if (currentMoveSpeed > currentMoveSpeedMax)
                        currentMoveSpeed = currentMoveSpeedMax;
                }
                else
                {
                    if (currentMoveSpeed > moveSpeedinit)
                        currentMoveSpeed -= deccelerationManu * Time.deltaTime;

                    if (currentMoveSpeed < moveSpeedinit)
                        currentMoveSpeed = moveSpeedinit;
                }

            }
            else
            {
                currentMoveSpeed -= deccelerationAuto * Time.deltaTime;
                if (currentMoveSpeed < moveSpeedinit)
                    currentMoveSpeed = moveSpeedinit;
            }
        }

        //FR.currentMoveSpeed = currentMoveSpeed;
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxisRaw("Vertical");
        //rb.AddForce(move * transform.forward.normalized * currentMoveSpeed * 10, ForceMode.Force);
        rb.AddForce( transform.forward.normalized * currentMoveSpeed * 10, ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > currentMoveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currentMoveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(currentMoveSpeed > speedAccident && ((obstaclesLayers & (1 << collision.gameObject.layer)) != 0))
        //{
        //    previousConstraints = rb.constraints;
        //    rb.constraints = RigidbodyConstraints.FreezeAll;
        //
        //    Debug.Log("Accident");
        //    anim.SetTrigger("Chute");
        //    currentMoveSpeed = 0;
        //    rb.velocity = Vector3.zero;
        //    cdRun = 0;
        //    canRun = false;
        //
        //    currentVie -= 1;
        //    StartCoroutine(remiseSurPied());
        //    
        //}

        if(currentMoveSpeed > 0.25 * moveSpeedMax && ((obstaclesLayers & (1 << collision.gameObject.layer)) != 0))
        {
            int pertePhysique = 0;

            if(collision.gameObject.tag == "murRecre")
            {
                if (currentMoveSpeed > 0.75f * moveSpeedMax)
                    pertePhysique = -10;
                else if (currentMoveSpeed > 0.5f * moveSpeedMax)
                    pertePhysique = -8;
                else
                    pertePhysique = 0; // -6;

            }
            else if(collision.gameObject.tag == "persoRecre")
            {
                if (currentMoveSpeed > 0.75f * moveSpeedMax)
                    pertePhysique = -6;
                else if (currentMoveSpeed > 0.5f * moveSpeedMax)
                    pertePhysique = -4;
                else
                    pertePhysique = 0; // -6;
            }

            if (pertePhysique == 0)
                return;

            //FR.enabled = false;
            
            previousConstraints = rb.constraints;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            currentMoveSpeed = 0;
            rb.velocity = Vector3.zero;
            cdRun = 0;
            canRun = false;

            jauges.addPhysique(pertePhysique);

            MessageFeedback mf = Instantiate(canvasMess).GetComponent<MessageFeedback>();
            mf.showString(messagesFeedback[Random.Range((int)0, messagesFeedback.Count)], followerName, followerColor);

            if (jauges.jauges.physique <= limitePhysique)
            {
                anim.SetTrigger("Chute");
                StartCoroutine(remiseSurPied());
            }
            else
            {
                anim.SetTrigger("Recul");
                StartCoroutine(PetiteCollision());
            }

        }
    }

    void updateGoutteCouleur()
    {
        if (currentMoveSpeed > 0.75 * moveSpeedMax)
            goutteDir.color = couleur4;
        else if (currentMoveSpeed > 0.5 * moveSpeedMax)
            goutteDir.color = couleur3;
        else if (currentMoveSpeed > 0.25 * moveSpeedMax)
            goutteDir.color = couleur2;
        else 
            goutteDir.color = couleur1;
    }

    IEnumerator PetiteCollision()
    {
        float t = 0;
        goutteDir.gameObject.SetActive(false);

        while (cdRun < dureeColli)
        {
            cdRun += Time.deltaTime;

            t += Time.deltaTime;

            if (t > clignotementRateColli)
            {
                t = 0;
                goutteDir.gameObject.SetActive(!goutteDir.gameObject.activeSelf);
            }

            yield return null;
        }

        goutteDir.gameObject.SetActive(true);

        rb.constraints = previousConstraints;
        //FR.enabled = true;
        //FR.mustFollow = true;

        canRun = true;
    }

    IEnumerator remiseSurPied()
    {
        FR.mustFollow = false;

        float t = 0;
        goutteDir.gameObject.SetActive(false);
        crossCollision.gameObject.SetActive(true);

        while (cdRun < dureeBonk)
        {
            cdRun += Time.deltaTime;

            t += Time.deltaTime;

            if (t > clignotementRate)
            {
                t = 0;
                crossCollision.gameObject.SetActive(!crossCollision.gameObject.activeSelf);
            }

            yield return null;
        }

        goutteDir.gameObject.SetActive(true);
        crossCollision.gameObject.SetActive(false);

        rb.constraints = previousConstraints;
        //FR.enabled = true;
        FR.mustFollow = true;

        //if (currentVie != 0)
        //    canRun = true;
        //else
        //{
        //    Debug.Log("lancer quesiton");
        //    GM.lancerQuestionAccident();
        //}
        GM.lancerQuestionAccident();
    }

    public void lancerPartie()
    {
        enCours = true;
        goutteDir.enabled = true;
        crossCollision.enabled = true;
    }

    public void seBlesse()
    {
        enCours = true;
        canRun = true;
        goutteDir.enabled = true;
        crossCollision.enabled = true;
        currentMoveSpeedMax = 0.25f * moveSpeedMax;
    }

    public void finPartie()
    {
        currentMoveSpeed = 0;
        rb.velocity = Vector3.zero;
        anim.SetFloat("vitesse", currentMoveSpeed);
        enCours = false;
        goutteDir.enabled = false;
        crossCollision.enabled = false;
    }

    public void setViesMax(float v)
    {
        //vieMax = v;
        //currentVie = vieMax;

        currentVie = v;
    }

    public float getVelocity()
    {
        return rb.velocity.magnitude;
    }
}
