using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_2 : MonoBehaviour
{
    public float moveSpeed;
    public float normMoveSpeed;
    public float shieldMoveSpeed;

    public Rigidbody theRB;

    public float slowDownFactor = 0.85f;
    public float slowDownLength = 3f;

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

        normMoveSpeed = moveSpeed;
        shieldMoveSpeed = normMoveSpeed - 2;

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);



        


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
        if (Input.GetMouseButton(1)) //right click
        {
            slowTime();
        }
        if ( Input.GetMouseButton(0) ) //left click
        {
           

            //if (cdTimer == 0 && cooldownTimer > 0)
            //{
                shield.SetActive(true);
                cooldownTimer -= Time.unscaledDeltaTime;
           // }

        } else if (shield.active == true)
        { 
            shield.SetActive(false);
            //cdTimer = cdTimer_reset;
            //cooldownTimer = coolDown;

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
