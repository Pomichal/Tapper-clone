using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestBehaviour : MonoBehaviour
{
    public GameObject beerObject;
    public SpawnerBehaviour spawner;
    public Animator animator;
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
        beerObject.SetActive(true);
        animator.SetTrigger("drink");
        spawner.guestList.Remove(this);
        rb.velocity = new Vector3();
        GetComponent<BoxCollider>().enabled = false;
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length + 2f;
        Destroy(gameObject,animationLength);
    }
}
