using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Collision : MonoBehaviour
{
    public GameObject explosion;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // the boxes should explode even when an explosion hit them
        if(collision.gameObject.tag != "Enemy")
        {
            Invoke("DestroyBox", 1f);
        }
    }

    void DestroyBox()
    {
        spriteRenderer.enabled = false;
        var explosionPly = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosionPly, 0.25f);
        Destroy(gameObject, 1f);
    }

    private void OnDestroy()
    {
        GameManager.Instance.countBoxesLvl1--;
        GameManager.Instance.score += 100;
    }

}
