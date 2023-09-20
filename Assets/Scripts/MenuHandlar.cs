using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandlar : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject HSCanvas;
    public GameObject explosion;
    public Transform strBtnTransform;
    public void StartGame()
    {
       
        /*var ex =*/ Instantiate(explosion, strBtnTransform.position, Quaternion.identity);
        //Destroy(ex, 0.25f);
        Invoke("LoadScenesOV", 0.2f);
        if(Time.timeScale == 0)
            Time.timeScale = 1;
    }

    public void HighScores()
    {
        //Show leaderboard.
        HSCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    void LoadScenesOV()
    {
        SceneManager.LoadScene(1);

    }
    public void menu()
    {
        HSCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }


}
