using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Collision : MonoBehaviour
{
    public GameObject explosion;
    public GameObject tiktikBarrale;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag != "Enemy")
        {
            spriteRenderer.enabled = false;
            GameObject titik = Instantiate(tiktikBarrale, transform.position, Quaternion.identity);
            Destroy(titik, 1f);
            Invoke("DestroyBox", 1f);
        }
    }

    void DestroyBox()
    {
        var explosionPly = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosionPly, 0.45f);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.Instance.countBoxesLvl1--;
        GameManager.Instance.score += 100;
    }

}
