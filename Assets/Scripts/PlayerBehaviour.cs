using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public SpawnerBehaviour spawner;

    public List<BeerBehaviour> beersList = new List<BeerBehaviour>();

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        spawner.RefreshHighScore();
        beerTimer.maxValue = timeUntilBeer;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawner.gameOn)
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

            if(Input.GetKeyDown(KeyCode.Space))
            {
                timer = timeUntilBeer;
                timeOn = true;
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                timeOn = false;
                beerTimer.value = timeUntilBeer;
            }
            animator.SetBool("timerOn", timeOn);
        }
    }

    public void GameOver()
    {
        foreach(BeerBehaviour b in beersList)
        {
            Destroy(b.gameObject);
        }
        beersList.Clear();
        if(highScore < points)
        {
            highScore = points;
            PlayerPrefs.SetInt("highScore", points);
            spawner.RefreshHighScore();
        }
        spawner.GameOver();
    }

    public void BeerDelivered(BeerBehaviour beer)
    {
        beersList.Remove(beer);
        points++;
        spawner.RefreshPoints();
    }

    void SpawnBeer()
    {
        BeerBehaviour b = Instantiate(beer, transform.position + new Vector3(1.5f, 0.75f, 0), Quaternion.identity);
        b.player = this;
        beersList.Add(b);
    }
}
