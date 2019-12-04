using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldRegen : MonoBehaviour
{ 
    public Image barImage;
    public PlayerMovement playerMovement;

    
    private void Update()
    {
        if(barImage == null)
            print("bar img null");
        

        if(playerMovement == null)
            print("player null");

        barImage.fillAmount = playerMovement.shieldDuration / playerMovement.shieldDurationLength;

    }
}
