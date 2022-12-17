using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    public int scoreValue;

    private float speed = 5f;
    private const float maxLeft = -5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        if(transform.position.x <= maxLeft)
        {
            Destroy(gameObject);
        }
    }
}
