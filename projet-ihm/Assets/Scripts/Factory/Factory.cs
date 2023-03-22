using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    private static GameObject canvas;
    private static GameObject bubble;
    private static SpriteRenderer bubbleImage;
    private static int remainingTurn;
    private static string currentResearch;
    private static string currentUnitCreation;
    private static Sprite[] soldierSprite;
    private static Sprite[] tankSprite;
    private static Sprite[] planeSprite;
    private static Dictionary<string, int> unitTime= new Dictionary<string, int>()
    {
        {"soldier",2 },
        {"tank", 4 },
        {"plane", 8 },
        {"reinforcedSoldier", 3 },
        {"reinforceTank", 5 },
        {"reinforcedPlane", 7 }
    };
    

    // Start is called before the first frame update

    public static int getRemainingTurn()
    {
        return remainingTurn;
    }

    void Start()
    {
        
    }

    void Awake()
    {
        canvas = GameObject.Find("CanvasModalWindowFactory");
        bubble = GameObject.Find("bubble");
        GameObject obj = GameObject.Find("bubbleImage");
        bubbleImage = obj.GetComponent<SpriteRenderer>();
        canvas.SetActive(false);
        DisableBubble();
        soldierSprite = Resources.LoadAll<Sprite>("soldier");
        tankSprite = Resources.LoadAll<Sprite>("tank");
        planeSprite = Resources.LoadAll<Sprite>("plane");

        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public static void OnNewTurn()
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

    public static void createUnit(string unit)
    {
        currentUnitCreation = unit;
        remainingTurn = unitTime[unit];
        EnableBubble();
        SetBubbleImage(unit);
        closeModal();
        Debug.Log("currentCreation" + currentUnitCreation + "reaminingTurn" + remainingTurn);
    }

    public static void closeModal()
    {
        FactoryPanel.openBoth();
        canvas.SetActive(false);
    }

    public static void SetBubbleImage(string unit)
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

    public static void EnableBubble()
    {
        bubble.SetActive(true);
    }

    public static void DisableBubble()
    {
        bubble.SetActive(false);
    }


}
