using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private double positionX = -9.48;
    private double positionY = -3.54;
    private double scaleX = 0.4;
    private double scaleY = 0.4;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    void Awake()
    {
        Debug.Log("hello World");
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnMouseDown()
    {
        Debug.Log("factory");
    }
}
