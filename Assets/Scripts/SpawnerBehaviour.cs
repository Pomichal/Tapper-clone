using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerBehaviour : MonoBehaviour
{
    public GuestBehaviour guest;

    public float spawnGuestTime;
    public float timer;
    public float speed;

    public App app;
    public bool gameOn;

    public List<GuestBehaviour> guestList;

    // Start is called before the first frame update
    void Start()
    {
        InitGame();
        app.onGameOver.AddListener(GameOver);
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

        InitGame();
    }

    public void InitGame()
    {
        gameOn = false;
        app.player.transform.position = Vector3.zero;
        app.player.CheckHighScore();
        app.player.points = 0;
        app.onScoreChanged.Invoke();
    }

    public void ChangeGameStatus()
    {
        gameOn = true;
        app.uiManager.ChangeGameStatus(gameOn);

    }

    void SpawnGuest()
    {
        int position = Random.Range(0, 4);  // 0 , 1, 2, 3
        GuestBehaviour g = Instantiate(guest, new Vector3(10, 0, position * 3 - 1), Quaternion.identity);
        g.app = app;
        guestList.Add(g);
    }
}
