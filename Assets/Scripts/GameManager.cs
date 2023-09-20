using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //SINGELTON
    public static GameManager Instance;

    [Header ("--REFS--")]
    public PlayerController player;
    public GameObject[] levels;
    public GameObject explosion;
    public GameObject DoorElL1;
    public GameObject DoorElL2;
    public GameObject DoorElL3;

    public GameObject DeadCat;

    [Header("--Boxes Count--")]
    public int countBoxesLvl1 = 5;
    public int countBoxesLvl2 = 5;
    public int countBoxesLvl3 = 5;

    [Header("--REF Doors--")]
    public GameObject door1; 
    public GameObject door2;

    [Header("--STARTING POINTS--")]
    public Transform startPointlvl1;
    public Transform startPointlvl2;
    public Transform startPointlvl3;

    [Header("--LEVEL SWITCHS --")]
    public bool lvl1 = true;
    public bool lvl2 = false;
    public bool lvl3 = false;
    public bool isDead = false;

    [Header ("--UI MANAGEMENT--")]
    public int lives = 5;
    public int score = 0;


    [Header("--UI--")]
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI livesTxt;
    public Image ghostMeeterFill;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        door1.SetActive(false);
        spriteRenderer = player.GetComponent<SpriteRenderer>();

        for (int i = 1; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ghostMeeterFill.fillAmount = player.GMamount;
        if (countBoxesLvl1 <= 0 && lvl1)
        {
            door1.SetActive(true);
            DoorElL1.SetActive(false);
            Debug.Log("Open seseme.");
        }
        if (countBoxesLvl2 <= 0 && lvl2)
        {
            door2.SetActive(true);
            DoorElL2.SetActive(false);

            Debug.Log("Open seseme.");
        }

        if (player.lives > 0 && isDead)
        {
            StartCoroutine(Death());
            isDead = false;
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

    public IEnumerator Death()
    {
        spriteRenderer.enabled = false;
        var deadCat = Instantiate(DeadCat, player.transform.position, Quaternion.identity);
        Destroy(deadCat, .7f);
        yield return new WaitForSeconds(.8f);
        if (lvl1)
            player.Teleport(startPointlvl1.position);
        if(lvl2)
            player.Teleport(startPointlvl2.position);
        if (lvl3)
            player.Teleport(startPointlvl3.position);
        spriteRenderer.enabled = true;

        lives--;
        score -= 25;
    }
}
