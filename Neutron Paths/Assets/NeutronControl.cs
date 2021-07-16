using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutronControl : MonoBehaviour
{
    Vector3 direction;
    NeutronicsData neutronicsData;
    TimeTracker timeTracker;
    public GameObject neutronPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject neutronics = GameObject.Find("Neutronics Data");
        neutronicsData = GameObject.Find("Neutronics Data").GetComponent<NeutronicsData>();
        timeTracker = GameObject.Find("Time Tracker").GetComponent<TimeTracker>();
    }

    public void setIsotropicRandomDirection()
    {
        float x = 1;
        float y = 1;
        float z = 1;

        while (Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2) > 1)
        {
            x = Random.Range(-1.0f, 1.0f);
            y = Random.Range(-1.0f, 1.0f);
            z = Random.Range(-1.0f, 1.0f);
        }

        float magnitude = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2));

        direction = new Vector3(x, y, z) / magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        float simulatedInterval = Time.deltaTime * timeTracker.timeDilation;
        
        gameObject.transform.localPosition += direction * neutronicsData.speed * simulatedInterval;

        if(gameObject.transform.localPosition.x > 0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - 1, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        } else if (gameObject.transform.localPosition.x < -0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + 1, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        }
        else if (gameObject.transform.localPosition.y > 0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x , gameObject.transform.localPosition.y - 1, gameObject.transform.localPosition.z);
        }
        else if (gameObject.transform.localPosition.y < -0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 1, gameObject.transform.localPosition.z);
        }
        else if (gameObject.transform.localPosition.z > 0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z - 1);
        }
        else if (gameObject.transform.localPosition.z < -0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z + 1);
        }

        float timeToDecay = -Mathf.Log(Random.Range(0.0f, 1.0f)) * neutronicsData.lifetime;

        if (timeToDecay < simulatedInterval)
        {
            float fission_probability = neutronicsData.k / neutronicsData.chi_bar;

            if (Random.Range(0.0f, 1.0f) < fission_probability)
            {
                int n_f = 7;
                float rand_fission = Random.Range(0, 1.0f);
                for (int n=0; n < 7; n++)
                {
                    if(rand_fission < neutronicsData.p_f_n_cumulative[n])
                    {
                        n_f = n;
                    }
                }

                for(int i = 0; i < n_f; i++)
                {
                    GameObject new_neutron = Instantiate(neutronPrefab, gameObject.transform.localPosition, Quaternion.identity);

                    new_neutron.GetComponent<NeutronControl>().setIsotropicRandomDirection();
                }
            }

            


            Destroy(gameObject);
        }
    }
}
