using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    TimeTracker timeTracker;
    Text timeDisplay;
    // Start is called before the first frame update
    void Start()
    {
        timeTracker = GameObject.Find("Time Tracker").GetComponent<TimeTracker>();
        timeDisplay = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeDisplay.text = timeTracker.simulatedTime.ToString() + "s";
    }
}
