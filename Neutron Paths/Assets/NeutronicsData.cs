using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutronicsData : MonoBehaviour
{
    public float speed;
    public float k;
    public float lifetime;
    public float[] p_f_n;
    public float chi_bar;
    public float[] p_f_n_cumulative;

    // Start is called before the first frame update
    void Start()
    {
        p_f_n = new float[] {0.05f, 0.15f, 0.29f, 0.29f, 0.15f, 0.05f, 0.01f, 0.01f};
        chi_bar = 0;
        p_f_n_cumulative = new float[8];
        for (int i=0; i<8; i++)
        {
            chi_bar += p_f_n[i] * i;
        }

        p_f_n_cumulative[0] = 0.05f;
        for (int i = 1; i < 8; i++)
        {
            p_f_n_cumulative[i] = p_f_n_cumulative[i - 1] + p_f_n[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateData()
    {

    }
}
