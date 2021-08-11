using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusControls : MonoBehaviour
{
    public NeutronicsData neutronicsData;
    float kSlider;
    float sourceSlider;
    public float sliderSpeed;
    float hapticTimerLeft;
    float hapticTimerRight;
    public float hapticPulseLength;
    public float hapticPulseFrequency;
    public float hapticPulseAmplitude;

    // Start is called before the first frame update
    void Start()
    {
        kSlider = 0.5f;
        sourceSlider = 0f;

        hapticTimerLeft = 0;
        hapticTimerRight = 0;

        //neutronicsData = GameObject.Find("Neutronics Data").GetComponent<NeutronicsData>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if any relevant buttons are pressed, change the slider value and turn on the haptic pulse
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
        {
            sourceSlider = Mathf.Min(sourceSlider + Time.deltaTime * sliderSpeed, 1.0f);
            hapticTimerLeft = hapticPulseLength;
        }
        if(OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            sourceSlider = Mathf.Max(sourceSlider - Time.deltaTime * sliderSpeed, 0.0f);
            hapticTimerLeft = hapticPulseLength;
        }
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            kSlider = Mathf.Min(kSlider + Time.deltaTime * sliderSpeed, 1.0f);
            hapticTimerRight = hapticPulseLength;
        }
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            kSlider = Mathf.Max(kSlider - Time.deltaTime * sliderSpeed, 0.0f);
            hapticTimerRight = hapticPulseLength;
        }

        // Change the source strength and k-value
        neutronicsData.sourceStrength = Mathf.Pow(10, sourceSlider * 6);

        if (kSlider < 1.0f / 3.0f)
        {
            neutronicsData.k = 0.98f * 3.0f * kSlider;
        }
        else if (kSlider < 2.0f / 3.0f)
        {
            neutronicsData.k = 0.98f + (kSlider - 1.0f / 3.0f) * 3.0f * 0.04f;
        }
        else
        {
            neutronicsData.k = 1.02f + (kSlider - 2.0f / 3.0f) * 3.0f * (neutronicsData.chi_bar - 1.02f);
        }

        // Turn the haptic pulses on or off dependent on the value the haptic timers
        if (hapticTimerLeft > 0.0f)
        {
            OVRInput.SetControllerVibration(hapticPulseFrequency, hapticPulseAmplitude, OVRInput.Controller.LTouch);
        }
        if (hapticTimerRight > 0.0f)
        {
            OVRInput.SetControllerVibration(hapticPulseFrequency, hapticPulseAmplitude, OVRInput.Controller.RTouch);
        }

        if (hapticTimerLeft < 0.0f)
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
        if (hapticTimerRight < 0.0f)
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }

        // Decrease the haptic pulse timers
        hapticTimerLeft -= Time.deltaTime;
        hapticTimerRight -= Time.deltaTime;
    }
}
