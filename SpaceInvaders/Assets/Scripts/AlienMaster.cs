using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    public GameObject bulletPrefab;

    private Vector3 hMoveDist = new Vector3(0.05f, 0, 0);
    private Vector3 vMoveDist = new Vector3(0, 0.15f, 0);

    private const float maxLeft = -3.5f;
    private const float maxRight = 3.5f;

    public static List<GameObject> allAliens = new List<GameObject>();

    private bool movingRight;
    private float moveTimer = 0.01f;
    private float moveTime = 0.005f;
    private float maxMoveSpeed = 0.02f;

    private float shootTimer = 3f;
    private const float shootTime = 3f;

    [SerializeField] private ObjectPooling objPool = null;

    public GameObject motherShip;
    private Vector3 motherShipPos = new Vector3(4.5f, 4f, 0);
    private float motherShipTimer = 1f;
    private const float motherShip_Min = 15f;
    private const float motherShip_Max = 60f;

    private bool entering = true;
    private const float startY = 1.7f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Alien"))
        {
            allAliens.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (entering)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 10);

            if (transform.position.y <= startY)
            {
                entering = false;
            }
        }
        else
        {
            //Control Aliens Moving
            if (moveTimer <= 0)
            {
                MoveAliens();
            }
            moveTimer -= Time.deltaTime;

            //Control Aliens Shooting
            if (shootTimer <= 0)
            {
                AlienShoot();
            }
            shootTimer -= Time.deltaTime;

            //Control MotherShip Spawning
            if (motherShipTimer < 0)
            {
                MotherShipSpawner();
            }
            motherShipTimer -= Time.deltaTime;
        }
    }

    private void MoveAliens()
    {
        int hitMax = 0;

        if(allAliens.Count > 0)
        {
            for (int i = 0; i < allAliens.Count; i++)
            {
                if (movingRight)
                {
                    allAliens[i].transform.position += hMoveDist;
                }
                else
                {
                    allAliens[i].transform.position -= hMoveDist;
                }

                if(allAliens[i].transform.position.x > maxRight || allAliens[i].transform.position.x < maxLeft)
                {
                    hitMax++;
                }
            }

            if(hitMax > 0)
            {
                for (int i = 0; i < allAliens.Count; i++)
                {
                    allAliens[i].transform.position -= vMoveDist;
                }
                movingRight = !movingRight;
            }

            moveTimer = GetMoveSpeed();
        }
    }

    private float GetMoveSpeed()
    {
        float f = allAliens.Count * moveTime;

        if (f < maxMoveSpeed)
        {
            return maxMoveSpeed;
        }
        else
        {
            return f;
        }
        
    }

    private void AlienShoot()
    {
        Vector2 bulletPos = allAliens[Random.Range(0, allAliens.Count)].transform.position;

        //Instantiate(bulletPrefab, bulletPos, Quaternion.identity;

        GameObject obj = objPool.GetPooledObject();
        obj.transform.position = bulletPos;

        shootTimer = shootTime;
    }

    private void MotherShipSpawner()
    {
        Instantiate(motherShip, motherShipPos, Quaternion.identity);
        motherShipTimer = Random.Range(motherShip_Min, motherShip_Max);
    }
}
