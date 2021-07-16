using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutronControl : MonoBehaviour
{
    Vector3 direction;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setIsotropicRandomDirection()
    {
        float x = 1;
        float y = 1;
        float z = 1;

        while (Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2) > 1)
        {
            x = Random.Range(-1.0f, 1.0f);
            y = Random.Range(-1.0f, 1.0f);
            z = Random.Range(-1.0f, 1.0f);
        }

        float magnitude = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2));

        direction = new Vector3(x, y, z) / magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition += direction * speed * Time.deltaTime;

        if(gameObject.transform.localPosition.x > 0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - 1, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        } else if (gameObject.transform.localPosition.x < -0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + 1, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        }
        else if (gameObject.transform.localPosition.y > 0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x , gameObject.transform.localPosition.y - 1, gameObject.transform.localPosition.z);
        }
        else if (gameObject.transform.localPosition.y < -0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 1, gameObject.transform.localPosition.z);
        }
        else if (gameObject.transform.localPosition.z > 0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z - 1);
        }
        else if (gameObject.transform.localPosition.z < -0.5f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z + 1);
        }
    }
}
