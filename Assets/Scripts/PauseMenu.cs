using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	bool isPaused = false;
	GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
		pausePanel = this.gameObject;
		//pausePanel.Set;
    }

    // Update is called once per frame
    void Update()
    {
		//Pauses the game
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isPaused)//Paused to Not Paused
			{
				isPaused = !isPaused;
				pausePanel.SetActive(false);
			}
			else //Not Paused to Paused
			{
				isPaused = !isPaused;
				pausePanel.SetActive(true);
				//Stop Timer while the Game is paused
			}
		}
	}
	public void exitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}
	public void loadMainMenu() //Loads Title Screen Setting the amount of flares to zero.
	{
		GameManager.instance.numFlares = 0;
		SceneManager.LoadScene("TitleScreen");
	}
}
