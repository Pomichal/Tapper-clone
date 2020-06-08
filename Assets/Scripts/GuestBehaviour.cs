using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestBehaviour : MonoBehaviour
{

    public SpawnerBehaviour spawner;

    public Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    void Awake()
    {
        rb.AddForce(new Vector3(-1, 0, 0) * speed);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < 1)
        {
            spawner.GameOver();
        }
    }

    public void RemoveFromGame()
    {
        spawner.guestList.Remove(this);
        Destroy(gameObject);
    }
}
