using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    private static GameObject canvas;
    //private Button crossButton;
   
    // Start is called before the first frame update

    void Start()
    {
        
    }

    void Awake()
    {
        canvas = GameObject.Find("CanvasModalWindowFactory");
        canvas.SetActive(false);
        Debug.Log("hello World");

    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnMouseDown()
    {
        canvas.SetActive(true);
        FactoryPanel.openUnity();

    }

    public static void closeModal()
    {
        FactoryPanel.openBoth();
        canvas.SetActive(false);
    }

}
