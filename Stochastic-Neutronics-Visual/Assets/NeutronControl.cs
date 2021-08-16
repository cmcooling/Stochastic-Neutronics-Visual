using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to the Neutron prefab
// Defines a random direction for the neutron to head in on creation
// Moves the neutron along that direction every frame
// Checks to see if the neutron is absorbed or fissions each frame
// If the neutron fissions, also chooses how many neutrons are created and creates them
public class NeutronControl : MonoBehaviour
{
    Vector3 direction; // The direction the neutron is heading in
    NeutronicsData neutronicsData; // A reference to the neutronics data
    TimeTracker timeTracker; // A reference to the time tracker
    public GameObject neutronPrefab; // A reference to the neutron prefab
    public GameObject fissionEffect; // A reference to the fission effect prefab
    // Start is called before the first frame update
    void Start()
    {
        // Get the reference to the neutronics data
        neutronicsData = GameObject.Find("Neutronics Data").GetComponent<NeutronicsData>();
        timeTracker = GameObject.Find("Time Tracker").GetComponent<TimeTracker>();

        // Calcualte a random direction by choosing a location in a 2x2x2 cube centred on the origin
        float x = 1;
        float y = 1;
        float z = 1;

        while (Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2) > 1)
        {
            x = Random.Range(-1.0f, 1.0f);
            y = Random.Range(-1.0f, 1.0f);
            z = Random.Range(-1.0f, 1.0f);
        }

        // Normalise the vector from the origin to create a unit vector describing the direction
        float magnitude = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2));
        direction = new Vector3(x, y, z) / magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the neutron
        moveInsideBox();

        //Calcualte what happens to this neutron
        fateSelector();
    }

    void moveInsideBox()
    {
        // Modify the position of the neutron
        gameObject.transform.localPosition += direction * neutronicsData.speed * timeTracker.interval;

        // If the neutron is outside the box, teleport it to the other side of the box so it remains inside the box
        // This represents a periodic boundary condition
        if (gameObject.transform.localPosition.x > 0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - 1, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        }
        else if (gameObject.transform.localPosition.x < -0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + 1, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        }
        else if (gameObject.transform.localPosition.y > 0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 1, gameObject.transform.localPosition.z);
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
    }

    void fateSelector()
    {
        // Randomly determine how long it takes for the neutron to decay
        float timeToDecay = -Mathf.Log(Random.Range(0.0f, 1.0f)) * neutronicsData.lifetime;

        // If this is within the interval of the time-step, resolve the fate of the fission
        if (timeToDecay < timeTracker.interval)
        {
            // Calculate the probability the neutron causes a fission
            float fission_probability = neutronicsData.k / neutronicsData.chi_bar;

            // Find out if the neutron produced a fission
            if (Random.Range(0.0f, 1.0f) < fission_probability)
            {
                // Create a fission effect (the explosion)
                Instantiate(fissionEffect, gameObject.transform.localPosition, Quaternion.identity);

                // Calculate the number of neutrons produced by the fission
                int n_f = 7;
                float rand_fission = Random.Range(0, 1.0f);
                for (int n = 0; n < 7; n++)
                {
                    if (rand_fission < neutronicsData.p_f_n_cumulative[n])
                    {
                        n_f = n;
                        break;
                    }
                }

                // For each neutron that's produced, instaniate a new neutron
                for (int i = 0; i < n_f; i++)
                {
                    GameObject new_neutron = Instantiate(neutronPrefab, gameObject.transform.localPosition, Quaternion.identity);
                }
            }

            // Destroy the neutron this script is attached to
            Destroy(gameObject);
        }
    }
}
