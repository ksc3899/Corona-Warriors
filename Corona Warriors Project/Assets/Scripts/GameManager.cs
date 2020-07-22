using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int diceSideThrown = 0;
    public DiceBehaviour diceBehaviour;
    public TextMeshProUGUI playerLandingStatus;
    [HideInInspector] public int player1Index = 0;
    [HideInInspector] public int player2Index = 0;
    [HideInInspector] public bool gameOver = false;

    [Header("Player References")]
    public PlayerMovement player1;
    public PlayerMovement player2;
    [Space]
    public PlayerPoints player1Points;
    [Space]
    public GameObject player1Turn;
    public GameObject player2Turn;
    public GameObject showFactsButton;

    public AudioManager audioManager;

    private void Start()
    {
        player1.moveAllowed = false;
        player2.moveAllowed = false;

        player1Turn.SetActive(true);
        player2Turn.SetActive(false);
    }

    private void Update()
    {
        UpdateUI();
    }

    public void MovePlayer(int playerToMove)
    {
        switch (playerToMove)
        {
            case 1:
                player1.moveAllowed = true;
                break;
            case 2:
                player2.moveAllowed = true;
                break;
        }
    }

    public void UpdateUI()
    {
        if (player1.blockIndex > player1Index + diceSideThrown)
        {
            string block;
            player1.moveAllowed = false;
            audioManager.PlaySound("PlayerLanding");
            StartCoroutine(ShowUpdates1());
            player1.blockIndex -= 1;
            player1Index = player1.blockIndex;
            block = player1.gameBoardBlocks[player1.blockIndex].gameObject.tag;
            if(block != "LOCKDOWN")
            {
                player1.gameBoardBlocks[player1Index].localScale = new Vector3(1.5f, 1.5f, 1.5f);
                player1.gameBoardBlocks[player1Index].GetComponent<Canvas>().sortingOrder = 2;
            }    

            switch (block)
            {
                case "GAIN":
                    playerLandingStatus.SetText("You gained 2 points !");
                    player1Points.TweakPoints(1, -2);
                    break;
                case "LOSS":
                    playerLandingStatus.SetText("You lost 1 point !");
                    player1Points.TweakPoints(1, 1);
                    break;
                case "QUARANTINE":
                    playerLandingStatus.SetText("You are quaratined.\nGet a 6.");
                    player1.quarantined = true;
                    break;
                case "Q":
                    playerLandingStatus.SetText("Oops, you lost 1 point and are quaratined.");
                    player1.quarantined = true;
                    player1Points.TweakPoints(1, 1);
                    break;
                case "LOCKDOWN":
                    playerLandingStatus.SetText("Lockdown !\nSent back to Home.");
                    player1.ReturnHome();
                    player1Index = 0;
                    break;
                default:
                    playerLandingStatus.SetText("Neutral cell!\nYou are safe :)");
                    break;
            }
        }

        if (player2.blockIndex > player2Index + diceSideThrown)
        {
            string block;
            player2.moveAllowed = false;
            audioManager.PlaySound("PlayerLanding");
            StartCoroutine(ShowUpdates2());
            player2.blockIndex -= 1;
            player2Index = player2.blockIndex;
            block = player2.gameBoardBlocks[player2.blockIndex].gameObject.tag;

            switch (block)
            {
                case "GAIN":
                    break;
                case "LOSS":
                    break;
                case "QUARANTINE":
                    player2.quarantined = true;
                    break;
                case "Q":
                    player2.quarantined = true;
                    break;
                case "LOCKDOWN":
                    player2.ReturnHome();
                    player2Index = 0;
                    break;
            }
        }
    }

    private IEnumerator ShowUpdates1()
    {
        yield return new WaitForSeconds(2f);
        diceBehaviour.coroutineAllowed = true;
        player1Turn.SetActive(false);
        playerLandingStatus.SetText("");
        showFactsButton.SetActive(false);
        player2Turn.SetActive(true);
    }

    private IEnumerator ShowUpdates2()
    {
        yield return new WaitForSeconds(2f);
        diceBehaviour.coroutineAllowed = true;
        player1Turn.SetActive(true);
        player2Turn.SetActive(false);
        if (player1.quarantined)
        {
            showFactsButton.SetActive(true);
        }
    }

    public void ResetQuarantine()
    {
        player1.quarantined = false;
    }
}
