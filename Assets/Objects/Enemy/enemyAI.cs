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
       
    }

    // Update is called once per frame
    void Update()
    {
        if(shootTimer < 0)
        {
            GameObject p = Instantiate(projectile, transform.position, transform.rotation);

            p.GetComponent<Rigidbody>().velocity = new Vector3(3, 0, 3);


            shootTimer = shootTimerLength + Random.Range(0, 4);
        }
        else
        {
            shootTimer -= 20f*Time.deltaTime;
        }
                
               

    }
}
