using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandlar : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject explosion;
    public Transform strBtnTransform;
    public void StartGame()
    {
       
        Instantiate(explosion, strBtnTransform.position, Quaternion.identity);
        Invoke("LoadScenesOV", 0.2f);
        if(Time.timeScale == 0)
            Time.timeScale = 1;
    }

    void LoadScenesOV()
    {
        SceneManager.LoadScene(1);

    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }


}
