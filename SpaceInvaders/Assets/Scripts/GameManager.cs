using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] allAlienSets;
    private GameObject currentSet;
    public static GameManager instance;

    private Vector2 spawnPos = new Vector2(0, 10);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SpawnNewWave();
    }

    public static void SpawnNewWave()
    {
        instance.StartCoroutine(instance.SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        if(currentSet != null)
        {
            Destroy(currentSet);
        }

        yield return new WaitForSeconds(2);
        currentSet = Instantiate(allAlienSets[Random.Range(0, allAlienSets.Length)]);
        UIManager.UpdateWave();
    }
}
