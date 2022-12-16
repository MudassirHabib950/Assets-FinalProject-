using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public GameObject startingText;

    public static int numberOfCoins;
    public TextMeshProUGUI coinsText;
    //public textCoins coinsText; // Step #1 declaring the variable
  
    // Start is called before the first frame update
    void Start()
    {
     gameOver = false;
     Time.timeScale = 1;
     isGameStarted = false;
     numberOfCoins = 0;

    coinsText = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        // coinsText.text = "Coins:" + numberOfCoins; // Step #2 Use the Variable
        if(SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
        }
        
    }
}
