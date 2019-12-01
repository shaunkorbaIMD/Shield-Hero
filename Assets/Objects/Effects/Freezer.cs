using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour
{
    private float duration = 0.1f;

    float _pendingFreezeDuration = 0f;
    public bool _isFrozen = false;

 

    

    public void Freeze(float dur)
    {
        duration = dur;
        _pendingFreezeDuration = duration;

    }


    // Update is called once per frame
    void Update()
    {
        if (_pendingFreezeDuration > 0 && !_isFrozen)
        {
            StartCoroutine(DoFreeze());
        }
    }


    IEnumerator DoFreeze()
    {
        _isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        _pendingFreezeDuration = 0;
        _isFrozen = false;
    }
}
