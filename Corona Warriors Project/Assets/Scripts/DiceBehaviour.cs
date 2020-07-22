using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceBehaviour : MonoBehaviour
{
    public Image[] diceFaces;
    public bool coroutineAllowed = true;
    [Space]
    public PlayerMovement player1, player2;
    public AudioManager audioManager;

    private int imageActive;
    private int whosTurn = 1;
    private GameManager gameManager;

    private void Start()
    {
        imageActive = 5;
        diceFaces[imageActive].enabled = true;

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (whosTurn == -1 && !gameManager.gameOver && coroutineAllowed && !player1.moveAllowed)
        {
            StartCoroutine("RollTheDice");
        }
    }

    public void DiceRoll()
    {
        if (!gameManager.gameOver && coroutineAllowed && !player2.moveAllowed)
        {
            StartCoroutine("RollTheDice");
        }
    }

    private IEnumerator RollTheDice()
    {

        coroutineAllowed = false;
        if (whosTurn == -1)
        {
            yield return new WaitForSeconds(2f);
        }
        audioManager.PlaySound("DiceRoll");
        int randomSide = 0;
        for (int i = 0; i <= 15; i++)
        {
            randomSide = Random.Range(0, 6);
            diceFaces[imageActive].enabled = false;
            diceFaces[randomSide].enabled = true;
            imageActive = randomSide;

            yield return new WaitForSeconds(0.08f);
        }

        gameManager.diceSideThrown = randomSide + 1;
        if (whosTurn == 1)
        {
            gameManager.MovePlayer(1);
            player1.gameBoardBlocks[gameManager.player1Index].localScale = new Vector3(1f, 1f, 1f);
            player1.gameBoardBlocks[gameManager.player1Index].GetComponent<Canvas>().sortingOrder = 1;
        }
        else if (whosTurn == -1)
        {
            gameManager.MovePlayer(2);
        }
        whosTurn *= -1;
    }
}
