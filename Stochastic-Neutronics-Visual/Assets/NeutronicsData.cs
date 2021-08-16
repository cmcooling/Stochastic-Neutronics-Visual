using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to the "Neutronics Data" GameObject
// Stores various bits of neutronic data to define the system
public class NeutronicsData : MonoBehaviour
{
    public float speed; // The speed of the neutrons (m/s)
    public float k; // The multiplication coeeficient for the neutrons
    public float lifetime; // How long neutrons last on average before ebing absorbed of causing a fission
    public float[] p_f_n; // The probability of fissions producing different numbers of neutrons. p_f_n[0] is probability of zero neutrons being produced and so on.
    public float chi_bar; // The mean number of neutrons produced per fission
    public float[] p_f_n_cumulative; // The probability of at most n neutrons being produced in a fission
    public float sourceStrength; // The strength of the source (neutrons/s)

    // Start is called before the first frame update
    void Start()
    {
        // Set the value of various physical constants
        p_f_n = new float[] {0.0319004f, 0.1725213f, 0.3361397f, 0.3038798f, 0.1266155f, 0.0261843f, 0.0026170f, 0.0001421f };
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
}
