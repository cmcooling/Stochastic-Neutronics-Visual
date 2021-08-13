using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DekstopCameraControl : MonoBehaviour
{
    public Camera camera_ref; // A reference to the main camera
    public GameObject target; // A reference to a GameObject at the origin
    static float minDist = 1.0f; // The minimum distance of the camera from the origin
    static float maxDist = 2.0f; // The maximum distance of the camera from the origin
    static float distRange = maxDist - minDist; // The range of distances the camera may be from the origin
    Vector2 longLat; // The longitude and latitude of the camera
    float height; // The height of the camera
    static float minMoveSpeed = 1.0f; // The minimum move speed (used when the camera is closer to the origin)
    static float maxMoveSpeed = 2.0f; // The maximum move speed (used when the camera is further from the origin)
    KeyCode w; // The KeyCode for the "w" key
    KeyCode s; // The KeyCode for the "s" key
    KeyCode a; // The KeyCode for the "a" key
    KeyCode d; // The KeyCode for the "d" key
    KeyCode up; // The KeyCode for the "up-arrow" key
    KeyCode down; // The KeyCode for the "down-arrow" key
    KeyCode left; // The KeyCode for the "left-arrow" key
    KeyCode right; // The KeyCode for the "right-arrow" key


    // Start is called before the first frame update
    void Start()
    {
        // Set the initial location of the camera
        height = 2.0f;
        longLat = new Vector2(0, 0);

        UpdatePosition();

        // Get the KeyCodes of the various keys used to control the camera
        up = KeyCode.UpArrow;
        down = KeyCode.DownArrow;
        left = KeyCode.LeftArrow;
        right = KeyCode.RightArrow;
        w = KeyCode.W;
        s = KeyCode.S;
        a = KeyCode.A;
        d = KeyCode.D;
    }

    void UpdatePosition()
    {
        // Calcualte the x, y and z cooridinates of the camera from the latitude and longitude
        float x = Mathf.Cos(longLat.y) * Mathf.Cos(longLat.x) * height;
        float z = Mathf.Cos(longLat.y) * Mathf.Sin(longLat.x) * height;
        float y = Mathf.Sin(longLat.y) * height;

        // Update the position of the camera
        camera_ref.transform.position = new Vector3(x, y, z);

        // Cause the camera to look at the origin (the centre of the square)
        camera_ref.transform.LookAt(target.transform);
    }

    float MoveSpeed()
    {
        // Calcualte the speed the camera moves at
        return (((height - minDist) * maxMoveSpeed + (maxDist - height) * minMoveSpeed) / (distRange));
    }

    // Update is called once per frame
    void Update()
    {
        // Change the longitude and latitude of the camera dependent on the key(s) being pressed
        if (Input.GetKey(w) || Input.GetKey(up))
        {
            longLat.y = Mathf.Min(longLat.y + MoveSpeed() * Time.deltaTime, Mathf.PI / 2 - 1e-4f);
        }

        if (Input.GetKey(s) || Input.GetKey(down))
        {
            longLat.y = Mathf.Max(longLat.y - MoveSpeed() * Time.deltaTime, -Mathf.PI / 2 + 1e-4f);
        }

        if (Input.GetKey(d) || Input.GetKey(right))
        {
            longLat.x += MoveSpeed() * Time.deltaTime / Mathf.Max(0.3f, Mathf.Cos(longLat.y));
        }

        if (Input.GetKey(a) || Input.GetKey(left))
        {
            longLat.x -= MoveSpeed() * Time.deltaTime / Mathf.Max(0.3f, Mathf.Cos(longLat.y));
        }

        // Find how far the scroll wheel on the mouse has moved
        Vector2 scrollDelta = Input.mouseScrollDelta;

        // Zoom in or out dependent on the amount the scroll wheel moved
        if (scrollDelta.y > 0)
        {
            height = Mathf.Max(height - 0.1f * scrollDelta.y, minDist);
        }

        if (scrollDelta.y < 0)
        {
            height = Mathf.Min(height - 0.1f * scrollDelta.y, maxDist);
        }

        // Update the position of the camera based on the new latitude, longitude and height
        UpdatePosition();
    }
}
