using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    
    private float shootTimer = 0;
    public float shootTimerLength = 0.05f;

    public float projSpeed = 6;

    public float amountShot = 0;

    public float shootCooldown;
    public float shootshieldDuration;
    

    public ParticleSystem deathParts;

    public float killFreezeDuration = 0.08f;

    Freezer _freezer;

    public Animator animator;

    private bool isAttacking = false;
    public bool isHit = false;

    private float dyingTimer = -1f;

    // Start is called before the first frame update
    void Start()
    {

        GameObject mgr = GameObject.FindWithTag("FreezeManager");

        if(mgr)
        {
            _freezer = mgr.GetComponent<Freezer>();
        }

        player = GameObject.FindGameObjectWithTag("Player");

        amountShot = 5;

        shootshieldDuration = 0.6f+Random.Range(0f, 2f);
    }

    public void Die()
    {

        GetComponent<Collider>().enabled = false;

        _freezer.Freeze(killFreezeDuration);

        isHit = true;


        dyingTimer = 10;
        Instantiate(deathParts, transform.position, Quaternion.identity);
        
        


    }

    

    // Update is called once per frame
    void Update()
    {
        // Set the animator states

        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isHit", isHit);


        // Control the states

        if (isHit)
        {
            if (dyingTimer > 0)
            {
                dyingTimer -= 10f * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }

            return;
        }



        Vector3 toPlayer = player.transform.position - transform.position;

        toPlayer.y = player.transform.position.y + 0.24f;

        

        Ray r = new Ray(transform.position, toPlayer);
        Debug.DrawRay(r.origin, r.direction * 20, Color.white, 1);
        RaycastHit hit;

        bool playerInSight = false;

        if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 9))
        {
            

            if (hit.collider.tag != "Wall" )
            {
                //Debug.Log(hit.collider.tag);
                playerInSight = true;
                
            }
        }

        if(!playerInSight)
        {
            return;
        }


        Vector3 playerPos = player.transform.position;

        playerPos = new Vector3(playerPos.x, gameObject.transform.position.y, playerPos.z);

        transform.LookAt(playerPos);

        //burst shooting
        if (amountShot >= 2)
        {
            
            isAttacking = false;
            

            if (shootshieldDuration > 0)
            {
                shootshieldDuration -= Time.deltaTime;
            }
            else
            {
                
                amountShot = 0;
            }

        }
        else if (amountShot < 2 && Vector3.Magnitude(player.transform.position - transform.position) < 18f)
        {
            isAttacking = true;

            if (shootTimer < 0)
            {
                GameObject p = Instantiate(projectile, transform.position, transform.rotation);



                var vel = player.GetComponent<Rigidbody>().velocity;

                var dis = Vector3.Magnitude(player.transform.position - transform.position);



                p.GetComponent<Rigidbody>().velocity = Vector3.Normalize((player.transform.position + vel * (dis / 20)) - transform.position) * projSpeed;


                shootTimer = shootTimerLength;
                amountShot += 1;

                if (amountShot >= 2)
                {
                    shootshieldDuration = shootCooldown + Random.Range(0f, 5f);
                }

            }
            else
            {
                shootTimer -= 0.1f * Time.deltaTime;
            }
        }


        

       
        
    }
}
