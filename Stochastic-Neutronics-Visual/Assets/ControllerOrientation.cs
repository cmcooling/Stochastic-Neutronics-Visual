using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerOrientation : MonoBehaviour
{
    public Transform trackingSpace; // This will be a reference to the TrackingSpace component of OVRCameraRig component of OVRPLayerController
    public Transform playerTransform; // This will be a reference to the OVRPlayerController
    public OVRInput.Controller controller; // A reference to the physical controller being tracked

    // Update is called once per frame
    void Update()
    {
        // Get the rotation of the controller relative to the player
        Quaternion controllerLocalRotation = OVRInput.GetLocalControllerRotation(controller);
        // Transform the controller relative to the player by the player rotation relative to the world to get the controller rotation relative to the world
        Quaternion controllerWorldRotation = playerTransform.localRotation * controllerLocalRotation;
        // Set the rotation of the controller in the world
        transform.rotation = controllerWorldRotation;
        // Set the position of the controller in the world
        transform.position = trackingSpace.TransformPoint(OVRInput.GetLocalControllerPosition(controller));
    }
}
