using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameManager.Instance.countBoxesLvl1--;
            Debug.Log(GameManager.Instance.countBoxesLvl1);
            Destroy(gameObject, 1);
        }
    }

}
