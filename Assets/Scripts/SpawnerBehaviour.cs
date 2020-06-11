using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerBehaviour : MonoBehaviour
{
    public Animator animator;
    public GuestBehaviour guest;
    public Text pointsText;
    public Text[] highScores;
    public Text title;
    public Text buttonText;

    public float spawnGuestTime;
    public float timer;
    public float speed;

    public PlayerBehaviour player;
    public bool gameOn;

    public List<GuestBehaviour> guestList;

    // Start is called before the first frame update
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOn)
        {
            timer -= Time.deltaTime;

            if(timer < 0)
            {
                timer = spawnGuestTime;
                SpawnGuest();
            }
        }
    }

    public void GameOver()
    {
        foreach(GuestBehaviour g in guestList)
        {
            Destroy(g.gameObject);
        }
        guestList.Clear();
        if(player.beersList.Count > 0)
        {
            player.GameOver();
        }

        title.text = "Oh no, game over";
        buttonText.text = "Restart";
        animator.SetTrigger("StopGame");
        InitGame();
    }

    public void InitGame()
    {
        gameOn = false;
        player.transform.position = Vector3.zero;
        player.points = 0;
        RefreshPoints();
    }

    public void ChangeGameStatus()
    {
        gameOn = true;
        animator.SetTrigger("StartGame");
    }

    void SpawnGuest()
    {
        int position = Random.Range(0, 4);  // 0 , 1, 2, 3
        GuestBehaviour g = Instantiate(guest, new Vector3(10, 0, position * 3 - 1), Quaternion.identity);
        g.spawner = this;
        guestList.Add(g);
    }

    public void RefreshPoints()
    {
        pointsText.text = "Points: " + player.points;
    }

    public void RefreshHighScore()
    {
        foreach(Text t in highScores)
        {
            t.text = "High score: " + player.highScore;
        }
    }
}
