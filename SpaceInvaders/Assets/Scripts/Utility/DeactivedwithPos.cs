using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivedwithPos : MonoBehaviour
{
    private const float destroyBorderY = 7.0f;
 
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > destroyBorderY || transform.position.y < -destroyBorderY)
        {
            gameObject.SetActive(false);
        }
    }
}
