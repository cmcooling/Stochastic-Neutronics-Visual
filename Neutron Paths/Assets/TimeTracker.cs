using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTracker : MonoBehaviour
{
    float simulatedTime;
    public float timeDilation = 1;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        simulatedTime = 0;
        timeText = GameObject.Find("Canvas/Time Display").GetComponent("Text") as Text;
    }

    // Update is called once per frame
    void Update()
    {
        simulatedTime += Time.deltaTime * timeDilation;
        timeText.text = simulatedTime.ToString() + "s";
    }
}
