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

    Camera mainCam;

    public Transform transitionPos;

    void Start()
    {
        Cursor.visible = true;

        howToPlay.SetActive(false);
        open.SetActive(false);

        play = GameObject.Find("AudioManager").GetComponent<AudioMaster>();
        mainCam = Camera.main;
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
        StartCoroutine(ZoomCam());
    }

    private IEnumerator ZoomCam()
    {
        float elapsedTime = 0;
        float time = 1f;
        Vector3 startingPos = mainCam.transform.position;
        Vector3 newPos = transitionPos.position;
        float origSize = mainCam.orthographicSize;
        while (elapsedTime < time)
        {
            mainCam.transform.position = Vector3.Lerp(startingPos, newPos, (elapsedTime / time));
            mainCam.orthographicSize = Mathf.Lerp(origSize, 0.001f, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return 1;
        }
        mainCam.transform.position = newPos;
        mainCam.orthographicSize = 0.001f;
        yield return new WaitForSeconds(0.5f);
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
