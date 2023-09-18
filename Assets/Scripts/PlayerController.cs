using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 25f;
    public Transform movePoint;
    public LayerMask whatStops;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(x) == 1)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(x,0,0), 0.2f, whatStops))
                {
                    movePoint.position += new Vector3(x, 0, 0);
                }
            }
            else if (Mathf.Abs(y) == 1)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, y, 0), 0.2f, whatStops))
                {
                    movePoint.position += new Vector3(0, y, 0);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        movePoint.position = transform.position;
    }
}
