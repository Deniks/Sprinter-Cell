using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static float GetBestScore()
    {
        return PlayerPrefs.GetFloat("BESTSCORE");
    }

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt("COINS");
    }

    public static void SetScore(float currentScore)
    {
        PlayerPrefs.SetFloat("BESTSCORE", currentScore);
        PlayerPrefs.Save();
    }

    public static void SetCoins(int playerCoins)
    {
        PlayerPrefs.SetInt("COINS", PlayerPrefs.GetInt("COINS") + playerCoins);
        PlayerPrefs.Save();
    }
}
