using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // the boxes should explode even when an explosion hit them
        if(collision.gameObject.tag != "Enemy")
        {
            GameManager.Instance.countBoxesLvl1--;
            Debug.Log(GameManager.Instance.countBoxesLvl1);
            Destroy(gameObject, 1);
            StartCoroutine(GameManager.Instance.Explode(transform));

        }
    }


}
