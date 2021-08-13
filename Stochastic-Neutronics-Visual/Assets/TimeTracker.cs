using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTracker : MonoBehaviour
{
    public double simulatedTime = 0; // The current simulated time (s)
    public float timeDilationFactor; // The factor that time slows down by when neutrons are present (to allows neutrons to be better seen)
    public GameObject neutronPrefab; // A reference to the neutron prefab
    public float timeDilation; // The current value of time dilation. Should be 1 when no neutrons are present and equal to timeDilationFactor when neutrons are present
    float resetTimer; // The amount of time the reset banners have left to be displayed (s)
    bool reset = false; // Whether the reset banners are present or not
    public GameObject resetBanners; // A reference to the resetBanners GameObject
    public int nNeutronsMax; // The maximum number of neutrons that can be in the system before the system resets
    public float resetDuration; // How long the system resets for (s)
    public float interval; // The interval of the last time-step
    public NeutronicsData neutronicsData; // A reference to the neutronics data

    // Update is called once per frame
    void Update()
    {
        // If the reset timer has counted down to zero then the time for the reset banners being displayed is over
        if (resetTimer < 0)
        {
            resetBanners.SetActive(false); // Deactivate the banners
            simulatedTime = 0; // Reset the time to zero
            reset = false; // The system isn't resetting anymore
            resetTimer = 0; // Reset the reset timer
        }
        else if(reset) // The system is currently resetting
        {
            // Reduce the resetTimer and then return - nothing else should happen while the system resets
            resetTimer -= Time.deltaTime;
            return;
        }

        // Get a list of all the particles (neutron prefabs have the "Particle" tag and are the only objects which do
        GameObject[] neutrons = GameObject.FindGameObjectsWithTag("Particle");
        int n_neutron = neutrons.Length;

        // If there are more neutrons than the maximum number, the system needs to be reset
        if (n_neutron > nNeutronsMax)
        {
            // Destroy each neutron currently present
            for (int i = 0; i < n_neutron; i++)
            {
                Destroy(neutrons[i]);
            }
            reset = true; // The system is resetting
            resetBanners.SetActive(true); // Activate the resetting banners
            resetTimer = resetDuration; // Set the reset timer to its maximum value (preparing for it to count down)
            return; // Nothing else needs to happen

        }else if (n_neutron > 0) // If there are neutrons present, there is time dilation. If not, there isn't
        {
            timeDilation = timeDilationFactor;
        }
        else
        {
            timeDilation = 1;
        }

        // Spawn new neutrons from sources and use this to find out how much the simualted time should be incremented by
        // The maximum interval is the amount of simualted time that has passed
        interval = SpawnSources(Time.deltaTime * timeDilation);

        // Increase the simualted time
        simulatedTime += interval;
    }

    float SpawnSources(float maxInterval)
    {
        // Calcualte how long it will take until the next neutron is produced by a source
        float rand = Random.Range(0.0f, 1.0f);
        float timeToNext = -Mathf.Log(rand) / neutronicsData.sourceStrength;

        if (timeToNext < maxInterval) // If the neutron has been produced before the end of the time-step, create it
        {
             // Find a random location in the square
            Vector3 startCoordinate = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            // Instsntiate the new neutron at this location
            GameObject new_neutron = Instantiate(neutronPrefab, startCoordinate, Quaternion.identity);

            // Return a shorter time interval - essentially reducing the time step to the time taken to produce the neutron from the source
            return (timeToNext);
        }
        else
        {
            // If not neutrons were produced, simply return the maximum interval
            return (maxInterval);
        }
    }
}
