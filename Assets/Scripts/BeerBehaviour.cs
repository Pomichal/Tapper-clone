using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerBehaviour : MonoBehaviour
{

    public App app;
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
            app.player.BeerDelivered(this);
            c.gameObject.GetComponent<GuestBehaviour>().RemoveFromGame();
            Destroy(gameObject);
        }
        if(c.gameObject.CompareTag("GameOver"))
        {
            app.onGameOver.Invoke();
        }
    }
}
