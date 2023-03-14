using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private GameObject canvas;
    private GameObject cross;
    //private Button crossButton;
   
    // Start is called before the first frame update

    void Start()
    {
        
    }

    void Awake()
    {
        Debug.Log("hello World");
        canvas = GameObject.Find("CanvasModalWindowFactory");
        cross = GameObject.Find("factoryModalCross");
        //crossButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnMouseDown()
    {
        Debug.Log("factory");
        canvas.SetActive(false);       
    }

    private void closeModal()
    {

    }

}
