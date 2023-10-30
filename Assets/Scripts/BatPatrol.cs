using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speedUD;
    public float speedRL;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speedRL, speedUD);
        }
        else
        {
            rb.velocity = new Vector2(-speedRL, -speedUD);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "explosion")
        {
            GameManager.Instance.AddScore(150);
            Destroy(gameObject);
        }
    }

}

