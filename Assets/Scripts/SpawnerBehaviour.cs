using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{

    public Rigidbody guest;

    public float spawnGuestTime;
    public float timer;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            timer = spawnGuestTime;
            SpawnGuest();
        }
    }

    void SpawnGuest()
    {
        int position = Random.Range(0, 4);  // 0 , 1, 2, 3
        Rigidbody g = Instantiate(guest, new Vector3(10, 0, position * 3 - 1), Quaternion.identity);
        g.AddForce(new Vector3(-1, 0, 0) * speed);
    }

}
