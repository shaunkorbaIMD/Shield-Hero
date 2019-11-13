using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 999999999f;

    public Vector3 offset;
    public Vector3 startOffset;


    // Start is called before the first frame update
    void Start()
    {
        startOffset = offset;
        //transform.position = target.position + offset * 0.6f + new Vector3(0,6,-3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed* (Time.unscaledDeltaTime*41));


        transform.position = smoothedPosition;
    }
}
