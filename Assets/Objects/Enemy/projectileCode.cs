using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileCode : MonoBehaviour
{

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

    

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Shield")
        {

            Vector3 vel = Vector3.Normalize(thisRB.velocity);

            Vector3 shieldF = col.gameObject.transform.forward;

            vel = vel - 2 * Vector3.Dot(vel, shieldF) * shieldF;
            

            

            var vec = Vector3.Normalize(col.gameObject.transform.position - transform.position);



            // Raycast checking

            Ray r = new Ray(transform.position, vel);
            Debug.DrawRay(r.origin, r.direction * 20, Color.white, 1);
            RaycastHit hit;

            bool shouldHit = false;

            if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 8))
            {

                Debug.Log("Will");
                shouldHit = true;

                vec = Vector3.Normalize(hit.transform.position - transform.position);


            }
            else
            {

                r.direction = Quaternion.AngleAxis(4, Vector3.up) * r.direction;

                Debug.DrawRay(r.origin, r.direction * 20, Color.red, 1);
                if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 8))
                {

                    Debug.Log("redWill");
                    shouldHit = true;

                    vec = Vector3.Normalize(hit.transform.position - transform.position);

                }
                else
                { //if this ray didn't pickup an enemy..

                    r.direction = Quaternion.AngleAxis(-8, Vector3.up) * r.direction;

                    Debug.DrawRay(r.origin, r.direction * 20, Color.green, 1);

                    if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 8))
                    {

                        Debug.Log("greenWill");
                        shouldHit = true;

                        vec = Vector3.Normalize(hit.transform.position - transform.position);
                    } else
                    {
                        r.direction = Quaternion.AngleAxis(-4, Vector3.up) * r.direction;

                        Debug.DrawRay(r.origin, r.direction * 20, Color.yellow, 1);
                        if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 8))
                        {

                            Debug.Log("yellowWill");
                            shouldHit = true;

                            vec = Vector3.Normalize(hit.transform.position - transform.position);

                        } else
                        {
                            r.direction = Quaternion.AngleAxis(16, Vector3.up) * r.direction;

                            Debug.DrawRay(r.origin, r.direction * 20, Color.blue, 1);
                            if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 8))
                            {

                                Debug.Log("blueWill");
                                shouldHit = true;

                                vec = Vector3.Normalize(hit.transform.position - transform.position);

                            }
                        }
                    }


                } 

            }
            


            if (shouldHit == true)
            {
                vel = vec;
            }

            
            thisRB.velocity = vel * thisRB.velocity.magnitude * 1.2f;

            // Lock Velocity

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
            //PlayerMovement.health--;
            col.gameObject.GetComponent<PlayerMovement>().instance.health--;
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            
        }
        
        if(col.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}

