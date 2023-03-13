using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, secondaryColor;

    [SerializeField] private SpriteRenderer tileRenderer;

    public void Init(bool isOffset)
    {
        tileRenderer.color = isOffset ? secondaryColor : baseColor;
    }
}
