using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    public Text coinText;
    public Text lala;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.playerCoins += 1;
            coinText.text = $": {GameManager.playerCoins}";
            Debug.Log(GameManager.playerCoins);
        }
    }
}
