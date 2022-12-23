using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public GameObject bulletPrefab;

    private const float max_X = 3.5f;
    private const float min_X = -3.5f;

    private float speed = 3.0f;
    private bool isShooting;

    [SerializeField] private ObjectPooling objPooling = null;

    public ShipStats shipStats;

    private Vector2 offScreenPos = new Vector2(0, -20f);
    private Vector2 startPos = new Vector2(0, -5.7f);

    private float dirx;

    // Start is called before the first frame update
    void Start()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLife = shipStats.maxLife;

        transform.position = startPos;

        UIManager.UpdateHealthBar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLife);
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
        //Gyro Sensor Control on Mobile Phone
        dirx = Input.acceleration.x;

        if (dirx <= -0.1f && transform.position.x > min_X)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        else if (dirx >= 0.1f && transform.position.x < max_X)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
    }

    //Shoot with Button on Mobile Phone
    public void ShootButton()
    {
        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    public void AddHealth()
    {
        if (shipStats.currentHealth == shipStats.maxHealth)
        {
            UIManager.UpdateScore(250);
        }
        else
        {
            shipStats.currentHealth++;
            UIManager.UpdateHealthBar(shipStats.currentHealth);
        }
    }

    public void AddLifes()
    {
        if (shipStats.currentLife == shipStats.maxLife)
        {
            UIManager.UpdateScore(1000);
        }
        else
        {
            shipStats.currentLife++;
            UIManager.UpdateLives(shipStats.currentLife);
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            collision.gameObject.SetActive(false);
            TakeDamage();
        }
    }

    private IEnumerator Respawn()
    {
        transform.position = offScreenPos;
        yield return new WaitForSeconds(2);

        shipStats.currentHealth = shipStats.maxHealth;
        transform.position = startPos;

        UIManager.UpdateHealthBar(shipStats.currentHealth);
    }

    public void TakeDamage()
    {
        shipStats.currentHealth--;
        UIManager.UpdateHealthBar(shipStats.currentHealth);

        if (shipStats.currentHealth <= 0)
        {
            shipStats.currentLife--;
            UIManager.UpdateLives(shipStats.currentLife);

            if (shipStats.currentLife <= 0)
            {
                Debug.Log("Game Over");
            }
            else
            {
                StartCoroutine(Respawn());
            }
        }
    }    
}
