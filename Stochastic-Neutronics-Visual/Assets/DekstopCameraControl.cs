using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DekstopCameraControl : MonoBehaviour
{
    public Camera camera_ref;
    public GameObject target;
    static float minHeight = 1.0f;
    static float maxHeight = 2.0f;
    static float heightRange = maxHeight - minHeight;
    Vector2 longLat;
    float height;
    static float minMoveSpeed = 1.0f;
    static float maxMoveSpeed = 2f;
    KeyCode w;
    KeyCode s;
    KeyCode a;
    KeyCode d;
    KeyCode up;
    KeyCode down;
    KeyCode left;
    KeyCode right;


    // Start is called before the first frame update
    void Start()
    {
        camera_ref = Camera.main;
        height = 2.0f;
        longLat = new Vector2(0, 0);

        UpdatePosition();

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
        float x = Mathf.Cos(longLat.y) * Mathf.Cos(longLat.x) * height;
        float z = Mathf.Cos(longLat.y) * Mathf.Sin(longLat.x) * height;
        float y = Mathf.Sin(longLat.y) * height;

        Vector3 position = new Vector3(x, y, z);
        camera_ref.transform.position = position;

        camera_ref.transform.LookAt(target.transform);
    }

    float MoveSpeed()
    {
        return (((height - minHeight) * maxMoveSpeed + (maxHeight - height) * minMoveSpeed) / (heightRange));
    }

    // Update is called once per frame
    void Update()
    {
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

        Vector2 scrollDelta = Input.mouseScrollDelta;

        if (scrollDelta.y > 0)
        {
            height = Mathf.Max(height - 0.1f * scrollDelta.y, minHeight);
        }

        if (scrollDelta.y < 0)
        {
            height = Mathf.Min(height - 0.1f * scrollDelta.y, maxHeight);
        }

        UpdatePosition();
    }
}
