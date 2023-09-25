using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandlar : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject explosion;
    public GameObject tutClip;
    public Transform strBtnTransform;
    public void StartGame()
    {
        var ex= Instantiate(explosion, strBtnTransform.position, Quaternion.identity);
        Destroy(ex, .2f);

        Invoke("ActiveClip", 0.2f);
        if (Time.timeScale == 0)
            Time.timeScale = 1;
    }

    void ActiveClip()
    {
        menuCanvas.SetActive(false);
        tutClip.SetActive(true);
    }
    public void LoadScenesOV()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#endif
        Application.Quit();
    }

}
