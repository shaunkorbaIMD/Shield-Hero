using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileCode : MonoBehaviour
{

    public Rigidbody thisRB;

    private Vector3 lockedVelocity;
    private bool velIsLocked = false;
    // Start is called before the first frame update
    void Start()
    {
        var projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach(var projectile in projectiles)
        {
            if (!(projectile.GetComponent<Collider>() == GetComponent<Collider>()))
                Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
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
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Shield")
        {

            Vector3 vel = Vector3.Normalize(thisRB.velocity);

            Vector3 shieldF = col.gameObject.transform.forward;

            thisRB.velocity = vel - 2 * Vector3.Dot(vel, shieldF) * shieldF;
            thisRB.velocity = thisRB.velocity * 6f;

            lockedVelocity = thisRB.velocity;
            velIsLocked = true;

            //thisRB.velocity = shieldF * 30f;
            Debug.Log("Hit Shield");

        }
        
    }
}
