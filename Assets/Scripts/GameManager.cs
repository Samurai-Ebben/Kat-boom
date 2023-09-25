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
    public LevelSystem levelSystem;


    public bool isDead = false;

    [Header("--Boxes Count--")]
    public int countBoxesLvl1 = 5;
    public int countBoxesLvl2 = 5;
    public int countBoxesLvl3 = 5;
    public int countBoxesLvl0 = 1;

    [Header("--REF Doors--")]
    public GameObject door1;
    public GameObject door2;


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
        levelSystem = GetComponent<LevelSystem>();
        diafst = GetComponent<DiaTrigger>();
        Invoke("DiaPlay", 0.01f);
        HUD.SetActive(true);
        gameOverscrn.SetActive(false);
        victoryScreen.SetActive(false);

        door1.SetActive(false);
        door2.SetActive(false);
        Gaol.SetActive(false);
        
    }


    void Update()
    {
        //UI
        UIUpdate();

        CheckingForLeveling();

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

    public void CheckingForLeveling()
    {
        if (countBoxesLvl0 < 1 && levelSystem.tut)
        {
            levelSystem.NxtLvl(0, 1);
        }
        if (countBoxesLvl1 < 1 && levelSystem.lvl1)
        {
            door1.SetActive(true);
            doorSfx.Play();

            DoorElL1.SetActive(false);
        }

        if (countBoxesLvl2 < 1 && levelSystem.lvl2)
        {
            door2.SetActive(true);
            doorSfx.Play();

            DoorElL2.SetActive(false);

        }
        if (countBoxesLvl3 < 1 && levelSystem.lvl3)
        {
            Gaol.SetActive(true);

        }
    }

    private void UIUpdate()
    {
        ghostMeeterFill.fillAmount = player.GMamount;
        scoreTxt.text = "Score: " + score.ToString();
    }



    public IEnumerator Death()
    {
        player.spriteRenderer.enabled = false;
        player.canMove = false;
        var deadCat = Instantiate(DeadCat, player.transform.position, Quaternion.identity);
        levelSystem.SwitchingLevels();


        Destroy(deadCat, .7f);

        yield return new WaitForSeconds(.8f);

        player.spriteRenderer.enabled = true;
        hearts[player.lives - 1].fillAmount = 0;
        player.lives--;
        score -= 25;

        yield return new WaitForSeconds(1f);

        if (player.lives <= 0)
        {
            player.canMove = false;

            player.lives = 0;
            EndScreen("GameOver");
        }else
            player.canMove = true;

    }


    public void EndScreen(string text)
    {
        StartCoroutine(leaderboard.SubmitScoreRoutine(score));
        player.canMove = false;
        title.text = text;
        gameOverscrn.SetActive(true);
        HUD.SetActive(false);
        if (text == "Victory")
        {
            score += 100;
            victoryScreen.SetActive(true);
        }
        else
            victoryScreen.SetActive(false);

    }

    void DiaPlay()
    {
        diafst.TriggerDia();
    }

    public void Rstrt()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = Time.timeScale == 0 ? 1 : 1;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);

    }
}
