using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	bool isPaused = false;
	[SerializeField]
	GameObject pausePanel;
	[SerializeField]
	GameObject trapHandler;

    // Update is called once per frame
    void Update()
    {
		//Pauses the game
		if (Input.GetKeyDown(KeyCode.Escape) && !trapHandler.GetComponent<TrapEvent>().isTrapped)
		{
			if (isPaused)//Paused to Not Paused
			{
				UnpauseTheGame();
			}
			else //Not Paused to Paused
			{
				PauseTheGame();
			}
		}
	}
	void PauseTheGame()
	{
		isPaused = !isPaused;
		pausePanel.SetActive(true);
		//Stop Timer while the Game is paused
	}
	public void UnpauseTheGame()
	{
		isPaused = !isPaused;
		pausePanel.SetActive(false);
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
