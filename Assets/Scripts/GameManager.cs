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

    public Transform startPointlvl2;

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
        if (countBoxesLvl1 <= 0)
        {
            door1.SetActive(true);
            Debug.Log("Open seseme.");
        }
    }
}
