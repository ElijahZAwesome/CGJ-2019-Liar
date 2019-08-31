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

    private GameObject theManager;

    // Start is called before the first frame update
    void Start()
    {
        num.text = "-missing-";
        Cursor.visible = true;
        theManager = GameObject.Find("LogicHandler");
    }

    // Update is called once per frame
    void Update()
    {
        if (num.text == "-missing-")
        {
            num.text = theManager.GetComponent<MasterLogic>().allRooms.Count.ToString();
        }
    }

    public void ExitToMenu()
    {
        Destroy(theManager);
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
