using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TestTools;

public class MoveRange : MonoBehaviour
{
    public GameObject unitParent;

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
        UnitScript unitParent = this.transform.parent.GetComponent<UnitScript>();

        int range = unitParent.moveRange;

        this.GetComponent<BoxCollider2D>().size = new Vector2(2 + 4 * range, 2 + 4 * range);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Tile"))
        {
            Debug.Log("collision avec tile");
        }
    }
}
