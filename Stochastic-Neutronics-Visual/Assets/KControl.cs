using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KControl : MonoBehaviour
{
    public Text text;

    public void Recalculate(float value)
    {
        NeutronicsData neutronicsData = GameObject.Find("Neutronics Data").GetComponent<NeutronicsData>();
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

        neutronicsData.k = k;
        text.text = "k = " + k.ToString("0.000");
    }
}
