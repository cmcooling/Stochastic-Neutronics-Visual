using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingDecay : MonoBehaviour
{
    public float PingDecayRateRealTime = 3;
    float originalX;
    // Start is called before the first frame update
    void Start()
    {
        originalX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float frameTime = Time.deltaTime;
        float scaleRatio = Mathf.Exp(-PingDecayRateRealTime * frameTime);
        transform.localScale *= scaleRatio;
        
        if (transform.localScale.x / originalX < 0.01)
        {
            Destroy(gameObject);
        }
    }
}
