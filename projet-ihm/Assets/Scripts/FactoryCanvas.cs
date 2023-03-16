using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryCanvas : MonoBehaviour
{
    GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        canvas = GameObject.Find("YourGameObjectName");
    }
    private void Enable()
    {
        canvas.SetActive(true);
    }

    private void Disable()
    {
        canvas.SetActive(false);
    }
}
