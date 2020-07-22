using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Game Board")]
    public RectTransform[] gameBoardBlocks;
    [Space]
    public float moveSpeed = 5f;
    [HideInInspector] public bool moveAllowed = false;
    [HideInInspector] public bool quarantined = false;
    public int blockIndex = 0;
    public bool player1, player2;

    private RectTransform rectTransform;
    private GameManager gameManager;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        gameManager = FindObjectOfType<GameManager>();
        rectTransform.anchoredPosition = gameBoardBlocks[blockIndex].anchoredPosition;
    }

    private void Update()
    {
        if (moveAllowed)
        {
            if (quarantined && gameManager.diceSideThrown != 6)
            {
                gameManager.diceSideThrown = 0;
                moveAllowed = false;
                blockIndex += 1;
                return;
            }
            else
            {
                quarantined = false;
            }

            Move();
        }
    }

    private void Move()
    {
        if (blockIndex == gameBoardBlocks.Length)
        {
            if (player1)
            {
                gameManager.diceSideThrown -= (blockIndex - gameManager.player1Index);
                gameManager.player1Index = 0;
            }
            else if (player2)
            {
                gameManager.diceSideThrown -= (blockIndex - gameManager.player2Index);
                gameManager.player2Index = 0;
            }

            //blockIndex += 1;
            blockIndex %= gameBoardBlocks.Length;
        }

        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, gameBoardBlocks[blockIndex].anchoredPosition, moveSpeed);

        if (rectTransform.position == gameBoardBlocks[blockIndex].position)
        {
            blockIndex += 1;
        }
    }

    public void ReturnHome()
    {
        BackHome();
    }

    private void BackHome()
    {
        while (blockIndex >= 0)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, gameBoardBlocks[blockIndex].anchoredPosition, moveSpeed);

            if (rectTransform.position == gameBoardBlocks[blockIndex].position)
            {
                blockIndex -= 1;
            }
        }

        blockIndex = 0;
    }
}
