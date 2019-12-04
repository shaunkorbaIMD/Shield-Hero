using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileCode : MonoBehaviour
{

    public AudioClip deflected;
    public AudioClip deflected2;
    public AudioClip deflected3;
    public AudioClip playerHit;
    AudioSource audioSource;

    public PlayerMovement playerMovement;

    public Rigidbody thisRB;

    public TrailRenderer trail;
    public ParticleSystemRenderer particleSys;


    private Vector3 lockedVelocity;
    private bool velIsLocked = false;

    public Material reflectedMat;


    private PlayerMovement p;

    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        var projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach(var projectile in projectiles)
        {
            if (!(projectile.GetComponent<Collider>() == GetComponent<Collider>()))
                Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
        }

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            if (!(enemy.GetComponent<Collider>() == GetComponent<Collider>()))
                Physics.IgnoreCollision(enemy.GetComponent<Collider>(), GetComponent<Collider>());
        }

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(velIsLocked)
        {
            thisRB.velocity = lockedVelocity;
        }
        transform.forward = GetComponent<Rigidbody>().velocity;

        Transform[] enemTrans = new Transform[3];    

    }

    public void PlayOneShot(AudioClip clip, float volumeScale = 1.0F)
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Shield")
        {
            float randNum = Random.Range(0.0f, 30.0f);
            //Debug.Log("RandNum = " + randNum);
            if (randNum < 10.0f)
            {
                audioSource.PlayOneShot(deflected, 0.7f);
            }else if (randNum >= 10.0f && randNum < 20.0f)
            {
                audioSource.PlayOneShot(deflected2, 0.7f);
            }
            else{
                audioSource.PlayOneShot(deflected3, 0.7f);
            }
            

            Vector3 vel = Vector3.Normalize(thisRB.velocity);

            Vector3 shieldF = col.gameObject.transform.forward;

            vel = vel - 2 * Vector3.Dot(vel, shieldF) * shieldF;
            

            

            var vec = Vector3.Normalize(col.gameObject.transform.position - transform.position);



            // Raycast checking

            Ray r = new Ray(transform.position, vel);
            Debug.DrawRay(r.origin, r.direction * 20, Color.white, 1);
            RaycastHit hit;

            bool shouldHit = false;
            //Will the first ray hit an enemy?
            if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 8))
            {
                
                shouldHit = true;

                vec = Vector3.Normalize(hit.transform.position - transform.position);


            }
            else //No? well then will another ray rotated to the right hit?
            {

                r.direction = Quaternion.AngleAxis(4, Vector3.up) * r.direction;

                Debug.DrawRay(r.origin, r.direction * 20, Color.red, 1);
                if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 8))
                {
                    shouldHit = true;

                    vec = Vector3.Normalize(hit.transform.position - transform.position);

                }
                else // No? well then will another ray rotated to the left hit?
                { 
                    r.direction = Quaternion.AngleAxis(-8, Vector3.up) * r.direction;

                    Debug.DrawRay(r.origin, r.direction * 20, Color.green, 1);

                    if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 8))
                    {
                        shouldHit = true;

                        vec = Vector3.Normalize(hit.transform.position - transform.position);
                    } else // ETC. if they don't hit, this string of if and else statements create a fan of Rays to check for enemy collision
                    {
                        r.direction = Quaternion.AngleAxis(-4, Vector3.up) * r.direction;

                        Debug.DrawRay(r.origin, r.direction * 20, Color.yellow, 1);
                        if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 8))
                        {
                            shouldHit = true;

                            vec = Vector3.Normalize(hit.transform.position - transform.position);

                        } else
                        {
                            r.direction = Quaternion.AngleAxis(16, Vector3.up) * r.direction;

                            Debug.DrawRay(r.origin, r.direction * 20, Color.blue, 1);
                            if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 8))
                            {
                                shouldHit = true;

                                vec = Vector3.Normalize(hit.transform.position - transform.position);

                            }
                        }
                    }


                } 

            }
            
            //If any of the rays hit an enemy, then "shouldHit" was set to true.

            if (shouldHit == true)
            {

                //Set to the direction needed to hit the enemy the ray collided with
                vel = vec;
            }

            
            //Set Velocity
            thisRB.velocity = vel * thisRB.velocity.magnitude * 1.2f;

            // Lock Velocity (Prevents the projectile's RB from losing it's velocity)

                lockedVelocity = thisRB.velocity;
                velIsLocked = true;


            var enemies = GameObject.FindGameObjectsWithTag("Enemy");

            //Does collide with enemies
            foreach (var enemy in enemies)
            {
                if (!(enemy.GetComponent<Collider>() == GetComponent<Collider>()))
                    Physics.IgnoreCollision(enemy.GetComponent<Collider>(), GetComponent<Collider>(), false);
            }


            var shield = GameObject.FindGameObjectWithTag("Shield");

            if (!(shield.GetComponent<Collider>() == GetComponent<Collider>()))
                Physics.IgnoreCollision(shield.GetComponent<Collider>(), GetComponent<Collider>());


            
            if (!(p.GetComponent<Collider>() == GetComponent<Collider>()))
                Physics.IgnoreCollision(p.GetComponent<Collider>(), GetComponent<Collider>());


            gameObject.GetComponent<Renderer>().material = reflectedMat;
            


            trail.sharedMaterial = reflectedMat;

            particleSys.material = reflectedMat;

            //thisRB.velocity = shieldF * 30f;
            //Debug.Log("Hit Shield");

        }

        if (col.gameObject.tag == "Player")
        {         
            col.gameObject.GetComponent<PlayerMovement>().Ouch();
            col.gameObject.GetComponent<PlayerMovement>().hitSound();
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Enemy")
        {
            var enemyAi = col.gameObject.GetComponent<enemyAI>();
            //Delete the projectile only if this enemy hasn't already been hit
            if (enemyAi.isHit != true)
            {
                Destroy(gameObject);
            }


            enemyAi.Die();
            
        }
        
        if(col.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}

