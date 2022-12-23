using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    private int score, highScore, wave;
    public Text scoreText, highScoreText, coinText, waveText;

    public Image[] lifeSprites;
    public Image healtBar;
    public Sprite[] healthBars;

    private Color32 active = new Color(1, 1, 1, 1);
    private Color32 inactive = new Color(1, 1,1 , 0.25f);

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

    public static void UpdateLives(int l)
    {
        foreach (Image img in instance.lifeSprites)
        {
            img.color = instance.inactive;
        }

        for (int i = 0; i < l; i++)
        {
            instance.lifeSprites[i].color = instance.active;
        }
    }

    public static void UpdateHealthBar(int h)
    {
        instance.healtBar.sprite = instance.healthBars[h];
    }

    public static void UpdateScore(int s)
    {
        instance.score += s;
        instance.scoreText.text = instance.score.ToString("000000");
    }

    public static void UpdateHighScore()
    {
        //TO DO
    }

    public static void UpdateWave()
    {
        instance.wave++;
        instance.waveText.text = instance.wave.ToString("0000");
    }

    public static void UpdateCoin()
    {
        instance.coinText.text = Inventory.currentCoins.ToString("0000");
    }
}
