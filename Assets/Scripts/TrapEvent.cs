using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapEvent : MonoBehaviour
{

    public string[] QTEevents = new string[] { "ASDF", "NOUIDIOT", "THINKFAST", "EIFUHWBY", "IMOUTOFIDEAS", "MIDDLE DOOR", "LEFT DOOR", "LYING", "SAFE", "RIGHT DOOR", "ROCK", "MUSHROOM" };

    public Text trappedAlert;
    public Text trappedPhrase;

    public float trapTimeInSeconds = 5f;

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

    // Start is called before the first frame update
    void Start()
    {
        StartTrap();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrapped)
        {
            NeedsToBeTyped = QTEevents[QTEString][QTEPointer].ToString();
            KeyCode thisKeyCode;
            if(NeedsToBeTyped == " ")
            {
                thisKeyCode = KeyCode.Space;
            } else
            {
                thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), NeedsToBeTyped);
            }
            if (Input.GetKeyDown(thisKeyCode))
            {
                QTEPointer++;
                UpdatePhrase();
                if(QTEPointer >= QTEevents[QTEString].Length)
                {
                    EndTrap();
                }
            }
        }
    }
    
    public void StartTrap()
    {
        isTrapped = true;
        QTEPointer = 0;
        QTEString = Mathf.RoundToInt(UnityEngine.Random.Range(0, QTEevents.Length));
        QTEAsString = QTEevents[QTEString];
        trappedAlert.gameObject.SetActive(true);
        trappedPhrase.gameObject.SetActive(true);
        UpdatePhrase();
    }

    public void EndTrap()
    {
        isTrapped = false;
        trappedAlert.gameObject.SetActive(false);
        trappedPhrase.gameObject.SetActive(false);
    }
    
    public void UpdatePhrase()
    {
        phrase = "[";
        for(int i = 0; i < QTEevents[QTEString].Length; i++)
        {
            print(i + " " + QTEPointer);
            if(i < QTEPointer)
            {
                print("yes");
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
