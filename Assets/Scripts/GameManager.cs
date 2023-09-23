using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //SINGELTON
    public static GameManager Instance;

    [Header("--REFS--")]
    public PlayerController player;
    public GameObject[] levels;
    public GameObject explosion;
    public GameObject DoorElL1;
    public GameObject DoorElL2;
    public GameObject DoorElL3;
    public GameObject DeadCat;
    public Leaderboard leaderboard;
    public GameObject victoryScreen;
    public GameObject Gaol;
    public DiaTrigger lastDia;
    private DiaTrigger diafst;


    [Header("--Boxes Count--")]
    public int countBoxesLvl1 = 5;
    public int countBoxesLvl2 = 5;
    public int countBoxesLvl3 = 5;
    public int countBoxesLvl0 = 1;

    [Header("--REF Doors--")]
    public GameObject door1;
    public GameObject door2;

    [Header("--STARTING POINTS--")]
    public Transform startPointlvl1;
    public Transform startPointlvl2;
    public Transform startPointlvl3;

    [Header("--LEVEL SWITCHS --")]
    public bool tut = false;
    public bool lvl1 = true;
    public bool lvl2 = false;
    public bool lvl3 = false;
    public bool isDead = false;

    [Header("--UI MANAGEMENT--")]
    public int score = 0;

    [Header("--UI--")]
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI livesTxt;
    public Image ghostMeeterFill;
    public Image[] hearts = new Image[9];
    public GameObject gameOverscrn;
    public GameObject HUD;

    [Header("--GAMEOVERUI--")]
    public TextMeshProUGUI title;

    [Header("--AUDIO--")]
    public AudioSource doorSfx;
    public AudioSource doorSfx2;
    public AudioSource elHurt; 
    public AudioSource batHurt;



    public bool GMready { get { return ghostMeeterFill.fillAmount >= 1; } }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        diafst = GetComponent<DiaTrigger>();
        Invoke("DiaPlay", 0.01f);
        HUD.SetActive(true);
        gameOverscrn.SetActive(false);
        victoryScreen.SetActive(false);

        door1.SetActive(false);
        door2.SetActive(false);
        Gaol.SetActive(false);
        for (int i = 1; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
    }

    void DiaPlay()
    {
        diafst.TriggerDia();
    }
    // Update is called once per frame
    void Update()
    {
        //UI
        ghostMeeterFill.fillAmount = player.GMamount;
        scoreTxt.text = "Score: " + score.ToString();

        if(countBoxesLvl0 <= 0 && tut)
        {
            player.Teleport(startPointlvl1.position);
        }
        if (countBoxesLvl1 <= 0 && lvl1)
        {
            door1.SetActive(true);
            doorSfx.Play();

            DoorElL1.SetActive(false);
        }

        if (countBoxesLvl2 <= 0 && lvl2)
        {
            door2.SetActive(true);
            doorSfx.Play();

            DoorElL2.SetActive(false);

        }
        if(countBoxesLvl3 <= 0 && lvl3)
        {
            Gaol.SetActive(true);

        }

        if (player.lives > 0 && isDead)
        {
            StartCoroutine(Death());
            isDead = false;
        }

        if (!GMready)
        {
            ghostMeeterFill.fillAmount = Mathf.MoveTowards(ghostMeeterFill.fillAmount, 1, Time.deltaTime);
            player.GMamount = Mathf.MoveTowards(player.GMamount, 1, Time.deltaTime * 0.26f);
        }

    }

    public void NextLvl()
    {
        levels[1].SetActive(true); //lvl2 active
        player.transform.position = startPointlvl2.position;
        player.movePoint.position = player.transform.position;
        Camera.main.transform.position = new Vector3(-0.219999999f, 0, -10);
        levels[0].SetActive(false);
        lvl1 = false;
        lvl2 = true;
    }


    public void NextLvl2()
    {
        levels[2].SetActive(true);
        player.transform.position = startPointlvl3.position;
        player.movePoint.position = player.transform.position;
        Camera.main.transform.position = new Vector3(21.9699993f, 0, -10);
        levels[1].SetActive(false);
        lvl2 = false;
        lvl3 = true;
    }
    public void NextLvl3()
    {
        levels[3].SetActive(true); //lvl2 active
        player.transform.position = startPointlvl2.position;
        player.movePoint.position = player.transform.position;
        Camera.main.transform.position = new Vector3(22f, 13.2f, -10);
        levels[2].SetActive(false);
        lvl1 = false;
        lvl2 = true;
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
        player.spriteRenderer.enabled = false;
        player.canMove = false;
        var deadCat = Instantiate(DeadCat, player.transform.position, Quaternion.identity);
        if (lvl1)
            player.Teleport(startPointlvl1.position);
        if (lvl2)
            player.Teleport(startPointlvl2.position);
        if (lvl3)
            player.Teleport(startPointlvl3.position);

        Destroy(deadCat, .7f);

        yield return new WaitForSeconds(.8f);

        player.spriteRenderer.enabled = true;
        hearts[player.lives - 1].fillAmount = 0;
        player.lives--;
        score -= 25;
        yield return new WaitForSeconds(1f);
        player.canMove = true;

        if (player.lives <= 0)
        {
            player.lives = 0;
            yield return leaderboard.SubmitScoreRoutine(score);
            EndScreen("GameOver");
        }

    }


    public void EndScreen(string text)
    {
        title.text = text;
        gameOverscrn.SetActive(true);
        HUD.SetActive(false);
        player.canMove = false;
        if (text == "Victory")
            victoryScreen.SetActive(true);
        else
            victoryScreen.SetActive(false);

    }


    public void Rstrt()
    {
        SceneManager.LoadScene(1);
        //Time.timeScale = Time.timescale = 0 ? 1:1;
        if(Time.timeScale == 0)
            Time.timeScale = 1;
    }

    public void menu()
    {
        SceneManager.LoadScene(0);

    }
}
