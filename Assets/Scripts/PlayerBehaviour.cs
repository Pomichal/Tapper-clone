using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public Slider beerTimer;
    public Animator animator;
    public BeerBehaviour beer;
    public float timeUntilBeer;
    public float timer;
    public bool timeOn;

    public int points;
    public int highScore;

    public App app;

    public List<BeerBehaviour> beersList = new List<BeerBehaviour>();

    public Vector3 touchPoint;
    public float swipeSensitivity;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        beerTimer.maxValue = timeUntilBeer;
        app.onGameOver.AddListener(GameOver);
        app.onHighScoreChanged.Invoke();
    }

    public void PrintSomething()
    {
        print("something");
    }

    // Update is called once per frame
    void Update()
    {
        if(app.spawner.gameOn)
        {
            if(Input.anyKeyDown)
            {
                float ver = Input.GetAxisRaw("Vertical");

                if(ver == 1 && transform.position.z > 0)
                {
                    transform.position += new Vector3(0, 0, -3);
                }
                else if(ver == -1 && transform.position.z < 9)
                {
                    transform.position += new Vector3(0, 0, 3);
                }
            }

            if(timeOn)
            {
                timer -= Time.deltaTime;
                beerTimer.value = timer;
                if(timer < 0)
                {
                    SpawnBeer();
                    timeOn = false;
                }
            }
            animator.SetBool("timerOn", timeOn);

            if(Input.GetMouseButtonDown(0))
            {
                touchPoint = Input.mousePosition;
            }

            if(Input.GetMouseButtonUp(0))
            {
                Vector3 releasePoint = Input.mousePosition;

                float diff = releasePoint.y - touchPoint.y;

                if(diff > swipeSensitivity && transform.position.z > 0)
                {
                    transform.position += new Vector3(0, 0, -3);
                }
                else if(diff < -swipeSensitivity && transform.position.z < 9)
                {
                    transform.position += new Vector3(0, 0, 3);
                }

            }
        }
    }

    public void StartTapping()
    {
        if(app.spawner.gameOn)
        {
            timer = timeUntilBeer;
            timeOn = true;
        }
    }

    public void EndTapping()
    {
        if(app.spawner.gameOn)
        {
            timeOn = false;
            beerTimer.value = timeUntilBeer;
        }
    }

    public void GameOver()
    {
        foreach(BeerBehaviour b in beersList)
        {
            Destroy(b.gameObject);
        }
        beersList.Clear();
    }

    public void CheckHighScore()
    {
        if(highScore < points)
        {
            highScore = points;
            PlayerPrefs.SetInt("highScore", points);
            app.onHighScoreChanged.Invoke();
        }
    }

    public void BeerDelivered(BeerBehaviour beer)
    {
        beersList.Remove(beer);
        points++;
        app.onScoreChanged.Invoke();
    }

    void SpawnBeer()
    {
        BeerBehaviour b = Instantiate(beer, transform.position + new Vector3(1.5f, 0.75f, 0), Quaternion.identity);
        b.app = app;
        beersList.Add(b);
    }
}
