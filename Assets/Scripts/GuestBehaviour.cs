using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < 1)
        {
            print("GAME OVER");
            Destroy(gameObject);
        }
    }
}
