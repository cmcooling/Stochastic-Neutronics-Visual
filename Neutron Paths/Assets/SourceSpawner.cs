using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceSpawner : MonoBehaviour
{
    public float sourceStrength = 1;
    public GameObject sourcePingPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float simulatedInterval = 0.01f;
        float rand = Random.Range(0.0f, 1.0f);
        

        float currentTime = 0.0f;
        while( currentTime < simulatedInterval)
        {
            float timeToNext = -Mathf.Log(rand) / sourceStrength;
            currentTime += timeToNext;

            if (currentTime < simulatedInterval)
            {
                Vector3 startCoordinate = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                Instantiate(sourcePingPrefab, startCoordinate, Quaternion.identity);
            }
        }

        

    }
}
