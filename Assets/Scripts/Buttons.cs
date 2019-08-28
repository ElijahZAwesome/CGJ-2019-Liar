using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject howToPlay;

    void Start()
    {
        howToPlay.SetActive(false);
    }

    public void openRules()
    {
        howToPlay.SetActive(true);
    }

    public void closeRules()
    {
        howToPlay.SetActive(false);
    }

    public void loadGame()
    {
        SceneManager.LoadScene("OtherSampleScene");
    }

    public void exitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
