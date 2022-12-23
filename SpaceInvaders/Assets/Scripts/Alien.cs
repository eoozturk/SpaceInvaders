using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public int scoreValue;
    public GameObject explosion, coinPrefab, healthPrefab, lifePrefab;

    private const int lifeChance = 50;
    private const int healthChance = 100;
    private const int coinChance = 150;

    public void Kill()
    {
        UIManager.UpdateScore(scoreValue);
        AlienMaster.allAliens.Remove(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);

        int rand = Random.Range(0, 1000);

        if (rand <= lifeChance)
        {
            Instantiate(lifePrefab, transform.position, Quaternion.identity);
        }
        else if (rand <= healthChance)
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        }
        else if (rand <= coinChance)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }

        if (AlienMaster.allAliens.Count == 0)
        {
            GameManager.SpawnNewWave();
        }
        gameObject.SetActive(false);
    }
}
