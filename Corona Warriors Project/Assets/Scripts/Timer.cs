using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int secondsLeft = 00;
    public GameObject gameOverPanel;
    public TextMeshProUGUI winText;

    private TextMeshProUGUI timer;
    private GameManager gameManager;

    private void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
        gameManager = FindObjectOfType<GameManager>();

        InvokeRepeating("UpdateTimer", 0f, 1f);
    }

    private void UpdateTimer()
    {
        if (gameManager.gameOver)
            return;     

        secondsLeft++;
        timer.SetText(Mathf.FloorToInt(secondsLeft / 60).ToString("00") + ":" + Mathf.RoundToInt(secondsLeft % 60).ToString("00"));
    }
}
