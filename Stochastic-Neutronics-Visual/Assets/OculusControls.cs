using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to the "Oculus Components" GameOBject
// Uses the controls on the controller to adjust the source strength and k-value
// Causes a controller to vinrate when a button on it is pressed
public class OculusControls : MonoBehaviour
{
    public NeutronicsData neutronicsData; // A reference to the neutronics data
    float kSlider; // A value between 0 and 1 to mirror the slider value of the K Control prefab. 0 represents the minimum value and 1 the maximum
    float sourceSlider; // A value between 0 and 1 to mirror the slider value of the K Control prefab. 0 represents the minimum value and 1 the maximum
    public float sliderSpeed; // The rate at which sliders change per second if the appropriate button is pressed
    float hapticTimerLeft; // If positive the left controller is vibrating. If negative, it's not.
    float hapticTimerRight; // If positive the right controller is vibrating. If negative, it's not.
    public float hapticPulseLength; // The length of time that haptic pulses continue after the relevant trigger is released
    public float hapticPulseFrequency; // The frequency of the haptic pulse
    public float hapticPulseAmplitude; // The intensity of the haptic pulse

    // Start is called before the first frame update
    void Start()
    {
        // Set some intial values
        kSlider = 0.5f;
        sourceSlider = 0f;
        hapticTimerLeft = 0;
        hapticTimerRight = 0;

    }

    // Update is called once per frame
    void Update()
    {
        // Check if any relevant buttons are pressed, change the slider value and turn on the haptic pulse
        // The hapticTimer is set to the haptic pulse length so it will remain on for at least that long
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

        // Calcualte the source strength and k-value from the slider values
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

        // Turn the haptic pulses on if the haptic timers are positive
        if (hapticTimerLeft > 0.0f)
        {
            OVRInput.SetControllerVibration(hapticPulseFrequency, hapticPulseAmplitude, OVRInput.Controller.LTouch);
        }
        if (hapticTimerRight > 0.0f)
        {
            OVRInput.SetControllerVibration(hapticPulseFrequency, hapticPulseAmplitude, OVRInput.Controller.RTouch);
        }

        // Turn the haptic pulses off if the haptic timers are negative
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
