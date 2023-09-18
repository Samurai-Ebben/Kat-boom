using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 25f;
    public Transform movePoint;


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
                movePoint.position += new Vector3(x, 0, 0);
            }
            else if (Mathf.Abs(y) == 1)
            {
                movePoint.position += new Vector3(0, y, 0);
            }
        }
        

        //int isDia = x * y != 0 ? 0 : 1;
        //transform.position += new Vector3(x, y, 0) * isDia * Time.deltaTime * speed;
    }
}
