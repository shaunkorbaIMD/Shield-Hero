using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_2 : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody theRB;

    public float slowDownFactor = 0.2f;
    public float slowDownLength = 1f;

    public GameObject shield;

    public float cooldownTimer;

    public float cdTimer;
    public float cdTimer_reset;
    
    public float coolDown;


    void slowTime()
    {
        //Time.timeScale = slowDownFactor;
        Time.timeScale -= (3f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, slowDownFactor, 1f);

        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody>();
        //shield.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);



        if (Input.GetMouseButton(0))
        {
            slowTime();
            Debug.Log("Yep");
        }


        theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

        //cooldownTimer
        if(cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }
        if(cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        //cdtimer
        if (cdTimer < 0)
        {
            cdTimer = 0;
        }
        if (cdTimer > 0 && cooldownTimer == 0)
        {
            cdTimer -= Time.deltaTime;
        }

       
            if (cooldownTimer == 0) 
            {
                if (Input.GetMouseButtonDown(1) && cdTimer == 0) //right click
                    {
                        shield.SetActive(true);
                        Debug.Log("Shield ACTIVE");
                        cooldownTimer = coolDown;
                        cdTimer = cdTimer_reset;
                    }
                    else
                    {
                        shield.SetActive(false);
                        
                    }
            }
        
        
        

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float midPoint = (transform.position - Camera.main.transform.position).magnitude; //dist between this and camera

        Vector3 lookPoint = mouseRay.origin + mouseRay.direction * midPoint;

        lookPoint.y = transform.position.y;

        transform.LookAt(lookPoint);




        //Ortho
        //transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));

    }
}
