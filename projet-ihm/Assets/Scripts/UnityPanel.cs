using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityPanel : MonoBehaviour
{
    Button Button1;
    Button Button2;
    Button Button3;
    Button Button4;
    Button Button5;
    Button Button6;
    static public GameObject unityPanel;

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
        
    }

    private void OnEnable()
    {
        unityPanel = GameObject.Find("unityPanel");
        GameObject obj1 = GameObject.Find("invisibleButton1");
        GameObject obj2 = GameObject.Find("invisibleButton2");
        GameObject obj3 = GameObject.Find("invisibleButton3");
        GameObject obj4 = GameObject.Find("invisibleButton4");
        GameObject obj5 = GameObject.Find("invisibleButton5");
        GameObject obj6 = GameObject.Find("invisibleButton6");
        Button1 = obj1.GetComponent<Button>();
        Button2 = obj2.GetComponent<Button>();
        Button3 = obj3.GetComponent<Button>();
        Button4 = obj4.GetComponent<Button>();
        Button5 = obj5.GetComponent<Button>();
        Button6 = obj6.GetComponent<Button>();
        Button1.onClick.AddListener(() => CreateUnit("soldier"));
        Button2.onClick.AddListener(() => CreateUnit("tank"));
        Button3.onClick.AddListener(() => CreateUnit("plane"));
        Button4.onClick.AddListener(() => CreateUnit("reinforcedSoldier"));
        Button5.onClick.AddListener(() => CreateUnit("reinforcedTank"));
        Button6.onClick.AddListener(() => CreateUnit("reinforcedPlane"));
        Debug.Log("unityPanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateUnit(string unit)
    {
        if(Factory.getRemainingTurn() > 0)
        {
            //popup
        }
        else
        {
            Factory.createUnit(unit);
        }
    }
}
