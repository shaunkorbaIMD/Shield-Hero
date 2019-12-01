using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 999999999f;

    public Vector3 offset;
    public Vector3 startOffset;

    private bool isShaking = false;
    private float shakeTimer = 0f;
    
    public float shakeAmount = 0.15f;


    // Start is called before the first frame update
    void Start()
    {
        startOffset = offset;
        //transform.position = target.position + offset * 0.6f + new Vector3(0,6,-3);
    }

    public void ShakeTheCamera(float timer)
    {
        shakeTimer = timer;
        isShaking = true;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed* (Time.unscaledDeltaTime*41));
        
        transform.position = smoothedPosition;

        if (shakeTimer > 0f)
        {
            transform.position += new Vector3(Random.Range(-shakeAmount, shakeAmount), 0, Random.Range(-shakeAmount, shakeAmount));

            shakeTimer -= 4.0f * Time.deltaTime;

        } else if(isShaking) {

            isShaking = false;
        }
    }
}
