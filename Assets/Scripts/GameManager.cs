using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Vector2 spacing;
    [SerializeField] private Vector2 offset;

    [SerializeField] private TMP_Text scoreText;
    private int score;

    [SerializeField] private TMP_Text endGameText;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BallController ballController;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        SetUpBricks();
    }

    public void SetUpBricks()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector2 brickPos = new Vector2();
                brickPos.x = i * spacing.x - offset.x;
                brickPos.y = j * spacing.y + offset.y;

                Instantiate(brickPrefab, brickPos, Quaternion.identity);
            }
        }
    }

    public void UpdateScore()
    {
        score += 100;
        scoreText.text = "Score: " + score;

        if(score >= 4000)
        {
            endGameText.gameObject.SetActive(true);
            ballController.gameObject.SetActive(false);
            playerController.gameObject.SetActive(false);
        }
    }

    public void LoseGame()
    {
        endGameText.text = "You lose...";
        endGameText.gameObject.SetActive(true);
        ballController.gameObject.SetActive(false);
        playerController.canReceiveInput = false;
    }
}
