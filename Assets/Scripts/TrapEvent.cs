using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrapEvent : MonoBehaviour
{

    public string[] QTEevents;

    private GameManager GM;

    public GameObject trappedAlert;
    public Text trappedPhrase;
    public Text trapTimer;

    public float trapTimeInSeconds = 4f;
    [SerializeField]
    private float trapTimeLeft = 4f;

    [SerializeField]
    private int QTEPointer = 0;

    [SerializeField]
    private int QTEString = 0;

    [SerializeField]
    private string QTEAsString = "";

    [SerializeField]
    private string NeedsToBeTyped = "";

    [SerializeField]
    private string phrase = "";

    public bool isTrapped = false;

    AudioMaster AM;

    // Start is called before the first frame update
    void Start()
    {
        QTEevents = new string[] { "ASDF", "NOUIDIOT", "THINKFAST", "EIFUHWKY", "IMOUTOFIDEAS", "MIDDLEDOOR", "LEFTDOOR", "LYING", "SAFE", "RIGHTDOOR", "ROCK", "MUSHROOM", "SIGNISLYING", "CANURAED", "JEFFWENTLEFT", "WWVWWV" };
        //StartTrap();
        GM = GameManager.instance;
        AM = GameObject.Find("AudioManager").GetComponent<AudioMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GM == null)
        {
            GM = GameManager.instance;
        }

        if(isTrapped)
        {
            AM.lowHealth();
            trapTimeLeft -= Time.deltaTime;
            trapTimer.text = System.Math.Round(trapTimeLeft, 2).ToString();
            if(trapTimeLeft <= 0f)
            {
                EndTrap();
                FailTrap();
            }
            NeedsToBeTyped = QTEevents[QTEString][QTEPointer].ToString();
            KeyCode thisKeyCode;
            if(NeedsToBeTyped == " ")
            {
                thisKeyCode = KeyCode.Space;
            } else
            {
                thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), NeedsToBeTyped);
            }
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(thisKeyCode))
                {
                    QTEPointer++;
                    UpdatePhrase();
                    if (QTEPointer >= QTEevents[QTEString].Length)
                    {
                        EndTrap();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                {
                    // stub
                }
                else
                {
                    EndTrap();
                    FailTrap();
                }
            }
        }
        else
        {
            trappedAlert.SetActive(false);
            trappedPhrase.gameObject.SetActive(false);
            trapTimer.gameObject.SetActive(false);
        }
    }
    
    public void StartTrap()
    {
        //print("THERE IS A TRAP");
        GM.currentlyTrapped = true;
        isTrapped = true;
        trapTimeLeft = trapTimeInSeconds;
        QTEPointer = 0;
        QTEString = Random.Range(0, QTEevents.Length);
        print("Selecting phrase: " + QTEString);
        QTEAsString = QTEevents[QTEString];
        trappedAlert.gameObject.SetActive(true);
        trappedPhrase.gameObject.SetActive(true);
        trapTimer.gameObject.SetActive(true);
        UpdatePhrase();
    }

    public void EndTrap()
    {
        isTrapped = false;
        GM.currentlyTrapped = false;
        trappedAlert.gameObject.SetActive(false);
        trappedPhrase.gameObject.SetActive(false);
        trapTimer.gameObject.SetActive(false);
    }

    public void FailTrap()
    {
        if (GM.isDamaged == true)
        {
			// Kill player
			GM.isTiming = false;
			SceneManager.LoadScene("Death");
        }
        else
        {
            // Damage player
            GM.isDamaged = true;
        }
        GM.currentlyTrapped = false;
    }
    
    public void UpdatePhrase()
    {
        phrase = "[";
        for(int i = 0; i < QTEevents[QTEString].Length; i++)
        {
            if(i < QTEPointer)
            {
                phrase += "<color=green>" + QTEevents[QTEString][i].ToString() + "</color>";
            } else
            {
                phrase += QTEevents[QTEString][i].ToString();
            }
        }
        phrase += "]";
        trappedPhrase.text = phrase;
    }

}
