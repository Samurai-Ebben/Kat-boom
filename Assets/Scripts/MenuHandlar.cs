using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandlar : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject HSCanvas;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        if(Time.timeScale == 0)
            Time.timeScale = 1;
    }

    public void HighScores()
    {
        //Show leaderboard.
        HSCanvas.SetActive(true);
        menuCanvas.SetActive(false);
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
