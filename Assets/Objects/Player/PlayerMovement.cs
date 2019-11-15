﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private float normMoveSpeed;
    public float shieldMoveSpeed;

    public GameObject cam;

    public Rigidbody theRB;

    public float slowDownFactor = 0.9f;
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

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);






        theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

        //cooldownTimer
        if (cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }
        if (cooldownTimer > 0)
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

        var offset = cam.GetComponent<CameraFollow>().offset;
        var startOffset = cam.GetComponent<CameraFollow>().startOffset;


        // SLOWING TIME ////////////////////////////////////////////////////////

        if (Input.GetMouseButton(1)) //right click
        {
            slowTime();



            cam.GetComponent<CameraFollow>().offset = Vector3.Lerp(offset, startOffset + new Vector3(0, -1, 1), 0.01f);
        } else
        {
            cam.GetComponent<CameraFollow>().offset = Vector3.Lerp(offset, startOffset, 0.04f);
        }

        // TAKING OUT SHIELD ////////////////////////////////////////////////////////

        
        

        if (Input.GetMouseButton(0)) //left click
        {

            shield.SetActive(true);
            cooldownTimer -= Time.unscaledDeltaTime;
                    
            moveSpeed = Mathf.Lerp(moveSpeed, shieldMoveSpeed, 0.07f);

        } else  {
            if (shield.active == true)
            { 
                shield.SetActive(false);
            }
            moveSpeed = Mathf.Lerp(moveSpeed, normMoveSpeed, 0.07f);
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
