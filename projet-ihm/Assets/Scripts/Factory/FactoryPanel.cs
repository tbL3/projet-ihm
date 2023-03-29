using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FactoryPanel : MonoBehaviour
{
    private GameObject unityPanel;
    private GameObject researchPanel;
    private GameObject canevas;
    private Button crossButton;
    private Button researchButton;
    private Button unityButton;
    private Button Button1;
    private Button Button2;
    private Button Button3;
    private Button Button4;
    private Button Button5;
    private Button Button6;
    private Button Button7;
    private Button Button8;
    private TMP_Text myText;
    [SerializeField] private GameObject myFactory;

    public enum UnitType
    {
        soldier,
        reinforcedSoldier,
        tank,
        reinforcedTank,
        plane,
        reinforcedPlane,
    }

    // Start is called before the first frame update
    void Start()
    {
        DisableCanevas();
    }

    void OnEnable()
    {
        canevas = GameObject.Find("CanvasModalWindowFactory");
        unityPanel = GameObject.Find("unityPanel");
        researchPanel = GameObject.Find("researchPanel");
        GameObject obj = GameObject.Find("crossButton");
        GameObject obj2 = GameObject.Find("unityButton");
        GameObject obj3= GameObject.Find("researchButton");
        GameObject obj4 = GameObject.Find("invisibleButton1");
        GameObject obj5 = GameObject.Find("invisibleButton2");
        GameObject obj6 = GameObject.Find("invisibleButton3");
        GameObject obj7 = GameObject.Find("invisibleButton4");
        GameObject obj8 = GameObject.Find("invisibleButton5");
        GameObject obj9 = GameObject.Find("invisibleButton6");
        Button7= GameObject.Find("invisibleResearchButton1").GetComponent<Button>();
        Button8 = GameObject.Find("invisibleResearchButton2").GetComponent<Button>();
        crossButton = obj.GetComponent<Button>();
        unityButton = obj2.GetComponent<Button>();
        researchButton = obj3.GetComponent<Button>();
        Button1 = obj4.GetComponent<Button>();
        Button2 = obj5.GetComponent<Button>();
        Button3 = obj6.GetComponent<Button>();
        Button4 = obj7.GetComponent<Button>();
        Button5 = obj8.GetComponent<Button>();
        Button6 = obj9.GetComponent<Button>();
        crossButton.onClick.AddListener(closeModal);
        unityButton.onClick.AddListener(openUnity);
        researchButton.onClick.AddListener(openResearch);
        Button1.onClick.AddListener(() => CreateUnit("soldier"));
        Button2.onClick.AddListener(() => CreateUnit("tank"));
        Button3.onClick.AddListener(() => CreateUnit("plane"));
        Button4.onClick.AddListener(() => CreateUnit("reinforcedSoldier"));
        Button5.onClick.AddListener(() => CreateUnit("reinforcedTank"));
        Button6.onClick.AddListener(() => CreateUnit("reinforcedPlane"));
        Button7.onClick.AddListener(() => MakeResearch("UnlockUnit"));
        Button8.onClick.AddListener(() => MakeResearch("ReduceTime"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openUnity()
    {
        unityPanel.SetActive(true);
        researchPanel.SetActive(false);
    }
    public void closeModal()
    {
        openBoth();
        DisableCanevas();
    }

    public void openResearch()
    {     
        unityPanel.SetActive(false);
        researchPanel.SetActive(true);
    }

    //si les panel sont setActive(false) au moment ou le canvas est fermer alors à la reouveture de ce dernier une erreur va être lever
    //la fonction openBoth permet à la classe Factory de réactiver les deux panel avant de fermer le canvas
    public void openBoth()
    {
        unityPanel.SetActive(true);
        researchPanel.SetActive(true);
    }

    public void DisableCanevas()
    {
        canevas.SetActive(false);
    }

    public void EnableCanevas()
    {
        canevas.SetActive(true);
    }

    //permet de décroitre le nombre de tour nécéssaire à la création de l'unité à mesure que l'on avance dans les tours
    //(quand une création est en cours) 
    //unit peut représenter une unité ou une recherche
    public void changeTimeDisplay(string unit)
    {
        //la référence du texte indiquant le nombre de tour nécéssaire est = au nom de l'unité + TimeText 
        EnableCanevas();
        myText = GameObject.Find(unit + "TimeText").GetComponent<TMP_Text>();
        myText.text = (int.Parse(myText.text) - 1).ToString();
        DisableCanevas();
    }
    public void restoreTimeDisplay(int time, string unit)
    {
        EnableCanevas();
        myText = GameObject.Find(unit + "TimeText").GetComponent<TMP_Text>();
        myText.text = time.ToString();
        DisableCanevas();
    }

    public void CreateUnit(string unit)
    {
        if (myFactory.GetComponent<Factory>().getRemainingTurn() > 0)
        {
            myFactory.GetComponent<Factory>().EnablePopup(unit);
        }
        else
        {       
            myFactory.GetComponent<Factory>().createUnit(unit);
        }
    }

    public void MakeResearch(string research)
    {
        if (myFactory.GetComponent<Factory>().getRemainingTurn() > 0)
        {        
            myFactory.GetComponent<Factory>().EnablePopup(research);
        }
        else
        {
            myFactory.GetComponent<Factory>().StartResearch(research);
        }
    }

    public void SwitchReinforcedUnitButton(bool enable)
    {
        this.Button4.interactable = enable;
        this.Button5.interactable = enable;
        this.Button6.interactable = enable;
    }

}
