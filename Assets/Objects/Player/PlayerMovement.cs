using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody theRB;

    public float slowDownFactor = 0.2f;
    public float slowDownLength = 1f;

    public GameObject shield;



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


    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);


        if (Input.GetMouseButton(0))
        {
            slowTime();
        }


        theRB.velocity = new Vector3(Input.GetAxis("Horizontal")*moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical")* moveSpeed);

        if(theRB.velocity.magnitude == 0)
        {
            //spawn shield
            shield.SetActive(true);
        }
        else
        {
            //spawn shield
            shield.SetActive(false);
            
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
