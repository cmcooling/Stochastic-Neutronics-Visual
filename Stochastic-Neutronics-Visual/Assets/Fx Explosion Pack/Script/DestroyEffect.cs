using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {
    private float elapsedTime;
    private void Start()
    {
        elapsedTime = 0;
    }


    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 1)
        {
            Destroy(gameObject);
        }
    }
}
