using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerBehaviour : MonoBehaviour
{

    public PlayerBehaviour player;
    public float speed;
    public Rigidbody rb;

    public void Awake()
    {
        rb.AddForce(new Vector3(1,0,0) * speed);
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.CompareTag("Guest"))
        {
            player.beersList.Remove(this);
            Destroy(c.gameObject);
            Destroy(gameObject);
        }
        if(c.gameObject.CompareTag("GameOver"))
        {
            player.GameOver();
        }
    }
}
