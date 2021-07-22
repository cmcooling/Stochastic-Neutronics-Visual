using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTracker : MonoBehaviour
{
    public float simulatedTime;
    public float timeDilationFactor = 1e-1f;
    public GameObject neutronPrefab;
    public float timeDilation = 1;

    // Start is called before the first frame update
    void Start()
    {
        simulatedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] neutrons = GameObject.FindGameObjectsWithTag("Particle");
        int n_neutron = neutrons.Length;

        if (n_neutron > 500)
        {
            for (int i = 0; i < n_neutron; i++)
            {
                Destroy(neutrons[i]);
            }
            timeDilation = timeDilation = timeDilationFactor;
        }else if (n_neutron > 0)
        {
            timeDilation = timeDilationFactor;
        }
        else
        {
            timeDilation = 1;
        }

        float interval = SpawnSources(Time.deltaTime * timeDilation);

        simulatedTime += interval;
    }

    float SpawnSources(float maxInterval)
    {
        float rand = Random.Range(0.0f, 1.0f);
        NeutronicsData neutronicsData = GameObject.Find("Neutronics Data").GetComponent<NeutronicsData>();
        float timeToNext = -Mathf.Log(rand) / neutronicsData.sourceStrength;

        if (timeToNext < maxInterval)
        {
            
            Vector3 startCoordinate = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            GameObject new_neutron = Instantiate(neutronPrefab, startCoordinate, Quaternion.identity);

            new_neutron.GetComponent<NeutronControl>().setIsotropicRandomDirection();

            return (timeToNext);
        }
        else
        {
            return (maxInterval);
        }

    }
}
