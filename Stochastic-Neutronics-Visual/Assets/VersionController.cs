using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionController : MonoBehaviour
{
    public GameObject desktopUIPrefab;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        Instantiate(desktopUIPrefab);
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
