using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SourceControl : MonoBehaviour
{
    public Text text;

    public void Recalculate(float value)
    {
        NeutronicsData neutronicsData = GameObject.Find("Neutronics Data").GetComponent<NeutronicsData>();
        float source = Mathf.Pow(10, value * 6);

        neutronicsData.sourceStrength = source;

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

        text.text = "Source = " + source.ToString(format) + "n/s";
    }
}
