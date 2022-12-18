using UnityEngine;


[System.Serializable]
public class ShipStats
{
    [Range(1, 5)]
    public int maxHealth;
    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public int maxLife = 3;
    [HideInInspector]
    public int currentLife = 3;

    //public float shipSpeed;
    //public float fireRate;
}
