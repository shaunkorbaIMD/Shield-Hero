using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    
    private float shootTimer = 0;
    public float shootTimerLength = 0.05f;

    public float amountShot = 0;

    public float shootCooldown;
    public float shootCooldownTimer;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        amountShot = 5;

        shootCooldownTimer = 1f+Random.Range(0f, 4f);
    }

    // Update is called once per frame
    void Update()
    {


        //burst shooting
        if (amountShot >= 2)
        {
            if (shootCooldownTimer > 0)
            {
                shootCooldownTimer -= Time.deltaTime;
            } else {
                amountShot = 0;
            }

        } else if (amountShot < 2 && Vector3.Magnitude(player.transform.position - transform.position) < 18f) {

            if (shootTimer < 0)
            {
                GameObject p = Instantiate(projectile, transform.position, transform.rotation);

                var projSpeed = 6;

                var vel = player.GetComponent<Rigidbody>().velocity;

                var dis = Vector3.Magnitude(player.transform.position - transform.position);



                p.GetComponent<Rigidbody>().velocity = Vector3.Normalize((player.transform.position + vel * (dis / 20)) - transform.position) * projSpeed;


                shootTimer = shootTimerLength;
                amountShot += 1;

                if(amountShot>=2)
                {
                    shootCooldownTimer = shootCooldown + Random.Range(0f, 5f);
                }

            }
            else
            {
                shootTimer -= 0.1f * Time.deltaTime;
            }
        }

        
        


    }//end of update
}
