using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<GameObject> playerUnits;
    private Button skipTurn;
    [SerializeField] private GameObject myFactory;
    public GridManager map;
    private Button mainMenuButton;
    private GameObject winScreenCanevas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        winScreenCanevas = GameObject.Find("winScreenCanevas");
        mainMenuButton = GameObject.Find("mainMenuButton").GetComponent<Button>();
        mainMenuButton.onClick.AddListener(() => changeScene.ChangeScene(0));
        GameObject obj = GameObject.Find("skipTurnButton");
        skipTurn = obj.GetComponent<Button>();
        skipTurn.onClick.AddListener(() => myFactory.GetComponent<Factory>().OnNewTurn()) ;
        skipTurn.onClick.AddListener(() => OnNewTurnUnits());
        map.selectedUnit = null;
        winScreenCanevas.SetActive(false);
    }

    public void OnNewTurnUnits()
    {
        foreach (GameObject unit in playerUnits)
        {
            unit.GetComponent<UnitScript>().OnNewTurn();
        }
    }

    public void OnWin()
    {
        winScreenCanevas.SetActive(true);
    }
}
