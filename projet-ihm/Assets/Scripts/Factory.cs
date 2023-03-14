using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    private GameObject canvas;
    private Button crossButton;
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
        crossButton = GetComponent<Button>();
        crossButton.
        crossButton.onClick.AddListener(() => closeModal());
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnMouseDown()
    {
        Debug.Log("factory");
        canvas.SetActive(true);       
    }

    private void closeModal()
    {
        Debug.Log("aa");
        canvas.SetActive(false);
    }

}
