using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<GameObject> playerUnits;
    private Button skipTurn;
    [SerializeField] private GameObject myFactory;
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
        GameObject obj = GameObject.Find("skipTurnButton");
        skipTurn = obj.GetComponent<Button>();
        skipTurn.onClick.AddListener(() => Factory.OnNewTurn());
        foreach (GameObject unit in playerUnits)
        {
            skipTurn.onClick.AddListener(() => unit.GetComponent<UnitScript>().OnNewTurn());
        }
        skipTurn.onClick.AddListener(() => myFactory.GetComponent<Factory>().OnNewTurn());
    }


}
