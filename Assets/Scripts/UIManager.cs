using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator animator;
    public Text pointsText;
    public Text[] highScores;
    public Text title;
    public Text buttonText;

    public App app;

    // Start is called before the first frame update
    void Start()
    {
        app.onGameOver.AddListener(GameOver);
        app.onScoreChanged.AddListener(RefreshPoints);
        app.onHighScoreChanged.AddListener(RefreshHighScore);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeGameStatus(bool gameOn)
    {
        animator.SetBool("GameStatus", gameOn);
    }

    void GameOver()
    {
        title.text = "Oh no, game over";
        buttonText.text = "Restart";
        animator.SetBool("GameStatus", false);
    }

    public void RefreshPoints()
    {
        pointsText.text = "Points: " + app.player.points;
    }

    public void RefreshHighScore()
    {
        foreach(Text t in highScores)
        {
            t.text = "High score: " + app.player.highScore;
        }
    }
}
