using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, secondaryColor;

    [SerializeField] private SpriteRenderer tileRenderer;

    [SerializeField] private GameObject hoverTile;

    private bool tileOccupied = false;

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

}
