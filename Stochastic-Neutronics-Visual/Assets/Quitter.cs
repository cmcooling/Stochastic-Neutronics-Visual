using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to the "Desktop Components/Quitter" GameObject
// Quits the game if the escape key is pressed (in the Standalone build of the app)
public class Quitter : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // This is a compiler directive that conditionally compiles this code if the app is built as a standalone app
        #if UNITY_STANDALONE
            // Check if the escape key has been pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Quit the app
                Application.Quit();
            }
        #endif
    }
}
