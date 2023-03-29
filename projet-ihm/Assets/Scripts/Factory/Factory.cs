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
    private bool isReinforcedUnitEnabled = false;
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
    private Dictionary<string, int> researchTime = new Dictionary<string, int>()
    {
        {"UnlockUnit", 4 },
        {"ReduceTime", 6 },
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
        PanelManager.GetComponent<FactoryPanel>().closeModal();
        if (remainingTurn > 1)
        {
            if(currentResearch != null)
            {             
                PanelManager.GetComponent<FactoryPanel>().changeTimeDisplay(currentResearch);
            }
            else
            {
                PanelManager.GetComponent<FactoryPanel>().changeTimeDisplay(currentUnitCreation);
            }
            remainingTurn--;
           
        }
        else if (remainingTurn == 1)
        {
            if (currentResearch != null)
            {
                
                if(currentResearch == "ReduceTime")
                {
                    RduceTime();
                }
                else if(currentResearch == "UnlockUnit")
                {
                    UnlockUnit();
                }
               
                PanelManager.GetComponent<FactoryPanel>().restoreTimeDisplay(researchTime[currentResearch], currentResearch);
                currentResearch = null;
            }
            else
            {
                spawner.GetComponent<Spawner>().SpawnUnit(0, 0, unitType);        
                PanelManager.GetComponent<FactoryPanel>().restoreTimeDisplay(unitTime[currentUnitCreation], currentUnitCreation);
                currentUnitCreation = null;
            }
            myBubble.GetComponent<Bubble>().DisableBubble();
            remainingTurn = 0;
        }
    }

    private void OnMouseDown()
    {
        PanelManager.GetComponent<FactoryPanel>().EnableCanevas();
        PanelManager.GetComponent<FactoryPanel>().openUnity();
        PanelManager.GetComponent<FactoryPanel>().SwitchReinforcedUnitButton(isReinforcedUnitEnabled);
    }

    public void createUnit(string unit)
    {
        currentUnitCreation = unit;
        //au cas ou on à annuler une recherche pour lancer la production
        currentResearch = null;
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
            case "reinforcedSoldier":
                unitType = 4;
                break;
            case "reinforcedTank":
                unitType = 5;
                break;
            case "reinforcedPlane":
                unitType = 6;
                break;
        }
        remainingTurn = unitTime[unit];
        myBubble.GetComponent<Bubble>().EnableBubble();
        myBubble.GetComponent<Bubble>().SetBubbleImage(unit);
        PanelManager.GetComponent<FactoryPanel>().closeModal();
    }

    public void StartResearch(string research)
    {
        remainingTurn = researchTime[research];
        currentResearch = research;
        //au cas ou on à annuler une production pour lancer la recherche
        currentUnitCreation = null;
        myBubble.GetComponent<Bubble>().EnableBubble();
        myBubble.GetComponent<Bubble>().SetBubbleImage("research");
        PanelManager.GetComponent<FactoryPanel>().closeModal();
    }

    public void EndResearch()
    {

    }

    //unit peut être une unité ou une recherche
    public void EnablePopup(string unit)
    {
        canvasPopup.SetActive(true);
        waitingResponseFor = unit;
    }

    public void DisablePopup(Boolean my_bool)
    {
        if (my_bool)
        {
            if (unitTime.ContainsKey(waitingResponseFor))
            {
                createUnit(waitingResponseFor);
            }
            else if(researchTime.ContainsKey(waitingResponseFor))
            {
                StartResearch(waitingResponseFor);
            }
            PanelManager.GetComponent<FactoryPanel>().DisableCanevas();
        }
        canvasPopup.SetActive(false);
    }

    private void RduceTime() {
        List<string> keys = new List<string>(unitTime.Keys);
        foreach (String key in keys)
        {
            unitTime[key] -= 1;
            PanelManager.GetComponent<FactoryPanel>().changeTimeDisplay(key);
        }
    }

    private void UnlockUnit()
    {
        this.isReinforcedUnitEnabled = true;
    }



}
