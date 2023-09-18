using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 25f;
    public Transform movePoint;
    public LayerMask whatStops;

    public bool isTransparent = false;
    public float ghostMeter = 1.5f;

    bool isRight = true;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movePoint.parent = null;
        spriteRenderer.color = Color.white;

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(x) == 1)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(x,0,0), 0.2f, whatStops))
                {
                    movePoint.position += new Vector3(x, 0, 0);
                    if (x > 0 && !isRight)
                        FlipHori();
                    else if (x < 0 && isRight)
                        FlipHori();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isTransparent = true;
            var newColor = new Color(255, 255, 255, 40);

            StartCoroutine(Transparent());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        movePoint.position = transform.position;
    }

    public IEnumerator Transparent()
    {
        if(isTransparent)
        {
            isTransparent = false;

            spriteRenderer.color = new Color(255, 255, 255, 30);
            Debug.Log(spriteRenderer.color.a);
            yield return new WaitForSeconds(ghostMeter);
            isTransparent = true;
            spriteRenderer.color = Color.white;

        }
    }

    void FlipHori()
    {
        var currScale = transform.localScale;
        currScale.x *= -1;
        transform.localScale = currScale;
        isRight = !isRight;
    }
}
