using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public GameObject bulletPrefab;

    private const float max_X = 2.5f;
    private const float min_X = -2.5f;

    private float speed = 3.0f;
    private bool isShooting;

    [SerializeField] private ObjectPooling objPooling = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        if(Input.GetKey(KeyCode.LeftArrow) && transform.position.x > min_X)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < max_X)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if(Input.GetKey(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
#endif
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject obj = objPooling.GetPooledObject();
        obj.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(0.5f);
        isShooting = false;
    }


}
