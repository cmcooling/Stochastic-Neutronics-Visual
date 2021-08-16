using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Attached to the "Desktop Components/Canvas/Time Display" GameObject
// Updates the time displayed
public class TimeDisplay : MonoBehaviour
{
    public TimeTracker timeTracker; // A reference to the time tracker
    public Text timeDisplay; // A reference ot the time display in the UI

    // Update is called once per frame
    void Update()
    {
        // Update the time display on the screen
        timeDisplay.text = "t = " + timeTracker.simulatedTime.ToString("0.000000") + "s";
    }
}
