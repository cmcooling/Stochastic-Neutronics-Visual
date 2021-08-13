using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KControl : MonoBehaviour
{
    public NeutronicsData neutronicsData; // A reference to the neutronics data
    public Text text; // A reference to the text displaying the k-value

    // This function is called if the slider in the K Slider GameObject is adjusted
    // The value passed is the value of the slider
    public void Recalculate(float value)
    {
        // Calcualte the value of k
        // Bias it so the middle third of the slider is the region 0.98-1.02
        float k = 0;
        if (value < 1.0f / 3.0f)
        {
            k = 0.98f * 3.0f * value;
        }else if(value < 2.0f / 3.0f){
            k = 0.98f + (value - 1.0f / 3.0f) * 3.0f * 0.04f;
        }
        else
        {
            k = 1.02f + (value - 2.0f / 3.0f) * 3.0f * (neutronicsData.chi_bar - 1.02f);
        }

        // Update the k value in neutronics data
        neutronicsData.k = k;
        // Update the text displaying the k-value
        text.text = "k = " + k.ToString("0.000");
    }
}
