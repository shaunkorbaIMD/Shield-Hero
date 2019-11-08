using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    
    private float shootTimer = 0;
    public float shootTimerLength = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(shootTimer < 0)
        {
            GameObject p = Instantiate(projectile, transform.position, transform.rotation);

            var projSpeed = 9;

            var vel = player.GetComponent<Rigidbody>().velocity;

            var dis = Vector3.Magnitude(player.transform.position - transform.position);



            p.GetComponent<Rigidbody>().velocity = Vector3.Normalize((player.transform.position + vel*(dis/20)) - transform.position)*projSpeed;


            shootTimer = shootTimerLength;
        }
        else
        {
            shootTimer -= 0.1f*Time.deltaTime;
        }
                
               

    }
}
