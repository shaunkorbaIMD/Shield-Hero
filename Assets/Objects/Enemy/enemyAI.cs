using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    
    private float shootTimer = 0;
    public float shootTimerLength = 0.05f;

    public float amountShot;

    public float shootCooldown;
    public float shootCooldownTimer;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

       
        //burst shooting
        if (amountShot >= 3)
        {
            //cooldownTimer
            if (shootCooldownTimer < 0)
            {
                shootCooldownTimer = 0;
            }
            if (shootCooldownTimer > 0)
            {
                shootCooldownTimer -= Time.deltaTime;
            }
            if(shootCooldownTimer == 0)
            {

                amountShot = 0;
                if (shootCooldown < 0)
                {
                    shootCooldown = 0;
                }
                if (shootCooldown > 0)
                {
                    shootCooldown -= Time.deltaTime;
                }
                if(shootCooldown == 0)
                {
                    shootCooldownTimer = 3.0f;
                }

            }


            //Debug.Log("timer: " + shootCooldownTimer);
        }




        if (amountShot < 3)
        {
            if (shootTimer < 0)
            {
                GameObject p = Instantiate(projectile, transform.position, transform.rotation);

                var projSpeed = 9;

                var vel = player.GetComponent<Rigidbody>().velocity;

                var dis = Vector3.Magnitude(player.transform.position - transform.position);



                p.GetComponent<Rigidbody>().velocity = Vector3.Normalize((player.transform.position + vel * (dis / 20)) - transform.position) * projSpeed;


                shootTimer = shootTimerLength;
                amountShot += 1;

            }
            else
            {
                shootTimer -= 0.1f * Time.deltaTime;
            }
        }

        
        


    }//end of update
}
