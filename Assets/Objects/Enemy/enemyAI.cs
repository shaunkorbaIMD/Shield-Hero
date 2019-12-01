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
    public float shootCooldownTimer;
    

    public ParticleSystem deathParts;

    public float killFreezeDuration = 0.2f;

    Freezer _freezer;

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

        shootCooldownTimer = 1f+Random.Range(0f, 4f);
    }

    public void Die()
    {
        _freezer.Freeze(killFreezeDuration);
        Instantiate(deathParts, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }


    // Update is called once per frame
    void Update()
    {
        
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
                Debug.Log(hit.collider.tag);
                playerInSight = true;
                
            }
        }

        if(!playerInSight)
        {
            return;
        }


        transform.LookAt(player.transform.position);

        //burst shooting
        if (amountShot >= 2)
        {
            if (shootCooldownTimer > 0)
            {
                shootCooldownTimer -= Time.deltaTime;
            }
            else
            {
                amountShot = 0;
            }

        }
        else if (amountShot < 2 && Vector3.Magnitude(player.transform.position - transform.position) < 18f)
        {

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
                    shootCooldownTimer = shootCooldown + Random.Range(0f, 5f);
                }

            }
            else
            {
                shootTimer -= 0.1f * Time.deltaTime;
            }
        }

    }
}
