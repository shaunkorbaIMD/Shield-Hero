using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setHealth : MonoBehaviour
{
    public GameObject player;

    public Text healthText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.GetComponent<PlayerMovement>().health.ToString();        
    }
}
