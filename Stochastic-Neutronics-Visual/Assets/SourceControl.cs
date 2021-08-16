using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Attached to the "Desktop Components/Canvas/Source Display" component
// Activated when the value of "Desktop Component/Canvas/Source Slider" slider changes
// Updates the source strength of the sytem and the text displaying the source strength
public class SourceControl : MonoBehaviour
{
    public Text text; // A reference to the text displaying the source strength
    public NeutronicsData neutronicsData; // A reference to the neutronics data

    // This function is set to be called when the slider in the Source Slider GameObject is updated
    // The value passed is the value of the slider
    public void Recalculate(float value)
    {
        // Calculate the new source
        float source = Mathf.Pow(10, value * 6);

        // Update the source in the neutronics data
        neutronicsData.sourceStrength = source;

        // Calcualte the format required for the source display dependent on the size of the source
        string format = "0.0";
        if (source < 100)
        {
            format = "0.0";
        }else if(source<1000){
            format = "0.";
        }
        else
        {
            format = "0.00E+0";
        }

        // Update the message displaying the source in the UI
        text.text = "Source = " + source.ToString(format) + "n/s";
    }
}
