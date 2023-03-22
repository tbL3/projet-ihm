using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    private GameObject canvas;
    private GameObject canvasPopup;
    private GameObject bubble;
    private SpriteRenderer bubbleImage;
    private int remainingTurn;
    private string currentResearch;
    private string currentUnitCreation;
    private string waitingResponseFor;
    private Button yesButton;
    private Button noButton;
    private Sprite[] soldierSprite;
    private Sprite[] tankSprite;
    private Sprite[] planeSprite;
    private Dictionary<string, int> unitTime= new Dictionary<string, int>()
    {
        {"soldier",2 },
        {"tank", 4 },
        {"plane", 8 },
        {"reinforcedSoldier", 3 },
        {"reinforceTank", 5 },
        {"reinforcedPlane", 7 }
    };
    

    // Start is called before the first frame update

    public int getRemainingTurn()
    {
        return remainingTurn;
    }

    void Start()
    {
        
    }

    void Awake()
    {
        Debug.Log("start awake");
        canvas = GameObject.Find("CanvasModalWindowFactory");
        bubble = GameObject.Find("bubble");
        GameObject obj = GameObject.Find("bubbleImage");
        canvasPopup =  GameObject.Find("canvasPopup");
        bubbleImage = obj.GetComponent<SpriteRenderer>();
        soldierSprite = Resources.LoadAll<Sprite>("soldier");
        tankSprite = Resources.LoadAll<Sprite>("tank");
        planeSprite = Resources.LoadAll<Sprite>("plane");
        GameObject obj1 = GameObject.Find("yesButton");
        GameObject obj2 = GameObject.Find("noButton");
        yesButton = obj1.GetComponent<Button>();
        noButton = obj2.GetComponent<Button>();
        yesButton.onClick.AddListener(() => DisablePopup(true));
        noButton.onClick.AddListener(() => DisablePopup(false));

        canvas.SetActive(false);
        DisableBubble();
        DisablePopup(false);
        Debug.Log("fall asleep");
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void OnNewTurn()
    {
        Debug.Log("newTurn");
        if (remainingTurn > 1)
        {
            remainingTurn--;
        }
        else if (remainingTurn == 1)
        {
            if (currentResearch != null)
            {
                //do thing
                currentResearch = null;
            }
            else
            {
                UnitScript.spawnUnit(0, 0);
                currentUnitCreation = null;
                DisableBubble();
            }
            remainingTurn = 0;
        }
    }

    private void OnMouseDown()
    {
        canvas.SetActive(true);
        FactoryPanel.openUnity();
        Debug.Log("bbb");
        EnableBubble();

    }

    public void createUnit(string unit)
    {
        currentUnitCreation = unit;
        remainingTurn = unitTime[unit];
        EnableBubble();
        SetBubbleImage(unit);
        closeModal();
        Debug.Log("currentCreation" + currentUnitCreation + "reaminingTurn" + remainingTurn);
    }

    public void closeModal()
    {
        FactoryPanel.openBoth();
        canvas.SetActive(false);
    }

    public void SetBubbleImage(string unit)
    {
        switch (unit)
        {
            case "soldier":
                bubbleImage.sprite = soldierSprite[0];
                break;
            case "tank":
                bubbleImage.sprite = tankSprite[0];
                break;
            case "plane":
                bubbleImage.sprite = planeSprite[0];
                break;
            default:
                Debug.Log("something wrong");
                break;
        }
    }

    public void EnableBubble()
    {
        bubble.SetActive(true);
    }

    public void DisableBubble()
    {
        bubble.SetActive(false);
    }

    public void EnablePopup(string unit)
    {
        canvasPopup.SetActive(true);
        waitingResponseFor = unit;
    }

    public void DisablePopup(Boolean my_bool)
    {
        if (my_bool)
        {
            Debug.Log("c'est true");
            createUnit(waitingResponseFor);
            canvas.SetActive(false);
        }
        Debug.Log("c'est false");
        canvasPopup.SetActive(false);
    }


}
