using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    private const float destroySeconds = 2.0f;
    private const float destroyBorderY = 7.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, destroySeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > destroyBorderY)
        {
            gameObject.SetActive(false);
        }
    }
}
