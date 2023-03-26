using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    private static GameObject canvasPopup;
    private int remainingTurn;
    private string currentResearch;
    private string currentUnitCreation;
    private string waitingResponseFor;
    private Button yesButton;
    private Button noButton;
    [SerializeField] public GameObject PanelManager;
    [SerializeField] public GameObject myBubble;
    private Dictionary<string, int> unitTime = new Dictionary<string, int>()
    {
        {"soldier",2 },
        {"tank", 4 },
        {"plane", 8 },
        {"reinforcedSoldier", 3 },
        {"reinforcedTank", 5 },
        {"reinforcedPlane", 7 }
    };
    private int unitType;
    public GameObject spawner;
    


    // Start is called before the first frame update

    public int getRemainingTurn()
    {
        return remainingTurn;
    }
    public string getCurrentCreation()
    {
        return this.currentUnitCreation;
    }

    void Start()
    {
        DisablePopup(false);
    }

    void Awake()
    {  
        canvasPopup = GameObject.Find("canvasPopup");
        GameObject obj1 = GameObject.Find("yesButton");
        GameObject obj2 = GameObject.Find("noButton");
        yesButton = obj1.GetComponent<Button>();
        noButton = obj2.GetComponent<Button>();
        yesButton.onClick.AddListener(() => DisablePopup(true));
        noButton.onClick.AddListener(() => DisablePopup(false));
        myBubble.GetComponent<Bubble>().DisableBubble();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void OnNewTurn()
    {
        if (remainingTurn > 1)
        {
            remainingTurn--;
            PanelManager.GetComponent<FactoryPanel>().changeTimeDisplay(currentUnitCreation);
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
                spawner.GetComponent<Spawner>().SpawnUnit(0, 0, unitType);
                myBubble.GetComponent<Bubble>().DisableBubble();
                PanelManager.GetComponent<FactoryPanel>().restoreTimeDisplay(unitTime[currentUnitCreation], currentUnitCreation);
                currentUnitCreation = null;
            }
            remainingTurn = 0;
        }
    }

    private void OnMouseDown()
    {
        PanelManager.GetComponent<FactoryPanel>().EnableCanevas();
        PanelManager.GetComponent<FactoryPanel>().openUnity();
    }

    public void createUnit(string unit)
    {
        Debug.Log("ah bon ?");
        Debug.Log(unit);
        currentUnitCreation = unit;
        switch (currentUnitCreation)
        {
            case "soldier":
                unitType = 1;
                break;
            case "tank":
                unitType = 2;
                break;
            case "plane":
                unitType = 3;
                break;
        }
        remainingTurn = unitTime[unit];
        myBubble.GetComponent<Bubble>().EnableBubble();
        myBubble.GetComponent<Bubble>().SetBubbleImage(unit);
        PanelManager.GetComponent<FactoryPanel>().closeModal();
        Debug.Log("currentCreation" + currentUnitCreation + "reaminingTurn" + remainingTurn);
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
            createUnit(waitingResponseFor);
            PanelManager.GetComponent<FactoryPanel>().DisableCanevas();
        }
        canvasPopup.SetActive(false);
    }


}
