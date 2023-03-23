using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
        skipTurn.onClick.AddListener(() => myFactory.GetComponent<Factory>().OnNewTurn());
    }


}
