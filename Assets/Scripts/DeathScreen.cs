using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI num;

    [SerializeField]
    private Button menuButton, quitButton;

    // Start is called before the first frame update
    void Start()
    {
        num.text = "-missing-";
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (num.text == "-missing-")
        {
            num.text = MasterLogic.MLInstance.allRooms.Count.ToString();
        }
    }

    public void ExitToMenu()
    {
        Destroy(MasterLogic.MLInstance.gameObject);
        SceneManager.LoadScene("TitleScreen");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
