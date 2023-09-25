using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    public GameObject[] levels;


    [Header("--STARTING POINTS--")]
    public Transform startPointlvl1;
    public Transform startPointlvl2;
    public Transform startPointlvl3;

    private List<Transform> allStartingpoints;

    [Header("--LEVEL SWITCHS --")]
    public bool tut = false;
    public bool lvl1 = true;
    public bool lvl2 = false;
    public bool lvl3 = false;


    private void Start()
    {
        allStartingpoints = new List<Transform> { startPointlvl1, startPointlvl2, startPointlvl3 };
        for (int i = 1; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
    }
    void ChangeValidtyLvl(int lvlNum)
    {
        if (lvlNum == 1)
        {
            tut = false;
            lvl1 = true;
        }
        if (lvlNum == 2)
        {
            lvl1 = false;
            lvl2 = true;
        }
        if (lvlNum == 3)
        {
            lvl2 = false;
            lvl3 = true;
        }
    }

    public void SwitchingLevels()
    {
        if (tut)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (lvl1)
            GameManager.Instance.player.Teleport(startPointlvl1.position);
        if (lvl2)
            GameManager.Instance.player.Teleport(startPointlvl2.position);
        if (lvl3)
            GameManager.Instance.player.Teleport(startPointlvl3.position);
    }

    public void NxtLvl(int currLvl, int nxtLvl)
    {

        List<Vector3> camPositions = new List<Vector3> { new Vector3(-0.08f, 0.3f, -10),
            new Vector3(22.1f, 0.3f, -10), new Vector3(22.1f, 13.5f, -10) };
        levels[nxtLvl].SetActive(true);
        GameManager.Instance.player.Teleport(allStartingpoints[nxtLvl - 1].position);
        Camera.main.transform.position = camPositions[nxtLvl - 1];
        if (currLvl != 0)
            levels[currLvl].SetActive(false);
        ChangeValidtyLvl(nxtLvl);
    }

}
