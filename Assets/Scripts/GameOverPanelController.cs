using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelController : MonoBehaviour
{

    [SerializeField]
    Text coinText;
    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text bestScoreText;



    public void HandleUI(int playerCoins= 10, string time = "10:34", float score=15.32f)
    {
        Debug.Log(score);
        coinText.text = $"Coins : {playerCoins}";
        scoreText.text = time;
        bestScoreText.text = score.ToString();
    }
}
