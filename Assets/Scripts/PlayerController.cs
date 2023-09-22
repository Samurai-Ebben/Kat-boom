using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 25f;
    public Transform movePoint;

    public LayerMask whatStops;
    public LayerMask neverGoThrough;
    private LayerMask tempLayer;


    public bool isTransparent = false;
    private bool canUseGM = true;
    public float ghostMeterTimer = 0.5f;
    public float GMamount = 1;

    private Color origColor;
    bool isRight = true;

    public int lives = 9;

    public bool canMove = true;

    public SpriteRenderer spriteRenderer;
    new Collider2D collider;

    public float x;
    public float y;

    
    void Start()
    {
        canUseGM = GameManager.Instance.GMready;
        movePoint.parent = null;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

        spriteRenderer.color = Color.white;
        origColor = spriteRenderer.color;
        tempLayer = whatStops;
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (!canMove)
            return;

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) == 0)
        {
            if (Mathf.Abs(x) == 1)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(x, 0, 0), 0.2f, whatStops))
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
        if(other.gameObject.layer == neverGoThrough)
        {
            StartCoroutine(GameManager.Instance.Death());
        }
        if (other.gameObject == GameManager.Instance.door1)
        {
            GameManager.Instance.NextLvl();
        }
        if (other.gameObject == GameManager.Instance.door2)
        {
            GameManager.Instance.NextLvl2();
        }
        if(other.gameObject.tag == "explosion")
        {
            StartCoroutine(GameManager.Instance.Death());
        }
        if(other.gameObject.tag == "Enemy")
        {
            GameManager.Instance.batHurt = other.gameObject.GetComponent<AudioSource>();
            GameManager.Instance.batHurt.Play();
        }
        if(other.gameObject.tag == "El")
        {
            GameManager.Instance.elHurt = other.gameObject.GetComponent<AudioSource>();
            GameManager.Instance.elHurt.Play();
        }
        if (other.gameObject.tag == "DiaTrigger")
            GameManager.Instance.lastDia.TriggerDia();
        if (other.gameObject.tag == "Goal")
        {
            GameManager.Instance.EndScreen("Victory");
        }
    }

    public IEnumerator Transparent()
    {
        if(!isTransparent)
        {
            canUseGM = false;
            isTransparent = true;

            GMamount = 0;

            Color newColor = spriteRenderer.color;
            newColor.a = 0.3f;
            spriteRenderer.color = newColor;

            collider.isTrigger = true;

            whatStops &= LayerMask.GetMask("Walls");

            yield return new WaitForSeconds(ghostMeterTimer);

            isTransparent = false;
            collider.isTrigger = false;
            spriteRenderer.color = origColor;
            whatStops = tempLayer;
            yield return new WaitForSeconds(ghostMeterTimer * 6);
            //GMamount += 1;
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

    public void TakeDamage() {
        GameManager.Instance.hearts[lives - 1].fillAmount = 0;
        lives--;
    }
    public void Teleport(Vector3 position)
    {
        transform.position = position;
        movePoint.position = transform.position;
    }

}
