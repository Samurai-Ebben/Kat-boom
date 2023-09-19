using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 25f;
    public Transform movePoint;
    public LayerMask whatStops;
    private LayerMask tempLayer;

    public bool isTransparent = false;
    private bool canUseGM = true;
    public float ghostMeter = 0.5f;

    private Color origColor;
    bool isRight = true;



    SpriteRenderer spriteRenderer;
    new Collider2D collider;

    void Start()
    {

        movePoint.parent = null;

        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

        spriteRenderer.color = Color.white;
        origColor = spriteRenderer.color;
        tempLayer = whatStops;
    }

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

        if (Input.GetKeyDown(KeyCode.Space) && canUseGM)
        {
            StartCoroutine(Transparent());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        movePoint.position = transform.position;

        if (other.gameObject == GameManager.Instance.door1)
        {
            GameManager.Instance.NextLvl();
        }
    }

    public IEnumerator Transparent()
    {
        if(!isTransparent)
        {
            canUseGM = false;
            isTransparent = true;

            Color newColor = spriteRenderer.color;
            newColor.a = 0.3f;
            spriteRenderer.color = newColor;

            collider.isTrigger = true;
            whatStops = LayerMask.NameToLayer("Walls");
            Debug.Log(spriteRenderer.color.a);

            yield return new WaitForSeconds(ghostMeter);
            isTransparent = false;
            collider.isTrigger = false;
            spriteRenderer.color = origColor;
            whatStops = tempLayer;
            yield return new WaitForSeconds(ghostMeter * 6);
            canUseGM = true;

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
