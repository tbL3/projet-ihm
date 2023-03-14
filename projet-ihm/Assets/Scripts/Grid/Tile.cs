using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, secondaryColor;

    [SerializeField] private SpriteRenderer tileRenderer;

    [SerializeField] private GameObject hoverTile;

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
