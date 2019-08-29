using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject howToPlay;
    public GameObject closed;
    public GameObject open;

    void Start()
    {
        howToPlay.SetActive(false);
        open.SetActive(false);
    }

    public void openRules()
    {
        howToPlay.SetActive(true);
    }

    public void closeRules()
    {
        howToPlay.SetActive(false);
        open.SetActive(false);
        closed.SetActive(true);
    }

    public void loadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void exitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void openPack()
    {
        closed.SetActive(false);
        open.SetActive(true);
    }
}
