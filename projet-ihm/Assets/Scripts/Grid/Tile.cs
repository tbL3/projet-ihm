using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int tileX;

    public int tileY;

    public GridManager map;

    [SerializeField] private Color baseColor, secondaryColor;

    [SerializeField] private SpriteRenderer tileRenderer;

    [SerializeField] private GameObject hoverTile;

    public GameObject tileVisualPrefab;

    private bool tileOccupied = false;

    public bool isWalkable = true;

    public float movementCost = 1;
    public void Init(bool isOffset)
    {
        tileRenderer.color = isOffset ? secondaryColor : baseColor;
    }

    public void OnMouseEnter()
    {
        hoverTile.SetActive(true);
    }

    public void OnMouseExit()
    {
        hoverTile.SetActive(false);
    }

    void OnMouseUp()
    {
        Debug.Log("Click!");

        map.GeneratePathTo(tileX, tileY);
    }
}
