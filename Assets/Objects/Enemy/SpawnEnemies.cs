using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;

    public Vector3 spawnValues;

    public float spawnWaitDuration = 1f;
    private float spawnWait;
    

    public float maxEnemies = 6;

    private GameObject[] getCount;

    // Start is called before the first frame update
    void Start()
    {
        spawnWait = spawnWaitDuration;
        
    }

    void SpawnEnemy()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        Vector3 spawnPosition = new Vector3(
            Random.Range(x - spawnValues.x, x + spawnValues.x),
            y,
            Random.Range(z - spawnValues.z, z + spawnValues.z));

        Instantiate(enemy,  spawnPosition, gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        getCount = GameObject.FindGameObjectsWithTag("Enemy");
        int count = getCount.Length;


        if(count < maxEnemies)
        {
            spawnWait -= Time.deltaTime;

            if(spawnWait<0)
            {
                SpawnEnemy();
                spawnWait = spawnWaitDuration;
            }

        }



    }
}
