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

    public enum unitType
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
        GameObject obj1 = GameObject.Find("Button1");
        GameObject obj2 = GameObject.Find("Button2");
        GameObject obj3 = GameObject.Find("Button3");
        GameObject obj4 = GameObject.Find("Button4");
        GameObject obj5 = GameObject.Find("Button5");
        GameObject obj6 = GameObject.Find("Button6");
        Button1 = obj1.GetComponent<Button>();
        Button2 = obj2.GetComponent<Button>();
        Button3 = obj3.GetComponent<Button>();
        Button4 = obj4.GetComponent<Button>();
        Button5 = obj5.GetComponent<Button>();
        Button6 = obj6.GetComponent<Button>();
        Button1.onClick.AddListener(CreateUnit());
        Button2.onClick.AddListener(CreateUnit());
        Button3.onClick.AddListener(CreateUnit());
        Button4.onClick.AddListener(CreateUnit());
        Button5.onClick.AddListener(CreateUnit());
        Button6.onClick.AddListener(CreateUnit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateUnit(enum UnitType)
    {

    }
}
