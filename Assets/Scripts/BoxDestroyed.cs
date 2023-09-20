using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
        public GameObject explosion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // the boxes should explode even when an explosion hit them
        if(collision.gameObject.tag != "Enemy")
        {
            GameManager.Instance.score += 100;
            GameManager.Instance.countBoxesLvl1--;
            Debug.Log(GameManager.Instance.countBoxesLvl1);
            StartCoroutine(GameManager.Instance.Explode(transform));
            Destroy(gameObject, 1);

        }
    }




}
