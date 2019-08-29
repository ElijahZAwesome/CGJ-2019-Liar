using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject howToPlay;
    public GameObject closed;
    public GameObject open;

    AudioMaster play;

    void Start()
    {
        Cursor.visible = true;

        howToPlay.SetActive(false);
        open.SetActive(false);

        play = GameObject.Find("AudioManager").GetComponent<AudioMaster>();
    }

    public void openRules()
    {
        howToPlay.SetActive(true);
        play.buttonClick();
    }

    public void closeRules()
    {
        howToPlay.SetActive(false);
        open.SetActive(false);
        play.buttonClick();
        closed.SetActive(true);
    }

    public void loadGame()
    {
        play.buttonClick();
        SceneManager.LoadScene("SampleScene");
    }

    public void mainMenu()
    {
        play.buttonClick();
        SceneManager.LoadScene("TitleScreen");
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
