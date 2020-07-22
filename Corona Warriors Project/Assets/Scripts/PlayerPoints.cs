using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoints : MonoBehaviour
{
    public TextMeshProUGUI points;
    public TextMeshProUGUI winText;
    public GameObject gameOverPanel;

    private int currentPoints = 14;

    public void TweakPoints(int player, int i)
    {
        currentPoints += i;
        points.SetText(currentPoints.ToString());

        if (currentPoints <=0)
        {
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
            FindObjectOfType<AudioManager>().StopSound();
            FindObjectOfType<GameManager>().gameOver = true;
            gameOverPanel.SetActive(true);
            if (player == 1)
                winText.SetText("Congratulations! You are a Corona Warrior ");
        }
    }
}