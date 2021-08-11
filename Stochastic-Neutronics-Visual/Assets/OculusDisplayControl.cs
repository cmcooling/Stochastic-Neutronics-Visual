using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OculusDisplayControl : MonoBehaviour
{
    public TextMesh kText;
    public TextMesh sourceText;
    public TextMesh timeText;
    public NeutronicsData neutronicsData;
    public TimeTracker timeTracker;

    // Update is called once per frame
    void Update()
    {
        kText.text = "k: " + neutronicsData.k.ToString("0.000");

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

        sourceText.text = "Source = " + neutronicsData.sourceStrength.ToString(format) + "n/s";

        timeText.text = "Time: " + timeTracker.simulatedTime + "s";
    }
}
