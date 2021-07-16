using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceSpawner : MonoBehaviour
{
    public float sourceStrength = 1;
    public GameObject neutronPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float simulatedInterval = 0.01f;        

        float elapsedTime = 0.0f;
        while( elapsedTime < simulatedInterval)
        {
            float rand = Random.Range(0.0f, 1.0f);
            float timeToNext = -Mathf.Log(rand) / sourceStrength;
            elapsedTime += timeToNext;

            if (elapsedTime < simulatedInterval)
            {
                Vector3 startCoordinate = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                GameObject new_neutron = Instantiate(neutronPrefab, startCoordinate, Quaternion.identity);

                new_neutron.GetComponent<NeutronControl>().setIsotropicRandomDirection();
            }
        }

        

    }
}
