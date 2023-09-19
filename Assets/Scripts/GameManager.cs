using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public   PlayerController player;
    public GameObject[] levels;

    public int countBoxesLvl1 = 5;
    public int countBoxesLvl2 = 5;
    public int countBoxesLvl3 = 5;

    public GameObject door1; 
    public GameObject door2;

    public Transform startPointlvl1;
    public Transform startPointlvl2;
    public Transform startPointlvl3;

    public bool lvl1 = true;
    public bool lvl2 = false;
    public bool lvl3 = false;

    public GameObject explosion;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        door1.SetActive(false);
        for (int i = 1; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (countBoxesLvl1 <= 0 && lvl1)
        {
            door1.SetActive(true);
            Debug.Log("Open seseme.");
        }
        if (countBoxesLvl2 <= 0 && lvl2)
        {
            door2.SetActive(true);
            Debug.Log("Open seseme.");
        }


    }

    public void NextLvl()
    {
        levels[1].SetActive(true);
        player.transform.position = startPointlvl2.position;
        player.movePoint.position = player.transform.position;
        Camera.main.transform.position = new Vector3(21.9699993f, 0, -10);
        levels[0].SetActive(false);
        lvl1 = false;
        lvl2 = true;
    }
    public void NextLvl2()
    {
        lvl3 = true;
        levels[2].SetActive(true);
        player.transform.position = startPointlvl3.position;
        player.movePoint.position = player.transform.position;
        Camera.main.transform.position = new Vector3(22f, 13.2f, -10);
        levels[1].SetActive(false);
        lvl2 = false;
    }

    public IEnumerator Explode(Transform box)
    {
        yield return new WaitForSeconds(1);
        var explosionPly = Instantiate(explosion, box.position, Quaternion.identity);
        Destroy(explosionPly, 0.2f);
        Debug.Log("kaboom");
    }

    void Death()
    {
        if (lvl1)
            player.transform.position = startPointlvl1.position;
        if(lvl2)
            player.transform.position = startPointlvl2.position;
        if(lvl3)
            player.transform.position = startPointlvl3.position;

    }
}
