using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Attached to the "Oculus Components/Oculus Data Display" GameObject
// Updates the time, source and k values in the data display in the Oculus version
public class OculusDisplayControl : MonoBehaviour
{
    public TextMesh kText; // A reference to the text component which displays the value of k
    public TextMesh sourceText; // A reference to the text component which displays the source strength
    public TextMesh timeText; // A reference to the text component which displays the current simualted time
    public NeutronicsData neutronicsData; // A reference to the neutronics data
    public TimeTracker timeTracker; // A reference to the time tracker

    // Update is called once per frame
    void Update()
    {
        // Change the text in the kText component to match the current value of k
        kText.text = "k: " + neutronicsData.k.ToString("0.000");

        // Create a format string for the source strength. Different formats are relevant for different magnitudes of source strength
        string format = "0.0";
        if (neutronicsData.sourceStrength < 100)
        {
            format = "0.0";
        }
        else if (neutronicsData.sourceStrength < 1000)
        {
            format = "0.";
        }
        else
        {
            format = "0.00E+0";
        }

        // Update the text of the sourceText component to match the current source strength
        sourceText.text = "Source = " + neutronicsData.sourceStrength.ToString(format) + "n/s";

        // Update the text of the timeText component to match the current simulated time
        timeText.text = "Time: " + timeTracker.simulatedTime.ToString("0.000000") + "s";
    }
}
