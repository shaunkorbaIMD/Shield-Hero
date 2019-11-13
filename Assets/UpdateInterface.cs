using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateInterface : MonoBehaviour
{
    [SerializeField]
    private Text autoAimOn;

    [SerializeField]
    public Text ShieldMethod;

    [SerializeField]
    private PlayerMovement Player;

    
    private float shieldFillAmount;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.aimMethod)
        {
            ShieldMethod.text = "Movement";
        }
        else
        {
            ShieldMethod.text = "Click";
        }

        if (Player.autoAimOn)
        {
            autoAimOn.text = "isOn";
        }
        else
        {
            autoAimOn.text = "isOff";
        }
    }
}
