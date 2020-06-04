using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public BeerBehaviour beer;
    public float timeUntilBeer;
    public float timer;
    public bool timeOn;

    public List<BeerBehaviour> beersList = new List<BeerBehaviour>();

    // Update is called once per frame
    void Update()
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
        }
    }

    public void GameOver()
    {
        foreach(BeerBehaviour b in beersList)
        {
            Destroy(b.gameObject);
        }
        beersList.Clear();
        print("GAME OVER");
    }

    void SpawnBeer()
    {
        BeerBehaviour b = Instantiate(beer, transform.position + new Vector3(1.5f, 0.75f, 0), Quaternion.identity);
        b.player = this;
        beersList.Add(b);
    }
}
